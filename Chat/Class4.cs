using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    class HuffmanCompression
    {
        private static int GetIntFromBitArray(BitArray bitArray)
        {
            if (bitArray.Length > 32)
                throw new ArgumentException("Argument length shall be at most 32 bits.");

            int[] array = new int[1];
            bitArray.CopyTo(array, 0);
            return array[0];

        }

        public static byte[] BitArrayToByteArray(BitArray bits)
        {
            byte[] ret = new byte[(bits.Length - 1) / 8 + 1];
            bits.CopyTo(ret, 0);
            return ret;
        }

        private static HuffmanNode CreateHuffmanTree(byte[] source)
        {
            //calculate propability
            LinkedList<HuffmanNode> bytes = new LinkedList<HuffmanNode>();

            foreach (byte b in source)
            {
                bool isnExist = true;
                foreach (HuffmanNode hn in bytes)
                {
                    if (hn.GetByte() == b)
                    {
                        hn.IncCount();
                        isnExist = false;
                        break;
                    }
                }
                if (isnExist) bytes.AddFirst(new HuffmanNode(b));
            }

            // create huffman tree
            while (bytes.Count > 1)
            {
                HuffmanNode firstMin = bytes.First.Value;
                HuffmanNode secongMin = bytes.First.Next.Value;

                foreach (HuffmanNode hn in bytes)
                {
                    if (firstMin.GetCount() > hn.GetCount())
                    {
                        secongMin = firstMin;
                        firstMin = hn;
                    }
                }

                HuffmanNode newHN = new HuffmanNode(firstMin.GetCount() + secongMin.GetCount(), firstMin, secongMin);
                bytes.Remove(firstMin);
                bytes.Remove(secongMin);
                bytes.AddFirst(newHN);
            }

            return bytes.First.Value;
        }

        private static Hashtable CreateMap(byte[] source)
        {
            HuffmanNode huffmanTree = CreateHuffmanTree(source);
            Hashtable map = new Hashtable();
            huffmanTree.ToMap(map, new BitArray(0));

            return map;
        }

        private static BitArray Coding(Hashtable map, byte[] source)
        {
            BitArray[] results = new BitArray[source.Length];

            int length = 0;
            for (int i = 0; i < results.Length; i++)
            {
                results[i] = (BitArray)map[source[i]];
                length += results[i].Length;
            }

            int count = 0;
            BitArray result = new BitArray(length);
            for (int i = 0; i < results.Length; i++)
                for (int j = 0; j < results[i].Length; j++)
                {
                    result[count] = results[i][j];
                    count++;
                }
            return result;
        }

        private static BitArray CodeMap(Hashtable map)
        {
            BitArray[] results = new BitArray[map.Count];

            int length = 0;
            int count = 0;
            foreach (DictionaryEntry pair in map)
            {
                BitArray value = new BitArray(new byte[] { (byte)pair.Key });
                BitArray codeLength = new BitArray(new byte[] { Convert.ToByte(((BitArray)pair.Value).Length) });
                BitArray code = (BitArray)pair.Value;

                results[count] = new BitArray(value.Length + codeLength.Length + code.Length);
                for (int i = 0; i < value.Length; i++) results[count][i] = value[i];
                for (int i = 0; i < codeLength.Length; i++) results[count][value.Length + i] = codeLength[i];
                for (int i = 0; i < code.Length; i++) results[count][value.Length + codeLength.Length + i] = code[i];
                length += results[count].Length;
                count++;
            }

            BitArray result = new BitArray(length);

            count = 0;
            for (int i = 0; i < results.Length; i++)
                for (int j = 0; j < results[i].Length; j++)
                {
                    result[count] = results[i][j];
                    count++;
                }

            return result;
        }
        private static BitArray JoinAll(BitArray messange, BitArray codeMap, BitArray messangeLength, BitArray codeMapLength, BitArray numberOfZeroInEnd, int lenght)
        {
            BitArray result = new BitArray(lenght);

            for (int i = 0; i < messangeLength.Length; i++)
                result[i] = messangeLength[i];

            for (int i = 0; i < codeMapLength.Length; i++)
                result[messangeLength.Length + i] = codeMapLength[i];

            for (int i = 0; i < numberOfZeroInEnd.Length; i++)
                result[messangeLength.Length + codeMapLength.Length + i] = numberOfZeroInEnd[i];

            for (int i = 0; i < codeMap.Length; i++)
                result[messangeLength.Length + codeMapLength.Length + numberOfZeroInEnd.Length + i] = codeMap[i];

            for (int i = 0; i < messange.Length; i++)
                result[messangeLength.Length + codeMapLength.Length + numberOfZeroInEnd.Length + codeMap.Length + i] = messange[i];

            return result;
        }
        public static byte[] Compration(byte[] source)
        {
            Hashtable map = CreateMap(source);

            BitArray messange = Coding(map, source);
            BitArray codeMap = CodeMap(map);

            BitArray messangeLength = new BitArray(new int[] { messange.Length });
            BitArray codeMapLength = new BitArray(new byte[] { Convert.ToByte(map.Count - 1) });
            int fullLength = messangeLength.Length + codeMapLength.Length + 8 + codeMap.Length + messange.Length;
            BitArray numberOfZeroInEnd = new BitArray(new byte[] { Convert.ToByte(8 - fullLength % 8) });

            BitArray result = JoinAll(messange, codeMap, messangeLength, codeMapLength, numberOfZeroInEnd, (int)(fullLength + (8 - fullLength % 8)));

            return BitArrayToByteArray(result);
        }
        private static BitArray ReadSubBiteArrayFromPosition(BitArray source, int startPosition, int length)
        {
            BitArray result = new BitArray(length);
            for (int i = 0; i < length; i++)
                result[i] = source[startPosition + i];
            return result;
        }
        private static bool[] ReadSubBoolArrayFromPosition(BitArray source, int startPosition, int length)
        {
            bool[] result = new bool[length];
            for (int i = 0; i < length; i++)
                result[i] = source[startPosition + i];
            return result;
        }
        private static int GetIntFromPositionInBiteArray(BitArray source, int startPosition)
        {
            BitArray messangeLengthBitArray = ReadSubBiteArrayFromPosition(source, startPosition, 32);
            return GetIntFromBitArray(messangeLengthBitArray);
        }
        private static byte GetByteFromPositionInBiteArray(BitArray source, int startPosition)
        {
            BitArray messangeLengthBitArray = ReadSubBiteArrayFromPosition(source, startPosition, 8);
            return BitArrayToByteArray(messangeLengthBitArray)[0];
        }
        private static void PrintBoolArray(bool[] source)
        {
            for (int i = 0; i < source.Length; i++)
            {
                Console.Write(Convert.ToByte(source[i]));
            }
            Console.WriteLine();
        }
        private static void PrintBiteArray(BitArray source)
        {
            if (source != null)
            {
                for (int i = 0; i < source.Length; i++)
                {
                    Console.Write(Convert.ToByte(source[i]));
                }
                Console.WriteLine();
            }
        }
        private static int MapContains(BitArray[] map, BitArray element)
        {
            for (int i = 0; i < map.Length; i++)
            {
                if (map[i] == null) continue;
                if (map[i].Length != element.Length) continue;
                bool flag = true;
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] != element[j])
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    return i;
                }
            }
            return -1;
        }
        public static byte[] Decompration(byte[] input)
        {
            BitArray source = new BitArray(input);

            int messangeLength = GetIntFromPositionInBiteArray(source, 0);
            int codeMapLength = GetByteFromPositionInBiteArray(source, 32) + 1;
            byte numberOfZeroInEnd = GetByteFromPositionInBiteArray(source, 40);

            BitArray[] map = new BitArray[256];

            int cursor = 48;
            for (int i = 0; i < codeMapLength; i++)
            {
                byte value = GetByteFromPositionInBiteArray(source, cursor); cursor += 8;
                byte length = GetByteFromPositionInBiteArray(source, cursor); cursor += 8;
                map[value] = ReadSubBiteArrayFromPosition(source, cursor, length); cursor += length;
            }

            BitArray messange = ReadSubBiteArrayFromPosition(source, cursor, messangeLength);

            LinkedList<byte> bytes = new LinkedList<byte>();

            cursor = 0;
            while (cursor < messange.Length)
            {
                int value = -1;
                for (int i = 1; value == -1; i++)
                {
                    value = MapContains(map, ReadSubBiteArrayFromPosition(messange, cursor, i));
                }
                cursor += map[value].Length;

                bytes.AddLast(Convert.ToByte(value));
            }

            return bytes.ToArray(); ;
        }
    }
}