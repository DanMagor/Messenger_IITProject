using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITProject
{
    class HuffmanCompration
    {

        public static BitArray Compration(byte[] sourse)
        {
            LinkedList<HuffmanNode> bytes = new LinkedList<HuffmanNode>();

            foreach(byte b in sourse)
            {
                bool isnExist = true;
                foreach(HuffmanNode hn in bytes)
                {
                    if(hn.GetByte() == b)
                    {
                        hn.IncCount();
                        isnExist = false;
                        break;
                    }
                }
                if (isnExist) bytes.AddFirst(new HuffmanNode(b));
            }

            /*foreach(HuffmanNode hn in bytes)
            {
                Console.WriteLine(hn.GetByte().ToString() + '\t' + hn.GetCount().ToString());
            }//*/

            while (bytes.Count > 1)
            {
                HuffmanNode firstMin = bytes.First.Value;
                HuffmanNode secongMin = bytes.First.Next.Value;

                foreach(HuffmanNode hn in bytes)
                {
                    if(firstMin.GetCount() > hn.GetCount())
                    {
                        secongMin = firstMin;
                        firstMin = hn;
                    }
                }

                //Console.WriteLine(firstMin.GetByte().ToString() + '\t' + firstMin.GetCount().ToString());
                HuffmanNode newHN = new HuffmanNode(firstMin.GetCount()+secongMin.GetCount(),firstMin,secongMin);
                bytes.Remove(firstMin);
                bytes.Remove(secongMin);
                bytes.AddFirst(newHN);
            }

            bytes.First.Value.print();

            return null;
        }

    }
}
