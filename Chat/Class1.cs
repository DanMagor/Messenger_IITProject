﻿using System;
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
        static byte[] repetitionCode(byte[] source, int N)
        {
            BitArray temp = new BitArray(source);

            BitArray result = new BitArray(temp.Length * N);
            for (int i = 0; i < temp.Length; i++)
                for (int j = 0; j < N; j++)
                {
                    result[i * N + j] = temp.Get(i);
                }
            return BitArrayToByteArray(result);
        }

        static byte[] repetitionDecode(BitArray source, int N)
        {
            BitArray temp = new BitArray(source);
            BitArray result = new BitArray(temp.Length / N);
            for (int i = 0; i < result.Length; i++)
            {
                int b0 = 0, b1 = 0;
                for (int j = 0; j < N; j++)
                {
                    if (temp.Get(i * N + j)) b1++;
                    else b0++;
                }
                result[i] = b1 > b0;
            }
            return BitArrayToByteArray(result);
        }

        static byte[] hammingEncode(byte[] bin)
        {
            string result = "";
            byte[] encodedArr = new byte[bin.Length * 2];
            int encodeCounter = 0;
            int sum = 0;
            string[] tmp = new string[2];
            int[,] encodeMatrix = new int[,] { { 0,1,1,1},{ 1,0,1,1},{ 1,1,0,1},{ 1,0,0,0},
                {0,1,0,0 },{ 0,0,1,0},{ 0,0,0,1} };
            for (int i = 0; i < bin.Length; i++)
            {
                tmp[0] = Convert.ToString(bin[i], 2).PadLeft(8, '0').Substring(0, 4);
                tmp[1] = Convert.ToString(bin[i], 2).PadLeft(8, '0').Substring(4, 4);
                result = "0";
                for (int j = 0; j < 7; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        sum ^= (Convert.ToInt32(tmp[0][k]) - 48) & encodeMatrix[j, k];
                    }
                    result += sum;
                    sum = 0;
                }
                encodedArr[encodeCounter++] = Convert.ToByte(result, 2);
                result = "0";
                for (int j = 0; j < 7; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        sum ^= (Convert.ToInt32(tmp[1][k]) - 48) & encodeMatrix[j, k];
                    }
                    result += sum;
                    sum = 0;
                }
                encodedArr[encodeCounter++] = Convert.ToByte(result, 2);
            }
            return encodedArr;
        }

        static byte[] hammingDecode(byte[] bin)
        {
            string result = "";
            byte[] decodedArr = new byte[bin.Length / 2];
            int decodeCounter = 0;
            char[] arr = new char[7];
            string tmp = "";
            int sum = 0;
            int[] col = new int[3];
            int[,] decodeMatrix = new int[,] { { 1, 0, 0, 0, 1, 1, 1 }, { 0, 1, 0, 1, 0, 1, 1 }, { 0, 0, 1, 1, 1, 0, 1 } };
            for (int i = 0; i < bin.Length; i++)
            {
                tmp = Convert.ToString(bin[i], 2).PadLeft(8, '0').Substring(1, 7);
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 7; k++)
                    {
                        sum ^= (Convert.ToInt32(tmp[k]) - 48) & decodeMatrix[j, k];
                    }
                    col[j] = sum;
                    sum = 0;
                }
                if (col[0] == 0 && col[1] == 0 && col[2] == 0)
                {
                    result += tmp.Substring(3, 4);
                    if (result.Length == 8)
                    {
                        decodedArr[decodeCounter++] = Convert.ToByte(result, 2);
                        result = "";
                    }
                    continue;
                }
                for (int j = 0; j < 7; j++)
                {
                    if (decodeMatrix[0, j] == col[0] && decodeMatrix[1, j] == col[1] && decodeMatrix[2, j] == col[2])
                    {
                        switch (tmp[j])
                        {
                            case '0':
                                arr = tmp.ToCharArray();
                                arr[j] = '1';
                                tmp = new string(arr);
                                break;
                            case '1':
                                arr = tmp.ToCharArray();
                                arr[j] = '0';
                                tmp = new string(arr);
                                break;
                        }
                        result += tmp.Substring(3, 4);
                        if (result.Length == 8)
                        {
                            decodedArr[decodeCounter++] = Convert.ToByte(result, 2);
                            result = "";
                        }
                        break;
                    }
                }
            }
            return decodedArr;
        }

    }
}