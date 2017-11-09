using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITProject
{
    class Huffman
    {
        public static byte[] BitArrayToByteArray(BitArray bits)
        {
            byte[] ret = new byte[(bits.Length - 1) / 8 + 1];
            bits.CopyTo(ret, 0);
            return ret;
        }

        struct HuffmanNode
        {
            public byte value;
            public int number;

            public HuffmanNode(byte v, int n)
            {
                value = v;
                number = n;
            }
        }

        static BitArray HuffmanCoding(BitArray source)
        {
            byte[] buf = BitArrayToByteArray(source);
            List<HuffmanNode> nodes = new List<HuffmanNode>();

            foreach (byte b in buf)
            {
                HuffmanNode node = new HuffmanNode(b, 0);
                foreach (byte bn in buf)
                {
                    if (bn == node.value) node.number++;
                }
                nodes.Add(node);
            }
            return source;
        }
    }
}
