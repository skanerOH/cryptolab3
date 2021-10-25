using crypto_lab_3;
using crypto_lab_3.Kupyna;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ConsoleForTests
{
    class Program
    {
        static public void FindCollision(IHashFunc hashFunc, int diff, Random random)
        {
            byte[] inp = new byte[diff];
            string hash;
            HashSet<string> foundHashes = new HashSet<string>();
            ulong iterationsCounter = 0;

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            while (true)
            {
                iterationsCounter++;
                RandomizeByteArr(inp, random);

                hash = Encoding.ASCII.GetString(hashFunc.CalcHash(inp));

                if (foundHashes.Contains(hash))
                    break;
                else
                {
                    foundHashes.Add(hash);
                }
            }

            stopwatch.Stop();

            Console.WriteLine($"Found in {iterationsCounter} iterations \n Time spent {stopwatch.ElapsedMilliseconds} ms");
        }

        static public void RandomizeByteArr(byte[] inp, Random random)
        {
            for (int i = 0; i < inp.Length; ++i)
                inp[i] = (byte)random.Next(255);
        }

        static void Main(string[] args)
        {
            Random random = new Random();

            IHashFunc kupyna = new Kupyna();
            IHashFunc sha256Func = new SHA256();

            int diff = 4;

            Console.WriteLine("SHA256");
            FindCollision(sha256Func, diff, random);
            Console.WriteLine();

            Console.WriteLine("Kupyna");
            FindCollision(kupyna, diff, random);
            Console.WriteLine();
        }
    }
}
