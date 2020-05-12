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
    public sealed partial class AddProduct : Page
    {
        public AddProduct()
        {
            this.InitializeComponent();
        }
       
        //private void ProductQuantity_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        //{
        //    args.Cancel = args.NewText.Any(c => !char.IsDigit(c) );
        //}

        private async void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (AdminUsername.Text != "" &&AdminName.Text!="" && AdminEmail.Text != "" && Adminpassword.Password.ToString() != "" && AdminPhone.Text != "")

            {
                await AddUser();
                Frame.Navigate(typeof(Login));
            }
            else
            {
                ErrorMessage.Text = "Input can't be empty!!";
            }
        }
        //public async void POSTreq()
        //{
        //   // string Category= combo.SelectedValue.ToString();
        //   // Category =Category.Split(' ')[0];
        //    Uri requestUri = new Uri("http://localhost:7482/AddProduct"); //replace your Url  
        //    dynamic dynamicJson = new ExpandoObject();
        // //   JsonObject obj =await Get();
        //    dynamicJson.Name = ProductName.Text;
        //    dynamicJson.Description = ProductDescription.Text;
        //    dynamicJson.Price = ProductPrice.Text;
        //    dynamicJson.Quantity = ProductQuantity.Text;          
        //    dynamicJson.CategoryId = 1;
        //    string json = "";
        //    json = Newtonsoft.Json.JsonConvert.SerializeObject(dynamicJson);
        //    var objClint = new System.Net.Http.HttpClient();
        //    System.Net.Http.HttpResponseMessage respon = await objClint.PostAsync(requestUri, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
        //    string responJsonText = await respon.Content.ReadAsStringAsync();
        //    ErrorMessage.Text = responJsonText;
        //}
        
       public async Task<JsonObject>  Get()
        {
            string sURL = "http://localhost:12878/GetCategory/1";
            JsonObject jsonObject1 = new JsonObject();
            HttpClient client = new HttpClient();
            string sResponse ="["+ await client.GetStringAsync(sURL)+"]";
           
            JsonArray jsonArray = JsonArray.Parse(sResponse);
            if (jsonArray.Count > 0)
            {
                jsonObject1 = jsonArray[0].GetObject();
             /*   foreach (var jsonRow in jsonArray)
                {
                     jsonObject1 = jsonRow.GetObject();
                    
                }*/
               
            }
            return jsonObject1;
        }

        //async void FillCategory()
        //{
        //    try
        //    {
        //        string sURL = "http://localhost:7482/GetAllCategory";

        //        HttpClient client = new HttpClient();
        //        string sResponse = await client.GetStringAsync(sURL);
        //        JsonArray jsonArray = JsonArray.Parse(sResponse);
        //        if (jsonArray.Count > 0)
        //        {
        //            foreach (var jsonRow in jsonArray)
        //            {
        //                JsonObject jsonObject = jsonRow.GetObject();
        //               // JsonValue row =jsonRow.GetString()
        //                string Id = jsonObject["id"].ToString();
        //                string Name = jsonObject["name"].ToString();
                       
        //               Name= Name.Trim(new char[] { (char)34 });
        //               // test.Text = Name;
        //                combo.Items.Add(Id+"   "+ Name);

        //            }
        //        }
        //        else
        //        {

        //        }
        //    }
        //    catch
        //    {
        //        ErrorMessage.Text = "* Server Error";
        //    }
        //}

        //private void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ComboBox comboBox = sender as ComboBox;
        //    var vals = comboBox.SelectedValue.ToString().Split(' ')[0];
        //    test.Text = vals.ToString();
        //}
        async Task AddUser()
        {try
            {
                Uri requestUri = new Uri("http://localhost:12878/AddAdmin"); //replace your Url  
                dynamic dynamicJson = new ExpandoObject();
                dynamicJson.Name = AdminName.Text;
                dynamicJson.UserName = AdminUsername.Text;
                dynamicJson.Password = Adminpassword.Password.ToString();
                dynamicJson.Phone = AdminPhone.Text;
                dynamicJson.Email = AdminEmail.Text;

                string json = "";
                json = Newtonsoft.Json.JsonConvert.SerializeObject(dynamicJson);
                var objClint = new System.Net.Http.HttpClient();
                System.Net.Http.HttpResponseMessage respon = await objClint.PostAsync(requestUri, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
                string responJsonText = await respon.Content.ReadAsStringAsync();
                ErrorMessage.Text = responJsonText;
            }
            catch
            {
                ErrorMessage.Text = "Server error";
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login));
        }
    }
}
