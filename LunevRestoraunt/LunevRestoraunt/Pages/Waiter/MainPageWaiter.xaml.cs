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

namespace LunevRestoraunt.Pages.Waiter
{
    /// <summary>
    /// Interaction logic for MainPageWaiter.xaml
    /// </summary>
    public partial class MainPageWaiter : Page
    {
        public MainPageWaiter()
        {
            InitializeComponent();
            UpdateDataGrd();
        }

        private void UpdateDataGrd()
        {
            try
            {
                DataGrdWaitersOrders.ItemsSource = AppData.Context.Order.ToList().Where(p => p.WaiterId == AppData.CurrUser.Id && p.PaymentId == null).ToList();
            }
            catch
            {
                MessageBox.Show("Проблемы с подключением к базе, перезапустите приложение", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void BtnCreatePayment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEditOrder_Click(object sender, RoutedEventArgs e)
        {
            var currOrder = (sender as Button).DataContext as Order;
            var buffListOfDish = AppData.Context.Dish.ToList();
            foreach (var item in buffListOfDish.ToList())
            {
                item.CountOnOrder = 0;
            }
            var listOfDishOfOrder = AppData.Context.DishOfOrder.ToList().Where(p => p.OrderId == currOrder.Id).ToList();
            List<Dish> listOfDish = new List<Dish>();
            foreach (var item in listOfDishOfOrder.ToList())
            {
                var currDish = item.Dish;
                currDish.CountOnOrder = item.Count;
                listOfDish.Add(currDish);
            }
            AppData.MainFrame.Navigate(new Pages.Waiter.CreateOrderPage(listOfDish, currOrder));
        }

        private void BtnCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            AppData.MainFrame.Navigate(new Pages.Waiter.CreateOrderPage());
        }

        private void TxtBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                DataGrdWaitersOrders.ItemsSource = AppData.Context.Order.ToList().Where(p => p.WaiterId == AppData.CurrUser.Id
                    && p.PaymentId != null && p.Table.Name.ToLower().Contains(TxtBoxSearch.Text.ToLower())).ToList();
            }
            catch 
            {
                MessageBox.Show("Проблемы с подключением к базе, перезапустите приложение", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDataGrd();

        }
    }
}
