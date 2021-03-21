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

namespace LunevRestoraunt.Pages
{
    /// <summary>
    /// Interaction logic for AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var errors = "";
            var bufUser = AppData.Context.User.ToList().Where(p => p.Login.Trim() == TxtBoxLogin.Text.Trim()
                && p.Password.Trim() == PassBoxPassword.Password.Trim()).FirstOrDefault();
            if (string.IsNullOrWhiteSpace(TxtBoxLogin.Text)) errors += "Вы не ввели логин \r\n";
            if (string.IsNullOrWhiteSpace(PassBoxPassword.Password)) errors += "Вы не ввели пароль \r\n";
            if (bufUser == null) errors += "Такого пользователя не существует\r\n";

            if (errors.Length == 0)
            {
                AppData.CurrUser = bufUser;

                switch (bufUser.RoleId)
                {
                    case 1:
                        break;
                    case 2:
                        AppData.MainFrame.Navigate(new Pages.Cooker.CookersMainPage());
                        break;
                    case 3:
                        AppData.MainFrame.Navigate(new Pages.Waiter.MainPageWaiter());
                        break;
                    case 4:
                        AppData.MainFrame.Navigate(new Pages.Cooker.CookersMainPage());
                        break;
                }
            }
            else
                MessageBox.Show(errors, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
