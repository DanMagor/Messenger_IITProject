using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITProject
{
    class Program
    {





        static void Main(string[] args)
        {

            string extention = ".pptx";// + Console.ReadLine();
            byte[] file = File.ReadAllBytes("in"+extention);
            //BitArray source = new BitArray(file);

            /*foreach(bool b in source)
                Console.Write(Convert.ToByte(b));//*/

            //file = RepetitionCoding.RepetitionCode(file,3);
            //Console.WriteLine('\n');
            /*foreach (bool b in source)
                Console.Write(Convert.ToByte(b));//*/
            //file = RepetitionCoding.RepetitionDECode(file, 3);
            /*Console.WriteLine('\n');
            foreach (bool b in source)
                Console.Write(Convert.ToByte(b));//*/



            byte[] mes = HuffmanCompration.Compration(file);
            Console.WriteLine(file.Length);
            Console.WriteLine(mes.Length);

            BinaryWriter bw = new BinaryWriter(File.OpenWrite("out" + extention));
            bw.Write(HuffmanCompration.Decompration(mes));            

            bw.Close();
            Console.ReadLine();
        }
    }
}
