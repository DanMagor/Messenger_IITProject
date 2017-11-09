using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading;
using Newtonsoft.Json;

namespace Chat
{
    public partial class Form2 : Form
    {
        private Connection connection;
        private string login;
        private string uid = "user1";
        string prefix = "http://localhost:4201/";

        private Thread listener;

        /*
         * 1-Repetition 3
         * 2-Repetition 5
         * 3-Hamming
         * 4-LDPC
         */
        private int encoding;
        /*
        * 1-Huffman
        * 2-RLE
        * 3-LZ-78
        */

        private int compression;

        public Form2(Connection con, string login)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.connection = con;
            this.login = login;
            this.Header.Text = "Chat-" + login;
            this.encoding = 1;
            this.compression = 1;
            listener = new Thread(HttpListener);
            listener.IsBackground = true;
            listener.Start();
            
           

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.messageViewer.ScrollToCaret();
            this.messageViewer.GotFocus += (s, ev) =>
            {
                this.Header.Focus();

            };

           
        }

        public void printMessage(string login, string message)
        {
            this.messageViewer.AppendText(login + ": " + message);
            this.messageViewer.AppendText("\n");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 settings = new Form3(this.setSettings, this.encoding, this.compression);
            settings.Show();
        }

        private int setSettings(int encode, int compression)
        {
            this.encoding = encode;
            this.compression = compression;
            MessageBox.Show("Encode" + encode);
            MessageBox.Show("Compression" + compression);
            return 1;
        }

        private void sendMessage_Click(object sender, EventArgs e)
        {
            string message = this.messageBox.Text;
            string constMessage = message;
            string mimeType = "msg";
            byte[] byteArray = Encoding.UTF8.GetBytes(message);

            message = Convert.ToBase64String(byteArray);

            if (message.Length != 0)
            {
                string data = "{\"uid\":\"" + this.uid + "\",\"login\":\"" + 
                    this.login + "\",\"data\":\"" + message + "\",\"offset\":\"0\"}";
                if (connection.POST(mimeType, data) == "done")
                {
                    printMessage(login, constMessage);
                    this.messageBox.Text = "";
                }

            }
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            openFile.InitialDirectory = "c:\\";
            openFile.Filter = "Text files(*.txt; *.rtf)| *.txt; *.rtf; |Audio files(*.wav, *.aiff)| *.wav; *.aiff; |Image files(*.tif; *.bmp; *.gif )| *.tif; *.bmp; *.gif;| *.All files (*.*)|*.*;";
            string fileDir = "";
            string fileExt = "";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(openFile.FileName);
                fileDir = openFile.FileName;
                string[] splitted = fileDir.Split('.');
                fileExt = splitted[splitted.Length - 1];
            }
            if (fileDir.Length != 0)
            {
                
                byte[] byteArr = File.ReadAllBytes(fileDir);
                string b64String = Convert.ToBase64String(byteArr);
                string[] splitted = fileDir.Split('\\');
                string fileName = splitted[splitted.Length - 1];
                string data = "{\"uid\":\"" + this.uid + "\",\"login\":\"" + 
                    this.login + "\",\"data\":\"" + b64String + "\",\"Fname\":\"" + fileName + "\",\"offset\":\"0\"}";

                if (connection.POST(fileExt, data) == "done")
                {
                    MessageBox.Show("File successfully sent");
                    
                    printMessage(login, "file:///" + fileDir.Replace(' ', (char)160));
                    //ssylka na otkrytie
                }

            }

           
            // string s=openFile.ShowDialog().ToString;
            // MessageBox.Show(s);

        }
        private void Link_Clicked(object sender, LinkClickedEventArgs e)
        {
            string linkText = e.LinkText.Replace((char)160, ' ');

            // For some reason rich text boxes strip off the 
            // trailing ')' character for URL's which end in a 
            // ')' character, so if we had a '(' opening bracket
            // but no ')' closing bracket, we'll assume there was
            // meant to be one at the end and add it back on. This 
            // problem is commonly encountered with wikipedia links!

            if ((linkText.IndexOf('(') > -1) && (linkText.IndexOf(')') == -1))
                linkText += ")";

            System.Diagnostics.Process.Start(e.LinkText);
        }

        private void HttpListener() {
            

            HttpListener listener = new HttpListener();

            listener.Prefixes.Add(prefix);
            listener.Start();
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

        private void Proccess(HttpListenerContext context)
        {
            string filename = context.Request.Url.Query;
            

            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;
            

            string text; // Recieved JSON Object
            using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
            {
                text = reader.ReadToEnd();
            }

            var data = JsonConvert.DeserializeObject<MyData>(text); //Deserealizing JSON

            if (request.ContentType == "text/message") // Printing message
            {
                printMessage(data.Login, Encoding.UTF8.GetString(Convert.FromBase64String(data.Data)));
            }
            else
            {
                if (!Directory.Exists(data.UID))
                    Directory.CreateDirectory(data.UID);
                BinaryWriter writer = new BinaryWriter(File.Open(data.UID+"\\"+data.Fname,FileMode.Create));
               
                writer.Write(Convert.FromBase64String(data.Data));
                writer.Close();
                printMessage("System", "You received a new file from " + data.UID);

            }

            string responseString = "done";
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;

            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);

            output.Close();
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

    }
}

