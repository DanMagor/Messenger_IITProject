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
    public partial class Form2 : Form
    {
      private   Connection connection;
       private string login;
        public Form2(Connection con, string login)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.connection = con;
            this.login = login;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

      
    }
}
