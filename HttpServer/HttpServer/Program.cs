using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using System.Threading;

namespace HttpServer
{


    class Server
    {

        public static void HttpListener()
        {
            string prefix = "http://localhost:4200/";

            HttpListener listener = new HttpListener();

            listener.Prefixes.Add(prefix);

            
            
            listener.Start();
            Console.WriteLine("Listening...");
            Console.WriteLine(DateTime.Now);


            while (true)
            {
                try
                {
                    HttpListenerContext context = listener.GetContext();
                    Proccess(context);
                }
                catch (Exception e)
                {

                }
            }


            void Proccess(HttpListenerContext context)
            {
                string filename = context.Request.Url.Query;
                Console.WriteLine(filename);

                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;
                

                string text;
                using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                {
                    text = reader.ReadToEnd();
                }

                var data = JsonConvert.DeserializeObject<MyData>(text);

                Console.WriteLine("\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\");
                Console.WriteLine(DateTime.Now);
                Console.WriteLine(data.Fname);
                Console.WriteLine("Content MimeType: " + request.ContentType);
                Console.WriteLine("Login: " + data.login);
                Console.WriteLine("Offset: " + data.offset);
                Console.WriteLine("\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\");


                string responseString = "done";
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;

                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);

                output.Close();
            }
                      
            listener.Stop();
        }


        public class MyData
        {
            public string data
            {
                get;
                set;
            }

            public string login
            {
                get;
                set;
            }

            public string offset
            {
                get;
                set;
            }

            public string Fname
            {
                get;
                set;
            }
        }


        public static void Main()
        {
            HttpListener();
        }

    }
}
