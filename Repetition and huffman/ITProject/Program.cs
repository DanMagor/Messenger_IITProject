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
        public static byte[] BitArrayToByteArray(BitArray bits)
        {
            byte[] ret = new byte[(bits.Length - 1) / 8 + 1];
            bits.CopyTo(ret, 0);
            return ret;
        }

        //start repetition coding
        static BitArray repetitionCode(BitArray sourse,int N)
        {
            BitArray result = new BitArray(sourse.Length*N);
            for(int i = 0; i < sourse.Length; i++)
                for(int j = 0; j < N; j++)
                {
                    result[i * N + j] = sourse.Get(i);
                }
            return result;
        }

        static BitArray repetitionDECode(BitArray sourse, int N)
        {
            BitArray result = new BitArray(sourse.Length / N);
            for (int i = 0; i < result.Length; i++)
            {
                int b0 = 0, b1 = 0;
                for (int j = 0; j < N; j++)
                {
                    if (sourse.Get(i * N + j)) b1++;
                    else b0++;
                }
                result[i] = b1 > b0;
            }
            return result;
        }
        //end repetition coding
        
        static void Main(string[] args)
        {

            string extention = "." + Console.ReadLine();
            byte[] file = File.ReadAllBytes("in"+extention);
            BitArray source = new BitArray(file); 

            /*foreach(bool b in source)
                Console.Write(Convert.ToByte(b));//*/
            source = repetitionCode(source,3);
            Console.WriteLine('\n');
            /*foreach (bool b in source)
                Console.Write(Convert.ToByte(b));//*/
            source = repetitionDECode(source, 3);
            Console.WriteLine('\n');
            /*foreach (bool b in source)
                Console.Write(Convert.ToByte(b));//*/
          
            BinaryWriter bw = new BinaryWriter(File.OpenWrite("out" + extention));
            bw.Write(BitArrayToByteArray(source));            
            
            bw.Close();
            //Console.ReadLine();
        }
    }
}
