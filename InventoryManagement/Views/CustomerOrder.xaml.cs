using InventoryManagement.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Data.Json;
using Windows.Graphics.Printing;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Printing;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace InventoryManagement.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomerOrder : Page
    {
        private PrintManager printMan;
        private PrintDocument printDoc;
        private IPrintDocumentSource printDocSource;
      //  private Printhe
        //  private FixedDocumentSequence _document;
        public CustomerOrder()
        {
            this.InitializeComponent();
            dateText.Text = DateTime.Now.ToShortDateString();
          //  GetCustomer();
        }
       
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
          cId.Text=  e.Parameter as string;
            //base.OnNavigatedTo(e);
          await  GetCustomer();
          await  FillOrders();
            //  printHelper = new PrintHelper(this);
            // if (printMan != null)
            // {
            // UnregisterForPrinting();
            // }
           
            RegisterPrint();
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            UnregisterForPrinting();
        }
        void RegisterPrint()
        {
            printMan = PrintManager.GetForCurrentView();
            printMan.PrintTaskRequested += PrintTaskRequested;

            // Build a PrintDocument and register for callbacks
            printDoc = new PrintDocument();
            printDocSource = printDoc.DocumentSource;
            //  printDoc.Paginate += Paginate;
            printDoc.GetPreviewPage += GetPreviewPage;
            printDoc.AddPages += AddPages;
        }

        public  void UnregisterForPrinting()
        {
            if (printDoc == null)
            {
                return;
            }

          //  printDoc.Paginate -= CreatePrintPreviewPages;
            printDoc.GetPreviewPage -= GetPreviewPage;
            printDoc.AddPages -= AddPages;

            PrintManager printMan = PrintManager.GetForCurrentView();
            printMan.PrintTaskRequested -= PrintTaskRequested;
        }
     
        private  void GetPreviewPage(object sender, GetPreviewPageEventArgs e)
        {
            PrintDocument printDoc = (PrintDocument)sender;
            printDoc.SetPreviewPage(e.PageNumber, Canvas1);
        }

        private void Paginate(object sender, PaginateEventArgs e)
        {

            throw new NotImplementedException();
        }

      
        
        public async Task GetCustomer()

        {
            try
            {
                string sURL = "http://localhost:12878/GetCustomerById/"+ cId.Text ;

                HttpClient client = new HttpClient();

                string sResponse ="["+ await client.GetStringAsync(sURL)+"]";

                JsonArray jsonArray = JsonArray.Parse(sResponse);
                if (jsonArray.Count > 0)
                {
                  //  dataGrid1.Items.Clear();
                    foreach (var jsonRow in jsonArray)

                    {

                        JsonObject jsonObject = jsonRow.GetObject();

                        int Id = Convert.ToInt32(jsonObject["id"].ToString());

                        string name = (jsonObject["name"].ToString()).Trim(new char[] { (char)34 });
                        string email = jsonObject["email"].ToString().Trim(new char[] { (char)34 });
                        string phone = jsonObject["phone"].ToString().Trim(new char[] { (char)34 });
                        cId.Text = Id.ToString();
                        Name.Text = name.ToString(); 
                        // Email.Text = email;
                        Email.Text = email;
                        Phone.Text = phone;
                       // dataGrid1.Items.Add(new Customer(Id, Name, Email, Phone));
                    }
                }
                else
                {
                  
                    ErrorMessage.Text = "No Data to display";
                }
            }
            catch
            {
                ErrorMessage.Text = "Server error";
            }
        }

        public async Task FillOrders()

        {
            try
            {
                string sURL = "http://localhost:12878/GetOrderByCustomer/" + cId.Text;

                HttpClient client = new HttpClient();

                string sResponse = await client.GetStringAsync(sURL);

                JsonArray jsonArray = JsonArray.Parse(sResponse);
                if (jsonArray.Count > 0)
                {
                    // OrderText.Text = string.Empty;
                    dataGrid1.Items.Clear();
                    foreach (var jsonRow in jsonArray)

                    {

                        JsonObject jsonObject = jsonRow.GetObject();

                        int Id = Convert.ToInt32(jsonObject["id"].ToString());

                        string productName = (jsonObject["productName"].ToString()).Trim(new char[] { (char)34 });
                        string created = (jsonObject["created"].ToString().Trim(new char[] { (char)34 }));
                        int quantity = Convert.ToInt32(jsonObject["quantity"].ToString());
                        double orderamount = Convert.ToDouble(jsonObject["amount"].ToString());
                        int custid = Convert.ToInt32(jsonObject["customerId"].ToString());
                        int productid = Convert.ToInt32(jsonObject["productId"].ToString());

                        // OrderId.Text = productName;
                        // Products.Add(new Product(Id, Name,Description,quantity,price,catid));
                        dataGrid1.Items.Add(new OrderList(Id, productName, quantity, orderamount, created, custid, productid));
                    }
                }
                else
                {
                    dataGrid1.Items.Clear();
                    ErrorMessage.Text = "No Orders to display";
                }
            }
            catch
            { ErrorMessage.Text = "server error"; }
            }

        private  void BackBtn_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.GoBack();
         
        }

      
        private void PrintTaskRequested(PrintManager sender, PrintTaskRequestedEventArgs e)
        {
            PrintTask printTask = null;
            printTask = e.Request.CreatePrintTask("C# Printing SDK Sample", sourceRequested =>
            {
                printTask.Completed += async (s, args) =>
                {
                    if (args.Completion == PrintTaskCompletion.Failed)
                    {
                        await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
                        {
                            ContentDialog dialog = new ContentDialog
                            {
                                Title = "Printing error",
                                Content = "\nSorry, failed to print.Task error",
                                PrimaryButtonText = "OK"
                            };
                            //("Something went wrong while trying to print. Please try again.");
                            await dialog.ShowAsync();
                        });
                    }
                };
                sourceRequested.SetSource(printDocSource);
            });
        }

        private void PrintTaskSourceRequrested(PrintTaskSourceRequestedArgs args)
        {
            // Set the document source.
            args.SetSource(printDocSource);
        }

