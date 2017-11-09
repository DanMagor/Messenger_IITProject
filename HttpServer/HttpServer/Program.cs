using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using System.Collections;
using System.Threading;

namespace HttpServer
{


    class Server
    {

        private static IDictionary<string, string> _usermap = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
        {
            {"user1", "http://localhost:4202/"},
            {"user2",  "http://localhost:4201/"}
        };


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
                      
            listener.Stop();
        }


        static void Proccess(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;
            Console.WriteLine();

            string text;
            using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
            {
                text = reader.ReadToEnd();
            }

            var data = JsonConvert.DeserializeObject<MyData>(text); // Deserealizing JSON

            Console.WriteLine("\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\");
            Console.WriteLine(DateTime.Now);
            Console.WriteLine(data.Fname);
            Console.WriteLine("Content MimeType: " + request.ContentType);
            Console.WriteLine("Login: " + data.Login);
            Console.WriteLine("UID: " + data.UID);
            Console.WriteLine("Offset: " + data.Offset);
            Console.WriteLine("\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\");


            //File.WriteAllBytes(@"C:\Users\Herman\Desktop\temprep\" + data.Fname,
            //    Convert.FromBase64String(AddNoise(Convert.FromBase64String(data.data))));


            string responseString = "done";
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;

            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);

            output.Close();


            POST(text, _usermap[data.UID], request.ContentType); // Making POST to client
        }

        /*
         * 
         * Class representation of JSON object 
         * 
         */
        public class MyData
        {
            public string Data
            {
                get;
                set;
            }

            public string Login
            {
                get;
                set;
            }

            public string Offset
            {
                get;
                set;
            }

            public string Fname
            {
                get;
                set;
            }

            public string UID
            {
                get;
                set;
            }
        }

        /*
         * 
         * Making noise in data.
         * 
         * noiseKoeff is percentage of noise
         * 
         */
        private static string AddNoise(byte[] data)
        {
            const double noiseKoeff = 0.01;

            var databits = new BitArray(data);
            Random random = new Random();

            for(int i = 0; i < databits.Count; i++)
            {
                if(random.NextDouble() <= noiseKoeff)
                {
                    databits.Set(i, !databits.Get(i)); // Inverting
                }
            }

            byte[] noisyData = ToByteArray(databits);

            return Convert.ToBase64String(noisyData);

        }

        /*
         * 
         * Converts BitArray to ByteArray
         * 
         */
        private static byte[] ToByteArray(BitArray bits)
        {
            int numBytes = bits.Count / 8;
            if (bits.Count % 8 != 0) numBytes++;

            byte[] bytes = new byte[numBytes];
            int byteIndex = 0, bitIndex = 0;

            for (int i = 0; i < bits.Count; i++)
            {
                if (bits[i])
                    bytes[byteIndex] |= (byte)(1 << (7 - bitIndex));

                bitIndex++;
                if (bitIndex == 8)
                {
                    bitIndex = 0;
                    byteIndex++;
                }
            }

            return bytes;
        }



        public static string POST(string data, string url, string MIMEType)
        {
            WebRequest request = WebRequest.Create(url);
            // Set the Method property of the request to POST.
            request.Method = "POST";
            // Create POST data and convert it to a byte array.
            byte[] byteArray = Encoding.UTF8.GetBytes(data);
            // Set the ContentType property of the WebRequest.
            request.ContentType = MIMEType;
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
