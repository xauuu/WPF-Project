using Microsoft.Win32;
using StoreManagement.BUS;
using StoreManagement.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StoreManagement.Views.ViewModels
{
    public class UCProductViewModel : BaseViewModel
    {
        ProductBUS productBUS = new ProductBUS();
        private CategoryBUS categoryBUS = new CategoryBUS();
        private BrandBUS brandBUS = new BrandBUS();

        #region Command
        public ICommand LoadCommand { get; set; }
        public ICommand ProductSelectionChanged { get; set; }
        public ICommand ImageCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand CategorySelectionChanged { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        #endregion

        #region Properties
        private ObservableCollection<Product> _ListProducts;
        public ObservableCollection<Product> ListProducts { get => _ListProducts; set { if (value == _ListProducts) return; _ListProducts = value; OnPropertyChanged(); } }

        private ObservableCollection<Brand> _ListBrands;
        public ObservableCollection<Brand> ListBrands { get => _ListBrands; set { if (value == _ListBrands) return; _ListBrands = value; OnPropertyChanged(); } }

        private ObservableCollection<Category> _ListCategories;
        public ObservableCollection<Category> ListCategories { get => _ListCategories; set { if (value == _ListCategories) return; _ListCategories = value; OnPropertyChanged(); } }

        private Product _ProductSelectedItem;
        public Product ProductSelectedItem { get => _ProductSelectedItem; set { if (value == _ProductSelectedItem) return; _ProductSelectedItem = value; OnPropertyChanged(); } }

        private int _Id;
        public int Id { get => _Id; set { if (value == _Id) return; _Id = value; OnPropertyChanged(); } }

        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { if (value == _DisplayName) return; _DisplayName = value; OnPropertyChanged(); } }

        private Brand _BrandSelectedItem;
        public Brand BrandSelectedItem { get => _BrandSelectedItem; set { if (value == _BrandSelectedItem) return; _BrandSelectedItem = value; OnPropertyChanged(); } }

        private Category _CategorySelectedItem;
        public Category CategorySelectedItem { get => _CategorySelectedItem; set { if (value == _CategorySelectedItem) return; _CategorySelectedItem = value; OnPropertyChanged(); } }

        private double _Price;
        public double Price { get => _Price; set { if (value == _Price) return; _Price = value; OnPropertyChanged(); } }

        private string _Description;
        public string Description { get => _Description; set { if (value == _Description) return; _Description = value; OnPropertyChanged(); } }

        private int _Quantity;
        public int Quantity { get => _Quantity; set { if (value == _Quantity) return; _Quantity = value; OnPropertyChanged(); } }

        private string _ImageURL;
        public string ImageURL { get => _ImageURL; set { if (value == _ImageURL) return; _ImageURL = value; OnPropertyChanged(); } }

        private double? _OriginalPrice;
        public double? OriginalPrice { get => _OriginalPrice; set { if (value == _OriginalPrice) return; _OriginalPrice = value; OnPropertyChanged(); } }



        #endregion

        public UCProductViewModel()
        {
            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadList();
            });
            ProductSelectionChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadProductInfo();
            });
            ImageCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                GetImage();
            });
            AddCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (AddProduct())
                {
                    MessageBox.Show("Thêm sản phẩm thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Thêm sản phẩm thất bại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            });
            UpdateCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (UpdateProduct())
                {
                    MessageBox.Show("Sửa sản phẩm thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Sửa sản phẩm thất bại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            });
            CategorySelectionChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadListBrands();
            });
            DeleteCommand = new RelayCommand<Product>((p) => { return true; }, (p) =>
            {
                MessageBoxResult res = MessageBox.Show("Bạn có muốn xóa sản phẩm này không?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (res)
                {
                    case MessageBoxResult.Yes:
                        if (DeleteProduct())
                        {
                            MessageBox.Show("Xóa sản phẩm thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Xóa sản phẩm thất bại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                    default:
                        break;
                }
            });
        }

        private bool DeleteProduct()
        {
            if (ProductSelectedItem != null)
            {
                productBUS.Delete(ProductSelectedItem);

                ProductSelectedItem = null;
                LoadList();
                return true;
            }
            return false;
        }

        private bool UpdateProduct()
        {
            if (CheckAll())
            {
                Product product = new Product()
                {
                    Id = Id,
                    DisplayName = DisplayName,
                    CategoryID = CategorySelectedItem.CategoryID,
                    BrandID = BrandSelectedItem.BrandID,
                    Price = Price,
                    Quantity = Quantity,
                    Description = Description,
                    ImageURL = ImageURL,
                    OriginalPrice = OriginalPrice
                };
                if (product != null)
                {
                    productBUS.Update(product);
                    LoadList();
                    return true;
                }
            }
            return false;
        }

        private void LoadListBrands()
        {
            if (CategorySelectedItem.CategoryID != 0)
            {
                //ListBrands = new ObservableCollection<Brand>(brandBUS.GetAllBrandsByCategoryID(CategorySelectedItem.CategoryID));
                ListBrands = new ObservableCollection<Brand>(brandBUS.GetAll());
            }
        }

        private bool AddProduct()
        {
            // Nếu kiểm tra các thông tin hợp lệ thì thêm vào
            if (CheckAll())
            {
                if (productBUS.Get(Id) == null)
                {
                    Product product = new Product()
                    {
                        Id = Id,
                        DisplayName = DisplayName,
                        CategoryID = CategorySelectedItem.CategoryID,
                        BrandID = BrandSelectedItem.BrandID,
                        Price = Price,
                        Quantity = Quantity,
                        Description = Description,
                        ImageURL = ImageURL,
                        OriginalPrice = OriginalPrice
                    };

                    productBUS.Add(product);
                    LoadList();
                    return true;
                }
            }
            return false;
        }

        private bool CheckAll()
        {
            if (string.IsNullOrEmpty(DisplayName) || string.IsNullOrEmpty(Description) || string.IsNullOrEmpty(ImageURL) || CategorySelectedItem == null || BrandSelectedItem == null)
                return false;

            if (Price < 0 || Quantity < 0)
                return false;

            return true;
        }

        private void GetImage()
        {
            OpenFileDialog ofd = new OpenFileDialog() { Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg", ValidateNames = true, Multiselect = false };

            var res = ofd.ShowDialog();
            if (res == true)
            {
                ImageURL = ofd.FileName;
            }
        }

        private void LoadProductInfo()
        {
            if (ProductSelectedItem != null)
            {
                Id = ProductSelectedItem.Id;
                DisplayName = ProductSelectedItem.DisplayName;
                CategorySelectedItem = ProductSelectedItem.Category;
                BrandSelectedItem = ProductSelectedItem.Brand;
                Price = ProductSelectedItem.Price;
                Quantity = ProductSelectedItem.Quantity;
                Description = ProductSelectedItem.Description;
                ImageURL = ProductSelectedItem.ImageURL;
                OriginalPrice = ProductSelectedItem.OriginalPrice;
            }
        }

        private void LoadList()
        {
            ListProducts = new ObservableCollection<Product>(productBUS.GetAll());
            ListCategories = new ObservableCollection<Category>(categoryBUS.GetAll());
            //ListBrands = new ObservableCollection<Brand>(brandBUS.GetAll());
        }
    }
}
