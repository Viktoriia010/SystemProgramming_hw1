using System.Collections.Concurrent;
using System.Threading;
namespace SystemProgramming;


internal class Program
{
    static ConcurrentQueue<Order> ordersQueue = new ConcurrentQueue<Order>();
    private static bool isWorking = true;
    private static object _locker = new();
    private static int total = 0;
    static void TestThread2()
    {
        for (int i = 0; i <= 20; i++)
        {
            Thread.Sleep(500);
            Console.WriteLine($"Thread # {Thread.CurrentThread.ManagedThreadId} \t{i}");

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

    static void Up()
    {
        const int N = 10;
        for (int i = 0; i < N; i++)
        {
            lock(_locker)
            {

                ++total;
            }
            Thread.Sleep(1);
        }
    }
    static void ProcessOrders()
    {
        while (isWorking || !ordersQueue.IsEmpty)
        {
            if (ordersQueue.TryDequeue(out Order order))
            {
                Console.WriteLine(
                    $"Працівник {Thread.CurrentThread.ManagedThreadId} обробляє замовлення {order.Id}: {order.ProductName}"
                );

                Thread.Sleep(500);

                Console.WriteLine(
                    $"Order # {order.Id} for product {order.ProductName} has been completed"
                );
            }
        }
    }

    static void PrintLow()
    {
        for (int i = 0; i <= 20; i++)
        {
            Console.WriteLine($"Low - {i}");
            Thread.Sleep(100);
        }
    }
    static void PrintNormal()
    {
        for (int i = 0; i <= 20; i++)
        {
            Console.WriteLine($"Normal - {i}");
            Thread.Sleep(100);
        }
    }
    static void PrintHigh()
    {
        for (int i = 0; i <= 20; i++)
        {
            Console.WriteLine($"High - {i}");
            Thread.Sleep(100);
        }
    }
    static void Main(string[] args)
    {
        Thread threadLow = new Thread(PrintLow);
        Thread threadNormal = new Thread(PrintNormal);
        Thread threadHigh = new Thread(PrintHigh);

        threadLow.Priority = ThreadPriority.Lowest;
        threadNormal.Priority = ThreadPriority.Normal;
        threadHigh.Priority = ThreadPriority.Highest;

        threadLow.Start();
        threadNormal.Start();
        threadHigh.Start();

        threadLow.Join();
        threadNormal.Join();
        threadHigh.Join();

        //const int N = 3;
        //CountdownEvent done = new(N);
        //Console.WriteLine("Start main");
        //for (int i = 0; i < N; i++)
        //{
        //    ThreadPool.QueueUserWorkItem(_ =>
        //    {
        //        Console.WriteLine("Thread start");
        //        Thread.Sleep(500);
        //        Console.WriteLine("Thread finish");
        //        done.Signal();
        //    });
        //}

        //done.Wait();
        //Console.WriteLine("End main");

        //ManualResetEvent done = new ManualResetEvent(false);
        //Console.WriteLine("Start main");
        //ThreadPool.QueueUserWorkItem(_ =>
        //{
        //    Console.WriteLine("Thread start");
        //    Thread.Sleep(1000);
        //    Console.WriteLine("Thread finish");
        //    done.Set();
        //});
        //done.WaitOne();
        //Console.WriteLine("End main");

        //Console.WriteLine("Worker is running");

        //Thread[] workers = new Thread[3];

        //for (int i = 0; i < workers.Length; i++)
        //{
        //    workers[i] = new Thread(ProcessOrders);

        //    workers[i].Start();
        //}

        //while (true)
        //{
        //    Console.WriteLine("Enter product name:");
        //    string product = Console.ReadLine();

        //    if (string.IsNullOrWhiteSpace(product))
        //    {
        //        isWorking = false;
        //        break;
        //    }
        //    var order = new Order { ProductName = product };
        //    ordersQueue.Enqueue(order);

        //    Console.WriteLine("Order pushed to queue");

        //}


        //foreach (var worker in workers)
        //{
        //    worker.Join();
        //}
        //Console.WriteLine("All workers finished");

        //const int COUNT_THREADS = 5;
        //Thread[] treads = new Thread[COUNT_THREADS];
        //for (int i = 0; i < COUNT_THREADS; i++)
        //{
        //    treads[i] = new Thread(Up);
        //    treads[i].Start();
        //}
        //for (int i = 0; i < COUNT_THREADS; i++)
        //{
        //    treads[i].Join();
        //}
        //Console.WriteLine("Total {0}", total);
        //new WorkerDllPoint().Run();

        //new ProcessDemo().Run();
        //new WorkerDllCPlusPlus().Run();
        //Console.WriteLine("Start Main");
        //Thread thread = new Thread(TestThread2);
        //thread.Start();
        //for (int i = 0; i <= 20; i++)
        //{
        //    Thread.Sleep(1000);
        //    Console.WriteLine($"Thread # {Thread.CurrentThread.ManagedThreadId} \t{i}");

        //}
        //Console.WriteLine("Finish Main");

        //Console.WriteLine("Start Main");
        //Console.WriteLine("Cores: {0}", Environment.ProcessorCount);
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
