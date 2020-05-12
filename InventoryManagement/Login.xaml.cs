using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Data.Json;
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
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();
        }
      
        private async  void BtnLogin_Click(object sender, RoutedEventArgs e)
        {

            await LoginUser();
          
        }



        async Task LoginUser()
        {
            string sURL = "http://localhost:12878/CheckUser/" + Username.Text + "/" + Password.Password.ToString();

            HttpClient client = new HttpClient();

            string sResponse =  await client.GetStringAsync(sURL) ;
            ErrorMessage.Text = sResponse.Trim(new char[] { (char)34 });
           if(ErrorMessage.Text=="success")
                this.Frame.Navigate(typeof(AppRoot));
           
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            CoreApplication.Exit();
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddProduct));
        }
    }
}
