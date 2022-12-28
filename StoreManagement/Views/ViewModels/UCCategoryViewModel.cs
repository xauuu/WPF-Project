using StoreManagement.BUS;
using StoreManagement.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private int _Id;
        public int Id { get => _Id; set { if (value == _Id) return; _Id = value; OnPropertyChanged(); } }

        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { if (value == _DisplayName) return; _DisplayName = value; OnPropertyChanged(); } }

        public UCCategoryViewModel()
        {
            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadCategoryList();
            });
        }

        private void LoadCategoryList()
        {
            ListCategories = new ObservableCollection<Category>(categoryBUS.GetAll());
        }

    }
}
