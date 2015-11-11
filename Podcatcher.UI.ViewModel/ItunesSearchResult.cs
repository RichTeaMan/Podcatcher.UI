using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcatcher.UI.ViewModel
{
    public class ItunesSearchResult
    {
        public string Name { get; set; }
        public string Artist { get; set; }
        public string ImageUrl { get; set; }

        public ItunesSearchResult()
        {

        }

        public ItunesSearchResult(Search.Itunes.Result result)
        {
            Name = result.collectionName;
            Artist = result.collectionArtistName;
            ImageUrl = result.artworkUrl60;
        }
    }
}
