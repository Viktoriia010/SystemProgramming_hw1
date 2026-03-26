using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Text.Json;
namespace SystemProgramming;


class Pizza
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public override string ToString()
    {
        return $"{Name} - {Price}";
    }
}

class Document
{
    public string Name { get; set; }

    public override string ToString()
    {
        return $"{Name}";
    }

}

class Currency
{
    public int r030 { get; set; }
    public string txt { get; set; }
    public decimal rate { get; set; }

    public override string ToString()
    {
        return $"Currency: {txt} Rate: {rate}";
    }
}
internal class Program
{
    private static readonly string URL = "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json";
    //static ConcurrentQueue<Order> ordersQueue = new ConcurrentQueue<Order>();
    //private static bool isWorking = true;
    //private static object _locker = new();
    //private static int total = 0;
    //static void TestThread2()
    //{
    //    for (int i = 0; i <= 20; i++)
    //    {
    //        Thread.Sleep(500);
    //        Console.WriteLine($"Thread # {Thread.CurrentThread.ManagedThreadId} \t{i}");

    //    }
    //}
    //static void TestThread()
    //{
    //    Thread.Sleep(3000);
    //    Console.WriteLine("Start TestThread");
    //    Console.WriteLine($"Core Main {Thread.GetCurrentProcessorId()}");
    //    Console.WriteLine($"Thread # {Thread.CurrentThread.ManagedThreadId}");

    //    Console.WriteLine("Finish TestThread");
    //}

    //static void Up()
    //{
    //    const int N = 10;
    //    for (int i = 0; i < N; i++)
    //    {
    //        lock(_locker)
    //        {

    //            ++total;
    //        }
    //        Thread.Sleep(1);
    //    }
    //}
    //static void ProcessOrders()
    //{
    //    while (isWorking || !ordersQueue.IsEmpty)
    //    {
    //        if (ordersQueue.TryDequeue(out Order order))
    //        {
    //            Console.WriteLine(
    //                $"Працівник {Thread.CurrentThread.ManagedThreadId} обробляє замовлення {order.Id}: {order.ProductName}"
    //            );

    //            Thread.Sleep(500);

    //            Console.WriteLine(
    //                $"Order # {order.Id} for product {order.ProductName} has been completed"
    //            );
    //        }
    //    }
    //}

    //static void PrintLow()
    //{
    //    for (int i = 0; i <= 20; i++)
    //    {
    //        Console.WriteLine($"Low - {i}");
    //        Thread.Sleep(100);
    //    }
    //}
    //static void PrintNormal()
    //{
    //    for (int i = 0; i <= 20; i++)
    //    {
    //        Console.WriteLine($"Normal - {i}");
    //        Thread.Sleep(100);
    //    }
    //}
    //static void PrintHigh()
    //{
    //    for (int i = 0; i <= 20; i++)
    //    {
    //        Console.WriteLine($"High - {i}");
    //        Thread.Sleep(100);
    //    }
    //}


    static async Task<Pizza> DoPizza()
    {
        Console.WriteLine("Отримали замовлення");
        Console.WriteLine("Починвємо готувати");
        await Task.Delay(500);
        Console.WriteLine("Готуємо тісто");
        await Task.Delay(1000);
        Console.WriteLine("Готуємо начинку");
        await Task.Delay(1000);
        Console.WriteLine("Випікаємо піцу");
        await Task.Delay(1000);
        Console.WriteLine("Все готово");
        return new Pizza { Name = "Margarita", Price = 300 };

    }

    static async Task<List<Document>> UploadingDocuments()
    {
        List<Document> docs = new List<Document>();
        Console.WriteLine("Початок завантаження документів");

        for (int i = 1; i <= 3; i++)
        {
            await Task.Delay(1000);
            Console.WriteLine($"Завантажуємо документ {i}");

            await Task.Delay(1000);
            Console.WriteLine($"Заповнюємо дані документу {i}");

            docs.Add(new Document { Name = $"Документ {i}" });
        }

        Console.WriteLine("Все готово");
        return docs;
    }

    static async Task<List<Currency>> GetCurrency(){

        using (HttpClient client = new HttpClient())
        {
            try
            {
                var response = await client.GetStringAsync(URL);
                var obj = JsonSerializer.Deserialize<List<Currency>>(response);
                if(obj != null)
                {
                    return obj;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }


    }


    static decimal ConvertToDolars(decimal grn, List<Currency> currencies)
    {
        var dollar = currencies.FirstOrDefault(d => d.txt == "Долар США");
        return grn / dollar.rate;
    }
    static async Task Main(string[] args)
    {
        BankAcount bankAcount = new BankAcount();
        Thread thread1 = new Thread(_ =>
        {
            bankAcount.AdditionAcount(100);
        });
        Thread thread2 = new Thread(_ =>
        {
            bankAcount.AdditionAcount(200);
        });
        Thread thread3 = new Thread(_ =>
        {
            bankAcount.AdditionAcount(150);
        });



        Thread thread4 = new Thread(_ =>
        {
            bankAcount.WithdrawMoney(400);
        });
        Thread thread5 = new Thread(_ =>
        {
            bankAcount.WithdrawMoney(400);
        });
        Thread thread6 = new Thread(_ =>
        {
            bankAcount.WithdrawMoney(400);
        });

        bankAcount.ShowBalance();
        thread1.Start();
        thread2.Start();
        thread3.Start();

        thread1.Join();
        thread2.Join();
        thread3.Join();
        bankAcount.ShowBalance();

        thread4.Start();
        thread5.Start();
        thread6.Start();

        thread4.Join();
        thread5.Join();
        thread6.Join();

        bankAcount.ShowBalance();

        //Console.WriteLine("Введіть скільки грн: ");
        //decimal grn = Convert.ToDecimal(Console.ReadLine());


        //var data = await GetCurrency();
        //if (data != null)
        //{
        //    //foreach (var item in data)
        //    //{
        //    //    Console.WriteLine(item);
        //    //}
        //    var result = ConvertToDolars(grn, data);
        //    Console.WriteLine($"Приблизно: {result} USD");
        //}



        //var documents = UploadingDocuments();
        //for (int i = 0; i < 10; i++)
        //{
        //    Console.WriteLine("Ми працюємо...");
        //    Thread.Sleep(500);
        //}
        //documents.Wait();
        //Console.WriteLine("Отримали документи");
        //foreach (var item in documents.Result)
        //{
        //    Console.WriteLine(item);
        //}


        //Thread threadLow = new Thread(PrintLow);
        //Thread threadNormal = new Thread(PrintNormal);
        //Thread threadHigh = new Thread(PrintHigh);

        //threadLow.Priority = ThreadPriority.Lowest;
        //threadNormal.Priority = ThreadPriority.Normal;
        //threadHigh.Priority = ThreadPriority.Highest;

        //threadLow.Start();
        //threadNormal.Start();
        //threadHigh.Start();

        //threadLow.Join();
        //threadNormal.Join();
        //threadHigh.Join();

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
