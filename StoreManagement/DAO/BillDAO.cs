using StoreManagement.Entities;
using StoreManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.DAO
{
    public class BillDAO: IDao<Bill>
    {

        public void Delete(Bill t)
        {
            try
            {
                var del = (from p in DataProvider.Ins.DB.Bills
                           where p.BillID == t.BillID
                           select p).Single();
                if (del != null)
                {
                    DataProvider.Ins.DB.Bills.Remove(del);
                    DataProvider.Ins.DB.SaveChanges();
                }
            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " Delete() is error" + "\n" + e.Message);
                log.Show();
            }
        }

        public Bill Get(int id)
        {
            try
            {
                return DataProvider.Ins.DB.Bills.Find(id);
            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " Get() is error" + "\n" + e.Message);
                log.Show();
            }
            return null;

        }

        public List<Bill> GetAll()
        {
            try
            {
                return DataProvider.Ins.DB.Bills.ToList();
            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " GetAll() is error" + "\n" + e.Message);
                log.Show();
            }
            return null;
        }

        public void Add(Bill t)
        {
            try
            {

                DataProvider.Ins.DB.Bills.Add(t);
                DataProvider.Ins.DB.SaveChanges();
            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " Add() is error" + "\n" + e.Message);
                log.Show();
            }
        }

        public void Update(Bill t)
        {
            try
            {

                var entity = DataProvider.Ins.DB.Bills.Find(t.BillID);
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
