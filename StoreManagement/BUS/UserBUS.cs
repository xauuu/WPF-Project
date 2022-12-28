using StoreManagement.DAO;
using StoreManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.BUS
{
    public class UserBUS
    {
        private UserDAO UserDAO = new UserDAO();

        public void Delete(User t)
        {
            UserDAO.Delete(t);
        }

        public User Get(int id)
        {
            return UserDAO.Get(id);
        }
        public User Get(string UserName)
        {
            return UserDAO.Get(UserName);
        }

        public List<User> GetAll()
        {
            return UserDAO.GetAll();
        }

        public void Add(User t)
        {
            UserDAO.Add(t);
        }

        public void Update(User t)
        {
            UserDAO.Update(t);
        }
    }
}
