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

                
                //File.WriteAllBytes(@"C:\Users\Herman\Desktop\temprep\" + data.Fname,
                //    Convert.FromBase64String(AddNoise(Convert.FromBase64String(data.data))));

                string responseString = "done";
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;

                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);

                output.Close();
            }
                      
            listener.Stop();
        }

        /*
         * 
         * Class representation of JSON object 
         * 
         */
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


        public static void Main()
        {
            HttpListener();
        }

    }
}
