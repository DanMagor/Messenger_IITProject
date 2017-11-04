using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Chat
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

   public class Connection
    {

    
        static string url = "127.0.0.1";
         
        

        public Connection()
        {
           
            
        }
        public string POST( string Data)
        {
            WebRequest req = WebRequest.Create(url);
            req.Method = "POST";
            req.Timeout = 100000;
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] sentData = Encoding.GetEncoding(1251).GetBytes(Data);
            req.ContentLength = sentData.Length;
            Stream sendStream = req.GetRequestStream();
            sendStream.Write(sentData, 0, sentData.Length);
            sendStream.Close();
            WebResponse res = req.GetResponse();
            Stream ReceiveStream = res.GetResponseStream();
            StreamReader sr = new StreamReader(ReceiveStream, Encoding.UTF8);
            //Кодировка указывается в зависимости от кодировки ответа сервера
            Char[] read = new Char[256];
            int count = sr.Read(read, 0, 256);
            string Out = String.Empty;
            while (count > 0)
  {
                String str = new String(read, 0, count);
                Out += str;
                count = sr.Read(read, 0, 256);
            }
            return Out;
        }
        /*   public bool sendLogin(string login)
           {

               byte[] data = Encoding.Unicode.GetBytes(login);
               socket.Send(data);

               data = new byte[256]; // буфер для ответа
               StringBuilder builder = new StringBuilder();
               int bytes = 0; // количество полученных байт

               do
               {
                   bytes = socket.Receive(data, data.Length, 0);
                   builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
               }
               while (socket.Available > 0);
               string callback = builder.ToString();
               if (callback == "logged")
               {
                   return true;
               }
               else
               {
                   return false;
               }
               // return 0;
           }*/



    }
}
