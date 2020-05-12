using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace InventoryManagement
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppRoot : Page
    {
        public AppRoot()
        {
            this.InitializeComponent();
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if(args.IsSettingsSelected)
            {
                ContentFrame.Navigate(typeof(MainPage));
            }
            else
            {
                NavigationViewItem item = args.SelectedItem as NavigationViewItem;

                switch (item.Tag.ToString())
                {
                    case "category":
                        ContentFrame.Navigate(typeof(CategoryPage));
                        NavView.Header = "Category";
                        break;
                    case "product":
                        ContentFrame.Navigate(typeof(ProductOpearation));
                        NavView.Header = "Product";
                        break;
                    case "customer":
                        ContentFrame.Navigate(typeof(CustomerPage));
                        NavView.Header = "Customer";
                        break;
                    case "order":
                        ContentFrame.Navigate(typeof(Order));
                        NavView.Header = "Order";
                        break;
                    case "user":
                        ContentFrame.Navigate(typeof(UserPage));
                        NavView.Header = "Admin";
                        break;
                }
            }
        }
    }
}
