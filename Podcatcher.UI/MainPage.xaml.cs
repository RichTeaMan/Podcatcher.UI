using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Podcatcher.UI.Resources;
using System.Threading.Tasks;
using Podcatcher.UI.ViewModel;
using System.Windows.Input;

namespace Podcatcher.UI
{
    public partial class MainPage : PhoneApplicationPage
    {
        public ItunesSearch Search { get; set; }

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            Search = new ItunesSearch();

            // Set the data context of the listbox control to the sample data
            DataContext = Search;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        private async void searchBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                //DisableUI(); // Make the "list" disabled, so the user can't "select and item" and cause an error, etc
                IsEnabled = false;
                try
                {
                    // Run your operation asynchronously
                    await Search.DoSearch(searchBox.Text);
                }
                finally
                {
                    //EnableUI(); // Re-enable everything after the above completes
                    IsEnabled = true;
                }
                
            }
        }

        private void StackPanel_Tap(object sender, GestureEventArgs e)
        {
            var panel = (StackPanel)sender;
            Navigation.GoTo(this, "/PodcastPage.xaml", panel.Tag);
        }
        
        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}