using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Chat
{
   static class Compressions
    {


        public static byte[] RLECompress(byte[] binary)
        {
            List<byte> compressed = new List<byte>();
            int counter = 1;
            for (int i = 0; i < binary.Length; i++)
            {
                while (i + 1 < binary.Length && binary[i] == binary[i + 1])
                {
                    counter++;
                    if (counter == 256)
                    {
                        compressed.Add(0);
                        counter = 1;
                    }
                    i++;
                }
                compressed.Add(Convert.ToByte(counter));
                compressed.Add(binary[i]);
                counter = 1;
            }
            return compressed.ToArray();
        }

        public static byte[] RLEDecompress(byte[] compressed)
        {
            List<byte> decompressed = new List<byte>();
            byte tmp = 0;
            int count = 0;
            for (int i = 0; i < compressed.Length; i++)
            {
                tmp = compressed[i];
                if (tmp == 0)
                {
                    count += 255;
                    continue;
                }
                count += tmp;
                for (int j = 0; j < count; j++)
                {
                    decompressed.Add(compressed[i + 1]);
                }
                i++;
                count = 0;
            }
            return decompressed.ToArray();
        }


        public static byte[] huffmanCompress(byte[] toCompress) {
            return HuffmanCompression.Compration(toCompress);
        }
        public static byte[] huffmanDecompress(byte[] toDecompress) {
            return HuffmanCompression.Decompration(toDecompress);
        }


        public static byte[] CompressionLZ78(byte[] byteArray)
        {
            BitArray bitArray = new BitArray(byteArray);
            // PrintBitArray(bitArray);
            // byte[] retByteArray = new byte[0];
            Node root = new Node(0);
            List<bool> retBits = new List<bool>();
            int currentDictionarySize = 1;
            int IndexBits = (int)Math.Ceiling(Math.Log(currentDictionarySize, 2));
            Node previousNode = null;
            Node currentNode = root;
            for (int i = 0; i < bitArray.Count; i++)
            {
                if (bitArray[i])
                {
                    if (currentNode.getLeft() == null)
                    {
                        currentNode.setLeft(new Node(currentDictionarySize));
                        int previousIndex = currentNode.getIndexOfElement();
                        int numberOfUsingBits = 0;
                        while (previousIndex != 0)
                        {
                            int currentBit = previousIndex & 1;
                            if (currentBit == 1)
                            {
                                retBits.Add(true);
                            }
                            else
                            {
                                retBits.Add(false);
                            }
                            previousIndex >>= 1;
                            numberOfUsingBits++;

                        }
                        for (int j = 0; j < (IndexBits - numberOfUsingBits); j++)
                        {
                            retBits.Add(false);
                        }

                        retBits.Add(true);
                        currentDictionarySize++;
                        IndexBits = (int)Math.Ceiling(Math.Log(currentDictionarySize, 2));
                        currentNode = root;
                        previousNode = null;
                        // PrintBitArray(new BitArray(retBits.ToArray()));
                    }
                    else
                    {
                        previousNode = currentNode;
                        currentNode = currentNode.getLeft();
                    }
                }
                else
                {
                    if (currentNode.getRight() == null)
                    {
                        currentNode.setRight(new Node(currentDictionarySize));
                        int previousIndex = currentNode.getIndexOfElement();
                        int numberOfUsingBits = 0;
                        while (previousIndex != 0)
                        {
                            int currentBit = previousIndex & 1;
                            if (currentBit == 1)
                            {
                                retBits.Add(true);
                            }
                            else
                            {
                                retBits.Add(false);
                            }
                            previousIndex >>= 1;
                            numberOfUsingBits++;

                        }
                        for (int j = 0; j < (IndexBits - numberOfUsingBits); j++)
                        {
                            retBits.Add(false);
                        }

                        retBits.Add(false);
                        currentDictionarySize++;
                        IndexBits = (int)Math.Ceiling(Math.Log(currentDictionarySize, 2));
                        currentNode = root;
                        previousNode = null;
                        // PrintBitArray(new BitArray(retBits.ToArray()));
                    }
                    else
                    {
                        previousNode = currentNode;
                        currentNode = currentNode.getRight();
                    }
                }

            }


            if (previousNode != null)
            {
                int previousIndex = previousNode.getIndexOfElement();
                int numberOfUsingBits = 0;
                while (previousIndex != 0)
                {
                    int currentBit = previousIndex & 1;
                    if (currentBit == 1)
                    {
                        retBits.Add(true);
                    }
                    else
                    {
                        retBits.Add(false);
                    }
                    previousIndex >>= 1;
                    numberOfUsingBits++;

                }
                for (int j = 0; j < (IndexBits - numberOfUsingBits); j++)
                {
                    retBits.Add(false);
                }
                retBits.Add(bitArray[bitArray.Count - 1]);

            }
            //  PrintBitArray(new BitArray(retBits.ToArray()));
            int addBits = 8 - retBits.Count % 8;
            // Console.WriteLine(retBits.Count % 8);
            for (int j = 0; j < addBits; j++)
            {
                retBits.Add(false);
            }
            //  PrintBitArray(new BitArray(retBits.ToArray()));
            int numberOfUsingAddBits = 0;
            while (addBits != 0)
            {
                int currentBit = addBits & 1;
                if (currentBit == 1)
                {
                    retBits.Add(true);
                }
                else
                {
                    retBits.Add(false);
                }
                addBits >>= 1;
                numberOfUsingAddBits++;

            }
            for (int j = 0; j < (8 - numberOfUsingAddBits); j++)
            {
                retBits.Add(false);
            }

            // PrintBitArray(new BitArray(retBits.ToArray()));
            return BitArrayToByteArray(new BitArray(retBits.ToArray()));


        }
        public static byte[] BitArrayToByteArray(BitArray bitArray)
        {
            byte[] returnArray = new byte[bitArray.Length / 8];
            bitArray.CopyTo(returnArray, 0);
            return returnArray;
        }
        public static byte[] DecompressionLZ78(byte[] byteArray)
        {
            BitArray bitArray = new BitArray(byteArray);
            //PrintBitArray(bitArray);
            int numberOfAddBits = 0;
            for (int i = 0; i < 8; i++)
            {
                if (bitArray[bitArray.Length - 1 - i])
                {
                    numberOfAddBits = numberOfAddBits << 1;
                    int t = 1;
                    numberOfAddBits = numberOfAddBits | t;
                }
                else
                {
                    int t = 0;
                    numberOfAddBits <<= 1;
                    numberOfAddBits = numberOfAddBits | t;
                }
            }
            numberOfAddBits += 8;

            //for (int i = 0; i < bitArray.Length-numberOfAddBits; i++)
            //{
            //    if (bitArray[i] == false)
            //    {
            //        Console.Write("0 ");
            //    }
            //    else
            //    {
            //        Console.Write("1 ");
            //    }

            //}
            //Console.WriteLine();

            List<bool> retBits = new List<bool>();
            Dictionary<int, List<bool>> dictionary = new Dictionary<int, List<bool>>();
            dictionary.Add(0, null);
            int currentDictionarySize = 1;
            int IndexBits = (int)Math.Ceiling(Math.Log(currentDictionarySize, 2));
            int index = 0;
            while (index < bitArray.Length - numberOfAddBits)
            {
                int dictionaryIndex = 0;
                for (int j = index + IndexBits - 1; j >= index; j--)
                {
                    if (bitArray[j])
                    {
                        dictionaryIndex = dictionaryIndex << 1;
                        int t = 1;
                        dictionaryIndex = dictionaryIndex | t;
                    }
                    else
                    {
                        int t = 0;
                        dictionaryIndex <<= 1;
                        dictionaryIndex = dictionaryIndex | t;
                    }
                }
                index += IndexBits;
                if (dictionaryIndex == 0)
                {
                    dictionary.Add(currentDictionarySize, new List<bool> { bitArray[index] });
                    retBits.Add(bitArray[index]);
                }
                else
                {
                    List<bool> addedList = new List<bool>();
                    foreach (bool element in dictionary[dictionaryIndex])
                    {
                        addedList.Add(element);
                        retBits.Add(element);
                    }
                    retBits.Add(bitArray[index]);
                    addedList.Add(bitArray[index]);
                    dictionary.Add(currentDictionarySize, addedList);
                }
                currentDictionarySize++;
                IndexBits = (int)Math.Ceiling(Math.Log(currentDictionarySize, 2));
                index++;

            }

            //PrintBitArray(new BitArray(retBits.ToArray()));
            return BitArrayToByteArray(new BitArray(retBits.ToArray()));
        }
    }
    public class Node
    {
        private Node left;
        private Node right;
        private int IndexOfElement;
        public Node(int IndexOfElement)
        {
            left = null;
            right = null;
            this.IndexOfElement = IndexOfElement;
        }

        public Node getLeft()
        {
            return left;
        }
        public Node getRight()
        {
            return right;
        }
        public void setLeft(Node node)
        {
            this.left = node;
        }
        public void setRight(Node node)
        {
            this.right = node;

        }
        public int getIndexOfElement()
        {
            return IndexOfElement;
        }
    }
}
