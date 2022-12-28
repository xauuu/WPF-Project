using StoreManagement.BUS;
using StoreManagement.Entities;
using StoreManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace StoreManagement.Views.ViewModels
{
    public class BillViewModel: BaseViewModel
    {
        CodePromotionBUS codeBUS = new CodePromotionBUS();
        CustomerInfoBUS customerBUS = new CustomerInfoBUS();
        BillBUS billBUS = new BillBUS();
        ProductBUS productBUS = new ProductBUS();

        #region Command
        public ICommand LoadCommand { get; set; }
        public ICommand PayCommand { get; set; }
        public ICommand CodeTextChangeCommand { get; set; }
        public ICommand ExchangeTextChangeCommand { get; set; }
        public ICommand FilterCommand { get; set; }
        public ICommand CustomerSelectionChanged { get; set; }
        public ICommand DeleteDetailCommand { get; set; }        
        public ICommand EditCommand { get; set; }

        #endregion

        #region Properties
        private ObservableCollection<BasketDetails> _ListBasketDetails;
        public ObservableCollection<BasketDetails> ListBasketDetails { get => _ListBasketDetails; set { if (value == _ListBasketDetails) return; _ListBasketDetails = value; OnPropertyChanged(); } }

        private ObservableCollection<CustomerInfo> _ListCustomers;
        public ObservableCollection<CustomerInfo> ListCustomers { get => _ListCustomers; set { if (value == _ListCustomers) return; _ListCustomers = value; OnPropertyChanged(); } }
       
        private double _TotalPrice;
        public double TotalPrice { get => _TotalPrice; set { if (value == _TotalPrice) return; _TotalPrice = value; OnPropertyChanged(); } }

        private string _CustomerMoney;
        public string CustomerMoney { get => _CustomerMoney; set { if (value == _CustomerMoney) return; _CustomerMoney = value; OnPropertyChanged(); } }

        private double _LastTotalPrice;
        public double LastTotalPrice { get => _LastTotalPrice; set { if (value == _LastTotalPrice) return; _LastTotalPrice = value; OnPropertyChanged(); } }

        private double _DiscountMoney;
        public double DiscountMoney { get => _DiscountMoney; set { if (value == _DiscountMoney) return; _DiscountMoney = value; OnPropertyChanged(); } }

        private double _ExchangeMoney;
        public double ExchangeMoney { get => _ExchangeMoney; set { if (value == _ExchangeMoney) return; _ExchangeMoney = value; OnPropertyChanged(); } }

        private string _Code;
        public string Code { get => _Code; set { if (value == _Code) return; _Code = value; OnPropertyChanged(); } }

        private string _Notify;
        public string Notify { get => _Notify; set { if (value == _Notify) return; _Notify = value; OnPropertyChanged(); } }

        private string _PhoneNumberFilter;
        public string PhoneNumberFilter { get => _PhoneNumberFilter; set{if (value == _PhoneNumberFilter) return; _PhoneNumberFilter = value; OnPropertyChanged();  }}

        private Visibility _NotifyVisible;
        public Visibility NotifyVisible { get => _NotifyVisible; set { if (value == _NotifyVisible) return; _NotifyVisible = value; OnPropertyChanged(); } }

        private string _PhoneNumber;
        public string PhoneNumber { get => _PhoneNumber; set { if (value == _PhoneNumber) return; _PhoneNumber = value; OnPropertyChanged(); } }

        private string _CustomerName;
        public string CustomerName { get => _CustomerName; set { if (value == _CustomerName) return; _CustomerName = value; OnPropertyChanged(); } }

        private string _Email;
        public string Email { get => _Email; set { if (value == _Email) return; _Email = value; OnPropertyChanged(); } }

        private string _Address;
        public string Address { get => _Address; set { if (value == _Address) return; _Address = value; OnPropertyChanged(); } }

        private CustomerInfo _CustomerSelectedItem;
        public CustomerInfo CustomerSelectedItem { get => _CustomerSelectedItem; set { if (value == _CustomerSelectedItem) return; _CustomerSelectedItem = value; OnPropertyChanged(); } }

        private BasketDetails _SelectedItem;
        public BasketDetails SelectedItem { get => _SelectedItem; set { if (value == _SelectedItem) return; _SelectedItem = value; OnPropertyChanged(); } }

        private bool _PayEnabled;
        public bool PayEnabled { get => _PayEnabled; set { if (value == _PayEnabled) return; _PayEnabled = value; OnPropertyChanged(); } }

        private bool _EditEnabled;
        public bool EditEnabled { get => _EditEnabled; set { if (value == _EditEnabled) return; _EditEnabled = value; OnPropertyChanged(); } }

        #endregion

        public BillViewModel()
        {
            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadList();
            });
            PayCommand = new RelayCommand<Window>((p) => 
            {
                if (CheckAll())
                {
                    PayEnabled = true;
                    return true;
                }
                PayEnabled = false;
                return false;
            }, (p) =>
            {
                MessageBoxResult res = MessageBox.Show("Bạn có chắc chắn thanh toán không?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (res)
                {
                    case MessageBoxResult.Yes:
                        Pay(p);
                        break;
                    case MessageBoxResult.No:
                        break;
                    default:
                        break;
                }
            });
            CodeTextChangeCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CheckCodeValid();              
            });
            ExchangeTextChangeCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CalculateExchangeMoney();              
            });
            FilterCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CustomerFilter();
            });
            CustomerSelectionChanged = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SelectCustomer();
            });
            DeleteDetailCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MessageBoxResult res = MessageBox.Show("Bạn có muốn xóa sản phẩm khỏi giỏ hàng?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (res)
                {
                    case MessageBoxResult.Yes:
                        if (DeleteDetail())
                        {
                            LoadList();
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                    default:
                        break;
                }               
            });
            EditCommand = new RelayCommand<object>((p) => { return IsBasketDetailValid(); }, (p) =>
            {
                if (EditDetail())
                {
                    LoadList();
                }
            });

        }

        private bool EditDetail()
        {
            if (SelectedItem != null)
            {
                return Basket.Instance.UpdateDetail(SelectedItem);
            }
            return false;
        }

        private bool IsBasketDetailValid()
        {
            int totalQuantity = productBUS.Get(SelectedItem.ProductId).Quantity;
            return (SelectedItem.Quantity > 0 && SelectedItem.Quantity <= totalQuantity);             
        }

        private bool DeleteDetail()
        {
            if (SelectedItem != null)
            {
                bool res = Basket.Instance.RemoveDetail(SelectedItem);

                PayEnabled = false;
                SelectedItem = null;
                return res;
            }
            return false;
        }

        private void SelectCustomer()
        {
            if (CustomerSelectedItem != null)
            {
                PhoneNumber = CustomerSelectedItem.PhoneNumber;
                CustomerName = CustomerSelectedItem.CustomerName;
                Email = CustomerSelectedItem.Email;
                Address = CustomerSelectedItem.Address;
            }
        }

        private void CustomerFilter()
        {
            ListCustomers.Clear();
            if (!string.IsNullOrEmpty(PhoneNumberFilter))
            {
                ListCustomers = new ObservableCollection<CustomerInfo>(customerBUS.GetAll().Where(c => c.PhoneNumber.Contains(PhoneNumberFilter)).ToList());
            }
        }

        private void CalculateExchangeMoney()
        {
            if (!string.IsNullOrEmpty(CustomerMoney))
            {
                if (double.Parse(CustomerMoney) - LastTotalPrice >= 0)
                {
                    ExchangeMoney = double.Parse(CustomerMoney) - LastTotalPrice;
                }
                else
                {
                    ExchangeMoney = 0;
                }
            }    
        }

        private void CheckCodeValid()
        {
            if (!Code.Equals(""))
            {
                // Lấy mã code từ khách hàng
                CodePromotion code = codeBUS.Get(Code);
                NotifyVisible = Visibility.Visible;

                if (code == null)
                {
                    Notify = "Mã không hợp lệ";
                    DiscountMoney = 0;
                }
                else
                {
                    Notify = "Mã hợp lệ";
                    DiscountMoney = code.CodePercent * TotalPrice;
                    LastTotalPrice = LastTotalPrice - DiscountMoney;
                    CalculateExchangeMoney();
                }
            }
            else
            {
                NotifyVisible = Visibility.Hidden;
            }
        }

        private void Pay(Window p)
        {
            CustomerInfo customer = new CustomerInfo()
            {
                PhoneNumber = PhoneNumber,
                CustomerName = CustomerName,
                Email = Email,
                Address = Address
            };
            customerBUS.Add(customer);

            if (Code != null)
            {
                Code = Code.ToUpper();
            }
            //Tạo bill
            Bill bill = new Bill()
            {
                BillDate = DateTime.Now,
                CashierID = StaticData.UserID,
                TotalPrice = TotalPrice,
                DiscountCode = Code,
                LastTotalPrice = LastTotalPrice
            };
          
            //Tạo bảo hành 


            bool res = billBUS.Add(bill, customer);
            if (res)
            {
                UpdateProducts();
                MessageBox.Show("Thanh toán thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                ClearAll();
                p.Close();
            }
            else
            {
                MessageBox.Show("Thanh toán thất bại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void UpdateProducts()
        {
            foreach (var item in Basket.Instance.Details)
            {
                Product product = productBUS.Get(item.ProductId);
                if (product != null)
                {
                    product.Quantity = product.Quantity - item.Quantity;
                    productBUS.Update(product);
                }
            }
        }

        private void LoadList()
        {
            ListCustomers = new ObservableCollection<CustomerInfo>();
            ListBasketDetails = new ObservableCollection<BasketDetails>();

            //Danh sách sản phẩm
            ListBasketDetails = Basket.Instance.Details;

            //Hóa đơn
            TotalPrice = Basket.Instance.TotalPrice();
            LastTotalPrice = TotalPrice - DiscountMoney;            
        }   

        private bool CheckAll()
        {
            if (Basket.Instance.BasketCount() == 0)
                return false;

            if (ExchangeMoney <= 0)
                return false;

            if (string.IsNullOrEmpty(PhoneNumber) || string.IsNullOrEmpty(CustomerName) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Address))
                return false;
            
            if (string.IsNullOrEmpty(CustomerMoney) || Utility<double>.IsNull(TotalPrice) || Utility<double>.IsNull(LastTotalPrice) ||
                 Utility<double>.IsNull(ExchangeMoney))
                return false;
            
            return true;
        }

        private void ClearAll()
        {
            Basket.Instance.isPaid = true;
            CustomerName = PhoneNumber = Address = Email = Code = PhoneNumberFilter = CustomerMoney = "";
            TotalPrice = LastTotalPrice = ExchangeMoney = 0;
            Basket.Instance.ClearAll();

        }
    }
}
