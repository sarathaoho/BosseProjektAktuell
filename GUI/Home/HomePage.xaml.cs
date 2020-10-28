using Logic.Database.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.Home
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {

        //TODO: Testa blbla
        public HomePage()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            string userName = tbUserName.Text;
            string password = tbPassword.Text;

            User user = new BasicUser() { Username = userName, Password = password };

            Users.UserList.Add(user);

            string json = JsonSerializer.Serialize(Users.UserList);
            
            FileStream fs = File.OpenWrite(@"C:\GitHub\BosseProjektAktuell\UsersTest.json");
            StreamWriter sw = new StreamWriter(fs);

            sw.WriteLine(json);
            sw.Close();

            MessageBox.Show("Användare tillagd.");
        }
    }
}
