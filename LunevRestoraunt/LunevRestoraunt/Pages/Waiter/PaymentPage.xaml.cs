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
    /// Interaction logic for PaymentPage.xaml
    /// </summary>
    public partial class PaymentPage : Page
    {
        private Order _currOrder;
        public PaymentPage(Order currOrder)
        {
            InitializeComponent();
            _currOrder = currOrder;
        }

        private void BtnConfirmPayment_Click(object sender, RoutedEventArgs e)
        {
            var errors = "";

            if (string.IsNullOrWhiteSpace(TxtBoxCardNubmer.Text)) errors += "Вы не ввели номер карты\r\n";
            if (string.IsNullOrWhiteSpace(TxtBoxCardOwner.Text)) errors += "Вы не ввели владельца карты\r\n";

            if (errors.Length == 0)
            {
                var newPayment = new Payment()
                {
                    CardNumber = TxtBoxCardNubmer.Text.Trim(),
                    CardOwner = TxtBoxCardOwner.Text.Trim(),
                    DateTimePayment = DateTime.Now,
                    StatusOfPaymentId = 1,
                };
                AppData.Context.Payment.Add(newPayment);
                AppData.Context.SaveChanges();
                _currOrder.PaymentId = newPayment.Id;
                AppData.Context.SaveChanges();
                AppData.MainFrame.GoBack();
            }
            else
                MessageBox.Show(errors, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
