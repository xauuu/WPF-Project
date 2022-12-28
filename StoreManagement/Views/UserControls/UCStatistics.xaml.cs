using StoreManagement.DAO;
using StoreManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StoreManagement.Views.UserControls
{
    /// <summary>
    /// Interaction logic for UCStatistics.xaml
    /// </summary>
    public partial class UCStatistics : UserControl
    {
        List<BillDetail> list;
        public UCStatistics()
        {
            InitializeComponent();
            //Get all bill details
            list = new BillDetailDAO().GetAll();
            //Return if list contains no products
            if (list.Count() == 0)
                return;
            LoadStatisticsByProduct();
        }
        private void LoadStatisticsByProduct()
        {
            //Only get productID and unit price
            List<KeyValuePair<string, double>> sources = new List<KeyValuePair<string, double>>{ };
            foreach (BillDetail bd in list)
            {
                sources.Add(new KeyValuePair<string, double>(bd.ProductId.ToString(), bd.UnitPrice*bd.Amount));
            }
            //Sum duplicated value
            List<KeyValuePair<string, double>> result = new List<KeyValuePair<string, double>> { };
            foreach (KeyValuePair<string, double> pair in sources)
            {
                if (result.FindIndex(x => x.Key == pair.Key) < 0)
                {
                    double total = sources.Where(x => x.Key == pair.Key).Sum(x => x.Value);
                    result.Add(new KeyValuePair<string, double>(pair.Key, total));
                }
            }
            //Replace key with Product DisplayName
            ProductDAO productDAO = new ProductDAO();
            for (int i=0;i<result.Count();i++)
            {
                result[i] = new KeyValuePair<string, double>(productDAO.Get(int.Parse(result[i].Key)).DisplayName, result[i].Value);
            }
            ((PieSeries)chartStatistics.Series[0]).ItemsSource = result;

            chartStatistics.Title = "Statistics by products";
        }

        private void LoadStatisticsByBrand()
        {
            //Only get productID and unit price
            List<KeyValuePair<string, double>> sources = new List<KeyValuePair<string, double>> { };
            foreach (BillDetail bd in list)
            {
                sources.Add(new KeyValuePair<string, double>(bd.ProductId.ToString(), bd.UnitPrice));
            }
            
            //Replace key with Product DisplayName
            ProductDAO productDAO = new ProductDAO();
            for (int i = 0; i < sources.Count(); i++)
            {
                sources[i] = new KeyValuePair<string, double>(productDAO.Get(int.Parse(sources[i].Key)).Brand.BrandName, sources[i].Value);
            }

            //Sum duplicated value
            List<KeyValuePair<string, double>> result = new List<KeyValuePair<string, double>> { };
            foreach (KeyValuePair<string, double> pair in sources)
            {
                if (result.FindIndex(x => x.Key == pair.Key) < 0)
                {
                    double total = sources.Where(x => x.Key == pair.Key).Sum(x => x.Value);
                    result.Add(new KeyValuePair<string, double>(pair.Key, total));
                }
            }
            ((PieSeries)chartStatistics.Series[0]).ItemsSource = result;

            chartStatistics.Title = "Statistics by brands";
        }

        private void LoadStatisticsByRevenue()
        {
            //Only get productID and unit price
            List<KeyValuePair<string, double>> sources = new List<KeyValuePair<string, double>> { };
            foreach (BillDetail bd in list)
            {
                Nullable <double> originalPrice = new ProductDAO().Get(bd.ProductId).OriginalPrice;
                sources.Add(new KeyValuePair<string, double>(bd.ProductId.ToString(), (bd.UnitPrice - (double)originalPrice) * bd.Amount));
            }
            //Sum duplicated value
            List<KeyValuePair<string, double>> result = new List<KeyValuePair<string, double>> { };
            foreach (KeyValuePair<string, double> pair in sources)
            {
                if (result.FindIndex(x => x.Key == pair.Key) < 0)
                {
                    double total = sources.Where(x => x.Key == pair.Key).Sum(x => x.Value);
                    result.Add(new KeyValuePair<string, double>(pair.Key, total));
                }
            }
            //Replace key with Product DisplayName
            ProductDAO productDAO = new ProductDAO();
            for (int i = 0; i < result.Count(); i++)
            {
                result[i] = new KeyValuePair<string, double>(productDAO.Get(int.Parse(result[i].Key)).DisplayName, result[i].Value);
            }
            ((PieSeries)chartStatistics.Series[0]).ItemsSource = result;

            chartStatistics.Title = "Statistics by revenue";
        }

        private void ByProducts_Click(object sender, RoutedEventArgs e)
        {
            LoadStatisticsByProduct();
        }

        private void ByBrands_Click(object sender, RoutedEventArgs e)
        {
            LoadStatisticsByBrand();
        }

        private void ByRevenue_Click(object sender, RoutedEventArgs e)
        {
            LoadStatisticsByRevenue();
        }
    }
}
