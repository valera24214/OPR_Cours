using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Data.Entity;
using System.Net.Mail;
using System.Net;

namespace Test2
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде и файле конфигурации.
    public class Service1 : IService1
    {
        private RSAParameters _publicKey_left;
        private readonly RSA rsa;

        private string global_salt;

        public Service1()
        {
            rsa = new RSA();
            global_salt = "#&}✓~¥€";
        }

        public string Generate_local_salt()
        {
            Random rnd = new Random();
            char[] mass = "✓~¥€!#$%&\'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[]^_`abcdefghijklmnopqrstuvwxyz{|}~'".ToCharArray();
            string rez = mass[rnd.Next(mass.Length - 1)].ToString();
            for (int i = 0; i < 6; i++)
            {
                rez += mass[rnd.Next(mass.Length - 1)].ToString();
            }

            return rez;

            return rez;
        }

        string GetHashString(string s, string local_salt)
        {
            s = global_salt + s + local_salt;
            //переводим строку в байт-массим  
            byte[] bytes = Encoding.Unicode.GetBytes(s);

            //создаем объект для получения средст шифрования  
            MD5CryptoServiceProvider CSP =
                new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }

        public string Verification(string email)
        {
            Random rnd = new Random();
            string str = rnd.Next(9).ToString();
            for (int i = 0; i < 5; i++)
            {
                str += rnd.Next(9).ToString();
            }

            //Авторизация на SMTP сервере
            SmtpClient Smtp = new SmtpClient("smtp.mail.ru", 587);
            Smtp.Credentials = new NetworkCredential("valera.astroukh@mail.ru", "CYbXCkCJanpyT2iTmPBF");
            Smtp.EnableSsl = true;

            //Формирование письма
            MailMessage Message = new MailMessage();
            Message.From = new MailAddress("valera.astroukh@mail.ru");
            Message.To.Add(new MailAddress(email));
            Message.Subject = "Регистрация";
            Message.Body = "Здравствуйте. Вы отправили заявку для регистрации на сервисе AstroukhKursach. Ваш код регистрации: " + str;


            Smtp.Send(Message);//отправка

            return rsa.Encrypt(_publicKey_left, str);
        }

        public bool Register(string login, string password)
        {
            login = rsa.Decrypt(login);

            bool not_registered = true;

            List<Users> users;
            using (var m = new Model1())
            {
                users = m.Users.ToList();
                foreach (var u in users)
                {
                    if (u.login == login)
                        not_registered = false;
                }

                if (not_registered)
                {
                    password = rsa.Decrypt(password);
                    string local_salt = Generate_local_salt();
                    string password_hash = GetHashString(password, local_salt);

                    Users user = new Users()
                    {
                        login = login,
                        password = password_hash,
                        local_salt = local_salt
                    };

                    m.Users.Add(user);
                    m.SaveChanges();
                }
            }

            return not_registered;
        }

    public int Authorization(string login, string password)
        {
            List<Users> users;
            using (var m = new Model1())
            {
                users = m.Users.ToList();
            }

            login = rsa.Decrypt(login);
            Users user = users.Find(u => u.login == login);

            if (user == null)
            {
                return -1;
            }
            else
            {
                string local_salt = user.local_salt;
                string db_password = user.password;
                string prog_password = GetHashString(rsa.Decrypt(password), local_salt);
                if (prog_password == db_password)
                {
                    return 1;
                }
                else
                    return 0;
            }
        }



        public string Count_Generic(string[] parameters, out List<string> Proc)
        {
            int count_of_generations = Convert.ToInt32(rsa.Decrypt(parameters[0]));
            int population_length = Convert.ToInt32(rsa.Decrypt(parameters[1]));
            double[,] coefs = JsonConvert.DeserializeObject<double[,]>(rsa.Decrypt(parameters[2]));
            int low_gen = Convert.ToInt32(rsa.Decrypt(parameters[3]));
            int up_gen = Convert.ToInt32(rsa.Decrypt(parameters[4]));
            int low_bound = Convert.ToInt32(rsa.Decrypt(parameters[5]));
            int up_bound = Convert.ToInt32(rsa.Decrypt(parameters[6]));
            double mut_rate = Convert.ToDouble(rsa.Decrypt(parameters[7]));

            Generic generic = new Generic(count_of_generations, population_length, up_gen, low_gen, up_bound, low_bound, mut_rate, coefs);

            double[] rez = generic.Count();

            double[] fit = generic.Return_fit();

            Proc = new List<string>();
            foreach (double f in fit)
            {
                Proc.Add(rsa.Encrypt(_publicKey_left, f.ToString()));
            }

            return rsa.Encrypt(_publicKey_left, JsonConvert.SerializeObject(rez));
        }


        public string Count_Simplex(string[] str, out List<string> Proc)
        {
            int flag = 0;
            double[,] matr = new double[5,8];
            for (int i = 0; i < matr.GetLength(0); i++)
            {
                for (int j = 0; j < matr.GetLength(1); j++)
                {
                    matr[i, j] = Convert.ToDouble(rsa.Decrypt(str[flag]));
                    flag++;
                }
            }
            double[] result = new double[3];

            Simplex S = new Simplex(matr);
            S.Calculate(result);

            Proc = new List<string>();
            List<double[,]> proc = S.Return_process();

            foreach (var p in proc)
            {
                Proc.Add(RSA_SendMessage(JsonConvert.SerializeObject(p)));
            }

            return RSA_SendMessage(JsonConvert.SerializeObject(result));
        }

        public string RSA_Connect(string param)
        {
            _publicKey_left = JsonConvert.DeserializeObject<RSAParameters>(param);
            return rsa.GetPublicKey();
        }

        private string RSA_GetMessage(string mess)
        {
            return rsa.Decrypt(mess);
        }

        private string RSA_SendMessage(string mess)
        {
            return rsa.Encrypt(_publicKey_left, mess);
        }

    }
}
