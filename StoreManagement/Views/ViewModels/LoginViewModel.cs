using StoreManagement.DAO;
using StoreManagement.Utilities;
using StoreManagement.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StoreManagement.Views.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        public ICommand CloseCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }

        public LoginViewModel()
        {
            UserName = "admin";
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (IsSucceedLogin(p))
                {
                    DashboardWindow db = new DashboardWindow();
                    db.Show();
                    p.Close();
                }
            });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
        }

        private bool IsSucceedLogin(Window p)
        {
            if (p == null)
                return false;
            if (UserName != null && Password != null)
            {
                string passEncode = StaticData.Base64Encode(Password);
                var roleId = new UserDAO().GetUserRole(UserName, passEncode);

                if (roleId != -1)
                {
                    StaticData.User = new UserDAO().Get(UserName);
                    StaticData.UserID = new UserDAO().GetUserID(UserName);
                    StaticData.RoleID = roleId;
                    return true;

                }
                else
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
                }

            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
            }
            return false;

        }
    }
}
