using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BmstuCSharpBot
{
    [ServiceContract]
    public interface IControlService
    {
        /// <summary>
        /// Запрос состояния бота
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string Query();

        /// <summary>
        /// Начало трассировки 
        /// </summary>
        /// <param name="ip">Адрес приемника сообщений</param>
        [OperationContract]
        void StartTrace(string ip);

        /// <summary>
        /// Окончание трассировки
        /// </summary>
        [OperationContract]
        void StopTrace();
    }
}
