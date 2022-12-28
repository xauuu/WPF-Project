
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
    public class UCBrandViewModel : BaseViewModel
    {
        BrandBUS brandBUS = new BrandBUS();

        public ICommand LoadCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand BrandSelectionChanged { get; set; }

        private ObservableCollection<Brand> _ListBrands;
        public ObservableCollection<Brand> ListBrands { get => _ListBrands; set { if (value == _ListBrands) return; _ListBrands = value; OnPropertyChanged(); } }

        private Brand _BrandSelectedItem;
        public Brand BrandSelectedItem { get => _BrandSelectedItem; set { if (value == _BrandSelectedItem) return; _BrandSelectedItem = value; OnPropertyChanged(); } }

        private int _BrandID;
        public int BrandID { get => _BrandID; set { if (value == _BrandID) return; _BrandID = value; OnPropertyChanged(); } }

        private string _BrandName;
        public string BrandName { get => _BrandName; set { if (value == _BrandName) return; _BrandName = value; OnPropertyChanged(); } }

        public UCBrandViewModel()
        {
            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadBrandList();
            });

            BrandSelectionChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadBrandInfo();
            });

            AddCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (AddBrand())
                {
                    MessageBox.Show("Thêm thương hiệu thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Thêm thương hiệu thất bại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

            UpdateCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (UpdateBrand())
                {
                    MessageBox.Show("Cập nhật thương hiệu thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Cập nhật thương hiệu thất bại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

            DeleteCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (DeleteBrand())
                {
                    MessageBox.Show("Xóa thương hiệu thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Xóa thương hiệu thất bại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

        }

        private void LoadBrandList()
        {
            ListBrands = new ObservableCollection<Brand>(brandBUS.GetAll());
        }

        private void LoadBrandInfo()
        {
            if (BrandSelectedItem != null)
            {
                BrandID = BrandSelectedItem.BrandID;
                BrandName = BrandSelectedItem.BrandName;
            }
        }

        private bool AddBrand()
        {
            if (BrandName == null || BrandName == "")
            {
                MessageBox.Show("Tên thương hiệu không được để trống", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            Brand brand = new Brand()
            {
                BrandName = BrandName
            };
            brandBUS.Add(brand);
            LoadBrandList();
            return true;
        }

        private bool UpdateBrand()
        {
            if (BrandName == null || BrandName == "")
            {
                MessageBox.Show("Tên thương hiệu không được để trống", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            Brand brand = new Brand()
            {
                BrandID = BrandID,
                BrandName = BrandName
            };

            brandBUS.Update(brand);
            LoadBrandList();

            return true;
        }

        private bool DeleteBrand()
        {
            if (BrandSelectedItem != null)
            {
                brandBUS.Delete(BrandSelectedItem);
                BrandSelectedItem = null;
                LoadBrandList();
                return true;
            }
            return false;
        }

    }
}
