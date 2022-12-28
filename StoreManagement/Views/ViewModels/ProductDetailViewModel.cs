using StoreManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace StoreManagement.Views.ViewModels
{
    public class ProductDetailViewModel: BaseViewModel
    {
        Product product = new Product();

        #region Command
        public ICommand LoadCommand { get; set; }

        #endregion

        #region Properties
        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { if (value == _DisplayName) return; _DisplayName = value; OnPropertyChanged(); } }

        private string _Brand;
        public string Brand { get => _Brand; set { if (value == _Brand) return; _Brand = value; OnPropertyChanged(); } }

        private string _Description;
        public string Description { get => _Description; set { if (value == _Description) return; _Description = value; OnPropertyChanged(); } }

        private double _Price;
        public double Price { get => _Price; set { if (value == _Price) return; _Price = value; OnPropertyChanged(); } }


        private int _Quantity;
        public int Quantity { get => _Quantity; set { if (value == _Quantity) return; _Quantity = value; OnPropertyChanged(); } }

        private string _ImageURL;
        public string ImageURL { get => _ImageURL; set { if (value == _ImageURL) return; _ImageURL = value; OnPropertyChanged(); } }

        #endregion

        public ProductDetailViewModel()
        {
            
            LoadCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                LoadDetail();
            });

        }
        private void LoadDetail()
        {
            product = (Product)Application.Current.Resources["ProductSelectedItem"];
            if (product != null)
            {
                ImageURL = product.ImageURL;
                DisplayName = product.DisplayName;
                Brand = product.Brand.BrandName;
                Description = product.Description;
                Quantity = product.Quantity;
                Price = product.Price;
            }
            Application.Current.Resources.Remove("ProductSelectedItem");

        }
    }
}
