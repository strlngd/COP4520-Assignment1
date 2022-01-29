using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment1
{
    public class PrimeFinder
    {
        public uint UpperBound { get; set; }
        public byte ThreadCount { get; set; }

        public uint CurrentPrimeIndex { get; private set; } = 1;
        public List<uint> Primes { get; }
        
        private readonly object _locker = new object();

        private readonly bool[] _markedPrime;
        
        public PrimeFinder(uint upperBound, byte threadCount)
        {
            UpperBound = upperBound;
            ThreadCount = threadCount;
            Primes = new List<uint>();
            _markedPrime = new bool[(int)UpperBound + 1];
        }

        /// <summary>
        /// Determines prime numbers using Sieve of Eratosthenes.
        /// </summary>
        public void Run()
        {
            for (var i = 0; i < _markedPrime.Length; i++) // Initialize all boolean values in array as true
                _markedPrime[i] = true;

            var threads = new Thread[ThreadCount]; // Thread array for checking when all threads are done working
            var startingTime = DateTime.Now; // Store starting time
            
            // create threads
            for (uint th = 0; th < ThreadCount; th++)
            {
                threads[th] = new Thread(() =>
                {
                    var i = NextPrimeIndex();
                    while(i * i <= UpperBound)
                    {
                        if (!_markedPrime[i]) continue;

                        // Find multiples of primes number and set them to not prime
                        for (var j = i * i; j <= UpperBound; j += i)
                        {
                            _markedPrime[j] = false;
                        }

                        i = NextPrimeIndex();
                    }
                    
                });
                threads[th].Start();
            }
            
            // Wait until all threads are done working
            while(threads.Any(t => t.IsAlive)){ }

            // Store remaining primes in list
            for (var i = 2; i < _markedPrime.Length; i++)
            {
                if (_markedPrime[i])
                {
                    Primes.Add((uint)i);
                }
            }
            
            // output to file
            var exTime = (DateTime.Now - startingTime).Milliseconds;
            var primesSum = Primes.Sum(x => x);
            var topTen = string.Join(", ", Primes.Skip(Primes.Count - 10));
            File.WriteAllText("primes.txt",$"{exTime}ms {Primes.Count} {primesSum}{Environment.NewLine}{topTen}");
        }

        /// <summary>
        /// Returns the next index for sieve of eratosthenes. Locks to synchronize access.
        /// </summary>
        /// <returns></returns>
        private uint NextPrimeIndex()
        {
            lock (_locker)
            {
                for (var i = CurrentPrimeIndex + 1; i < _markedPrime.Length; i++)
                {
                    // next index to use must be still marked as prime
                    if (_markedPrime[i])
                    {
                        CurrentPrimeIndex = i;
                        return i;
                    }
                }

                return (uint) _markedPrime.Length;
            }
        }
    }
}