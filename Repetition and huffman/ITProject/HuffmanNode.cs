using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITProject
{
    class HuffmanNode
    {
        private HuffmanNode left, right;
        private byte value;
        private int count;
        private bool isLeaaf ;

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

        public void print()
        {
            if (!isLeaaf) { left.print(); right.print(); }
            else
                Console.WriteLine(value.ToString() + '\t' + count.ToString());
                

        }
    }
}
