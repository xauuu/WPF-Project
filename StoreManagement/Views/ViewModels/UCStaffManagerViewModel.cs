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
using System.Windows.Controls;
using System.Windows.Input;

namespace StoreManagement.Views.ViewModels
{
    public class UCStaffManagerViewModel: BaseViewModel
    {
        UserBUS userBUS = new UserBUS();
        RoleBUS roleBUS = new RoleBUS();

        #region Command
        public ICommand LoadCommand { get; set; }
        public ICommand UserSelectionChanged { get; set; }
      
        public ICommand RoleSelectionChanged { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateCommand { get; set; }

        #endregion

        #region Properties
        private ObservableCollection<User> _ListUsers;
        public ObservableCollection<User> ListUsers { get => _ListUsers; set { if (value == _ListUsers) return; _ListUsers = value; OnPropertyChanged(); } }

        private ObservableCollection<Role> _ListRoles;
        public ObservableCollection<Role> ListRoles { get => _ListRoles; set { if (value == _ListRoles) return; _ListRoles = value; OnPropertyChanged(); } }

        private int _Id;
        public int Id { get => _Id; set { if (value == _Id) return; _Id = value; OnPropertyChanged(); } }

        private Role _Role;
        public Role Role { get => _Role; set { if (value == _Role) return; _Role = value; OnPropertyChanged(); } }

        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { if (value == _DisplayName) return; _DisplayName = value; OnPropertyChanged(); } }

        private string _UserName;
        public string UserName { get => _UserName; set { if (value == _UserName) return; _UserName = value; OnPropertyChanged(); } }

        private string _Password;
        public string Password { get => _Password; set { if (value == _Password) return; _Password = value; OnPropertyChanged(); } }

        private string _IdentityCard;
        public string IdentityCard { get => _IdentityCard; set { if (value == _IdentityCard) return; _IdentityCard = value; OnPropertyChanged(); } }

        private string _Address;
        public string Address { get => _Address; set { if (value == _Address) return; _Address = value; OnPropertyChanged(); } }

        private DateTime? _Birthdate;
        public DateTime? Birthdate { get => _Birthdate; set { if (value == _Birthdate) return; _Birthdate = value; OnPropertyChanged(); } }
  
        private User _SelectedItem;
        public User SelectedItem { get => _SelectedItem; set { if (value == _SelectedItem) return; _SelectedItem = value; OnPropertyChanged(); } }

        private Role _RoleSelectedItem;
        public Role RoleSelectedItem { get => _RoleSelectedItem; set { if (value == _RoleSelectedItem) return; _RoleSelectedItem = value; OnPropertyChanged(); } }
        #endregion

        public UCStaffManagerViewModel()
        {
            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadList();
            });
            UserSelectionChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadUserInfo();
            });
            RoleSelectionChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                
            });
            AddCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (AddUser())
                {
                    MessageBox.Show("Thêm tài khoản thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Thêm tài khoản thất bại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            });
            UpdateCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (Update())
                {
                    MessageBox.Show("Cập nhật tài khoản thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Cập nhật tài khoản thất bại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            });
         
        }

        private bool Update()
        {
            if (CheckAll())
            {
                User user = new User()
                {
                    Id = Id,
                    DisplayName = DisplayName,
                    UserName = UserName,
                    Password = StaticData.Base64Encode(Password),
                    IdentityCard = IdentityCard,
                    Birthdate = Birthdate,
                    Address = Address,
                    RoleId = RoleSelectedItem.RoleId
                };
                if (user != null)
                {
                    userBUS.Update(user);
                    LoadList();
                    return true;
                }
            }
            return false;
        }

        private bool CheckAll()
        {
            if (string.IsNullOrEmpty(DisplayName) || string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(IdentityCard) || string.IsNullOrEmpty(Address) || RoleSelectedItem == null)
                return false;
           
            return true;
        }

        private bool AddUser()
        {
            if (CheckAll())
            {
                if (userBUS.Get(UserName) == null)
                {
                    User user = new User()
                    {
                        DisplayName = DisplayName,
                        UserName = UserName,
                        Password = StaticData.Base64Encode(Password),
                        IdentityCard = IdentityCard,
                        Birthdate = Birthdate,
                        Address = Address,
                        RoleId = RoleSelectedItem.RoleId
                    };

                    userBUS.Add(user);
                    LoadList();
                    return true;
                }
            }
            return false;
        }

        private void LoadUserInfo()
        {
            if (SelectedItem != null)
            {
                Id = SelectedItem.Id;
                DisplayName = SelectedItem.DisplayName;
                UserName = SelectedItem.UserName;
                RoleSelectedItem = SelectedItem.Role;
                IdentityCard = SelectedItem.IdentityCard;
                Address = SelectedItem.Address;
                Birthdate = SelectedItem.Birthdate;
                Password = StaticData.Base64Decode(SelectedItem.Password);
            }
        }

        private void LoadList()
        {
            ListUsers = new ObservableCollection<User>(userBUS.GetAll());
            ListRoles = new ObservableCollection<Role>(roleBUS.GetAll());
        }
    }
}
