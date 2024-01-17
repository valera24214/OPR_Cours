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
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace ClientWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServiceReference1.Service1Client client;
        
        RSA rsa;
        RSAParameters _publicKey_right;

        public MainWindow()
        {
            InitializeComponent();

            client = new ServiceReference1.Service1Client();
            rsa = new RSA();
            _publicKey_right = JsonConvert.DeserializeObject<RSAParameters>(client.RSA_Connect(rsa.GetPublicKey()));
        }

        private void button_enter_Click(object sender, RoutedEventArgs e)
        {

           /* if (client.Authorization(rsa.Encrypt(_publicKey_right, textBoxLogin.Text), rsa.Encrypt(_publicKey_right, passwordBox.Password)) == 1)*/
            {
                MessageBox.Show("Добро пожаловать");
                
                Window1 window = new Window1(client, rsa, _publicKey_right);
                window.Show();
                this.Hide();
            }
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow(client, rsa, _publicKey_right);
            registrationWindow.Show();
        }
    }
}
