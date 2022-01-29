# Assignment 1 (COP4520)
Created by Sterling Downs

## Problem
Your non-technical manager assigns you the task to find all primes between 1 and 108.  The assumption is that your company is going to use a parallel machine that supports eight concurrent threads. Thus, in your design you should plan to spawn 8 threads that will perform the necessary computation. Your boss does not have a strong technical background but she is a reasonable person. Therefore, she expects to see that the work is distributed such that the computational execution time is approximately equivalent among the threads. Finally, you need to provide a brief summary of your approach and an informal statement reasoning about the correctness and efficiency of your design. Provide a summary of the experimental evaluation of your approach. Remember, that your company cannot afford a supercomputer and rents a machine by the minute, so the longer your program takes, the more it costs. Feel free to use any programming language of your choice that supports multi-threading as long as you provide a ReadMe file with instructions for your manager explaining how to compile and run your program from the command prompt.

## Installation & Usage
To run this program, it is recommended that you use the dotnet-CLI.
.NET 5 is required to run this project. Instructions for how to install it via CLI are listed below.

1. Run the following commands (via command line) in order:


```
sudo apt update
sudo apt install apt-transport-https
```
#### IMPORTANT NOTE: If not on Ubuntu 20.04, you will have to use a different wget command. You can find this command at: https://docs.microsoft.com/en-us/dotnet/core/install/linux-ubuntu
```
wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
```
```
sudo apt update
sudo apt install dotnet-sdk-5.0
```

2. Verify your .NET 5 installation by executing the `dotnet --list-sdks`"` command.
3. Navigate to the source directory (should contain Assignment1.csproj)
4. To compile __and__ run the program, use the `dotnet run -c release` command.
5. A `primes.txt` file with the results of the program will be created in the project source code directory.

## Output
The program will print all output to a `primes.txt` file.
The output is in the following format:
```
<execution time>  <total number of primes found>  <sum of all primes found>

<top ten maximum primes, listed in order from lowest to highest>
```

## Proof of Correctness
This program makes use of the Sieve of Eratosthenes method for finding primes.
This method has been around for centuries and it is not only extremely fast, but 
also relatively easy to understand. On top of this algorithm, the program also 
divides the work into 8 separate threads. Each thread is assigned a number to check 
via the Sieve of Eratosthenes. When finished, the thread calls a method to assign 
a new number to itself, locking access to this method in the meantime.

The results of this program were verified by relating them to both the single-threaded 
version, as well as comparing them to a known prime values count at:
https://primes.utm.edu/howmany.html

## Efficiency
This program was tested on an i5-9600k CPU @4.60GHz with 6 cores and 6 threads.

### Attempt 1
When originally developing a solution, I wrote a brute-force method with additional 
logic to skip all even numbers besides 2. This naive solution took approximately 50 
seconds to finish running. I used this solution for later comparisons.

### Attempt 2
After researching methods of determination for prime numbers, I discovered the Sieve of 
Eratosthenes. Upon initial testing, I was able to get the program to run in only 1.6 seconds.
However, after studying the code, I noticed a few optimizations that I could make. These 
optimizations allowed the program to run in only 0.55 seconds, an improvement by a factor of 3.
Compared to attempt 1, attempt 2 ran over 90x faster.

## Evaluation
While there are still a few things that I might be able to improve with more experimentation, 
I think that a runtime of less than 1 second is already a top solution in relation to the 
original problem. A single-threaded solution is fast, but a multi-threaded solution really 
helped to decrease the runtime that much further, into giving near-instantaneous feedback.
