using FireAuth.Helper_Class;
using FireAuth.Models;
using GoogleVisionBarCodeScanner;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;


namespace FireAuth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoggedPage : ContentPage
    {
        public ObservableCollection<product> ShoppingCart { get; set; }
        string orderuid = string.Empty;

        //Google vision API is used for barcode scanner.
        public LoggedPage()
        {
            InitializeComponent();
            GoogleVisionBarCodeScanner.Methods.SetSupportBarcodeFormat(BarcodeFormats.Ean13);
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            ShoppingCart = new ObservableCollection<product>();
            ShoppingCartListView.ItemsSource = ShoppingCart;
            ShoppingCartListView.ItemTapped += OnListItemTapped;
        }


        private void LogoutButton_Clicked(object sender, EventArgs e)
        {

            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PopToRootAsync(true);
                await DisplayAlert("Logged out successfully!", "Be back soon :)", "Ok");
            }); 
        }

        private void FlashlightButton_Clicked(object sender, EventArgs e)
        {
            GoogleVisionBarCodeScanner.Methods.ToggleFlashlight();
        }

        private int GenerateRandomNo()
        {
            int _min = 100000;
            int _max = 999999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }
        private async void Checkout_Clicked(object sender, EventArgs e)
        {
            FirebaseHelper OrderPlacer = new FirebaseHelper();
            bool choice = await DisplayAlert("Hi!","You're about to checkout","Proceed","Cancel");
            if (choice == true)
            {
                if (ShoppingCart.Count!=0)
                {
                    orderuid = GenerateRandomNo().ToString();
                    for (int j = 0; j < ShoppingCart.Count; j++)
                    {
                        try
                        {
                            await OrderPlacer.AddOrder(orderuid, ShoppingCart[j].ProductBarcode, ShoppingCart[j].Price, ShoppingCart[j].Product);
                        }
                        catch (Exception a) { await DisplayAlert("Oops! An error occurred.", "Please try again!", "Ok"); }
                    }

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        GoogleVisionBarCodeScanner.Methods.SetIsScanning(false);
                        await DisplayAlert("Order succesful!", $"Your order ID is: {orderuid}{Environment.NewLine}Please note it for future reference.", "Proceed");
                        await Navigation.PushAsync(new BillSummary(ShoppingCart));
                    });
                }

                else
                {
                    await DisplayAlert("Cannot Proceed!","Your cart is empty.","Ok");
                }
            }
        }

        private void ShoppingList_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TodoListPage());
        }

        private async void ClearCart(object sender, EventArgs e)
        {
            bool choice = await DisplayAlert("Alert!", "Are you sure you want to clear cart?","Yes","No");
            if (choice == true)
            {
                ShoppingCart.Clear();
            }
        }
        private async void OnListItemTapped(object sender, ItemTappedEventArgs e)
        {
            product selectedItem = e.Item as product;
            string iteminfo;
            if (selectedItem != null)
            {
                iteminfo = $"{Environment.NewLine}Barcode : {selectedItem.ProductBarcode}{Environment.NewLine}{Environment.NewLine}Product : {selectedItem.Product}{Environment.NewLine}{Environment.NewLine}Price : Rs.{selectedItem.Price}{Environment.NewLine}{Environment.NewLine}Weight : {selectedItem.Weight} Grams{Environment.NewLine}{Environment.NewLine}Do you want to remove {selectedItem.Product} from the cart?";
                bool choice = await DisplayAlert("Product Details",iteminfo,"Yes, please","No, thanks");
                if (choice == true)
                {
                    ShoppingCart.Remove(selectedItem);
                }
            }
        }
        private async void CameraView_OnDetected(object sender, GoogleVisionBarCodeScanner.OnDetectedEventArg e)
        {
            List<GoogleVisionBarCodeScanner.BarcodeResult> obj = e.BarcodeResults;
            FirebaseHelper firebaseHelper = new FirebaseHelper();
            product request = new product();
            string result = string.Empty;
            for (int i = 0; i < obj.Count; i++)
            {
                try
                {
                    request = await firebaseHelper.GetProduct(obj[i].DisplayValue);
                    result += $"{Environment.NewLine}Barcode : {obj[i].DisplayValue}{Environment.NewLine}{Environment.NewLine}Product : {request.Product}{Environment.NewLine}{Environment.NewLine}Price : Rs.{request.Price}{Environment.NewLine}{Environment.NewLine}Weight : {request.Weight} Grams{Environment.NewLine}";
                    bool choice = await DisplayAlert("Product Details", result, "Add to cart","Cancel");
                    result = string.Empty;
                    if (choice == true)
                    {
                        ShoppingCart.Add(new product
                        {
                            Product = request.Product,
                            Price = request.Price,
                            Weight = request.Weight,
                            ProductBarcode = request.ProductBarcode,
                        });
                    }
                }
                catch (System.NullReferenceException) { await DisplayAlert("Oops! An error occurred.", "Please try again!", "Ok"); }
            }
            Device.BeginInvokeOnMainThread(async () =>
            {
                GoogleVisionBarCodeScanner.Methods.SetIsScanning(true);
            });

        }
    }
}