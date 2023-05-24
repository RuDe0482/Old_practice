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
using MySql.Data.MySqlClient;

namespace ПР25.Pages
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        MainWindow mainWindow;
        public Login(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void UserLogin(object sender, RoutedEventArgs e)
        {
            if (login_tb.Text.Length > 0)
            {
                if (password_tb.Text.Length > 0)
                {
                    MySqlDataReader user = mainWindow.Select($"SELECT * FROM `users` WHERE login='{login_tb.Text}' AND password='{password_tb.Text}'");

                    if (user.HasRows)
                    {
                        MessageBox.Show("Авторизация прошла успешно", "Уведомление");

                        user.Read();
                        mainWindow.Select($"INSERT INTO `zaxod`(`user`,`date`)" +
                                 $" VALUES ('{Convert.ToInt32(user[0].ToString())}','{DateTime.Now}')");
                        if (Convert.ToInt32(user[3].ToString()) == 1)
                        {
                            mainWindow.frame.Navigate(new Pages.Main(mainWindow, Convert.ToInt32(user.GetValue(0)), true));
                        }
                        else
                        {
                            mainWindow.frame.Navigate(new Pages.Main(mainWindow, Convert.ToInt32(user.GetValue(0)), false));
                        }
                    }
                    else MessageBox.Show("Пароль или логин введены не верно", "Уведомление");
                }
                else MessageBox.Show("Пожалуйста, укажитие пароль пользователя", "Уведомление");
            }
            else MessageBox.Show("Пожалуйста, укажитие логин пользователя", "Уведомление");
        }

        private void UserRegin(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.regin);
        }
    }
}
