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
    public partial class Form3 : Form
    {
        Func<int, int, int> changeParent;

        public Form3(Func<int,int,int> func,int encoding,int compression)
        {
            InitializeComponent();
            this.changeParent = func;

            switch (encoding)
            {
                case 1:
                    this.repetition3.Checked = true ;
                    break;

                case 2:
                    this.repetition5.Checked = true;
                    break;

                case 3:
                    this.hamming.Checked = true;
                    break;

                case 4:
                    this.convolution.Checked = true;
                    break;
                default:
                   
                    break;
            }

            switch (compression)
            {
                case 1:
                    this.huffman.Checked = true;
                    break;

                case 2:
                    this.rle.Checked = true;
                    break;

                case 3:
                    this.lz78.Checked = true;
                    break;
                    
                default:

                    break;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int encoding=1;
            int compression=1;
            if (repetition3.Checked) {
                encoding = 1;
            }
            if (repetition5.Checked)
            {
                encoding = 2;
            }
            if (hamming.Checked)
            {
                encoding = 3;
            }
            if (convolution.Checked)
            {
                encoding = 4;
            }
            if (huffman.Checked)
            {
                compression = 1;
            }
            if (rle.Checked)
            {
                compression = 2;
            }
            if (lz78.Checked) {
                compression = 3;
            }
            changeParent(encoding,compression);
            this.Close();

        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
