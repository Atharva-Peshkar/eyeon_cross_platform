using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FireAuth.Models;
using System.Security.Cryptography.X509Certificates;

namespace FireAuth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BillSummary : ContentPage
    {
        private float Billt;
        private float CartW;
        public string Billtotal { get; }
        public string CartWeight { get; }
        public BillSummary(ObservableCollection<product> Bill)
        {
            InitializeComponent();
            for(int a=0; a < Bill.Count; a++)
            {
                try
                {
                    Billt += float.Parse(Bill[a].Price);
                    CartW += (float.Parse(Bill[a].Weight)/1000);
                } catch(Exception z) { DisplayAlert("Oops! An error occured", "Try again!", "Ok"); }
            }
            BillSummaryList.ItemsSource = Bill;
            BillSummaryList.ItemTapped += BillItemTapped;
            Billtotal = Billt.ToString();
            CartWeight = CartW.ToString();
            BindingContext = this;
    }

        private async void Appexit(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync(true);
            await DisplayAlert("Logged out succesfully!", "Be back soon :)","Ok");
        }

        private async void BillItemTapped(object sender, ItemTappedEventArgs e)
        {
            product TappedItem = e.Item as product;
            string iteminfo;
            if (TappedItem != null)
            {
                iteminfo = $"{Environment.NewLine} Barcode: {TappedItem.ProductBarcode}{Environment.NewLine}{Environment.NewLine} Product : {TappedItem.Product}{Environment.NewLine}{Environment.NewLine} Price : Rs.{TappedItem.Price}{Environment.NewLine}{Environment.NewLine} Weight : {TappedItem.Weight} Grams{Environment.NewLine}";
                await DisplayAlert("Product Details",iteminfo,"Ok");
            }
        }
    }
}