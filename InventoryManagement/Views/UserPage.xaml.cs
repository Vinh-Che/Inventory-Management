using InventoryManagement.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
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
    public sealed partial class UserPage : Page
    {
 
      
       
        public  UserPage()
        {
            this.InitializeComponent();

            FillUsers();
          
        }
    
        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
           var user = (UserAdmin)e.ClickedItem;
            AdminId.Text = user.Id.ToString();
            AdminName.Text = user.Name;
            AdminUsername.Text = user.UserName;
            Adminpassword.Text = user.Password;
            AdminEmail.Text = user.Email;
            AdminPhone.Text = user.Phone;
        }

        private async  void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (AdminName.Text != "" && AdminEmail.Text != "" && AdminPhone.Text != "" && Adminpassword.Text != "" && AdminUsername.Text != "")
            {
                await AddUser();
                await FillUsers();
            }
            else
            {
                ErrorMessage.Text = "Input field can't be empty";
            }

        }
        public async Task FillUsers()

        {
            try
            {
                string sURL = "http://localhost:12878/GetAllUser";

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
                        string userName = (jsonObject["userName"].ToString()).Trim(new char[] { (char)34 });
                        string Password = (jsonObject["password"].ToString()).Trim(new char[] { (char)34 });
                        string Email = jsonObject["email"].ToString().Trim(new char[] { (char)34 });
                        string Phone = jsonObject["phone"].ToString().Trim(new char[] { (char)34 });

                        dataGrid1.Items.Add(new UserAdmin(Id, Name, Email, userName, Password, Phone));
                    }
                }
                else
                {
                    dataGrid1.Items.Clear();
                    ErrorMessage.Text = "No Data to display";
                }
            }
            catch {
                ErrorMessage.Text = "Server error";
            }
        }

        async Task AddUser()
        {
            try
            {
                Uri requestUri = new Uri("http://localhost:12878/AddAdmin"); //replace your Url  
                dynamic dynamicJson = new ExpandoObject();
                dynamicJson.Name = AdminName.Text;
                dynamicJson.UserName = AdminUsername.Text;
                dynamicJson.Password = Adminpassword.Text;
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

        private async void ButonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (AdminId.Text!=""&& AdminName.Text != "" && AdminEmail.Text != "" && AdminPhone.Text != "" && Adminpassword.Text != "" && AdminUsername.Text != "")
            {
                await UpdateUser();
                await FillUsers();
            }
            else
            {
                ErrorMessage.Text = "Input field can't be empty";
            }
        }

        public async Task UpdateUser()
        {
            try
            {
                Uri requestUri = new Uri("http://localhost:12878/UpdateUser/" + AdminId.Text); //replace your Url  
                dynamic dynamicJson = new ExpandoObject();
                dynamicJson.Name = AdminName.Text;
                dynamicJson.UserName = AdminUsername.Text;
                dynamicJson.Password = Adminpassword.Text;
                dynamicJson.Phone = AdminPhone.Text;
                dynamicJson.Email = AdminEmail.Text;
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
                ErrorMessage.Text = "server error";
            }
        }

        private async void Btndel_Click(object sender, RoutedEventArgs e)
        {
            if (AdminId.Text != "")

            {
                await DeleteUser();
                await FillUsers();
            }
            else
            {
                ErrorMessage.Text = "Select the User first";
            }
        }
        async Task DeleteUser()
        {
            try

            {
                Uri requestUri = new Uri("http://localhost:12878/DeleteUser/" + AdminId.Text); //replace your Url       
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
        void Clearfield()
        {
            AdminId.Text = string.Empty;
            AdminName.Text = string.Empty;
            AdminEmail.Text = string.Empty;
            Adminpassword.Text = string.Empty;
            AdminUsername.Text = string.Empty;
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Clearfield();
            await FillUsers();
        }
    }
}
