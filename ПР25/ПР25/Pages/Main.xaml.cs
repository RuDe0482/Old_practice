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
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        MainWindow mainWindow;
        int id = -1;
        public bool isAdmin = false;
        public Main(MainWindow _mainWindow, int id, bool ias)
        {
            InitializeComponent();
            lb_users.Items.Clear();
            mainWindow = _mainWindow;
            isAdmin = ias;
            this.id = id;
            MySqlDataReader user = mainWindow.Select($"select * from `users` where `id` = {id}");
            user.Read();

            user = mainWindow.Select($"select * from `zaxod` where `user` = {id}");
            while (user.Read())
            {
                lb.Items.Add($"Код - {user.GetValue(0)}. Дата и время вхождения/регистрации - {user.GetValue(2)} устройство - компьютер");
            }

            if (isAdmin)
            {

                user = mainWindow.Select($"select * from `users`");
                while (user.Read())
                {

                    lb_users.Items.Add($"Пользователь: {user.GetValue(1)}");
                    users.Add(new User(int.Parse(user.GetValue(0).ToString()), user.GetValue(1).ToString()));
                }
            }
            else
            {
                Add.Visibility = Visibility.Hidden;
                change.Visibility = Visibility.Hidden;
                Delete.Visibility = Visibility.Hidden;
                Polzat.Visibility = Visibility.Hidden;
                lb_users.Visibility = Visibility.Hidden;
            }
        }
        public List<User> users = new List<User>();
        public class User
        {
            public User(int id, string login)
            {
                this.id = id;
                this.login = login;
            }

            public int id { get; set; }
            public string login { get; set; }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MySqlDataReader user = mainWindow.Select($"DELETE from `users` where `id` = {users[lb_users.SelectedIndex].id}");
            user = mainWindow.Select($"select * from `users`");
            users.Clear();
            lb_users.Items.Clear();
            while (user.Read())
            {
                lb_users.Items.Add($"Пользователь: {user.GetValue(1)}");
                users.Add(new User(int.Parse(user.GetValue(0).ToString()), user.GetValue(1).ToString()));
            }
            MessageBox.Show("Удален!");
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.frame.Navigate(new Pages.Regin(mainWindow, false, 0, id));
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.frame.Navigate(new Pages.Regin(mainWindow, true, users[lb_users.SelectedIndex].id, id));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.login);
        }
    }
}
