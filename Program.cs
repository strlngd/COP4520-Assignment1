/*
 * Program created by Sterling Downs
 * For COP4520 on January 22nd, 2022
 */
using System;

namespace Assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Find primes from 1 to 10^8 using 8 threads
            var primeFinder = new PrimeFinder((uint)Math.Pow(10, 8), 8);
            primeFinder.Run();
        }
    }
}