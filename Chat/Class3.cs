using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    class HuffmanNode
    {
        private HuffmanNode left, right;
        private byte value;
        private int count;
        private bool isLeaaf;


        public HuffmanNode(byte value)
        {
            this.value = value;
            count = 1;
            left = null;
            right = null;
            isLeaaf = true;
        }

        public HuffmanNode(int count, HuffmanNode left, HuffmanNode right)
        {
            this.count = count;
            this.left = left;
            this.right = right;
            isLeaaf = false;
        }

        public void SetLeft(HuffmanNode left)
        {
            this.left = left;
        }

        public void SetRight(HuffmanNode right)
        {
            this.right = right;
        }

        public void IncCount()
        {
            count++;
        }

        public byte GetByte()
        {
            return value;
        }

        public int GetCount()
        {
            return count;
        }

        public void Print()
        {
            if (isLeaaf)
                Console.WriteLine(value.ToString() + '\t' + count.ToString());
            else
            {
                left.Print();
                right.Print();
            }

        }

        public void ToMap(Hashtable map, BitArray code)
        {
            if (isLeaaf)
            {
                BitArray buf = new BitArray(code.Length);
                for (int i = 0; i < buf.Length; i++) buf[i] = code[i];
                map.Add(value, buf);
            }
            else
            {
                BitArray newCode = new BitArray(code.Length + 1);
                for (int i = 0; i < code.Length; i++)
                    newCode[i] = code[i];

                newCode[code.Length] = true;
                left.ToMap(map, newCode);

                newCode[code.Length] = false;
                right.ToMap(map, newCode);
            }
        }
    }
}