using StoreManagement.BUS;
using StoreManagement.DAO;
using StoreManagement.Entities;
using StoreManagement.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace StoreManagement.Views.ViewModels
{
    public class UCSaleViewModel : BaseViewModel
    {
        private ProductBUS productBUS = new ProductBUS();
        private CategoryBUS categoryBUS = new CategoryBUS();
        private BrandBUS brandBUS = new BrandBUS();
        private bool IsFirstLoaded = true;

        #region Command
        public ICommand LoadCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand CartWindowCommand { get; set; }
        public ICommand FilterSelectionChanged { get; set; }
        public ICommand SearchChangedCommand { get; set; }
        public ICommand ClearSearchTextCommand { get; set; }
        public ICommand CategorySelectionChanged { get; set; }
        public ICommand ViewDetailCommand { get; set; }
        public ICommand WarrantyCommand { get; set; }
        #endregion

        #region Properties
        private ObservableCollection<Product> _ListProducts;
        public ObservableCollection<Product> ListProducts { get => _ListProducts; set { if (value == _ListProducts) return; _ListProducts = value; OnPropertyChanged(); } }

        private ObservableCollection<Brand> _ListBrands;
        public ObservableCollection<Brand> ListBrands { get => _ListBrands; set { if (value == _ListBrands) return; _ListBrands = value; OnPropertyChanged(); } }

        private ObservableCollection<Category> _ListCategories;
        public ObservableCollection<Category> ListCategories { get => _ListCategories; set { if (value == _ListCategories) return; _ListCategories = value; OnPropertyChanged(); } }

        private Brand _BrandSelectedItem;
        public Brand BrandSelectedItem { get => _BrandSelectedItem; set { if (value == _BrandSelectedItem) return; _BrandSelectedItem = value; OnPropertyChanged(); } }

        private Category _CategorySelectedItem;
        public Category CategorySelectedItem { get => _CategorySelectedItem; set { if (value == _CategorySelectedItem) return; _CategorySelectedItem = value; OnPropertyChanged(); } }

        private Product _SelectedItem;
        public Product SelectedItem { get => _SelectedItem; set { if (value == _SelectedItem) return; _SelectedItem = value; OnPropertyChanged(); } }

        private int _Quantity;
        public int Quantity { get => _Quantity; set { if (value == _Quantity) return; _Quantity = value; OnPropertyChanged(); } }

        private string _SearchText;
        public string SearchText { get => _SearchText; set { if (value == _SearchText) return; _SearchText = value; OnPropertyChanged(); } }

        private Visibility _ClearSearchButton;
        public Visibility ClearSearchButton { get => _ClearSearchButton; set { if (value == _ClearSearchButton) return; _ClearSearchButton = value; OnPropertyChanged(); } }

        private Visibility _LoadingImageVisibility;
        public Visibility LoadingImageVisibility { get => _LoadingImageVisibility; set { if (value == _LoadingImageVisibility) return; _LoadingImageVisibility = value; OnPropertyChanged(); } }

        private ProductDetailViewModel _ProductDetailViewModel;
        public ProductDetailViewModel ProductDetailViewModel { get => _ProductDetailViewModel; set { if (value == _ProductDetailViewModel) return; _ProductDetailViewModel = value; OnPropertyChanged(); } }

        #endregion
        public UCSaleViewModel()
        {
            LoadingImageVisibility = Visibility.Visible;
            Thread thread = new Thread(new ThreadStart(() =>
            {
                LoadCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
                {
                    LoadList();
                });
                LoadingImageVisibility = Visibility.Collapsed;

            }));
            thread.Start();
            thread.Join();

            AddCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MessageBoxResult res = MessageBox.Show("Bạn có muốn thêm sản phẩm vào giỏ hàng?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (res)
                {
                    case MessageBoxResult.Yes:
                        AddProductToBasket();
                        break;
                    case MessageBoxResult.No:
                        break;
                    default:
                        break;
                }
            });
            FilterSelectionChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadListByCondition();
            });
            CategorySelectionChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                //LoadListBrands();
                LoadListByCondition();
            });
            CartWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (Basket.Instance.BasketCount() > 0)
                {
                    var billWindow = new BillWindow();
                    billWindow.ShowDialog();

                    //Nếu thanh toán thành công
                    if (Basket.Instance.isPaid)
                    {
                        UpdateView();
                        Basket.Instance.isPaid = false;
                    }
                }
            });
            SearchChangedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Search();
            });
            ClearSearchTextCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SearchText = "";
                ClearSearchButton = Visibility.Hidden;
            });
            ViewDetailCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Application.Current.Resources.Add("ProductSelectedItem", SelectedItem);
                var productdetail = new ProductDetailWindow();
                productdetail.ShowDialog();
            });
            WarrantyCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var warWindow = new WarrantyWindow();
                warWindow.ShowDialog();
            });


        }

        private void LoadListBrands()
        {
            if (CategorySelectedItem.CategoryID != 0)
            {
                ListBrands = new ObservableCollection<Brand>(brandBUS.GetAll());
            }
            else
            {
                ListBrands = new ObservableCollection<Brand>();

            }
            ListBrands.Add(new Brand { BrandID = 0, BrandName = "Tất cả" });
            BrandSelectedItem = ListBrands.Where(b => b.BrandID == 0).SingleOrDefault();
            LoadListByCondition();
        }


        private void Search()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                LoadListByCondition();
                ClearSearchButton = Visibility.Hidden;
            }
            else
            {
                ClearSearchButton = Visibility.Visible;
            }
        }

        private void UpdateView()
        {
            Quantity = 0;
            LoadList();
        }

        private void AddProductToBasket()
        {
            if (SelectedItem != null)
            {
                BasketDetails basketDetails = new BasketDetails()
                {
                    ProductId = SelectedItem.Id,
                    ProductName = SelectedItem.DisplayName,
                    Quantity = 1,
                    UnitPrice = SelectedItem.Price
                };
                bool isAdded = Basket.Instance.AddDetail(basketDetails, SelectedItem.Quantity);
                if (isAdded)
                {
                    UpdateCart();
                }
            }
        }

        private void UpdateCart()
        {
            Quantity = Basket.Instance.BasketCount();
        }

        private void LoadList()
        {
            if (IsFirstLoaded)
            {
                ListCategories = new ObservableCollection<Category>(categoryBUS.GetAll());
                //ListBrands = new ObservableCollection<Brand>();
                ListBrands = new ObservableCollection<Brand>(brandBUS.GetAll());

                ListCategories.Add(new Category { CategoryID = 0, CategoryName = "Tất cả" });
                CategorySelectedItem = ListCategories.Where(c => c.CategoryID == 0).SingleOrDefault();
                ListBrands.Add(new Brand { BrandID = 0, BrandName = "Tất cả" });
                BrandSelectedItem = ListBrands.Where(b => b.BrandID == 0).SingleOrDefault();

                IsFirstLoaded = false;
                ClearSearchButton = Visibility.Hidden;
            }
            ListProducts = new ObservableCollection<Product>(productBUS.GetAll());
        }

        private void LoadListByCondition()
        {
            if (CategorySelectedItem != null && BrandSelectedItem != null)
            {
                ListProducts = new ObservableCollection<Product>(productBUS.GetAll(SearchText, CategorySelectedItem.CategoryID, BrandSelectedItem.BrandID));

            }
        }
    }
}
