using StoreManagement.Entities;
using StoreManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.DAO
{
    public class RoleDAO: IDao<Role>
    {

        public void Delete(Role t)
        {
            try
            {
                var del = (from p in DataProvider.Ins.DB.Roles
                           where p.RoleId == t.RoleId
                           select p).Single();
                if (del != null)
                {
                    DataProvider.Ins.DB.Roles.Remove(del);
                    DataProvider.Ins.DB.SaveChanges();
                }
            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " Delete() is error" + "\n" + e.Message);
                log.Show();
            }
        }

        public Role Get(int id)
        {
            try
            {
                return DataProvider.Ins.DB.Roles.Find(id);
            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " Get() is error" + "\n" + e.Message);
                log.Show();
            }
            return null;

        }

        public List<Role> GetAll()
        {
            try
            {
                return DataProvider.Ins.DB.Roles.ToList();
            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " GetAll() is error" + "\n" + e.Message);
                log.Show();
            }
            return null;
        }

        public void Add(Role t)
        {
            try
            {

                DataProvider.Ins.DB.Roles.Add(t);
                DataProvider.Ins.DB.SaveChanges();

            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " Add() is error" + "\n" + e.Message);
                log.Show();
            }
        }

        public void Update(Role t)
        {
            try
            {

                var entity = DataProvider.Ins.DB.Products.Find(t.RoleId);
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
