using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace It_project
{
    class Program
    {

        static byte[] ConvolutionalСode(byte[] byteArray)
        {
            BitArray bitArray = new BitArray(byteArray);
          PrintBitArray(bitArray);
            BitArray codingBitArray = new BitArray(bitArray.Length*2+8);
            bool r1 = false, r2 = false;
            for (int i = 0; i < bitArray.Length; i++)
            {
                codingBitArray[2 * i] = r2 ^ bitArray[i];
                codingBitArray[2 * i+1] = (r1 ^ bitArray[i])^r2;
                r2 = r1;
                r1 = bitArray[i];
            }
            for (int i = 4; i > 0; i--)
            {
                codingBitArray[codingBitArray.Length - i * 2] = r2 ^ false;
                codingBitArray[codingBitArray.Length - i * 2 + 1] = (r1 ^ false) ^ r2;
                r2 = r1;
                r1 = false;
            }
            PrintBitArray(codingBitArray);
            return BitArrayToByteArray(codingBitArray);            
        }

        static byte[] DecodingConvolutionalCode(byte[] byteArray)
        {
            BitArray bitArray = new BitArray(byteArray);
            BitArray decodingBitArray = new BitArray((bitArray.Length-8)/ 2);
            BitArray verifyingBitArray = new BitArray((bitArray.Length-8) / 2 + 2);
            BitArray computedBitArray = new BitArray((bitArray.Length-8) / 2 + 2);
            PrintBitArray(bitArray);
            

            for (int i = 0; i < decodingBitArray.Length; i++)
            {
                decodingBitArray[i] = bitArray[2 * (i + 1)] ^ bitArray[2 * (i + 1) + 1];
            }
            
            bool d1= bitArray[2 * (decodingBitArray.Length + 1)] ^ bitArray[2 * (decodingBitArray.Length  + 1) + 1];
            bool d2 = bitArray[2 * (decodingBitArray.Length + 1 + 1)] ^ bitArray[2 * (decodingBitArray.Length + 1 + 1) + 1];
            PrintBitArray(decodingBitArray);

            for (int i = 0; i < verifyingBitArray.Length; i++)
            {
                verifyingBitArray[i] = bitArray[2 * i];
            }
            PrintBitArray(verifyingBitArray);

            bool r1 = false, r2 = false;
            for (int i = 0; i < decodingBitArray.Length; i++)
            {
                computedBitArray[i] = decodingBitArray[i] ^ r2;
                r2 = r1;
                r1 = decodingBitArray[i];

            }
            computedBitArray[computedBitArray.Length - 2] = d1 ^ r2;
            computedBitArray[computedBitArray.Length - 1] = d2 ^ r1;
            PrintBitArray(computedBitArray);

            for (int i = 0; i < decodingBitArray.Length; i++)
            {
                if ((verifyingBitArray[i] != computedBitArray[i]) && (verifyingBitArray[i + 2] != computedBitArray[i + 2]))
                {
                    computedBitArray[i] = computedBitArray[i] ^ true;
                    computedBitArray[i+2] = computedBitArray[i+2] ^ true;
                    decodingBitArray[i] = decodingBitArray[i] ^ true;
                }
            }
            return BitArrayToByteArray(decodingBitArray);
        }

        static void PrintBitArray(BitArray bitArray)
        {
            for (int i = 0; i < bitArray.Length; i++)
            {
                if (bitArray[i] == false)
                {
                    Console.Write("0 ");
                }
                else
                {
                    Console.Write("1 ");
                }
                
            }
            Console.WriteLine();
        }

        public static byte[] BitArrayToByteArray(BitArray bitArray)
        {
            byte[] returnArray = new byte[bitArray.Length/8];
            bitArray.CopyTo(returnArray, 0);
            return returnArray;
        }

        static void Main(string[] args)
        {
            byte[] array = File.ReadAllBytes(@"C:\Users\LENOVO\Downloads\02_Oxxxymiron_-_.aiff");
            BitArray bitArray = new BitArray(array);
            int a00 = 0, a01 = 0, a10 = 0, a11 = 0;
            for (int i = 0; i < bitArray.Length; i = i + 2)
            {
                if (bitArray[i] == false && bitArray[i + 1] == false)
                {
                    a00++;
                }
                else
                {
                    if (bitArray[i] == false && bitArray[i + 1] == true)
                    {
                        a01++;
                    }
                    else
                    {
                        if (bitArray[i] == true && bitArray[i + 1] == false)
                        {
                            a10++;
                        }
                        else
                        {
                            if (bitArray[i] == true && bitArray[i + 1] == true)
                            {
                                a11++;
                            }
                        }
                    }
                }
            }

            Console.WriteLine("00 = " + a00);
            Console.WriteLine("01 = " + a01);
            Console.WriteLine("10 = " + a10);
            Console.WriteLine("11 = " + a11);
            double a = a00 + a11 + a01 + a10;
            Console.WriteLine(a);
            double a0 = a00 / a;
            double a1 = a01 / a;
            double a2 = a10 / a;
            double a3 = a11 / a;
            Console.WriteLine(a0.ToString());
            Console.WriteLine(a1.ToString());
            Console.WriteLine(a2.ToString());
            Console.WriteLine(a3.ToString());

            int w =(int) Math.Ceiling(-Math.Log(a0, (double)2)) + 1;
            Console.WriteLine(w);
             w = (int)Math.Ceiling(-Math.Log(a1, (double)2)) + 1;
            Console.WriteLine(w);
             w = (int)Math.Ceiling(-Math.Log(a2, (double)2)) + 1;

            Console.WriteLine(w);

             w = (int)Math.Ceiling(-Math.Log(a3, (double)2)) + 1;
            Console.WriteLine(w);
            Console.ReadKey();
            

        }


        static byte[] CompressionLZ78(byte[] byteArray)
        {
            BitArray bitArray = new BitArray(byteArray);

            byte[] retByteArray = new byte[0];
            Dictionary<int, ulong[]> dictionaryValue = new Dictionary<int, ulong[]>
            {
                { 0, new ulong[2] { 0, 0 } }
            };
            Dictionary<ulong[], int> dictionaryKey = new Dictionary<ulong[], int>
            {
                {new ulong[2] { 0, 0 } , 0}
            };
            
            
           //// PrintBitArray(bitArray);
           //// Console.WriteLine(dictionary.Count);
           // string str = "";
           // int i = 0;
           // int elmentBefore = 0, elementBefore1=0;

           // char ch='p';
           // while (i < bitArray.Length)
           // {
           //     if (bitArray[i] == true)
           //     {
           //         ch = '1';
           //         str += ch;
           //     }
           //     else
           //     {
           //         ch = '0';
           //         str += ch;
           //     }

           //     if (!dictionaryKey.ContainsKey(str))
           //     {
           //         dictionaryValue.Add(dictionaryValue.Count, str);
           //         dictionaryKey.Add(str, dictionaryKey.Count);
           //         //string c = Convert.ToString(elmentBefore, 2);
           //         //string s="";
           //         //for (int j = 0; j < 15-c.Length; j++)
           //         //{
           //         //    s += "0";
           //         //}
           //         //s += c+ch;
           //         //byte[] newByte = new byte[2];
           //         //newByte[0] = Convert.ToByte(s.Substring(0,8), 2);
           //         //newByte[1] = Convert.ToByte(s.Substring(8, 8), 2);
           //         //retByteArray = CombineByteArray(retByteArray, newByte);
           //         //// Console.WriteLine(s);
           //         str = "";
           //         elementBefore1 = 0;
           //         elmentBefore = 0;
           //     }
           //     else
           //     {
           //         elementBefore1 = elmentBefore;
           //         elmentBefore = dictionaryKey[str];
           //     }
           //     //if (dictionaryValue.Count == 32768)
           //     //{
           //     //    //for (int j = 0; j < dictionary.Count; j++)
           //     //    //{
           //     //    //    Console.WriteLine(j + " " + dictionary[j]);
           //     //    //}
           //     //    dictionaryValue.Clear();
           //     //    dictionaryKey.Clear();
           //     //    dictionaryValue.Add(0, "");
           //     //    dictionaryKey.Add("", 0);
           //     //    elmentBefore = 0;
           //     //    elementBefore1=0;
           //     //    str = "";
                    
           //     //}

           //     i++;
           // }
           // Console.WriteLine(dictionaryValue.Count);
           // //if (str!="")
           // //{
                
           // //    string c = Convert.ToString(elementBefore1, 2);
           // //    string s = "";
           // //    for (int j = 0; j < 15 - c.Length; j++)
           // //    {
           // //        s += "0";
           // //    }
           // //    s += c + ch;
           // //    byte[] newByte = new byte[2];
           // //    newByte[0] = Convert.ToByte(s.Substring(0, 8), 2);
           // //    newByte[1] = Convert.ToByte(s.Substring(8, 8), 2);
           // //    retByteArray = CombineByteArray(retByteArray, newByte);
           // //  //  Console.WriteLine(s);
           // //    str = "";
           // //    elmentBefore = 0;
           // //}
           // //else
           // //{
           // //    Console.WriteLine("( ) ( )");
           // //}
           // //for (int j = 0; j < dictionary.Count; j++)
           // //{
           // //    Console.WriteLine(j + " " + dictionary[j]);
           // //}
            






            return retByteArray;
        }

        public static TKey GetKey<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TValue Value)
        {
            List<TKey> KeyList = new List<TKey>(dictionary.Keys);
            foreach (TKey key in KeyList)
                if (dictionary[key].Equals(Value))
                    return key;
            throw new KeyNotFoundException();
        }

        public static byte[] CombineByteArray(byte[] a1, byte[] a2)
        {
            byte[] ret = new byte[a1.Length + a2.Length];
            Array.Copy(a1, 0, ret, 0, a1.Length);
            Array.Copy(a2, 0, ret, a1.Length, a2.Length);
            
            return ret;
        }









    }
}