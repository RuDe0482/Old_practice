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
    /// Логика взаимодействия для Regin.xaml
    /// </summary>
    public partial class Regin : Page
    {
        MainWindow mainWindow;
        public bool isEdit = false;
        public int USERID = -1;
        public Regin(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
        }
        public Regin(MainWindow _mainWindow, bool isa, int id, int uSERID)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            isEdit = isa;
            this.id = id;
            USERID = uSERID;
        }
        int id = -1;
        private void UserRegin(object sender, RoutedEventArgs e)
        {
            if (!isEdit)
            {
                if (login_tb.Text.Length > 0)
                {
                    if (password_tb.Text.Length > 0)
                    {
                        if (password_tb_2.Text.Length > 0)
                        {
                            if (password_tb.Text == password_tb_2.Text)
                            {
                                MySqlDataReader user = mainWindow.Select($"select * from `users` where `login` = '{login_tb.Text}'");

                                if (!user.HasRows)
                                {
                                    mainWindow.Select($"INSERT INTO `users`(`login`,`password`,`roll`)" +
                                        $" VALUES ('{login_tb.Text}','{password_tb.Text}',2)");
                                    MessageBox.Show("Пользователь успшно зарегестрирован", "Уведомление");
                                    if (USERID != -1) { mainWindow.frame.Navigate(new Pages.Main(mainWindow, USERID, true)); }
                                }
                                else MessageBox.Show("Упс... Данный логин занят, пожалуйста, придумайте новый.", "Уведомление");
                            }
                            else MessageBox.Show("Упс... пароли не совпадают", "Уведомление");
                        }
                        else MessageBox.Show("Повторите пароль", "Уведомление");
                    }
                    else MessageBox.Show("Укажите пароль", "Уведомление");
                }
                else MessageBox.Show("Укажите логин", "Уведомление");
            }
            else
            {
                if (login_tb.Text.Length > 0)
                {
                    if (password_tb.Text.Length > 0)
                    {
                        if (password_tb_2.Text.Length > 0)
                        {
                            if (password_tb.Text == password_tb_2.Text)
                            {
                                MySqlDataReader user = mainWindow.Select($"select * from `users` where `login` = '{login_tb.Text}'");

                                if (!user.HasRows)
                                {
                                    mainWindow.Select($"UPDATE `users` SET `login`='{login_tb.Text}',`password`='{password_tb.Text}',`roll`=2 where id={id}");
                                    MessageBox.Show("Пользователь успшно изменен", "Уведомление");
                                    mainWindow.frame.Navigate(new Pages.Main(mainWindow, USERID, true));
                                }
                                else MessageBox.Show("Упс... Данный логин занят, пожалуйста, придумайте новый.", "Уведомление");
                            }
                            else MessageBox.Show("Упс... пароли не совпадают", "Уведомление");
                        }
                        else MessageBox.Show("Повторите пароль", "Уведомление");
                    }
                    else MessageBox.Show("Пожалуйста, укажите пароль", "Уведомление");
                }
                else MessageBox.Show("Пожалуйста, укажите логин", "Уведомление");
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.login);
        }
    }
}
