using StoreManagement.BUS;
using StoreManagement.Entities;
using StoreManagement.Utilities;
using StoreManagement.Views.Windows;
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
    public class UCBillViewModel: BaseViewModel
    {
        private BillBUS billBUS = new BillBUS();
        private UserBUS userBUS = new UserBUS();
        private bool isFirstLoaded = true;

        #region Command
        public ICommand LoadCommand { get; set; }
        public ICommand ViewBillDetailCommand { get; set; }
        public ICommand FilterCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        #endregion

        #region Properties
        private ObservableCollection<Bill> _ListBills;
        public ObservableCollection<Bill> ListBills { get => _ListBills; set { if (value == _ListBills) return; _ListBills = value; OnPropertyChanged(); } }

        private ObservableCollection<User> _ListUsers;
        public ObservableCollection<User> ListUsers { get => _ListUsers; set { if (value == _ListUsers) return; _ListUsers = value; OnPropertyChanged(); } }

        private DateTime _EndDate;
        public DateTime EndDate { get => _EndDate; set { if (value == _EndDate) return; _EndDate = value; OnPropertyChanged(); } }

        private DateTime _StartDate;
        public DateTime StartDate { get => _StartDate; set { if (value == _StartDate) return; _StartDate = value; OnPropertyChanged(); } }

        private Bill _SelectedItem;
        public Bill SelectedItem { get => _SelectedItem; set { if (value == _SelectedItem) return; _SelectedItem = value; OnPropertyChanged(); } }

        private User _UserSelectedItem;
        public User UserSelectedItem { get => _UserSelectedItem; set { if (value == _UserSelectedItem) return; _UserSelectedItem = value; OnPropertyChanged(); } }

        private Visibility _IDVisibility;
        public Visibility IDVisibility { get => _IDVisibility; set { if (value == _IDVisibility) return; _IDVisibility = value; OnPropertyChanged(); } }

        private Visibility _ListUsersVisibility;
        public Visibility ListUsersVisibility { get => _ListUsersVisibility; set { if (value == _ListUsersVisibility) return; _ListUsersVisibility = value; OnPropertyChanged(); } }

        private Visibility _DeleteButtonVisibility;
        public Visibility DeleteButtonVisibility { get => _DeleteButtonVisibility; set { if (value == _DeleteButtonVisibility) return; _DeleteButtonVisibility = value; OnPropertyChanged(); } }

        #endregion

        public UCBillViewModel()
        {
            LoadCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                LoadList();
            });
            ViewBillDetailCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                //Lấy danh sách chi tiết hóa đơn
                if (SelectedItem != null)
                {
                    Application.Current.Resources.Add("ListBillDetails", ListBills.Where(b => b.BillID == SelectedItem.BillID).SingleOrDefault().BillDetails.ToList());
                }
                var billDetailWindow = new BillDetailWindow();
                billDetailWindow.ShowDialog();
            });
            FilterCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Filter();
            });
            DeleteCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                
            });
        }

        private void Filter()
        {
            if (StartDate != null && EndDate != null)
            {
                LoadListByCondition(StartDate, EndDate, UserSelectedItem.Id);
            }
        }

        private void LoadListByCondition(DateTime startDate, DateTime endDate, int CashierID)
        {           
            ListBills = new ObservableCollection<Bill>(billBUS.GetAll(startDate, endDate, CashierID));
        }

        private void LoadList()
        {
            if (isFirstLoaded)
            {
                ListUsers = new ObservableCollection<User>(userBUS.GetAll());
                ListUsers.Add(new User { Id = 0, DisplayName = "Tất cả" });
                UserSelectedItem = ListUsers.Where(u => u.Id == 0).SingleOrDefault();

                StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                EndDate = DateTime.Now;
                isFirstLoaded = false;                
            }
            //Admin
            if (StaticData.RoleID == 1)
            {
                IDVisibility = Visibility.Visible;
                ListUsersVisibility = Visibility.Visible;
                DeleteButtonVisibility = Visibility.Collapsed;
            }
            //Nhân viên
            else if (StaticData.RoleID == 2)
            {
                IDVisibility = Visibility.Collapsed;
                ListUsersVisibility = Visibility.Collapsed;
                DeleteButtonVisibility = Visibility.Collapsed;
            }
            ListBills = new ObservableCollection<Bill>(billBUS.GetAll());
        }
    }
}
