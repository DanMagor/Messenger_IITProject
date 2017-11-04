﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
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
                Console.WriteLine(request.ContentType);

                string text;
                using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                {
                    text = reader.ReadToEnd();
                }

                Console.WriteLine(text);

                //string responseString = "<HTML><BODY> Request recieved! </BODY></HTML>";
                string responseString = "done";
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;

                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);

                output.Close();
            }
                      
            listener.Stop();
        }


        public static void Main()
        {
            HttpListener();
        }

    }
}
