using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcatcher.UI.ViewModel
{
    public class PodcastTrack
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Link { get; set; }
        public DateTime Published { get; set; }
        public string Summmary { get; set; }

        public PodcastTrack() { }

        public PodcastTrack(RssReader.rssChannelItem channelItem) : this()
        {
            Title = channelItem.title;
            SubTitle = channelItem.subtitle;
            Link = channelItem.link;
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
