using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcatcher.UI.ViewModel
{
    public class PodcastTrack : BaseNotify
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public int ContentLength { get; set; }
        public string Link { get; set; }
        public DateTime Published { get; set; }
        public string Summmary { get; set; }
        private bool _downloading;
        public bool Downloading
        {
            get { return _downloading; }
            set
            {
                _downloading = value;
                RaisePropertyChanged("Downloading");
            }
        }
        private bool _downloadComplete;
        public bool DownloadComplete
        {
            get { return _downloadComplete; }
            set
            {
                _downloadComplete = value;
                RaisePropertyChanged("DownloadComplete");
            }
        }
        private int _percentageDownloaded;
        public int PercentageDownloaded
        {
            get { return _percentageDownloaded; }
            set
            {
                _percentageDownloaded = value;
                RaisePropertyChanged("PercentageDownloaded");
            }
        }

        public PodcastTrack() { }

        public PodcastTrack(RssReader.rssChannelItem channelItem) : this()
        {
            Title = channelItem.title;
            SubTitle = channelItem.subtitle;
            ContentLength = (int)channelItem.enclosure.length;
            Link = channelItem.enclosure.url;
            DateTime published;
            if (DateTime.TryParse(channelItem.pubDate, out published))
            {
                Published = published;
            }
            else
            {
                Published = DateTime.MinValue;
            }
            Summmary = channelItem.summary;
        }

    }
}
