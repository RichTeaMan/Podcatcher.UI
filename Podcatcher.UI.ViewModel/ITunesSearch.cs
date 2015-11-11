using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcatcher.UI.ViewModel
{
    public class ItunesSearch : BaseNotify
    {
        private ItunesSearchResult[] searchResults;
        public ItunesSearchResult[] SearchResults
        {
            get
            {
                return searchResults;
            }
            private set
            {
                searchResults = value;
                RaisePropertyChanged("SearchResults");
            }
        }

        public ItunesSearch()
        {
            searchResults = new ItunesSearchResult[0];
        }

        public string SearchTerm { get; set; }

        public async Task DoSearch(string searchTerms)
        {
            var search = new Search.Itunes.ItunesSearchFactory();
            var results = await search.Search(searchTerms);
            SearchResults = results.results.Select(r => new ItunesSearchResult(r)).ToArray();
        }

    }
}
