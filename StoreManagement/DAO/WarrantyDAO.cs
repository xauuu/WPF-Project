using StoreManagement.Entities;
using StoreManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.DAO
{
    public class WarrantyDAO: IDao<Warranty>
    {

        public void Delete(Warranty t)
        {
            try
            {
                var del = (from p in DataProvider.Ins.DB.Warranties
                           where p.WarrantyID == t.WarrantyID
                           select p).Single();
                if (del != null)
                {
                    DataProvider.Ins.DB.Warranties.Remove(del);
                    DataProvider.Ins.DB.SaveChanges();
                }
            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " Delete() is error" + "\n" + e.Message);
                log.Show();
            }
        }

        public Warranty Get(int id)
        {
            try
            {
                return DataProvider.Ins.DB.Warranties.Find(id);
            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " Get() is error" + "\n" + e.Message);
                log.Show();
            }
            return null;

        }

        public List<Warranty> Get(string phoneNumber)
        {
            try
            {
                    return DataProvider.Ins.DB.Warranties.Where(w => w.PhoneNumber == phoneNumber).ToList();
                
            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " Get() is error" + "\n" + e.Message);
                log.Show();
            }
            return null;
        }

        public List<Warranty> GetAll()
        {
            try
            {
                return DataProvider.Ins.DB.Warranties.ToList();
            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " GetAll() is error" + "\n" + e.Message);
                log.Show();
            }
            return null;
        }

       
        public void Add(Warranty t)
        {
            try
            {

                DataProvider.Ins.DB.Warranties.Add(t);
                DataProvider.Ins.DB.SaveChanges();

            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " Add() is error" + "\n" + e.Message);
                log.Show();
            }
        }

        public void Update(Warranty t)
        {
            try
            {

                var entity = DataProvider.Ins.DB.Products.Find(t.WarrantyID);
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
