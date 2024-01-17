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
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Security.Cryptography;

namespace ClientWpf
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        ServiceReference1.Service1Client client;

        int flag;
        List<double[,]> proc_Simpex;
        double simplex_rez = 0;

        double[] proc_Generic;
        double generic_rez = 0;

/*        double[,] matrix;*/

        WindowsFormsHost host;
        DataGridView dataGridView;

        bool changed = false;
        string variant;

        RSA rsa;
        RSAParameters _publicKey_right;

        public Window1(ServiceReference1.Service1Client client, RSA rsa, RSAParameters _publicKey_right)
        {
            InitializeComponent();

            this.rsa = rsa;
            this.client = client;
            this._publicKey_right = _publicKey_right;

            flag = 0;

            host = new WindowsFormsHost();
            dataGridView = new DataGridView();
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            label_Title.Visibility = Visibility.Hidden;
            textBox_Simplex.Visibility = Visibility.Hidden;
            button_Prev.Visibility = Visibility.Hidden;
            button_Next.Visibility = Visibility.Hidden;

            textBox_Generic.Visibility = Visibility.Hidden;
        }

        /*private void Create_Matr()
        {
            var q = Convert.ToDouble(textBox_limit_result.Text) - Convert.ToDouble(textBox_limit_x1.Text) * Convert.ToDouble(textBox_x1.Text) - Convert.ToDouble(textBox_limit_x2.Text) * Convert.ToDouble(textBox_x2.Text) - Convert.ToDouble(textBox_limit_x3.Text) * Convert.ToDouble(textBox_x3.Text);

            var q2 = Convert.ToDouble(textBox_func_x1.Text) * Convert.ToDouble(textBox_x1.Text) + Convert.ToDouble(textBox_func_x2.Text) * Convert.ToDouble(textBox_x2.Text) + Convert.ToDouble(textBox_func_x3.Text) * Convert.ToDouble(textBox_x3.Text);

                matrix = new double[,]
                {
                    {Convert.ToDouble(textBox_x1.Text), 1, 0, 0, -1, 0, 0, 0 },
                    {Convert.ToDouble(textBox_x2.Text), 0, 1, 0, 0, -1, 0, 0 },
                    {Convert.ToDouble(textBox_x3.Text), 0, 0, 1, 0, 0, -1, 0  },
                    {q, 0, 0, 0, Convert.ToDouble(textBox_limit_x1.Text), Convert.ToDouble(textBox_limit_x2.Text), Convert.ToDouble(textBox_limit_x3.Text), 1  },
                    {q2, 0, 0, 0, -Convert.ToDouble(textBox_func_x1.Text), -Convert.ToDouble(textBox_func_x2.Text), -Convert.ToDouble(textBox_func_x3.Text),0 }
                };

            *//*{ 2000, 1, 0, 0, -1, 0, 0, 0},
                { 1800, 0, 1, 0, 0, -1, 0, 0},
                { 1500, 0, 0, 1, 0, 0, -1, 0},
                { 10500, 0, 0, 0, 8, 10, 11, 1 },
                { 45500, 0, 0, 0,-7,-10,-9, 0 }*//*
        }*/

        private void button_Simplex_Click(object sender, RoutedEventArgs e)
        {
            var q = Convert.ToDouble(textBox_limit_result.Text) - Convert.ToDouble(textBox_limit_x1.Text) * Convert.ToDouble(textBox_x1.Text) - Convert.ToDouble(textBox_limit_x2.Text) * Convert.ToDouble(textBox_x2.Text) - Convert.ToDouble(textBox_limit_x3.Text) * Convert.ToDouble(textBox_x3.Text);

            var q2 = Convert.ToDouble(textBox_func_x1.Text) * Convert.ToDouble(textBox_x1.Text) + Convert.ToDouble(textBox_func_x2.Text) * Convert.ToDouble(textBox_x2.Text) + Convert.ToDouble(textBox_func_x3.Text) * Convert.ToDouble(textBox_x3.Text);

            var matrix = new double[,]
                {
                    {Convert.ToDouble(textBox_x1.Text), 1, 0, 0, -1, 0, 0, 0 },
                    {Convert.ToDouble(textBox_x2.Text), 0, 1, 0, 0, -1, 0, 0 },
                    {Convert.ToDouble(textBox_x3.Text), 0, 0, 1, 0, 0, -1, 0  },
                    {q, 0, 0, 0, Convert.ToDouble(textBox_limit_x1.Text), Convert.ToDouble(textBox_limit_x2.Text), Convert.ToDouble(textBox_limit_x3.Text), 1  },
                    {q2, 0, 0, 0, -Convert.ToDouble(textBox_func_x1.Text), -Convert.ToDouble(textBox_func_x2.Text), -Convert.ToDouble(textBox_func_x3.Text),0 }
                };

            List<string> arg = new List<string>();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    arg.Add(matrix[i, j].ToString());

                }
            }

            for(int i = 0; i< arg.Count; i++)
            {
                arg[i] = rsa.Encrypt(_publicKey_right, arg[i]); 
            }

            string rez_str = client.Count_Simplex(arg.ToArray(), out string[] proc_string);
            double[] rez = JsonConvert.DeserializeObject<double[]>(rsa.Decrypt(rez_str));
            proc_Simpex = new List<double[,]>();

            foreach (var p_s in proc_string)
            {
                proc_Simpex.Add(JsonConvert.DeserializeObject<double[,]>(rsa.Decrypt(p_s)));
            }

            DGV_Fill();

            simplex_rez = Convert.ToDouble(textBox_func_x1.Text) * rez[0] + Convert.ToDouble(textBox_func_x2.Text) * rez[1] + Convert.ToDouble(textBox_func_x3.Text) * rez[2];

            label_Title.Visibility = Visibility.Visible;
            textBox_Simplex.Visibility = Visibility.Visible;
            button_Prev.Visibility = Visibility.Visible;
            button_Next.Visibility = Visibility.Visible;

            InfoFill(rez, textBox_Simplex);
        }

        private void InfoFill(double[] rez, System.Windows.Controls.TextBox textBox)
        {
            textBox.Clear();
            textBox.Text += "Оптимальный план выпуска деталей:" + Environment.NewLine;
            textBox.Text += rez[0] + " деталей первого вида;" + Environment.NewLine;
            textBox.Text += rez[1] + " деталей второго вида;" + Environment.NewLine;
            textBox.Text += rez[2] + " деталей третьего вида;" + Environment.NewLine;
            textBox.Text += Environment.NewLine;

            textBox.Text += "Полученная прибыль:" + Environment.NewLine;

            string expr = textBox_func_x1.Text + "*" + rez[0] + "+" + textBox_func_x2.Text + "*" + rez[1] + "+" + textBox_func_x3.Text + "*" + rez[2];
            double rez_expr = Convert.ToDouble(textBox_func_x1.Text) * rez[0] + Convert.ToDouble(textBox_func_x2.Text) * rez[1] + Convert.ToDouble(textBox_func_x3.Text) * rez[2];

            textBox.Text += expr + "=" + rez_expr + ";" + Environment.NewLine;
            textBox.Text += Environment.NewLine;

            textBox.Text += "Удовлетворение месячной программе выпуска:" + Environment.NewLine;
            textBox.Text += "Детали первого вида: " + rez[0] + ">=" + textBox_x1.Text + ";" + Environment.NewLine;
            textBox.Text += "Детали второго вида: " + rez[1] + ">=" + textBox_x2.Text + ";" + Environment.NewLine;
            textBox.Text += "Детали третьего вида: " + rez[2] + ">=" + textBox_x3.Text + ";" + Environment.NewLine;
            textBox.Text += Environment.NewLine;

            textBox.Text += "Расход материала" + Environment.NewLine;
            expr = textBox_limit_x1.Text + "*" + rez[0] + "+" + textBox_limit_x2.Text + "*" + rez[1] + "+" + textBox_limit_x3.Text + "*" + rez[2];
            rez_expr = Convert.ToDouble(textBox_limit_x1.Text) * rez[0] + Convert.ToDouble(textBox_limit_x2.Text) * rez[1] + Convert.ToDouble(textBox_limit_x3.Text) * rez[2];
            textBox.Text += expr + "=" + rez_expr + "<=" + textBox_limit_result.Text + ";" + Environment.NewLine;
            textBox.Text += Environment.NewLine;

            if (variant == "c" && proc_Simpex != null)
            {
                textBox.Text += "Теневая цена материала: " + proc_Simpex.Last()[4, 7] + "; ";
            }
        }

        private void DGV_Fill()
        {
            host.Child = null;
            stackPanel_Simplex.Children.Clear();

            double[,] matr = proc_Simpex[flag];

            label_Title.Content = "Итерация " + (flag + 1) + " из " + proc_Simpex.Count;
            dataGridView.ColumnCount = matr.GetLength(1);
            dataGridView.RowCount = matr.GetLength(0);

            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                for (int j = 0; j < dataGridView.ColumnCount; j++)
                {
                    dataGridView.Rows[i].Cells[j].Value = matr[i, j];
                }
            }

            host.Child = dataGridView;
            stackPanel_Simplex.Children.Add(host);
        }

        private void button_Next_Click(object sender, RoutedEventArgs e)
        {
            if (flag < proc_Simpex.Count - 1)
            {
                flag++;
                DGV_Fill();
            }
        }

        private void button_Prev_Click(object sender, RoutedEventArgs e)
        {
            if (flag > 0)
            {
                flag--;
                DGV_Fill();
            }
        }

        private void button_Generic_Click(object sender, RoutedEventArgs e)
        {
            var matrix = new double[,]
                {
                    {-Convert.ToDouble(textBox_x1.Text), -1, 0, 0},
                    {-Convert.ToDouble(textBox_x2.Text), 0, -1, 0,},
                    {-Convert.ToDouble(textBox_x3.Text), 0, 0, -1},
                    { Convert.ToDouble(textBox_limit_result.Text), Convert.ToDouble(textBox_limit_x1.Text), Convert.ToDouble(textBox_limit_x2.Text), Convert.ToDouble(textBox_limit_x3.Text)  },
                    { 0, -Convert.ToDouble(textBox_func_x1.Text), -Convert.ToDouble(textBox_func_x2.Text), -Convert.ToDouble(textBox_func_x3.Text)}
                };

            string[] str = new string[]
                {
                    rsa.Encrypt(_publicKey_right,textBox_count_of_generations.Text),
                    rsa.Encrypt(_publicKey_right,textBox_populationLength.Text),
                    rsa.Encrypt(_publicKey_right,JsonConvert.SerializeObject(matrix)),
                    rsa.Encrypt(_publicKey_right,"1300"),
                    rsa.Encrypt(_publicKey_right,"2700"),
                    rsa.Encrypt(_publicKey_right,"50"),
                    rsa.Encrypt(_publicKey_right,"200"),
                    rsa.Encrypt(_publicKey_right,"0,7"),

                };

            double[] rez = JsonConvert.DeserializeObject<double[]>(rsa.Decrypt(client.Count_Generic(str, out string[] proc_Generic_cyph)));

            proc_Generic = new double[proc_Generic_cyph.Length];
            for (int i = 0; i < proc_Generic_cyph.Length; i++)
            {
                proc_Generic[i] = Convert.ToDouble(rsa.Decrypt(proc_Generic_cyph[i]));
            }


            generic_rez = Convert.ToDouble(textBox_func_x1.Text) * rez[0] + Convert.ToDouble(textBox_func_x2.Text) * rez[1] + Convert.ToDouble(textBox_func_x3.Text) * rez[2];

            textBox_Generic.Visibility = Visibility.Visible;
            var q = variant;
            variant = "0";

            InfoFill(rez, textBox_Generic);

            variant = q;

            if (simplex_rez != 0)
            {
                textBox_Generic.Text += "Погрешность относительно симплекс метода: " + (Math.Round(1 - generic_rez / simplex_rez, 3)).ToString() + "%" + Environment.NewLine; 
            }

        }

        private void button_Settings_Click(object sender, RoutedEventArgs e)
        {
            if (!changed)
            {
                Window2 window2 = new Window2();
                if (window2.ShowDialog() == true)
                {
                    variant = window2.variant;
                    double[] coefs = window2.coefs;

                    if (variant == "a")
                    {
                        double x1 = Convert.ToDouble(textBox_x1.Text);
                        double x2 = Convert.ToDouble(textBox_x2.Text);
                        double x3 = Convert.ToDouble(textBox_x3.Text);

                        if (coefs[0] == 10)
                        {
                            textBox_x1.Text = (x1 + x1 * 0.1).ToString();
                        }
                        else if (coefs[0] == -10)
                        {
                            textBox_x1.Text = (x1 - x1 * 0.1).ToString();
                        }

                        if (coefs[1] == 10)
                        {
                            textBox_x2.Text = (x2 + x2 * 0.1).ToString();
                        }
                        else if (coefs[1] == -10)
                        {
                            textBox_x2.Text = (x2 - x2 * 0.1).ToString();
                        }

                        if (coefs[2] == 10)
                        {
                            textBox_x3.Text = (x3 + x3 * 0.1).ToString();
                        }
                        else if (coefs[2] == -10)
                        {
                            textBox_x3.Text = (x3 - x3 * 0.1).ToString();
                        }
                    }
                    else if (variant == "b")
                    {
                        double x1 = Convert.ToDouble(textBox_func_x1.Text);
                        double x2 = Convert.ToDouble(textBox_func_x2.Text);
                        double x3 = Convert.ToDouble(textBox_func_x3.Text);

                        if (coefs[0] == 10)
                        {
                            textBox_func_x1.Text = (x1 + x1 * 0.1).ToString();
                        }
                        else if (coefs[0] == -10)
                        {
                            textBox_func_x1.Text = (x1 - x1 * 0.1).ToString();
                        }

                        if (coefs[1] == 10)
                        {
                            textBox_func_x2.Text = (x2 + x2 * 0.1).ToString();
                        }
                        else if (coefs[1] == -10)
                        {
                            textBox_func_x2.Text = (x2 - x2 * 0.1).ToString();
                        }

                        if (coefs[2] == 10)
                        {
                            textBox_func_x3.Text = (x3 + x3 * 0.1).ToString();
                        }
                        else if (coefs[2] == -10)
                        {
                            textBox_func_x3.Text = (x3 - x3 * 0.1).ToString();
                        }
                    }
                    else if (variant == "c")
                    {
                        double x1 = Convert.ToDouble(textBox_limit_x1.Text);
                        double x2 = Convert.ToDouble(textBox_limit_x2.Text);
                        double x3 = Convert.ToDouble(textBox_limit_x3.Text);

                        textBox_limit_x1.Text = (x1 + x1 * 0.1).ToString();
                        textBox_limit_x2.Text = (x2 + x2 * 0.1).ToString();
                        textBox_limit_x3.Text = (x3 + x3 * 0.1).ToString();
                    }

                }

                changed = true;
            }
            
        }

        private void button_Chart_Click(object sender, RoutedEventArgs e)
        {
            ChartForm chartForm = new ChartForm(proc_Generic);
            chartForm.Show();
        }

        private void button_Default_Click(object sender, RoutedEventArgs e)
        {
            textBox_x1.Text = "2000";
            textBox_x2.Text = "1800";
            textBox_x3.Text = "1500";
            textBox_limit_result.Text = "61000";
            textBox_limit_x1.Text = "8";
            textBox_limit_x2.Text = "10";
            textBox_limit_x3.Text = "11";
            textBox_func_x1.Text = "7";
            textBox_func_x2.Text = "10";
            textBox_func_x3.Text = "9";

            changed = false;
        }
    }
}
