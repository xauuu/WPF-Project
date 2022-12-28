using StoreManagement.Entities;
using StoreManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.DAO
{
    public class UserDAO : IDao<User>
    {
        public void Add(User t)
        {
            try
            {
                DataProvider.Ins.DB.Users.Add(t);
                DataProvider.Ins.DB.SaveChanges();
            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " Add() is error" + "\n" + e.Message);
                log.Show();
            }
        }

        public void Delete(User t)
        {
            try
            {
                var del = (from p in DataProvider.Ins.DB.Users
                           where p.Id == t.Id
                           select p).Single();
                if (del != null)
                {
                    DataProvider.Ins.DB.Users.Remove(del);
                    DataProvider.Ins.DB.SaveChanges();
                }

            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " Delete() is error" + "\n" + e.Message);
                log.Show();
            }
        }

        public User Get(int id)
        {
            try
            {
                return DataProvider.Ins.DB.Users.Find(id);
            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " Get() is error" + "\n" + e.Message);
                log.Show();
            }
            return null;
        }

        public User Get(string username)
        {
            try
            {
                return DataProvider.Ins.DB.Users.Where(u => u.UserName == username.ToLower()).SingleOrDefault();
            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " Get() is error" + "\n" + e.Message);
                log.Show();
            }
            return null;
        }

        public List<User> GetAll()
        {
            try
            {
                return DataProvider.Ins.DB.Users.ToList();
            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " GetAll() is error" + "\n" + e.Message);
                log.Show();
            }
            return null;
        }

        public void Update(User t)
        {
            try
            {
                var entity = DataProvider.Ins.DB.Users.Find(t.Id);
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


        /// <summary>
        /// Return user RoleId.
        /// If not found, return -1
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns>RoleId</returns>
        public int GetUserRole(string UserName, string Password)
        {
            /*try
            {*/

            foreach (var user in DataProvider.Ins.DB.Users)
            {

                if (UserName.Equals(user.UserName)
                    && Password.Equals(user.Password))
                {
                    return user.Role.RoleId;
                }
            }

            /*}
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " GetUserRole() is error" + "\n" + e.Message);
                log.Show();
            }*/

            return -1;
        }

        /// <summary>
        /// Return userID by Username.
        /// If not found, return -1
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int GetUserID(string username)
        {
            var user = DataProvider.Ins.DB.Users.SingleOrDefault(d => d.UserName == username);
            if (user != null)
                return user.Id;

            return -1;
        }
    }
}
