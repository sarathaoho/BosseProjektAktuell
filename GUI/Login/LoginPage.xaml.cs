using GUI.Home;
using Logic.Database.Entities;
using Logic.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.Login
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private const string _errorMsg = "Inloggningen misslyckades";

        private LoginService _loginService;
        public LoginPage()
        {
            InitializeComponent();

            _loginService = new LoginService();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //string username = this.tbUsername.Text;
            //string password = this.pbPassword.Password;

            // Nu kan vi logga in genom att bara trycka på logga in
            string username = "Bosse";
            string password = "Meckarn123";
            
            var user = _loginService.Login(username, password);
            //Lade till denna för att kunna logga in, verkade behövas
            //user.IsAdmin = true;

            // Om användaren inte finns så skrivs felmeddelande ut
            if (user == null)
            {
                MessageBox.Show(_errorMsg);
                this.tbUsername.Clear();
                this.pbPassword.Clear();
            }

            // Om användaren finns men den inte är admin så öppnas en annan ruta
            else if (user.IsAdmin == false)
            {
                // Här hamnar koden för basic-användare
                // BasicUserHomePage asdasda = new BasicUserHomePage();
            }
            
            // Om användaren finns och om den är admin så öppnas huvudrutan
            else
            {
                HomePage homePage = new HomePage();
                this.NavigationService.Navigate(homePage);
               
            }
        }
    }
}
