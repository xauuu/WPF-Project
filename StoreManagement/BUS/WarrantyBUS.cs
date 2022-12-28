using StoreManagement.DAO;
using StoreManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.BUS
{
    public class WarrantyBUS
    {

        private WarrantyDAO warrantyDAO = new WarrantyDAO();

        public void Delete(Warranty t)
        {
            warrantyDAO.Delete(t);
        }

        public Warranty Get(int id)
        {
            return warrantyDAO.Get(id);
        }

        public List<Warranty> GetAll()
        {
            return warrantyDAO.GetAll();
        }

   
        public void Add(Warranty t)
        {
            warrantyDAO.Add(t);
        }

        public void Update(Warranty t)
        {
            warrantyDAO.Update(t);
        }

     
        
    }
}
