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
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
            CmbBoxRole.ItemsSource = AppData.Context.Role.ToList();
            var BuffList = CmbBoxRole.ItemsSource;
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            var errors = "";
            if (string.IsNullOrWhiteSpace(TxtBoxLogin.Text)) errors += "Вы не ввели логин\r\n";
            if (string.IsNullOrWhiteSpace(PassBoxPassword.Password)) errors += "Вы не ввели пароль\r\n";
            if (string.IsNullOrWhiteSpace(TxtBoxFirstName.Text)) errors += "Вы не ввели имя\r\n";
            if (string.IsNullOrWhiteSpace(TxtBoxLastName.Text)) errors += "Вы не ввели фамилию\r\n";
            if (CmbBoxRole.SelectedIndex == -1) errors += "Вы не выбрали роль \r\n";
            if (AppData.Context.User.ToList().Where(p => p.Login.ToLower().Trim()
                == TxtBoxLogin.Text.ToLower().Trim()).FirstOrDefault() != null) errors += "Такой логин уже существует\r\n";

            if (errors.Length == 0)
            {
                try
                {
                    var newUser = new User()
                    {
                        Login = TxtBoxLogin.Text.Trim(),
                        Password = PassBoxPassword.Password.Trim(),
                        FirstName = TxtBoxFirstName.Text.Trim(),
                        LastName = TxtBoxLastName.Text.Trim(),
                        Patronymic = TxtBoxPatronymic.Text.Trim() == "" ? null : TxtBoxPatronymic.Text.Trim(),
                        RoleId = (CmbBoxRole.SelectedItem as Role).Id
                    };
                    AppData.Context.User.Add(newUser);
                    AppData.Context.SaveChanges();
                    AppData.MainFrame.GoBack();
                }
                catch
                {
                    MessageBox.Show("Проблемы с подключением к базе, перезапустите приложение", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                MessageBox.Show(errors, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);

        }
    }
}
