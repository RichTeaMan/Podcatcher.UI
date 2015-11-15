using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Podcatcher.UI.ViewModel
{
    public class DownloadStore : BaseNotify
    {
        public delegate void DownloadStoreEventHandler(DownloadStore sender, EventArgs e);
        public event DownloadStoreEventHandler DownloadStoreChanged;

        private CancellationTokenSource CancellationTokenSource;
        private bool isDownloading;

        private List<PodcastTrack> _tracks;
        public PodcastTrack[] Tracks
        {
            get { return _tracks.ToArray(); }
            set
            {
                _tracks = value.ToList();
                RaisePropertyChanged("Tracks");
                FireChange();
            }
        }

        public DownloadStore()
        {
            _tracks = new List<PodcastTrack>();
            CancellationTokenSource = new CancellationTokenSource();
        }

        public void AddDownload(PodcastTrack track)
        {
            if (!track.Downloading)
            {
                _tracks.Add(track);
                track.Downloading = true;
                RaisePropertyChanged("Tracks");
                FireChange();
            }
        }

        public void BeginDownloads()
        {
            if (!isDownloading)
            {
                isDownloading = true;
                var token = CancellationTokenSource.Token;
                Task.Factory.StartNew(async () =>
                {
                    foreach (var t in Tracks.Where(t => !t.DownloadComplete))
                    {
                        if (token.IsCancellationRequested)
                        {
                            isDownloading = false;
                            return;
                        }
                        await Download(t);
                    }
                },
                token);
            }
        }

        /// <summary>
        /// Cancels download threads. This does not remove downloads or
        /// their progress.
        /// </summary>
        public void CancelDownloads()
        {
            CancellationTokenSource.Cancel();
        }

        private void FireChange()
        {
            if (DownloadStoreChanged != null)
            {
                DownloadStoreChanged.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// Download a track. Blocks until download is complete.
        /// </summary>
        /// <param name="track"></param>
        private async Task Download(PodcastTrack track)
        {
            var download = new Manager.FileDownload(track.Link, track.Title + ".mp3");
            download.ChunkSaved += async (sender, chunk) =>
            {
                var length = (double)track.ContentLength;
                var currentLength = await sender.GetBytesSavedCount();

                var percent = (currentLength / length) * 100.0;
                track.PercentageDownloaded = (int)Math.Round(percent);
                FireChange();
            };
            while (!download.Complete)
            {
                await download.DownloadAndSaveChunk();
                if (CancellationTokenSource.Token.IsCancellationRequested)
                {
                    return;
                }
            }
            track.DownloadComplete = true;
            track.PercentageDownloaded = 100;
        }

    }
}
