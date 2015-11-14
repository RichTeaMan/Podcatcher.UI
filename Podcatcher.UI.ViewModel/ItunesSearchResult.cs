using Podcatcher.RssReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcatcher.UI.ViewModel
{
    public class ItunesSearchResult : BaseNotify
    {
        public string Name { get; set; }
        public string Artist { get; set; }
        public string ImageUrl { get; set; }
        public int Position { get; set; }
        public string FeedUrl { get; set; }

        private PodcastTrack[] _tracks;
        public PodcastTrack[] Tracks
        {
            get { return _tracks; }
            set
            {
                _tracks = value;
                RaisePropertyChanged("Tracks");
            }
        }

        public ItunesSearchResult()
        {

        }

        public ItunesSearchResult(Search.Itunes.Result result)
        {
            Name = result.collectionName;
            Artist = result.collectionArtistName;
            ImageUrl = result.artworkUrl60;
            FeedUrl = result.feedUrl;
        }

        public async Task LoadPodcasts()
        {
            var rssFactory = new RssFactory();
            var rss = await rssFactory.CreateFromUrl(FeedUrl);
            var tracks = rss.channel.Items.Select(ci => new PodcastTrack(ci)).ToArray();
            Tracks = tracks;
        }
    }
}
