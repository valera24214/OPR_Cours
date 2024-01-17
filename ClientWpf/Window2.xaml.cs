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
using System.Windows.Shapes;

namespace ClientWpf
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public string variant;
        public double[] coefs;

        public Window2()
        {
            InitializeComponent();
            Disable();

            coefs = new double[3];
        }

        public void Disable()
        {
            foreach (var c in this.Grid.Children.OfType<CheckBox>())
            {
                c.IsEnabled = false;
                c.IsChecked = false;
            }

            foreach (var c in this.Grid.Children.OfType<ComboBox>())
            {
                c.IsEnabled = false;
                c.SelectedIndex = -1;
            }

        }

        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            Disable();

            if (sender == radioButton_a)
            {
                checkBox_x1_a.IsEnabled = true;
                checkBox_x2_a.IsEnabled = true;
                checkBox_x3_a.IsEnabled = true;

                comboBox_x1_a.IsEnabled = true;
                comboBox_x2_a.IsEnabled = true;
                comboBox_x3_a.IsEnabled = true;
            }
            else if (sender == radioButton_b)
            {
                checkBox_x1_b.IsEnabled = true;
                checkBox_x2_b.IsEnabled = true;
                checkBox_x3_b.IsEnabled = true;

                comboBox_x1_b.IsEnabled = true;
                comboBox_x2_b.IsEnabled = true;
                comboBox_x3_b.IsEnabled = true;
            }
        }

        private void button_OK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;

            if (radioButton_a.IsChecked == true)
            {
                variant = "a";

                switch (comboBox_x1_a.SelectedIndex)
                {
                    case 0:
                    default:
                        {
                            coefs[0] = 0;
                            break;
                        }

                    case 1:
                        {
                            coefs[0] = 10;
                            break;
                        }
                    case 2:
                        {
                            coefs[0] = -10;
                            break;
                        }
                }

                switch (comboBox_x2_a.SelectedIndex)
                {
                    case 0:
                    default:
                        {
                            coefs[1] = 0;
                            break;
                        }

                    case 1:
                        {
                            coefs[1] = 10;
                            break;
                        }
                    case 2:
                        {
                            coefs[1] = -10;
                            break;
                        }
                }

                switch (comboBox_x3_a.SelectedIndex)
                {
                    case 0:
                    default:
                        {
                            coefs[2] = 0;
                            break;
                        }

                    case 1:
                        {
                            coefs[2] = 10;
                            break;
                        }
                    case 2:
                        {
                            coefs[2] = -10;
                            break;
                        }
                }
            }

            else if (radioButton_b.IsChecked == true)
            {
                variant = "b";

                switch (comboBox_x1_b.SelectedIndex)
                {
                    case 0:
                    default:
                        {
                            coefs[0] = 0;
                            break;
                        }

                    case 1:
                        {
                            coefs[0] = 10;
                            break;
                        }
                    case 2:
                        {
                            coefs[0] = -10;
                            break;
                        }
                }

                switch (comboBox_x2_b.SelectedIndex)
                {
                    case 0:
                    default:
                        {
                            coefs[1] = 0;
                            break;
                        }

                    case 1:
                        {
                            coefs[1] = 10;
                            break;
                        }
                    case 2:
                        {
                            coefs[1] = -10;
                            break;
                        }
                }

                switch (comboBox_x3_b.SelectedIndex)
                {
                    case 0:
                    default:
                        {
                            coefs[2] = 0;
                            break;
                        }

                    case 1:
                        {
                            coefs[2] = 10;
                            break;
                        }
                    case 2:
                        {
                            coefs[2] = -10;
                            break;
                        }
                }
            }

            else if (radioButton_с.IsChecked == true)
            {
                variant = "c";
            }
        }

        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
