using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Podcatcher.UI.ViewModel;
using System.Threading.Tasks;

namespace Podcatcher.UI
{
    public partial class PodcastPage : PhoneApplicationPage
    {
        public ItunesSearchResult ItunesSearchResult { get; set; }

        public PodcastPage()
        {
            InitializeComponent();
        }

        private async Task LoadRss()
        {
            
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var itunesSearchResult = this.GetParam<ItunesSearchResult>();
            DataContext = itunesSearchResult;
            ItunesSearchResult = itunesSearchResult;
            await ItunesSearchResult.LoadPodcasts();
        }

        private void StackPanel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }
    }
}