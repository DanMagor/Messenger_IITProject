using System;
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
            listener.Stop();
        }


    static void  Proccess(HttpListenerContext context)
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
                POST("msg", text);
                output.Close();
            }


        public static string POST(string mimeType, string postData)
        {
            WebRequest request = WebRequest.Create("http://localhost:4201/");
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


        public static void Main()
        {
            HttpListener();
        }

    }
}
