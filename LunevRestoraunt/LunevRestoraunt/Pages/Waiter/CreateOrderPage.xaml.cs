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
using Table = LunevRestoraunt.Entites.Table;

namespace LunevRestoraunt.Pages.Waiter
{
    /// <summary>
    /// Interaction logic for CreateOrderPage.xaml
    /// </summary>
    public partial class CreateOrderPage : Page
    {
        private List<Dish> _orderList = new List<Dish>();
        private bool _isEdit = false;
        private Order _currOrder;
        public CreateOrderPage()
        {
            InitializeComponent();
            var currListOfDish = AppData.Context.Dish.ToList().OrderBy(p => p.TypeOfDish.Name).ToList();
            foreach (var item in currListOfDish.ToList())
            {
                item.CountOnOrder = 0;
            }
            DataGrdDish.ItemsSource = currListOfDish;
            CmbBoxTable.ItemsSource = AppData.Context.Table.ToList();
            _isEdit = false;
            BtnCreateOrder.Content = "Создать";
        }

        public CreateOrderPage(List<Dish> orderList, Order currOrder)
        {
            InitializeComponent();
            var listOfOrders = AppData.Context.Dish.ToList().OrderBy(p => p.TypeOfDish.Name).ToList();
         
            foreach (var item in orderList)
            {
                listOfOrders.Add(item);
            }
            listOfOrders = listOfOrders.GroupBy(p => p.Name).Select(p => p.Last()).ToList();
            DataGrdDish.ItemsSource = listOfOrders;
            CmbBoxTable.ItemsSource = AppData.Context.Table.ToList();
            _orderList = orderList;
            _isEdit = true;
            _currOrder = currOrder;
            BtnCreateOrder.Content = "Сохранить";
            CmbBoxTable.SelectedItem = currOrder.Table;
            CmbBoxTable.IsEnabled = false;
        }



        private void TxtBoxCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            var currTextBox = (sender as TextBox);
            var currDish = currTextBox.DataContext as Dish;
            int countOfDish = 0;
            try
            {
                if (currTextBox.Text.Count() != 0)
                    countOfDish = int.Parse(currTextBox.Text);
            }
            catch
            {

                MessageBox.Show("Вы не можете вводить такие значения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                currTextBox.Text = "0";

            }

            if (countOfDish == 0)
                _orderList.Remove(currDish);
            else
            {
                if (_orderList != null)
                {
                    var dishOfList = _orderList.Where(p => p.Name == currDish.Name).FirstOrDefault();
                    if (dishOfList != null)
                        _orderList.Remove(dishOfList);
                }

                currDish.CountOnOrder = countOfDish;
                _orderList.Add(currDish);
            }

        }

        private void BtnCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            var errors = "";
            if (CmbBoxTable.SelectedIndex == -1) errors += "Вы не выбрали столик \r\n";
            if (_orderList.Count == 0) errors += "Вы не выбрали блюда\r\n";

            if (errors.Length == 0)
            {
                if (_isEdit == false)
                {
                    var newOrder = new Order()
                    {
                        WaiterId = AppData.CurrUser.Id,
                        DateTimeOrdered = DateTime.Now,
                        TableId = (CmbBoxTable.SelectedItem as Table).Id,
                    };
                    AppData.Context.Order.Add(newOrder);
                    AppData.Context.SaveChanges();

                    foreach (var item in _orderList.ToList())
                    {
                        var newDishOfOrder = new DishOfOrder()
                        {
                            DishId = item.Id,
                            Count = item.CountOnOrder,
                            OrderId = newOrder.Id
                        };
                        AppData.Context.DishOfOrder.Add(newDishOfOrder);
                    }
                    AppData.Context.SaveChanges();
                    MessageBox.Show($"Вы успешно создали заказ", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    foreach (var itemOFDB in AppData.Context.DishOfOrder.ToList().Where(p => p.OrderId == _currOrder.Id).ToList())
                    {
                        AppData.Context.DishOfOrder.Remove(itemOFDB);
                    }
                    AppData.Context.SaveChanges();
                    foreach (var item in _orderList.ToList())
                    {
                        var newDishOfOrder = new DishOfOrder()
                        {
                            DishId = item.Id,
                            Count = item.CountOnOrder,
                            OrderId = _currOrder.Id
                        };
                        AppData.Context.DishOfOrder.Add(newDishOfOrder);
                    }
                    AppData.Context.SaveChanges();
                    MessageBox.Show($"Вы успешно изменили заказ", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
                MessageBox.Show(errors, "Ошбика", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
