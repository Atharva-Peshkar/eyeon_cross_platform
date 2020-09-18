using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using FireAuth;
using FireAuth.Models;
using GoogleVisionBarCodeScanner;

namespace FireAuth.Helper_Class
{
    class FirebaseHelper
    {
        private readonly string ChildName = "products";
        private readonly string OrderChildName = "orders";

        readonly FirebaseClient firebase = new FirebaseClient("https://eyeon-baf78.firebaseio.com/");

        //Helper functions to conduct business operations using Firebase realtime database.
        public async Task<List<product>> GetAllProducts()
        {
            return (await firebase
                .Child(ChildName)
                .OnceAsync<product>()).Select(item => new product
                {
                    ProductBarcode = item.Object.ProductBarcode,
                    Product = item.Object.Product,
                    Price = item.Object.Price,
                    Weight = item.Object.Weight
                }).ToList();
        }

        public async Task<product> GetProduct(string product_barcode)
        {
            var allproducts = await GetAllProducts();
            await firebase
                .Child(ChildName)
                .OnceAsync<product>();
            return allproducts.FirstOrDefault(a => a.ProductBarcode == product_barcode);
        }

        public async Task AddOrder(string orderID, string productBarcode, string price, string productName)
        {
            await firebase
                .Child(OrderChildName)
                .Child(orderID)
                .PostAsync(new product(){ProductBarcode = productBarcode, Price = price, Product=productName });
        }
    }
}
