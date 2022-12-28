using StoreManagement.Entities;
using StoreManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.DAO
{
    public class CategoryDAO : IDao<Category>
    {

        public void Delete(Category t)
        {
            try
            {
                var del = (from p in DataProvider.Ins.DB.Categories
                           where p.CategoryID == t.CategoryID
                           select p).Single();
                if (del != null)
                {
                    DataProvider.Ins.DB.Categories.Remove(del);
                    DataProvider.Ins.DB.SaveChanges();
                }
            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " Delete() is error" + "\n" + e.Message);
                log.Show();
            }
        }

        public Category Get(int id)
        {
            try
            {
                return DataProvider.Ins.DB.Categories.Find(id);
            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " Get() is error" + "\n" + e.Message);
                log.Show();
            }
            return null;

        }

        public List<Category> GetAll()
        {
            try
            {
                return DataProvider.Ins.DB.Categories.ToList();
            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " GetAll() is error" + "\n" + e.Message);
                log.Show();
            }
            return null;
        }

        public void Add(Category t)
        {
            try
            {

                DataProvider.Ins.DB.Categories.Add(t);
                DataProvider.Ins.DB.SaveChanges();

            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " Add() is error" + "\n" + e.Message);
                log.Show();
            }
        }

        public void Update(Category t)
        {
            try
            {

                var entity = DataProvider.Ins.DB.Products.Find(t.CategoryID);
                if (entity == null)
                {
                    return;
                }

                DataProvider.Ins.DB.Entry(entity).CurrentValues.SetValues(t);
                DataProvider.Ins.DB.SaveChanges();

            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " Update() is error" + "\n" + e.Message);
                log.Show();
            }
        }

    }
}
