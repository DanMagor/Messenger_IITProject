using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace Chat
{
   static class Encodings
    {
        private static byte[] BitArrayToByteArray(BitArray bits)
        {
            byte[] ret = new byte[(bits.Length - 1) / 8 + 1];
            bits.CopyTo(ret, 0);
            return ret;
        }

        //start repetition coding
        public static byte[] RepetitionCode(byte[] file, int N)
        {
            BitArray source = new BitArray(file);
            BitArray result = new BitArray(source.Length * N);
            for (int i = 0; i < source.Length; i++)
                for (int j = 0; j < N; j++)
                {
                    result[i * N + j] = source.Get(i);
                }
            return BitArrayToByteArray(result);
        }

        public static byte[] RepetitionDECode(byte[] file, int N)
        {
            BitArray source = new BitArray(file);
            BitArray result = new BitArray(source.Length / N);
            for (int i = 0; i < result.Length; i++)
            {
                int b0 = 0, b1 = 0;
                for (int j = 0; j < N; j++)
                {
                    if (source.Get(i * N + j)) b1++;
                    else b0++;
                }
                result[i] = b1 > b0;
            }

            return BitArrayToByteArray(result);
        }

        static byte BitArrayToByteReversed(BitArray bits)
        {
            for (int i = 0; i < bits.Length; i++)
                bits[i] = !bits[i];
            byte[] bytes = new byte[1];
            bits.CopyTo(bytes, 0);
            return bytes[0];
        }

        static byte BitArrayToByte(BitArray bits)
        {
            byte[] bytes = new byte[1];
            bits.CopyTo(bytes, 0);
            return bytes[0];
        }

       public static byte[] hammingEncode(byte[] bin)
        {
            byte[] encodedArr = new byte[bin.Length * 2];
            int encodeCounter = 0;
            bool sum = false;
            BitArray tmp;
            BitArray result = new BitArray(8);
            bool[,] encodeMatrix = new bool[,] { { false,true,true,true},{ true,false,true,true},{ true,true,false,true},{ true,false,false,false},
                {false,true,false,false },{ false,false,true,false},{ false,false,false,true} };
            for (int i = 0; i < bin.Length; i++)
            {
                tmp = new BitArray(new byte[] { bin[i] });
                for (int j = 0; j < tmp.Length; j++) tmp[j] = !tmp[j];
                result[7] = false;
                for (int j = 0; j < 7; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        sum ^= tmp[k] && encodeMatrix[j, k];
                    }
                    result[j] = sum;
                    sum = false;
                }
                encodedArr[encodeCounter++] = BitArrayToByte(result);
                result[7] = false;
                for (int j = 0; j < 7; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        sum ^= tmp[k + 4] && encodeMatrix[j, k];
                    }
                    result[j] = sum;
                    sum = false;
                }
                encodedArr[encodeCounter++] = BitArrayToByte(result);
            }
            return encodedArr;
        }

        public static byte[] hammingDecode(byte[] bin)
        {
            byte[] decodedArr = new byte[bin.Length / 2];
            int decodeCounter = 0;
            int count = 0;
            bool sum = false;
            int[] col = new int[3];
            BitArray tmp;
            BitArray result = new BitArray(8);
            bool[,] decodeMatrix = new bool[,] { { true, false, false, false, true, true, true }, { false, true, false, true, false, true, true },
                { false, false, true, true, true, false, true } };
            for (int i = 0; i < bin.Length; i++)
            {
                tmp = new BitArray(new byte[] { bin[i] });
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 7; k++)
                    {
                        sum ^= tmp[k] && decodeMatrix[j, k];
                    }
                    col[j] = Convert.ToInt32(sum);
                    sum = false;
                }
                if (col[0] == 0 && col[1] == 0 && col[2] == 0)
                {
                    if (count == 0)
                    {
                        for (int j = 3; j < 7; j++) result[j - 3] = tmp[j];
                        count++;
                    }
                    else
                    {
                        for (int j = 3; j < 7; j++) result[j + 1] = tmp[j];
                        decodedArr[decodeCounter++] = BitArrayToByteReversed(result);
                        count = 0;
                    }
                    continue;
                }
                for (int j = 0; j < 7; j++)
                {
                    if (Convert.ToInt32(decodeMatrix[0, j]) == col[0] && Convert.ToInt32(decodeMatrix[1, j]) == col[1]
                        && Convert.ToInt32(decodeMatrix[2, j]) == col[2])
                    {
                        switch (tmp[j])
                        {
                            case false:
                                tmp[j] = true;
                                break;
                            case true:
                                tmp[j] = false;
                                break;
                        }
                        if (count == 0)
                        {
                            for (int k = 3; k < 7; k++) result[k - 3] = tmp[k];
                            count++;
                        }
                        else
                        {
                            for (int k = 3; k < 7; k++) result[k + 1] = tmp[k];
                            decodedArr[decodeCounter++] = BitArrayToByteReversed(result);
                            count = 0;
                        }
                        break;
                    }
                }
            }
            return decodedArr;
        }



        public static byte[] ConvolutionalСode(byte[] byteArray)
        {
            BitArray bitArray = new BitArray(byteArray);
        
            BitArray codingBitArray = new BitArray(bitArray.Length * 2 + 8);
            bool r1 = false, r2 = false;
            for (int i = 0; i < bitArray.Length; i++)
            {
                codingBitArray[2 * i] = r2 ^ bitArray[i];
                codingBitArray[2 * i + 1] = (r1 ^ bitArray[i]) ^ r2;
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
                    return BitArrayToByteArray(codingBitArray);
        }

        public static byte[] DecodingConvolutionalCode(byte[] byteArray)
        {
            BitArray bitArray = new BitArray(byteArray);
            BitArray decodingBitArray = new BitArray((bitArray.Length - 8) / 2);
            BitArray verifyingBitArray = new BitArray((bitArray.Length - 8) / 2 + 2);
            BitArray computedBitArray = new BitArray((bitArray.Length - 8) / 2 + 2);
            //PrintBitArray(bitArray);


            for (int i = 0; i < decodingBitArray.Length; i++)
            {
                decodingBitArray[i] = bitArray[2 * (i + 1)] ^ bitArray[2 * (i + 1) + 1];
            }

            bool d1 = bitArray[2 * (decodingBitArray.Length + 1)] ^ bitArray[2 * (decodingBitArray.Length + 1) + 1];
            bool d2 = bitArray[2 * (decodingBitArray.Length + 1 + 1)] ^ bitArray[2 * (decodingBitArray.Length + 1 + 1) + 1];
            //PrintBitArray(decodingBitArray);

            for (int i = 0; i < verifyingBitArray.Length; i++)
            {
                verifyingBitArray[i] = bitArray[2 * i];
            }
            //PrintBitArray(verifyingBitArray);

            bool r1 = false, r2 = false;
            for (int i = 0; i < decodingBitArray.Length; i++)
            {
                computedBitArray[i] = decodingBitArray[i] ^ r2;
                r2 = r1;
                r1 = decodingBitArray[i];

            }
            computedBitArray[computedBitArray.Length - 2] = d1 ^ r2;
            computedBitArray[computedBitArray.Length - 1] = d2 ^ r1;
          //  PrintBitArray(computedBitArray);

            for (int i = 0; i < decodingBitArray.Length; i++)
            {
                if ((verifyingBitArray[i] != computedBitArray[i]) && (verifyingBitArray[i + 2] != computedBitArray[i + 2]))
                {
                    computedBitArray[i] = computedBitArray[i] ^ true;
                    computedBitArray[i + 2] = computedBitArray[i + 2] ^ true;
                    decodingBitArray[i] = decodingBitArray[i] ^ true;
                }
            }
            return BitArrayToByteArray(decodingBitArray);
        }


    }
}
