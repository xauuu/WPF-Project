using StoreManagement.DAO;
using StoreManagement.Entities;
using StoreManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.BUS
{
    public class BillBUS
    {

        private BillDAO billDAO = new BillDAO();
        private WarrantyDAO warrantyDAO = new WarrantyDAO();

        public void Delete(Bill t)
        {
            billDAO.Delete(t);
        }

        public Bill Get(int id)
        {
            return billDAO.Get(id);
        }

        public List<Bill> GetAll()
        {
            return billDAO.GetAll();
        }

        public bool Add(Bill bill, CustomerInfo customer)
        {
            foreach (var item in Basket.Instance.Details)
            {
                BillDetail billDetail = new BillDetail()
                {
                    BillID = bill.BillID,
                    ProductId = item.ProductId,
                    Amount = item.Quantity,
                    UnitPrice = item.UnitPrice
                };
                bill.BillDetails.Add(billDetail);

                //Thêm vào bảng bảo hành
                Warranty war = new Warranty()
                {
                    PhoneNumber = customer.PhoneNumber,
                    ProductID = item.ProductId,
                    StartDate = bill.BillDate,
                    EndDate = bill.BillDate.AddDays(StaticData.ONEYEAR)
                };
                warrantyDAO.Add(war);
            }

            billDAO.Add(bill);
            return true;
        }

        public void Update(Bill t)
        {
            billDAO.Update(t);
        }

        public List<Bill> GetAll(DateTime startDate, DateTime endDate, int cashierID)
        {
            List<Bill> list = billDAO.GetAll();

            if (startDate != null)
            {
                list = list.FindAll(b => b.BillDate >= startDate);                
            }
            if (endDate != null)
            {
                list = list.FindAll(b => b.BillDate <= endDate);              
            }
           
            if (cashierID != 0)
            {
                list = list.FindAll(b => b.CashierID == cashierID);
            }
            
            return list;
        }
    }
}
