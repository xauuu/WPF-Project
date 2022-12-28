using StoreManagement.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace StoreManagement.Views.ViewModels
{
    public class BillDetailViewModel: BaseViewModel
    {
        //BillDetailBUS

        #region Command
        public ICommand LoadCommand { get; set; }

        #endregion
        #region Properties
        private ObservableCollection<BillDetail> _ListBillDetails;
        public ObservableCollection<BillDetail> ListBillDetails { get => _ListBillDetails; set { if (value == _ListBillDetails) return; _ListBillDetails = value; OnPropertyChanged(); } }

        #endregion


        public BillDetailViewModel()
        {
            LoadCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                LoadList();
            });
        }

        private void LoadList()
        {
            ListBillDetails = new ObservableCollection<BillDetail>((List<BillDetail>)Application.Current.Resources["ListBillDetails"]);
            Application.Current.Resources.Remove("ListBillDetails");
        }
    }    
}
