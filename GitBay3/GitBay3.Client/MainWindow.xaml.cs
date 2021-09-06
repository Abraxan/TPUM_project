using GitBay3.Common;
using System;
using System.Collections.Generic;
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

namespace GitBay3.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            

        }

        private void btnBuy_c1_Clicked(object sender, RoutedEventArgs e)
        {
            float amount;
            if (float.TryParse(tbC1_buy_input.Text, out amount))
            {
                (DataContext as ViewModel).Buy(Currencies.BTC, amount);
            }
        }

        private void btnBuy_c2_Clicked(object sender, RoutedEventArgs e)
        {
            float amount;
            if (float.TryParse(tbC2_buy_input.Text, out amount))
            {
                (DataContext as ViewModel).Buy(Currencies.ETH, amount);
            }
        }

        private void btnBuy_c3_Clicked(object sender, RoutedEventArgs e)
        {
            float amount;
            if (float.TryParse(tbC3_buy_input.Text, out amount))
            {
                (DataContext as ViewModel).Buy(Currencies.LTC, amount);
            }
        }

        private void btnSell_c1_Clicked(object sender, RoutedEventArgs e)
        {
            float amount;
            if (float.TryParse(tbC1_sell_input.Text, out amount))
            {
                (DataContext as ViewModel).Sell(Currencies.BTC, amount);
            }
        }

        private void btnSell_c2_Clicked(object sender, RoutedEventArgs e)
        {
            float amount;
            if (float.TryParse(tbC2_sell_input.Text, out amount))
            {
                (DataContext as ViewModel).Sell(Currencies.ETH, amount);
            }
        }

        private void btnSell_c3_Clicked(object sender, RoutedEventArgs e)
        {
            float amount;
            if (float.TryParse(tbC3_sell_input.Text, out amount))
            {
                (DataContext as ViewModel).Sell(Currencies.LTC, amount);
            }
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            DataContext = new ViewModel();
            (DataContext as ViewModel).LoadAccount();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            (DataContext as ViewModel).Close();
        }
    }
}
