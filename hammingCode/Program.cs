using System;
using System.Collections;

namespace hammingCode
{
    class Program
    {
        static byte BitArrayToByteReversed(BitArray bits)
        {
            for (int i = 0; i < bits.Length; i++)
                bits[i] = !bits[i];
            byte[] bytes = new byte[1];
            bits.CopyTo(bytes, 0);
            return bytes[0];
        }

        static byte BitArrayToByte(BitArray bits) {
            byte[] bytes = new byte[1];
            bits.CopyTo(bytes, 0);
            return bytes[0];
        }

        static byte[] hammingEncode(byte[] bin) {
            byte[] encodedArr = new byte[bin.Length * 2];
            int encodeCounter = 0;
            bool sum=false;
            BitArray tmp;
            BitArray result = new BitArray(8);
            bool[,] encodeMatrix = new bool[,] { { false,true,true,true},{ true,false,true,true},{ true,true,false,true},{ true,false,false,false},
                {false,true,false,false },{ false,false,true,false},{ false,false,false,true} };
            for(int i=0; i<bin.Length; i++) {
                tmp = new BitArray(new byte[] { bin[i]});
                for (int j = 0; j < tmp.Length; j++) tmp[j] = !tmp[j];
                result[7] = false;
                for (int j = 0; j < 7; j++) {
                    for (int k = 0; k < 4; k++) {
                        sum^=  tmp[k] && encodeMatrix[j,k];
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
                        sum ^=  tmp[k+4] && encodeMatrix[j, k];
                    }
                    result[j] = sum;
                    sum = false;
                }
                encodedArr[encodeCounter++] = BitArrayToByte(result);
            }
            return encodedArr;
        }

        static byte[] hammingDecode(byte[] bin) {
            byte[] decodedArr = new byte[bin.Length / 2];
            int decodeCounter = 0;
            int count = 0;
            bool sum = false;
            int[] col = new int[3];
            BitArray tmp;
            BitArray result = new BitArray(8);
            bool[,] decodeMatrix = new bool[,] { { true, false, false, false, true, true, true }, { false, true, false, true, false, true, true }, 
                { false, false, true, true, true, false, true } };
            for (int i = 0; i < bin.Length; i++) {
                tmp = new BitArray(new byte[] { bin[i]});
                for (int j = 0; j < 3; j++) {
                    for (int k = 0; k < 7; k++) {
                        sum ^= tmp[k] && decodeMatrix[j, k];
                    }
                    col[j] = Convert.ToInt32(sum);
                    sum = false;
                }
                if (col[0] == 0 && col[1] == 0 && col[2] == 0) {
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
                for (int j = 0; j < 7; j++) {
                    if (Convert.ToInt32(decodeMatrix[0, j]) == col[0] && Convert.ToInt32(decodeMatrix[1, j]) == col[1] 
                        && Convert.ToInt32(decodeMatrix[2, j]) == col[2]) {
                        switch (tmp[j]) {
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

        static void Main(string[] args)
        {
            Console.WriteLine("Ti pidor");
            Console.Read();
        }
    }
}
