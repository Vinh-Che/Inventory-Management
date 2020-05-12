using System;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace InventoryManagement
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Order : Page
    {
        public Order()
        {
            this.InitializeComponent();
            FillCategory();
            FillCustomers();
            FillOrders();
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
                else ErrorMessage.Text = "No Data to display";
            }
            catch { }
        }

        private async void CategoryText_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            var vals = comboBox.SelectedValue.ToString().Split(' ')[0];
            CategoryId.Text = vals.ToString();
            await FillProducts();
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
                        CategoryText.Items.Add(Id + "   " + Name);

                    }
                }
                else
                {
                    ErrorMessage.Text = "No categories available to display";
                }
            }
            catch
            {
                ErrorMessage.Text = "* Server Error";
            }
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
                    ErrorProducts.Text = "No Product to display";
                }
            }
            catch
            {
                ErrorProducts.Text = "Server error";
            }

        }
       static string customerId, mail, name;
        static string productId, productName, price,quantity;

        private async void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            if (ProductId.Text != "" && CustomerId.Text != "" && ProductQuantity.Text != "")
            {
                if (int.Parse(ProductQuantity.Text) <= int.Parse(QuantityRemain.Text))
                {
                    int quan = -int.Parse(ProductQuantity.Text) ;
                    await AddOrder();
                    await UpdateProductQuantity(quan,int.Parse(ProductId.Text));
                    await FillOrders();
                    await FillProducts();
                    ClearField();
                }
                else
                {
                    OrderText.Text = "Quantity exceeds!";
                }
            }
            else
            {
                OrderText.Text = "Any field can't be empty";
            }
        }
        async Task AddOrder()
        {
            try
            {
                Uri requestUri = new Uri("http://localhost:12878/AddOrder"); //replace your Url  
                dynamic dynamicJson = new ExpandoObject();
                dynamicJson.CustomerId = CustomerId.Text;
                dynamicJson.ProductId = ProductId.Text;
                dynamicJson.ProductName = Productname.Text;
                dynamicJson.Quantity = ProductQuantity.Text;
                dynamicJson.Amount = ProductPrice.Text;
                dynamicJson.Created = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
                string json = "";
                json = Newtonsoft.Json.JsonConvert.SerializeObject(dynamicJson);
                var objClint = new System.Net.Http.HttpClient();
                System.Net.Http.HttpResponseMessage respon = await objClint.PostAsync(requestUri, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
                string responJsonText = await respon.Content.ReadAsStringAsync();
                OrderText.Text = responJsonText;
            }
            catch
            {
                OrderText.Text = "Server error";
            }
        }

        private void BtnClr_Click(object sender, RoutedEventArgs e)
        {
            //ProductId.Text = string.Empty;
            //Productname.Text= string.Empty;
            //ProductPrice.Text= string.Empty;
            //CustomerId.Text = string.Empty;
            //CustomerName.Text = string.Empty;
            //CustomerMail.Text = string.Empty;
            ClearField();

        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (productId != "")
            {
                ProductId.Text = productId;
                Productname.Text = productName;
                ProductPrice.Text = price;
                QuantityRemain.Text = quantity;
            }
            else
            {
                ErrorProducts.Text = "Select the product first!";
            }
        }

        private void dataGrid3_ItemClick(object sender, ItemClickEventArgs e)
        {
            var order = (OrderList)e.ClickedItem;
            OrderId.Text = order.Id.ToString();
            orderQuantity.Text = order.Quantity.ToString();
            pId.Text = order.ProductId.ToString();
        }

        private async void BtnOrders_Click(object sender, RoutedEventArgs e)
        {
            await FillOrders();
        }

        private async void BtnDlOrde_Click(object sender, RoutedEventArgs e)
        {
            if (OrderId.Text != "")
            {
                await DeleteOrder();
                await UpdateProductQuantity(int.Parse(orderQuantity.Text),int.Parse(pId.Text));
                await FillOrders();
                await FillProducts();
            }
            else
            {
                OrderText.Text = "Select the oeder first";
            }
        }

        private void dataGrid2_ItemClick(object sender, ItemClickEventArgs e)
        {
            var product = (Product)e.ClickedItem;
            productId = product.Id.ToString();
            productName = product.Name.ToString();
            price = product.Price.ToString();
            quantity = product.Quantity.ToString();

        }

        private void ProductQuantity_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var customer = (Customer)e.ClickedItem;
            customerId = customer.Id.ToString();
           name = customer.Name;
            mail = customer.Email;
        }
        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (customerId!="" )
            { CustomerId.Text = customerId;
                CustomerName.Text = name;
                CustomerMail.Text = mail;
            }
            else
            {
                ErrorMessage.Text = "Select the customer first!";
            }
        }

        public async Task FillOrders()

        {
            try
            {
                string sURL = "http://localhost:12878/GetOrders";

                HttpClient client = new HttpClient();

                string sResponse = await client.GetStringAsync(sURL);

                JsonArray jsonArray = JsonArray.Parse(sResponse);
                if (jsonArray.Count > 0)
                {
                   // OrderText.Text = string.Empty;
                    dataGrid3.Items.Clear();
                    foreach (var jsonRow in jsonArray)

                    {

                        JsonObject jsonObject = jsonRow.GetObject();

                        int Id = Convert.ToInt32(jsonObject["id"].ToString());

                        string productName = (jsonObject["productName"].ToString()).Trim(new char[] { (char)34 });   
                        string created =( jsonObject["created"].ToString().Trim(new char[] { (char)34 }));
                        int quantity = Convert.ToInt32(jsonObject["quantity"].ToString());
                        double orderamount = Convert.ToDouble(jsonObject["amount"].ToString());
                        int custid = Convert.ToInt32(jsonObject["customerId"].ToString());
                        int productid = Convert.ToInt32(jsonObject["productId"].ToString());

                       // OrderId.Text = productName;
                        // Products.Add(new Product(Id, Name,Description,quantity,price,catid));
                        dataGrid3.Items.Add(new OrderList(Id, productName, quantity, orderamount, created,custid,productid));
                    }
                }
                else
                {
                    dataGrid3.Items.Clear();
                   // OrderText.Text = "No Orders to display";
                }
            }
            catch
            { OrderText.Text = "server error"; }

        }

        async Task DeleteOrder()
        {
            try
            {
                Uri requestUri = new Uri("http://localhost:12878/DeleteOrder/" + OrderId.Text); //replace your Url       
                var objClint = new System.Net.Http.HttpClient();
                System.Net.Http.HttpResponseMessage respon = await objClint.DeleteAsync(requestUri);
                string responJsonText = await respon.Content.ReadAsStringAsync();
                OrderText.Text = responJsonText;
            }

            catch
            {
                OrderText.Text = "server error";
            }
        }

        void ClearField()
        {
            ProductId.Text = string.Empty;
            Productname.Text = string.Empty;
            ProductPrice.Text = string.Empty;
            CustomerId.Text = string.Empty;
            CustomerName.Text = string.Empty;
            CustomerMail.Text = string.Empty;
            productId = string.Empty;
            productName = string.Empty;
            price = string.Empty;
            customerId = string.Empty;
            name = string.Empty;
            mail = string.Empty;
            
        }
        public async Task UpdateProductQuantity(int quantityPro,int productId)
        {
            try
            {
                Uri requestUri = new Uri("http://localhost:12878/UpdateQuantity/" + productId+"/"+quantityPro); //replace your Url  
                dynamic dynamicJson = new ExpandoObject();
              
                dynamicJson.Quantity = quantityPro.ToString();
             
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
    }
}
