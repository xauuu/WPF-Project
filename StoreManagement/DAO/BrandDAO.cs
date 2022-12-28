using StoreManagement.Entities;
using StoreManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.DAO
{
    public class BrandDAO: IDao<Brand>
    {

        public void Delete(Brand t)
        {
            try
            {
                var del = (from p in DataProvider.Ins.DB.Brands
                           where p.BrandID == t.BrandID
                           select p).Single();
                if (del != null)
                {
                    DataProvider.Ins.DB.Brands.Remove(del);
                    DataProvider.Ins.DB.SaveChanges();
                }
            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " Delete() is error" + "\n" + e.Message);
                log.Show();
            }
        }

        public Brand Get(int id)
        {
            try
            {
                return DataProvider.Ins.DB.Brands.Find(id);
            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " Get() is error" + "\n" + e.Message);
                log.Show();
            }
            return null;

        }

        public List<Brand> GetAll()
        {
            try
            {
                return DataProvider.Ins.DB.Brands.ToList();
            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " GetAll() is error" + "\n" + e.Message);
                log.Show();
            }
            return null;
        }

        public List<Brand> GetAllBrandsByCategoryID(int CategoryID)
        {
            List<Brand> lstBrands = GetAll();

            if (CategoryID != 0)
            {
                lstBrands = (from p in DataProvider.Ins.DB.Products
                             where p.CategoryID == CategoryID
                             join b in DataProvider.Ins.DB.Brands
                             on p.BrandID equals b.BrandID
                             select b).Distinct().ToList();
            }

            return lstBrands;
        }

        public void Add(Brand t)
        {
            try
            {

                DataProvider.Ins.DB.Brands.Add(t);
                DataProvider.Ins.DB.SaveChanges();

            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " Add() is error" + "\n" + e.Message);
                log.Show();
            }
        }

        public void Update(Brand t)
        {
            try
            {

                var entity = DataProvider.Ins.DB.Products.Find(t.BrandID);
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
