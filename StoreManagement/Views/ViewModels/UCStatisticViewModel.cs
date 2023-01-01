using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore.SkiaSharpView;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore;
using System.Windows.Input;
using System.Collections.ObjectModel;
using StoreManagement.Entities;
using StoreManagement.DAO;
using static SkiaSharp.HarfBuzz.SKShaper;

namespace StoreManagement.Views.ViewModels
{
    public class UCStatisticViewModel : BaseViewModel
    {

        public ICommand LoadCommand { get; set; }

        private ObservableCollection<ISeries> _series { get; set; }
        public ObservableCollection<ISeries> Series { get => _series; set { if (value == _series) return; _series = value; OnPropertyChanged(); } }
        private List<Axis> _xaxes { get; set; }
        public List<Axis> XAxes { get => _xaxes; set { if (value == _xaxes) return; _xaxes = value; OnPropertyChanged(); } }
        private List<Axis> _yAxes { get; set; }
        public List<Axis> YAxes { get => _yAxes; set { if (value == _yAxes) return; _yAxes = value; OnPropertyChanged(); } }
        private LabelVisual _title { get; set; }
        public LabelVisual Title { get => _title; set { if (value == _title) return; _title = value; OnPropertyChanged(); } }

        public UCStatisticViewModel()
        {
            LoadCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Load();
            });
        }

        private void Load()
        {
            List<Bill> bill = new BillDAO().GetAll().ToList();

            Title = new LabelVisual
            {
                Text = "Thống kê doanh thu trong 7 ngày",
                TextSize = 25,
                Padding = new LiveChartsCore.Drawing.Padding(15),
                Paint = new SolidColorPaint(SKColors.DarkSlateGray)
            };
            List<KeyValuePair<string, double>> result = new List<KeyValuePair<string, double>> { };
            for (int i = 0; i < 6; i++)
            {
                DateTime date = DateTime.Now.AddDays(-i);
                double total = bill.Where(x => x.BillDate.Date == date.Date).Sum(x => x.TotalPrice);
                result.Add(new KeyValuePair<string, double>(date.Date.ToString("dd/MM/yyyy"), total));
            }
            result.Reverse();

            Series = new ObservableCollection<ISeries>
            {
                new ColumnSeries<double>
                {
                    Values = result.Select(x => x.Value).ToList(),
                    TooltipLabelFormatter = (chartPoint) => $"Doanh thu: {chartPoint.PrimaryValue:C2}",
                },

                new LineSeries<double>
                {
                    Values = result.Select(x => x.Value).ToList(),
                    Fill = null,
                    IsHoverable = false,
                }
            };

            XAxes = new List<Axis>
            {
                new Axis
                {
                    Labels = result.Select(x => x.Key).ToList(),
                    LabelsRotation = 45,
                    LabelsPaint = new SolidColorPaint(SKColors.DarkSlateGray),
                }
            };
        }
    }
}
