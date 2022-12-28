using StoreManagement.DAO;
using StoreManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.BUS
{
    public class CustomerInfoBUS
    {

        private CustomerInfoDAO customerInfoDAO = new CustomerInfoDAO();

        public void Delete(CustomerInfo t)
        {
            customerInfoDAO.Delete(t);
        }

        public CustomerInfo Get(string PhoneNumber)
        {
            return customerInfoDAO.Get(PhoneNumber);
        }

        public List<CustomerInfo> GetAll()
        {
            return customerInfoDAO.GetAll();
        }

        public void Add(CustomerInfo t)
        {
            // Nếu đã tồn tại
            if (Get(t.PhoneNumber) != null)
            {
                return;
            }
            customerInfoDAO.Add(t);
        }

        public void Update(CustomerInfo t)
        {
            customerInfoDAO.Update(t);
        }
    }
}
