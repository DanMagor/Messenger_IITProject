using System.Collections;

namespace ITProject
{
    class RepetitionCoding
    {
        public static byte[] BitArrayToByteArray(BitArray bits)
        {
            byte[] ret = new byte[(bits.Length - 1) / 8 + 1];
            bits.CopyTo(ret, 0);
            return ret;
        }
        
        public static byte[] RepetitionCode(byte[] file, int N)
        {
            BitArray sourse = new BitArray(file);
            BitArray result = new BitArray(sourse.Length * N);
            for (int i = 0; i < sourse.Length; i++)
                for (int j = 0; j < N; j++)
                {
                    result[i * N + j] = sourse.Get(i);
                }
            return BitArrayToByteArray(result);
        }

        public static byte[] RepetitionDECode(byte[] file, int N)
        {
            BitArray sourse = new BitArray(file);
            BitArray result = new BitArray(sourse.Length / N);
            for (int i = 0; i < result.Length; i++)
            {
                int b0 = 0, b1 = 0;
                for (int j = 0; j < N; j++)
                {
                    if (sourse.Get(i * N + j)) b1++;
                    else b0++;
                }
                result[i] = b1 > b0;
            }
            
            return BitArrayToByteArray(result);
        }
    }
}
