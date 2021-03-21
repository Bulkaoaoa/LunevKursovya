using LunevRestoraunt.Entites;
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

namespace LunevRestoraunt.Pages.Cooker
{
    /// <summary>
    /// Interaction logic for CookersMainPage.xaml
    /// </summary>
    public partial class CookersMainPage : Page
    {
        public CookersMainPage()
        {
            InitializeComponent();
            DataGrdOrders.ItemsSource = AppData.Context.Order.ToList().Where(p => p.PaymentId == null).ToList();
        }

        private void DataGrdOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currOrder = DataGrdOrders.SelectedItem as Order;
            DataGrdOrderDishes.Visibility = Visibility.Visible;
            DataGrdOrderDishes.ItemsSource = AppData.Context.DishOfOrder.ToList().Where(p => p.OrderId == currOrder.Id).ToList();
        }

    }
}
