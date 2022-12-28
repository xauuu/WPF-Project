using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Entities
{
    public class Basket
    { 
        private static Basket _instance = null;
        public static Basket Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Basket();
                }

                return _instance;
            }
        }

        public ObservableCollection<BasketDetails> Details
        {
            get => details;
            set
            {
                details = value;
            }
        }

        public bool isPaid { get; set; } = false;

        private ObservableCollection<BasketDetails> details = new ObservableCollection<BasketDetails>();

        public bool AddDetail(int productId, double unitPrice, string productName, int amount, int quantity = 1)
        {
            // Add new product to the basket if it doesn't exist.
            if (!Details.Any(d => d.ProductId == productId))
            {
                details.Add(new BasketDetails()
                {
                    ProductId = productId,
                    UnitPrice = unitPrice,
                    Quantity = quantity,
                    ProductName = productName
                });
                return true;
            }

            // Otherwise find the existing detail and update its quantity.
            var existingDetail = Details.FirstOrDefault(i => i.ProductId == productId);
            if (existingDetail.Quantity < amount)
            {
                existingDetail.Quantity += quantity;
                return true;
            }
            return false;
        }

        public bool AddDetail(BasketDetails basketDetails, int amount)
        {
            return AddDetail(basketDetails.ProductId, basketDetails.UnitPrice, basketDetails.ProductName, amount);
        }

        public bool RemoveDetail(BasketDetails basketDetail)
        {
            return Details.Remove(basketDetail);
        }

        public bool UpdateDetail(BasketDetails basketDetail)
        {
            BasketDetails bas = Details.Where(d => d.ProductId == basketDetail.ProductId).SingleOrDefault();
            if (bas != null)
            {
                int pos = Details.IndexOf(bas);
                Details[pos] = basketDetail;
                return true;
            }
            return false;         
        }

        public double TotalPrice()
        {
            return details.Sum(detail => detail.UnitPrice * detail.Quantity);
        }

        public int BasketCount()
        {
            return Details.Count;
        }

        public void ClearAll()
        {
            Details.Clear();
        }
    }
}
