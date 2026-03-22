using System.Threading;
namespace SystemProgramming;


internal class Program
{
    static void TestThread2()
    {
        for (int i = 0; i <= 20; i++)
        {
            Console.Write($"{i}\t");
            Thread.Sleep(500);
            Console.WriteLine($"Thread # {Thread.CurrentThread.ManagedThreadId}");

        }
    }
    static void TestThread()
    {
        Thread.Sleep(3000);
        Console.WriteLine("Start TestThread");
        Console.WriteLine($"Core Main {Thread.GetCurrentProcessorId()}");
        Console.WriteLine($"Thread # {Thread.CurrentThread.ManagedThreadId}");

        Console.WriteLine("Finish TestThread");
    }
    static void Main(string[] args)
    {
        new ProcessDemo().Run();
        //new WorkerDllCPlusPlus().Run();
        //Console.WriteLine("Start Main");

        //for (int i = 0; i <= 20; i++)
        //{
        //    Console.WriteLine(i);
        //    Thread.Sleep(1000);
        //    Console.WriteLine($"Thread # {Thread.CurrentThread.ManagedThreadId}");

        //}
        //Console.WriteLine("Finish Main");

        //Console.WriteLine("Start Main");
        //Console.WriteLine("Cores: {0}",Environment.ProcessorCount);
        //Console.WriteLine($"Core Main {Thread.GetCurrentProcessorId()}");
        //Console.WriteLine($"Thread # {Thread.CurrentThread.ManagedThreadId}");
        //Thread thread = new Thread(TestThread);
        //thread.Start();
        //for (int i = 0; i <= 20; i++)
        //{
        //    Console.WriteLine(i);

        //}
        //thread.Join();
        //Console.WriteLine("Finish Main");

    }
}
