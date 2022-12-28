using StoreManagement.DAO;
using StoreManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.BUS
{
    public class ProductBUS
    {
        private ProductDAO ProductDAO = new ProductDAO();
        private BrandDAO brandDAO = new BrandDAO();


        public void Delete(Product t)
        {
            ProductDAO.Delete(t);
        }

        public Product Get(int id)
        {
            return ProductDAO.Get(id);
        }

        public List<Product> GetAll()
        {
            return ProductDAO.GetAll();
        }

        public void Add(Product t)
        {
            ProductDAO.Add(t);
        }

        public void Update(Product t)
        {
            ProductDAO.Update(t);
        }

        public List<Product> GetAll(string searchText, int categoryID, int brandID)
        {
            List<Product> list = ProductDAO.GetAll();
            
            if (categoryID != 0)
            {
                list = list.Where(pro => pro.CategoryID == categoryID).ToList();

                if (brandID != 0)
                {
                    list = list.Where(pro => pro.BrandID == brandID).ToList();

                }
            }

            if (!string.IsNullOrEmpty(searchText))
            {
                list = list.Where(pro => pro.DisplayName.ToLower().Contains(searchText.ToLower())).ToList();
            }
            return list;
        }
    }
}
