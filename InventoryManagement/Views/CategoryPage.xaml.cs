using System;
using System.Dynamic;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using InventoryManagement.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace InventoryManagement
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CategoryPage : Page
    {
        public  CategoryPage()
        {
            this.InitializeComponent();
            FillCategories();
        }

        public async Task AddCategory()
        {
            try
            {
                Uri requestUri = new Uri("http://localhost:12878/AddCategory/"); //replace your Url  
                dynamic dynamicJson = new ExpandoObject();
                dynamicJson.Name = Name.Text;

                string json = "";
                json = Newtonsoft.Json.JsonConvert.SerializeObject(dynamicJson);
                var objClint = new System.Net.Http.HttpClient();
                System.Net.Http.HttpResponseMessage respon = await objClint.PostAsync(requestUri, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
                string responJsonText = await respon.Content.ReadAsStringAsync();
                //   ErrorMessage.Foreground = Brushes.Green;
                ErrorMessage.Text = responJsonText;
            }
            catch { ErrorMessage.Text = "Server error!"; }
        }
        public async Task FillCategories()

        {
            try
            {
                string sURL = "http://localhost:12878/GetAllCategory";

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

                        dataGrid1.Items.Add(new Category(Id, Name));
                    }
                }
                else
                {
                    dataGrid1.Items.Clear();
                    ErrorMessage.Text = "No Data to display";
                }
            }
            catch { ErrorMessage.Text = "Server error!"; }
        }
        private void GridView_ItemClick(object sender, ItemClickEventArgs e)

        {
            var category = (Category)e.ClickedItem;
            CategoryId.Text = category.Id.ToString();
            Name.Text = category.Name;

        }

        private async void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text != "" )
            {
                await AddCategory();
                await FillCategories();
            }
            else
            {
                ErrorMessage.Text = "Name can't be empty!!";
            }
        }

        private async void ButtonClr_Click(object sender, RoutedEventArgs e)
        {
            ErrorMessage.Text = string.Empty;
            ErrorProducts.Text = string.Empty;
            CategoryId.Text = string.Empty;
            Name.Text = string.Empty;
            await FillCategories();
            dataGrid2.Items.Clear();
        }

        private async void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (CategoryId.Text != "" && Name.Text != "" )
            {
                await UpdateCategory();
                await FillCategories();
            }
            else
                ErrorMessage.Text = "Select the category first!!";
        }
        private async void ButnDlt_Click(object sender, RoutedEventArgs e)
        {
            if (CategoryId.Text != "")
            {
                await DeleteCategory();
                await FillCategories();
            }
            else
                ErrorMessage.Text = "Select the category first!!";
        }
        public async Task UpdateCategory()
        {
            try
            {
                Uri requestUri = new Uri("http://localhost:12878/UpdateCategory/" + CategoryId.Text); //replace your Url  
                dynamic dynamicJson = new ExpandoObject();
                dynamicJson.Name = Name.Text;
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
                ErrorMessage.Text = "Server Error";
            }
        }

        async Task DeleteCategory()
        {
            try
            {
                Uri requestUri = new Uri("http://localhost:12878/DeleteCategory/" + CategoryId.Text); //replace your Url       
                var objClint = new System.Net.Http.HttpClient();
                System.Net.Http.HttpResponseMessage respon = await objClint.DeleteAsync(requestUri);
                string responJsonText = await respon.Content.ReadAsStringAsync();
                ErrorMessage.Text = responJsonText;
            }
            catch
            {
                ErrorMessage.Text = "Server Error";
            }
        }

        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (CategoryId.Text != "")
            {
                await FillProducts();
            }
            else ErrorProducts.Text = "Select the category";
        }
        public async Task FillProducts()

        {
            try
            {
                string sURL = "http://localhost:12878/ProductsByCategory/" + CategoryId.Text;

                HttpClient client = new HttpClient();

                string sResponse = await client.GetStringAsync(sURL);


                JsonArray jsonArray = JsonArray.Parse(sResponse);
                if (jsonArray.Count > 0)
                {
                   
                    ErrorProducts.Text = string.Empty;
                    dataGrid2.Items.Clear();
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
                        dataGrid2.Items.Add(new Product(Id, Name, Description, quantity, price, catid));
                    }
                }
                else

                {
                    dataGrid2.Items.Clear();
                    ErrorProducts.Text = "No products to display";
                }
            }
            catch
            {
                ErrorProducts.Text = "Server error";
            }
  
        }
    }
}
