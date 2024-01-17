using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClientWpf.Registration_Controls;
namespace ClientWpf
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        ServiceReference1.Service1Client client;
        RSA rsa;
        RSAParameters _publicKey_right;

        string email;
        string code;

        int flag = 0;
        public RegistrationWindow(ServiceReference1.Service1Client client, RSA rsa, RSAParameters _publicKey_right)
        {
            InitializeComponent();

            this.rsa = rsa;
            this.client = client;
            this._publicKey_right = _publicKey_right;

            UserControl1 userControl1 = new UserControl1();
            stackPanel.Children.Add(userControl1);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (flag == 0)
            {
                try
                {
                    var control1 = (UserControl1)stackPanel.Children[0];

                    email = control1.Return_email();
                    code = rsa.Decrypt(client.Verification(rsa.Encrypt(_publicKey_right, email)));

                    stackPanel.Children.Clear();
                    UserControl2 userControl2 = new UserControl2();
                    stackPanel.Children.Add(userControl2);

                    flag++;
                }
                catch 
                {
                    MessageBox.Show("Произошла ошибка. Проверьте правильность введенного e-mail адреса");
                }
                
            }
            else if (flag == 1)
            {
                try
                {
                    var control2 = (UserControl2)stackPanel.Children[0];

                    if (control2.Return_code() == code)
                    {
                        stackPanel.Children.Clear();
                        UserControl3 userControl3 = new UserControl3();
                        stackPanel.Children.Add(userControl3);
                        button.Content = "Завершить";
                        flag++;
                    }
                    else
                    {
                        MessageBox.Show("Введён неправильный код");
                    };

                }
                catch 
                {
                    MessageBox.Show("Произошла непредвиденная ошибка");
                }
                
            }
            else if (flag == 2)
            {
                try
                {
                    var control3 = (UserControl3)stackPanel.Children[0];
                    var strs = control3.Return_passwords();
                    if (strs[0] == strs[1] && strs[0].Length>=8)
                    {
                        if (client.Register(rsa.Encrypt(_publicKey_right, email), rsa.Encrypt(_publicKey_right, strs[0])))
                        {
                            MessageBox.Show("Ваша учётная запись успешно зарегистрирована");
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пароли в обоих полях должны совпадать и иметь длину более 8-ми символов");
                    }
                }
                catch 
                {
                    MessageBox.Show("Произошла непредвиденная ошибка");
                }
                
            }    


        }
    }
}
