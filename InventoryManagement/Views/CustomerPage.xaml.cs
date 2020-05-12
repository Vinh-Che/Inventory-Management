using InventoryManagement.Views;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class CustomerPage : Page
    {
        public CustomerPage()
        {
            this.InitializeComponent();
            FillCustomers();
        }
        void ClearField()
        {
            CustomerId.Text = string.Empty;
            Name.Text = string.Empty;
            Phone.Text = string.Empty;
            Email.Text = string.Empty;
        }
        private async void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if(Name.Text!=""&& Email.Text!="" && Phone.Text!="")
            {
              await  AddCustomer();
              await  FillCustomers();
              ClearField();
            }
            else
            {
                ErrorMessage.Text = "Input field can't be empty!";
            }
        }
        public async Task AddCustomer()
        {
            try
            {
                Uri requestUri = new Uri("http://localhost:12878/AddCustomer/"); //replace your Url  
                dynamic dynamicJson = new ExpandoObject();
                dynamicJson.Name = Name.Text;
                dynamicJson.Phone = Phone.Text;
                dynamicJson.Email = Email.Text;

                string json = "";
                json = Newtonsoft.Json.JsonConvert.SerializeObject(dynamicJson);
                var objClint = new System.Net.Http.HttpClient();
                System.Net.Http.HttpResponseMessage respon = await objClint.PostAsync(requestUri, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
                string responJsonText = await respon.Content.ReadAsStringAsync();
                //   ErrorMessage.Foreground = Brushes.Green;
                ErrorMessage.Text = responJsonText;
            }
            catch
            {
                ErrorMessage.Text = "Server error";
            }
        }
        public async Task FillCustomers()

        {
            try
            {
                string sURL = "http://localhost:12878/GetAllCustomers";

                HttpClient client = new HttpClient();

                string sResponse = await client.GetStringAsync(sURL);

                JsonArray jsonArray = JsonArray.Parse(sResponse);
                if (jsonArray.Count > 0)
                {
                    dataGrid1.Items.Clear();
                    foreach (var jsonRow in jsonArray)

                    {

                        JsonObject jsonObject = jsonRow.GetObject();

                        int Id = Convert.ToInt32(jsonObject["id"].ToString());

                        string Name = (jsonObject["name"].ToString()).Trim(new char[] { (char)34 });
                        string Email = jsonObject["email"].ToString().Trim(new char[] { (char)34 });
                        string Phone = jsonObject["phone"].ToString().Trim(new char[] { (char)34 });

                        dataGrid1.Items.Add(new Customer(Id, Name, Email, Phone));
                    }
                }
                else
                {
                    dataGrid1.Items.Clear();
                    ErrorMessage.Text = "No Data to display";
                }
            }
            catch
            {
                ErrorMessage.Text = "Server error";
            }
        }
        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var customer = (Customer)e.ClickedItem;
            CustomerId.Text =customer.Id.ToString();
            Name.Text = customer.Name;
            Email.Text = customer.Email;
            Phone.Text = customer.Phone;
           
        }

        private async void ButtonClr_Click(object sender, RoutedEventArgs e)
        {
            ClearField();
            await FillCustomers();
        }

        private async void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerId.Text!="" && Name.Text != "" && Email.Text != "" && Phone.Text != "")
            {
              await  UpdateCustomer();
               await FillCustomers();
            }
            else
            {
                ErrorMessage.Text = "Input field can't be empty!";
            }
        }
        public async Task UpdateCustomer()
        {
            try
            {
                Uri requestUri = new Uri("http://localhost:12878/UpdateCustomer/" + CustomerId.Text); //replace your Url  
                dynamic dynamicJson = new ExpandoObject();
                dynamicJson.Name = Name.Text;
                dynamicJson.Phone = Phone.Text;
                dynamicJson.Email = Email.Text;

                string json = "";
                json = Newtonsoft.Json.JsonConvert.SerializeObject(dynamicJson);
                var objClint = new System.Net.Http.HttpClient();
                System.Net.Http.HttpResponseMessage respon = await objClint.PostAsync(requestUri, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
                string responJsonText = await respon.Content.ReadAsStringAsync();
                //   ErrorMessage.Foreground = Brushes.Green;
                ErrorMessage.Text = responJsonText;
            }
            catch
            {
                ErrorMessage.Text = "Server error";
            }
        }

        private async void ButnDlt_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerId.Text != "")
            {
              await  DeleteProduct();
              await FillCustomers();
                ClearField();
            }
            else
            {
                ErrorMessage.Text = "Select a customer first";
            }
        }
        async Task DeleteProduct()
        {
            try
            {
                Uri requestUri = new Uri("http://localhost:12878/DeleteCustomer/" + CustomerId.Text); //replace your Url       
                var objClint = new System.Net.Http.HttpClient();
                System.Net.Http.HttpResponseMessage respon = await objClint.DeleteAsync(requestUri);
                string responJsonText = await respon.Content.ReadAsStringAsync();
                ErrorMessage.Text = responJsonText;
            }
            catch
            {
                ErrorMessage.Text = "Server error";
            }
        }

        private void BtnOrders_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerId.Text != "")
            {
               // Customer customer = new Customer(int.Parse( CustomerId.Text),Name.Text,Email.Text,Phone.Text);
                //string a=CustomerId.Text;
                Frame.Navigate(typeof(CustomerOrder),CustomerId.Text);
            }
            else
                ErrorMessage.Text = "Select a customer first";

        }
    }
}
