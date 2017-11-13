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

            string extention = "." + Console.ReadLine();
            byte[] file = File.ReadAllBytes("in"+extention);
            BitArray source = new BitArray(file);

            foreach(bool b in source)
                Console.Write(Convert.ToByte(b));//*/
            //source = RepetitionCoding.RepetitionCode(source,3);
            /*Console.WriteLine('\n');
            /foreach (bool b in source)
                Console.Write(Convert.ToByte(b));//*/
            //source = RepetitionCoding.repetitionDECode(source, 3);
            /*Console.WriteLine('\n');
            foreach (bool b in source)
                Console.Write(Convert.ToByte(b));//*/

            
            HuffmanCompration.Compration(file);



            //BinaryWriter bw = new BinaryWriter(File.OpenWrite("out" + extention));
            //bw.Write(RepetitionCoding.BitArrayToByteArray(source));            
            
            //bw.Close();
            Console.ReadLine();
        }
    }
}
