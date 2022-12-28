using StoreManagement.DAO;
using StoreManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.BUS
{
    public class CategoryBUS
    {
        private CategoryDAO CategoryDAO = new CategoryDAO();

        public void Delete(Category t)
        {
            CategoryDAO.Delete(t);
        }

        public Category Get(int id)
        {
            return CategoryDAO.Get(id);
        }

        public List<Category> GetAll()
        {
            return CategoryDAO.GetAll();
        }

        public void Add(Category t)
        {
            CategoryDAO.Add(t);
        }

        public void Update(Category t)
        {
            CategoryDAO.Update(t);
        }

        public List<string> GetAllCategoryName()
        {
            List<string> listNames = new List<string>();
            List<Category> listCategories = GetAll();
            foreach (var item in listCategories)
            {
                listNames.Add(item.CategoryName);
            }
            return listNames;
        }
    }
}
