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

namespace Chat
{
    public partial class Form2 : Form
    {
        private Connection connection;
        private string login;

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

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.messageViewer.ScrollToCaret();
            this.messageViewer.GotFocus += (s, ev) =>
            {
                this.Header.Focus();

            };
            printMessage(login, " я пидор");
            printMessage("Я залюпа", "sosi");
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
            if (message.Length != 0)
            {
                printMessage(login, message);

                this.messageBox.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            openFile.InitialDirectory = "c:\\";
            openFile.Filter = "Text files(*.txt; *.rtf)| *.txt; *.rtf |Audio files(*.wav, *.aiff)| *.wav; *.aiff |Image files(*.tiff; *.bmp; *.gif )| *.tiff; *.bmp; *.gif| *.All files (*.*)|*.*";
            string fileDir = "";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(openFile.FileName);
                fileDir = openFile.FileName;
            }
            if (fileDir.Length != 0)
            {
                printMessage(login, "aasadasafa");
                byte[] byteArr = File.ReadAllBytes(fileDir);
                string pop = "";
                foreach (byte s in byteArr) {
                    pop += s.ToString();
                    pop += " ";
                }
                MessageBox.Show(pop);
            }
            // string s=openFile.ShowDialog().ToString;
            // MessageBox.Show(s);

        }
    }
}
