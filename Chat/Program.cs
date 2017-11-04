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

    
        static string url = "http://localhost:4200/";

        private static IDictionary<string, string> _mappings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
        {
            {"txt",  "text/plain"},
            {"aiff", "audio/aiff"},
            {"bmp",  "image/bmp"},
            {"gif", "image/gif"},
            {"rtf", "application/rtf"},
            {"tif", "image/tif"},
            {"wav", "audio/wav"},
            {"msg", "text/message"}
        };

        public Connection()
        {
        }

        public string POST(string mimeType, string postData)
        {
            WebRequest request = WebRequest.Create(url);
            // Set the Method property of the request to POST.
            request.Method = "POST";
            // Create POST data and convert it to a byte array.
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.
            request.ContentType = _mappings[mimeType];
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.

            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
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
