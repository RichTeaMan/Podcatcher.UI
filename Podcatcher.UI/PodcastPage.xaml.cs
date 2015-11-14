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

namespace Podcatcher.UI
{
    public partial class PodcastPage : PhoneApplicationPage
    {
        public PodcastPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var itunesSearchResult = this.GetParam<ItunesSearchResult>();
            DataContext = itunesSearchResult;
        }
    }
}