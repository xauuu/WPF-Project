using StoreManagement.DAO;
using StoreManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.BUS
{
    public class BrandBUS
    {
        private BrandDAO brandDAO = new BrandDAO();
        private ProductDAO productDAO = new ProductDAO();
        
        public void Delete(Brand t)
        {
            brandDAO.Delete(t);
        }

        public Brand Get(int id)
        {
            return brandDAO.Get(id);
        }

        public List<Brand> GetAll()
        {
            return brandDAO.GetAll();
        }

        public List<Brand> GetAllBrandsByCategoryID(int CategoryID)
        {
            return brandDAO.GetAllBrandsByCategoryID(CategoryID);
        }

        public void Add(Brand t)
        {
            brandDAO.Add(t);
        }

        public void Update(Brand t)
        {
            brandDAO.Update(t);
        }

        public List<string> GetAllBrandName()
        {
            List<string> listNames = new List<string>();
            List<Brand> listBrands = GetAll();
            foreach (var item in listBrands)
            {
                listNames.Add(item.BrandName);
            }
            return listNames;
        }
    }
}
