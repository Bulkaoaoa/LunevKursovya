using LunevRestoraunt.Entites;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace LunevRestoraunt.Pages.Admin
{
    /// <summary>
    /// Interaction logic for AddDishPage.xaml
    /// </summary>
    public partial class AddDishPage : Page
    {
        byte[] _currImage = null;
        public AddDishPage()
        {
            InitializeComponent();
            CmbBoxTypeOfDish.ItemsSource = AppData.Context.TypeOfDish.ToList();
        }

        private void BtnCreateDish_Click(object sender, RoutedEventArgs e)
        {
            var errors = "";
            if (string.IsNullOrWhiteSpace(TxtBoxName.Text)) errors += " Вы не ввели имя \r\n";
            if (string.IsNullOrWhiteSpace(TxtBoxPrice.Text)) errors += " Вы не ввели цену\r\n";
            if (CmbBoxTypeOfDish.SelectedIndex == -1) errors += "Вы не выбрали тип блюда\r\n";
            if (_currImage == null) errors += " Вы не выбрали фотографию\r\n";

            decimal currPrice = 0;
            try
            {
                currPrice = decimal.Parse(TxtBoxPrice.Text);
            }
            catch 
            {
                errors += "Вы ввели неккоректные данные для цены\r\n";
            }

            if (errors.Length == 0)
            {
                var currDish = new Dish()
                {
                    Name = TxtBoxName.Text.Trim(),
                    Price = currPrice,
                    Image = _currImage,
                    TypeOfDishId = (CmbBoxTypeOfDish.SelectedItem as TypeOfDish).Id,
                };
                AppData.Context.Dish.Add(currDish);
                AppData.Context.SaveChanges();
                MessageBox.Show("Вы успешно добавили заказ", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

                TxtBoxName.Text = "";
                TxtBoxPrice.Text = "";
                _currImage = null;
                ImgOfDish.Source = null;
                CmbBoxTypeOfDish.SelectedIndex = 0;
            }
            else
                MessageBox.Show(errors, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnSelectPic_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images | *.jpg; *.png; *.jpeg";
            if (ofd.ShowDialog() == true)
            {
                _currImage = File.ReadAllBytes(ofd.FileName);
                ImgOfDish.Source = new BitmapImage(new Uri(ofd.FileName));
            }
        }
    }
}
