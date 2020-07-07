using GoogleVisionBarCodeScanner;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace FireAuth.Models
{
    public class product: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _barcode;
        private string _product;
        private string _price;
        private string _weight;
        public string ProductBarcode
        {
            get { return _barcode; }
            set
            {
                if (_barcode == value) { return; }
                this._barcode = value;
                this.OnPropertyChanged("ProductBarcode");
            }
        }
        public string Product
        {
            get { return _product; }
            set
            {
                if (_product == value) { return; }
                this._product = value;
                this.OnPropertyChanged("Product");
            }
        }
        public string Price
        {
            get { return _price; }
            set
            {
                if (_price == value) { return; }
                this._price = value;
                this.OnPropertyChanged("Price");
            }
        }
        public string Weight
        {
            get { return _weight; }
            set
            {
                if (_weight == value) { return; }
                this._weight = value;
                this.OnPropertyChanged("Weight");
            }
        }

        private void OnPropertyChanged([CallerMemberName] string PropertyName=null)
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(PropertyName));
        }

    }
}
