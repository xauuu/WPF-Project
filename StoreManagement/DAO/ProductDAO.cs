using StoreManagement.Entities;
using StoreManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.DAO
{
    public class ProductDAO : IDao<Product>
    {
        public void Delete(Product t)
        {
            try
            {
                var del = (from p in DataProvider.Ins.DB.Products
                           where p.Id == t.Id
                           select p).Single();
                if (del != null)
                {
                    DataProvider.Ins.DB.Products.Remove(del);
                    DataProvider.Ins.DB.SaveChanges();
                }
            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " Delete() is error" + "\n" + e.Message);
                log.Show();
            }
        }

        public Product Get(int id)
        {
            try
            {
                 return DataProvider.Ins.DB.Products.Find(id);
            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " Get() is error" + "\n" + e.Message);
                log.Show();
            }
            return null;

        }

        public List<Product> GetAll()
        {
            try
            {
                return DataProvider.Ins.DB.Products.ToList();
            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " GetAll() is error" + "\n" + e.Message);
                log.Show();
            }
            return null;
        }

        public void Add(Product t)
        {
            try
            {

                DataProvider.Ins.DB.Products.Add(t);
                DataProvider.Ins.DB.SaveChanges();

            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " Add() is error" + "\n" + e.Message);
                log.Show();
            }
        }

        public void Update(Product t)
        {
            try
            {

                var entity = DataProvider.Ins.DB.Products.Find(t.Id);
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

        public bool IsOutOfItems(int ProductId)
        {
            return !(Get(ProductId).Quantity > 0);
        }
    }
}