//#endregion
        private void AddPages(object sender, AddPagesEventArgs e)
        {
            printDoc.AddPage(this.Canvas1);

            // Indicate that all of the print pages have been provided
            printDoc.AddPagesComplete();
        }

        private async void PrintTaskCompleted(PrintTask sender, PrintTaskCompletedEventArgs args)
        {
            // Notify the user when the print operation fails.
            if (args.Completion == PrintTaskCompletion.Failed)
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                {
                    ContentDialog noPrintingDialog = new ContentDialog()
                    {
                        Title = "Printing error",
                        Content = "\nSorry, failed to print.Prevous Task is not done yet",
                        PrimaryButtonText = "OK"
                    };
                    await noPrintingDialog.ShowAsync();
                });
            }
        }

        private async void PrintButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (PrintManager.IsSupported())
            {
                //if (printMan != null)
                //    UnregisterForPrinting();
                try
                {
                    //  RegisterPrint();
                    // Show print UI
                  //  UnregisterForPrinting();
                  
                    await PrintManager.ShowPrintUIAsync();
                  //  UnregisterForPrinting();
                }
                catch
                {
                    // Printing cannot proceed at this time
                    ContentDialog noPrintingDialog = new ContentDialog()
                    {
                        Title = "Printing error",
                        Content = "\nSorry, printing can't proceed at this time.",
                        PrimaryButtonText = "OK"
                    };
                    await noPrintingDialog.ShowAsync();
                }
            }
            else
            {
                // Printing is not supported on this device
                ContentDialog noPrintingDialog = new ContentDialog()
                {
                    Title = "Printing not supported",
                    Content = "\nSorry, printing is not supported on this device.",
                    PrimaryButtonText = "OK"
                };
                await noPrintingDialog.ShowAsync();
            }
        }
    }
}



