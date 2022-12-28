using StoreManagement.Entities;
using StoreManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.DAO
{
    public class CustomerInfoDAO: IDao<CustomerInfo>
    {

        public void Add(CustomerInfo t)
        {
            try
            {
                DataProvider.Ins.DB.CustomerInfoes.Add(t);
                DataProvider.Ins.DB.SaveChanges();
            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " Add() is error" + "\n" + e.Message);
                log.Show();
            }
        }

        public void Delete(CustomerInfo t)
        {
            try
            {
                var del = (from p in DataProvider.Ins.DB.CustomerInfoes
                           where p.PhoneNumber == t.PhoneNumber
                           select p).Single();
                if (del != null)
                {
                    DataProvider.Ins.DB.CustomerInfoes.Remove(del);
                    DataProvider.Ins.DB.SaveChanges();
                }

            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " Delete() is error" + "\n" + e.Message);
                log.Show();
            }
        }

        public CustomerInfo Get(int id)
        {
            
            return null;
        }

        public CustomerInfo Get(string PhoneNumber)
        {
            try
            {
                return DataProvider.Ins.DB.CustomerInfoes.Find(PhoneNumber);
            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " Get() is error" + "\n" + e.Message);
                log.Show();
            }
            return null;
        }

        public List<CustomerInfo> GetAll()
        {
            try
            {
                return DataProvider.Ins.DB.CustomerInfoes.ToList();
            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " GetAll() is error" + "\n" + e.Message);
                log.Show();
            }
            return null;
        }

        public void Update(CustomerInfo t)
        {
            try
            {
                var entity = DataProvider.Ins.DB.CustomerInfoes.Find(t.PhoneNumber);
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
