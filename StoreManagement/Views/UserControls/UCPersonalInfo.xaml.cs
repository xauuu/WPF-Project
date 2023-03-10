using StoreManagement.DAO;
using StoreManagement.Entities;
using StoreManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StoreManagement.Views.UserControls
{
    /// <summary>
    /// Interaction logic for UCPersonalInfo.xaml
    /// </summary>
    public partial class UCPersonalInfo : UserControl
    {

        User user = new User();
        public UCPersonalInfo()
        {
            InitializeComponent();
            user = new UserDAO().Get(StaticData.UserID);
            txtUserName.Text = user.UserName;
            txtPassword.Text = user.Password;
            txtUserID.Text = user.Id.ToString();
            txtRole.Text = user.Role.DisplayName;
            txtFullName.Text = user.DisplayName;
            dpkBirthdate.Text = user.Birthdate.ToString();
            txtIdCard.Text = user.IdentityCard;
            txtAddress.Text = user.Address;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            user = new UserDAO().Get(StaticData.UserID);
            user.UserName = txtUserName.Text.ToString();
            user.Password = txtPassword.Text.ToString();
            user.DisplayName = txtFullName.Text.ToString();
            user.IdentityCard = txtIdCard.Text.ToString();
            user.Address = txtAddress.Text.ToString();
            user.Birthdate = Convert.ToDateTime(dpkBirthdate.Text.ToString());
            var obj = new UserDAO();
            obj.Update(user);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            user = new UserDAO().Get(StaticData.UserID);
            txtUserName.Text = user.UserName;
            txtPassword.Text = user.Password;
            txtUserID.Text = user.Id.ToString();
            txtRole.Text = user.RoleId.ToString();
            txtFullName.Text = user.DisplayName;
            dpkBirthdate.Text = user.Birthdate.ToString();
            txtIdCard.Text = user.IdentityCard;
            txtAddress.Text = user.Address;
        }


    }
}
