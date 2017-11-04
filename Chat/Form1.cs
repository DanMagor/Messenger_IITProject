using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Chat
{

    public partial class Form1 : Form
    {
        private Connection connection=null;
        private string login;

        public Form1()        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.connection = new Connection();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           this.login = this.loginBox.Text;
            startChat(connection, this.login);
           
        /*    if (login.Length != 0)
            {
                MessageBox.Show(this.loginBox.Text);
                if (connection.sendLogin(login))
                {
                    startChat(this.connection, login);
                }
                else {
                    MessageBox.Show("Login is invalid!");
                }
            }
            else {
                MessageBox.Show("Please input login");
            }*/
        }

        private void startChat(Connection connection,string login) {
            Form2 form = new Form2(connection, login);
            form.Show();
            this.Hide();
            form.Closed += (s, args) => this.Close();
            form.Show();
        }
    }





    

}
