using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfControlLibrary1
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class CustomTooltipAndLegendExample : UserControl
    {
        private LiveCharts.WinForms.CartesianChart cartesianChart1;

        public CustomTooltipAndLegendExample()
        {
            InitializeComponent();

            Customers = new ChartValues<CustomerVm>
            {
                new CustomerVm
                {
                    Name = "Irvin",
                    LastName = "Hale",
                    Phone = 123456789,
                    PurchasedItems = 8
                },
                new CustomerVm
                {
                    Name = "Malcolm",
                    LastName = "Revees",
                    Phone = 098765432,
                    PurchasedItems = 3
                },
                new CustomerVm
                {
                    Name = "Anne",
                    LastName = "Rios",
                    Phone = 758294026,
                    PurchasedItems = 6
                },
                new CustomerVm
                {
                    Name = "Vivian",
                    LastName = "Howell",
                    Phone = 309382739,
                    PurchasedItems = 3
                },
                new CustomerVm
                {
                    Name = "Caleb",
                    LastName = "Roy",
                    Phone = 682902826,
                    PurchasedItems = 2
                }
            };

            Labels = new[] { "Irvin", "Malcolm", "Anne", "Vivian", "Caleb" };

            //let create a mapper so LiveCharts know how to plot our CustomerViewModel class
            var customerVmMapper = Mappers.Xy<CustomerVm>()
                .X((value, index) => index) // lets use the position of the item as X
                .Y(value => value.PurchasedItems); //and PurchasedItems property as Y

            //lets save the mapper globally
            Charting.For<CustomerVm>(customerVmMapper);

            DataContext = this;
        }

        public CustomTooltipAndLegendExample(LiveCharts.WinForms.CartesianChart cartesianChart1)
        {
            this.cartesianChart1 = cartesianChart1;
            Points = new ChartValues<ToolTipClass>();
            foreach(var ser in cartesianChart1.Series[0].Values)
            {
                var point = new ToolTipClass()
                {
                    Value = ser.ToString()
                };
                //point.Name  
            }
        }

        public ChartValues<CustomerVm> Customers { get; set; }
        public ChartValues<ToolTipClass> Points { get; set; }
        public string[] Labels { get; set; }
    }
}
