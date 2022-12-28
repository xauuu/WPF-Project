using StoreManagement.Entities;
using StoreManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.DAO
{
    public class CodePromotionDAO: IDao<CodePromotion>
    {

        public void Delete(CodePromotion t)
        {
            try
            {
                var del = (from p in DataProvider.Ins.DB.CodePromotions
                           where p.Code == t.Code
                           select p).Single();
                if (del != null)
                {
                    DataProvider.Ins.DB.CodePromotions.Remove(del);
                    DataProvider.Ins.DB.SaveChanges();
                }
            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " Delete() is error" + "\n" + e.Message);
                log.Show();
            }
        }

        public CodePromotion Get(int id)
        {
            //try
            //{
            //    return DataProvider.Ins.DB.CodePromotions.Find(id);
            //}
            //catch (Exception e)
            //{
            //    var log = new LogError(GetType().Name + " Get() is error" + "\n" + e.Message);
            //    log.Show();
            //}
            return null;

        }
        public CodePromotion Get(string id)
        {
            try
            {
                return DataProvider.Ins.DB.CodePromotions.Find(id);
            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " Get() is error" + "\n" + e.Message);
                log.Show();
            }
            return null;

        }


        public List<CodePromotion> GetAll()
        {
            try
            {
                return DataProvider.Ins.DB.CodePromotions.ToList();
            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " GetAll() is error" + "\n" + e.Message);
                log.Show();
            }
            return null;
        }

        public void Add(CodePromotion t)
        {
            try
            {

                DataProvider.Ins.DB.CodePromotions.Add(t);
                DataProvider.Ins.DB.SaveChanges();

            }
            catch (Exception e)
            {
                var log = new LogError(GetType().Name + " Add() is error" + "\n" + e.Message);
                log.Show();
            }
        }

        public void Update(CodePromotion t)
        {
            try
            {

                var entity = DataProvider.Ins.DB.Products.Find(t.Code);
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
