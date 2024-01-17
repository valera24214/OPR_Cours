using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;

namespace Test2
{
    // ПРИМЕЧАНИЕ. Можно использовать команду "Переименовать" в меню "Рефакторинг", чтобы изменить имя интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string RSA_Connect(string param);

        [OperationContract]
        string Verification(string email);

        [OperationContract]
        bool Register(string login, string password);

        [OperationContract]
        int Authorization(string login, string password);

        [OperationContract]
        string Count_Simplex(string[] str, out List<string> Proc);

        [OperationContract]
        string Count_Generic(string[] parameters, out List<string> Proc);
    }
}
