using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
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

        static int port = 8005;
        static string address = "127.0.0.1";
        Socket socket;
        IPEndPoint ipPoint;
        public Connection()
        {
            this.ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipPoint);
        }

        public bool sendLogin(string login)
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
        }

      

    }
}
