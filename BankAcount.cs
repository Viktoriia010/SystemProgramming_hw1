using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemProgramming;

public class BankAcount
{
    public decimal Balance { get; private set; }
    public bool isBlocked { get; private set; } = false;
    private static object _locker = new();

    public void ShowBalance()
    {
        //if (isBlocked)
        //{
        //    Console.WriteLine("Рахунок заблокований");
        //    return;
        //}
        Console.WriteLine($"Your balance: {Balance}");
    }
    public void AdditionAcount(decimal money)
    {
        while (true)
        {
            lock (_locker)
            {
                if (isBlocked == true)
                {
                    Console.WriteLine("Рахунок заблокований");
                    return;
                }
                if (money > 0)
                {
                    Thread.Sleep(100);


                    Balance += money;
                    Console.WriteLine("Операція виконана успішно");
                    break;
                }
            }
        }
        
    }

    public void WithdrawMoney(decimal money)
    {
        while (true) {
            lock (_locker)
            {
                if (isBlocked == true)
                {

                    Console.WriteLine("Рахунок заблокований");
                    return;
                }

                if (money < 0)
                {
                    Console.WriteLine("Число має бути додатнє");
                    return;
                }
                if (money > Balance)
                {
                    Console.WriteLine("Недостатньо коштів");
                    isBlocked = true;
                    return;
                }

                Balance -= money;
                Thread.Sleep(100);

                Console.WriteLine("Операція виконана успішно");
                break;
            }

        }
    }
}
