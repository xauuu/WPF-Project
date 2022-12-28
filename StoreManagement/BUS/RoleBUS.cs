using StoreManagement.DAO;
using StoreManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.BUS
{
    public class RoleBUS
    {

        private RoleDAO roleDAO = new RoleDAO();
        

        public void Delete(Role t)
        {
            roleDAO.Delete(t);
        }

        public Role Get(int id)
        {
            return roleDAO.Get(id);
        }

        public List<Role> GetAll()
        {
            return roleDAO.GetAll();
        }

      
        public void Add(Role t)
        {
            roleDAO.Add(t);
        }

        public void Update(Role t)
        {
            roleDAO.Update(t);
        }

        
    }
}
