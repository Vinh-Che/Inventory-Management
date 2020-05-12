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
    public sealed partial class ProductOpearation : Page
    {
        private List<Product> Products = new List<Product>();
        public ProductOpearation()
        {
            this.InitializeComponent();
            FillProducts();
            FillCategory();
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var product = (Product)e.ClickedItem;
            ProductId.Text = product.Id.ToString();
            ProductName.Text = product.Name;
            ProductDescription.Text = product.Description;
            ProductPrice.Text = product.Price.ToString();
            ProductQuantity.Text = product.Quantity.ToString();
            CategoryId.Text = product.CategoryId.ToString();
           
        }
        private void ProductQuantity_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }
        public async Task FillProducts()

        {
            try
            {
                string sURL = "http://localhost:12878/GetAllProducts";

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
                        string Description = jsonObject["description"].ToString().Trim(new char[] { (char)34 });
                        int quantity = Convert.ToInt32(jsonObject["quantity"].ToString());
                        double price = Convert.ToDouble(jsonObject["price"].ToString());
                        int catid = Convert.ToInt32(jsonObject["categoryId"].ToString());


                        // Products.Add(new Product(Id, Name,Description,quantity,price,catid));
                        dataGrid1.Items.Add(new Product(Id, Name, Description, quantity, price, catid));
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
          //  dataGrid1.ItemsSource = null;

            //   dataGrid1.Items.Clear();
            //   dataGrid1.Items.Add(Products);
            //      dataGrid1.Items.Refresh();
           // dataGrid1.Items.Add(1,"a");
           // dataGrid1.ItemsSource = Products;
        }

         void ClearValues()
        {
            ProductId.Text = String.Empty;
            ProductName.Text = String.Empty;
            ProductDescription.Text = String.Empty;
            ProductPrice.Text = String.Empty;
            ProductQuantity.Text = String.Empty;
            CategoryId.Text = string.Empty;
            ErrorMessage.Text = string.Empty;
        }
        private async void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (float.TryParse(ProductPrice.Text, out float f))
            {
                if (ProductId.Text!="" && ProductName.Text != "" && ProductDescription.Text != "" && ProductQuantity.Text != "" && ProductPrice.Text != "")
                {
                  await  UpdateProduct();
                  await   FillProducts();
                    // dataGrid1.Items.Clear();
                    // dataGrid1.ItemsSource = Products;
                    ClearValues();
                }
              else  ErrorMessage.Text = "input field can't be empty";
            }
            else
            {
                ErrorMessage.Text = "Price Value is in invalid format";
            }
           
        }
        public async Task UpdateProduct()
        {
            try
            {
                Uri requestUri = new Uri("http://localhost:12878/UpdateProduct/" + ProductId.Text); //replace your Url  
                dynamic dynamicJson = new ExpandoObject();
                dynamicJson.Name = ProductName.Text;
                dynamicJson.Description = ProductDescription.Text;
                dynamicJson.Price = ProductPrice.Text;
                dynamicJson.Quantity = ProductQuantity.Text;
                dynamicJson.CategoryId = CategoryId.Text;
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

        private async void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ProductId.Text != "")
            {
                await DeleteProduct();
                await FillProducts();
                ClearValues();
            }
            else ErrorMessage.Text = "Select a product first!!";
        }

        async Task DeleteProduct()
        {
            try
            { Uri requestUri = new Uri("http://localhost:12878/DeleteProduct/" + ProductId.Text); //replace your Url       
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

        private async void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            ProductId.Text= String.Empty;
            ProductName.Text = String.Empty;
            ProductDescription.Text = String.Empty;
            ProductPrice.Text = String.Empty;
            ProductQuantity.Text = String.Empty;
            CategoryId.Text = string.Empty;
           await FillProducts();
        }

        private async void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (ProductName.Text != "" && ProductDescription.Text != "" && ProductQuantity.Text != "" && ProductPrice.Text != "" && combo.SelectedValue.ToString() != "")
            {
               await AddProduct();
                await FillProducts();
                ClearValues();
            }
            else ErrorMessage.Text = "input field can't be empty";

        }
        public async Task AddProduct()
        {
            try
            {
                Uri requestUri = new Uri("http://localhost:12878/AddProduct"); //replace your Url  
                dynamic dynamicJson = new ExpandoObject();
                dynamicJson.Name = ProductName.Text;
                dynamicJson.Description = ProductDescription.Text;
                dynamicJson.Price = ProductPrice.Text;
                dynamicJson.Quantity = ProductQuantity.Text;
                dynamicJson.CategoryId = CategoryId.Text;
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

        async Task FillCategory()
        {
            try
            {
                string sURL = "http://localhost:12878/GetAllCategory";

                HttpClient client = new HttpClient();
                string sResponse = await client.GetStringAsync(sURL);
                JsonArray jsonArray = JsonArray.Parse(sResponse);
                if (jsonArray.Count > 0)
                {
                    foreach (var jsonRow in jsonArray)
                    {
                        JsonObject jsonObject = jsonRow.GetObject();
                        // JsonValue row =jsonRow.GetString()
                        string Id = jsonObject["id"].ToString();
                        string Name = jsonObject["name"].ToString();

                        Name = Name.Trim(new char[] { (char)34 });
                        // test.Text = Name;
                        combo.Items.Add(Id + "   " + Name);

                    }
                }
                else
                {
                    ErrorMessage.Text = "No categories to display! Create a category to add new product!!";
                }
            }
            catch
            {
                ErrorMessage.Text = "* Server Error";
            }
        }
        private void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            CategoryId.Text = comboBox.SelectedValue.ToString().Split(' ')[0].ToString();
          //  CategoryId.Text = vals.ToString();
        }
    }
}
