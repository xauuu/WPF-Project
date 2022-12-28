using StoreManagement.BUS;
using StoreManagement.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StoreManagement.Views.ViewModels
{
    public class UCCategoryViewModel : BaseViewModel
    {
        CategoryBUS categoryBUS = new CategoryBUS();
        public ICommand LoadCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand CategorySelectionChanged { get; set; }

        private ObservableCollection<Category> _ListCategories;
        public ObservableCollection<Category> ListCategories { get => _ListCategories; set { if (value == _ListCategories) return; _ListCategories = value; OnPropertyChanged(); } }

        private Category _CategorySelectedItem;
        public Category CategorySelectedItem { get => _CategorySelectedItem; set { if (value == _CategorySelectedItem) return; _CategorySelectedItem = value; OnPropertyChanged(); } }

        private int _CategoryID;
        public int CategoryID { get => _CategoryID; set { if (value == _CategoryID) return; _CategoryID = value; OnPropertyChanged(); } }

        private string _CategoryName;
        public string CategoryName { get => _CategoryName; set { if (value == _CategoryName) return; _CategoryName = value; OnPropertyChanged(); } }

        public UCCategoryViewModel()
        {
            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadCategoryList();
            });

            CategorySelectionChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadCategoryInfo();
            });

            AddCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (AddCategory())
                {
                    MessageBox.Show("Thêm danh mục thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Thêm danh mục thất bại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

            UpdateCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (UpdateCategory())
                {
                    MessageBox.Show("Cập nhật danh mục thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Cập nhật danh mục thất bại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

            DeleteCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MessageBoxResult res = MessageBox.Show("Bạn có muốn xóa danh mục này không?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    if (DeleteCategory())
                    {
                        MessageBox.Show("Xóa danh mục thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Xóa danh mục thất bại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            });
        }

        private bool AddCategory()
        {
            if (CheckAll())
            {
                Category category = new Category();
                category.CategoryName = CategoryName;
                categoryBUS.Add(category);
                LoadCategoryList();
                return true;
            }
            return false;
        }

        private bool UpdateCategory()
        {
            if (CheckAll())
            {
                Category category = new Category()
                {
                    CategoryID = CategoryID,
                    CategoryName = CategoryName
                };
                if (category != null)
                {
                    categoryBUS.Update(category);
                    LoadCategoryList();
                    return true;
                }
            }
            return false;
        }

        private bool DeleteCategory()
        {
            if (CategorySelectedItem != null)
            {
                categoryBUS.Delete(CategorySelectedItem);
                CategorySelectedItem = null;
                LoadCategoryList();
                return true;
            }
            return false;
        }

        private void LoadCategoryList()
        {
            ListCategories = new ObservableCollection<Category>(categoryBUS.GetAll());
        }

        private void LoadCategoryInfo()
        {
            if (CategorySelectedItem != null)
            {
                CategoryID = CategorySelectedItem.CategoryID;
                CategoryName = CategorySelectedItem.CategoryName;
            }
        }

        private bool CheckAll()
        {
            if (string.IsNullOrEmpty(CategoryName))
                return false;

            return true;
        }

    }
}
