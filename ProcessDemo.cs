using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemProgramming;

internal class ProcessDemo
{
    /*
 1) Якщо ви процес запустили, і він ще працює, то знову не запускати
 2) Зробити метод, який дозволить вбити обраний процес
 */
    private Process _process;
    public void Run()
    {
        ConsoleKeyInfo key;
        do
        {
            Console.WriteLine("Process Demo");
            Console.WriteLine("1 - ShowAllProcessesFilter");
            Console.WriteLine("2 - ShowAllProcesses");
            Console.WriteLine("3 - GetProcessByPid");
            Console.WriteLine("4 - CreateProcess");
            Console.WriteLine("5 - KillProcess");
            Console.WriteLine("6 - CallTestProgram");
            Console.WriteLine("7 - Open dou.ua");
            Console.WriteLine("0 - Exist");
            key = Console.ReadKey();
            switch (key.KeyChar)
            {
                case '1':
                    ShowAllProcessesFilter();
                    break;
                case '2':
                    ShowAllProcesses();
                    break;
                case '3':
                    GetProcessByPid();
                    break;
                case '4':
                    CreateProcess();
                    break;
                case '5':
                    KillProcess();
                    break;
                case '6':
                    CallTestProgram();
                    break;
                case '7':
                    OpenDouUa();
                    break;
                default:
                    Console.WriteLine("unknown operation");
                    break;

            }
        } while (key.KeyChar != '0');


    }

    private void SaveProcessToFile(Process[] processes)
    {
        try
        {
            string fileName = $"processes_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt";

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var process in processes)
                {
                    writer.WriteLine($"{process.ProcessName} PID: {process.Id}");

                }
            }

            Console.WriteLine($"Saved to file: {fileName}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    private void OpenDouUa()
    {
        try
        {
            //Console.WriteLine("Enter site name: ");
            //string name = Console.ReadLine();
            //string url = "https://" + name;
            string url = "https://dou.ua";
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    private void KillProcess()
    {
        Console.WriteLine("Enter process Id: ");
        int id = Convert.ToInt32(Console.ReadLine());
        try
        {
            Process.GetProcessById(id).Kill();
            Console.WriteLine("Process killed");
        }
        catch(Exception)
        {
            Console.WriteLine("Process not found.");
        }
        
    }
    private void GetProcessByPid()
    {
        try
        {
            Console.WriteLine("Enter pid:");
            int pid = Convert.ToInt32(Console.ReadLine());
            var process = Process.GetProcessById(pid);
            Console.WriteLine($"{process.ProcessName}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }
    private void ShowAllProcessesFilter()
    {
        //Вивести у консоль перелік процесів та ті, які повторюються порахувати їх кількість і вивести напроти імені
        //процеса цю кількість
        Process[] processes = Process.GetProcesses();
        Dictionary<String, int> taskManager = new Dictionary<String, int>();
        foreach (var process in processes)
        {
            string processName = string.Empty;
            try
            {
                processName = process.ProcessName;
            }
            catch (Exception)
            {
                processName = "unknown";
            }
            if (taskManager.ContainsKey(processName))
            {
                taskManager[processName] += 1;
            }
            else
            {
                taskManager[processName] = 1;
            }
        }
        foreach (var process in taskManager)
        {
            Console.WriteLine($"{process.Key} {process.Value}");
        }
        SaveProcessToFile(processes);
    }
    private void ShowAllProcesses()
    {
        Process[] processes = Process.GetProcesses();

        foreach (var process in processes)
        {
            Console.WriteLine($"{process.ProcessName} PID: {process.Id}");
        }
        SaveProcessToFile(processes);

    }
    private void CreateProcess()
    {
        try
        {
            Console.WriteLine("Enter programm name: ");
            string? programm = Console.ReadLine();
            if (programm != null)
            {
                Console.WriteLine(Process.Start(programm).Id);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    private void CallTestProgram()
    {
        string exePath = @"C:\Users\ASUS\Documents\C#\SystemProgramming\TestProgram\bin\Debug\net8.0\TestProgram.exe";
        Console.WriteLine("Enter arg (hi, bye, ect)");
        string arg = Console.ReadLine()??"hi";
        ProcessStartInfo processInfo = new ProcessStartInfo()
        {
            FileName = exePath,
            Arguments = arg,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };
        using (Process process = new Process())
        {
            process.StartInfo = processInfo;
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string errors = process.StandardError.ReadToEnd();
            process.WaitForExit(); //чекаємо завершення процеса
            if (string.IsNullOrEmpty(errors))
            {
                Console.WriteLine($"Result: {output}");
            }
            else
            {

                Console.WriteLine($"Result: {errors}");
            }
        }
    }

    //public void Run()
    //{
    //    ConsoleKeyInfo key;
    //    do
    //    {
    //        Console.WriteLine("Process Demo");
    //        Console.WriteLine("0 - Exist");
    //        Console.WriteLine("1 - ShowAllProcessesFilter");
    //        Console.WriteLine("2 - ShowAllProcesses");
    //        Console.WriteLine("3 - GetProcessByPid");
    //        Console.WriteLine("4 - CreateProcess");
    //        Console.WriteLine("5 - KillProcess");
    //        key = Console.ReadKey();
    //        switch (key.KeyChar)
    //        {
    //            case '1':
    //                ShowAllProcessesFilter();
    //                break;
    //            case '2':
    //                ShowAllProcesses();
    //                break;
    //            case '3':
    //                GetProcessByPid();
    //                break;
    //            case '4':
    //                CreateProcess();
    //                break;
    //            default:
    //                Console.WriteLine("unknown operation");
    //                break;
    //        }
    //    } while (key.KeyChar != '0');


    //}

    //private void GetProcessByPid()
    //{
    //    try
    //    {
    //        Console.WriteLine("Enter pid: ");
    //        int pid = Convert.ToInt32(Console.ReadLine());
    //        var process = Process.GetProcessById(pid);
    //        Console.WriteLine($"{process.ProcessName}");
    //    }
    //    catch { }
    //}

    //private void CreateProcess()
    //{
    //    Console.WriteLine("Enter program name: ");
    //    string program = Console.ReadLine();
    //    if(program != null)
    //    {
    //        if(!Process.Start(program))
    //        Console.WriteLine(Process.Start(program).Id);

    //    }
    //}
    //private void ShowAllProcessesFilter()
    //{
    //    //Process[] processes = Process.GetProcesses();
    //    //foreach (var item in processes)
    //    //{
    //    //    try
    //    //    {
    //    //    Console.WriteLine($"{item.ProcessName} PID: {item.Id}");

    //    //    }
    //    //    catch (Exception){
    //    //        Console.WriteLine("Unknown process");
    //    //    }
    //    //}
    //    var processes = Process.GetProcesses().ToList();
    //    var processesGroup = processes.GroupBy(p => p.ProcessName).OrderByDescending(p => p.Count());
    //    foreach (var item in processesGroup)
    //    {
    //        Console.WriteLine($" {item.Key} - {item.Count()}");
    //    }

    //}

    //private void ShowAllProcesses()
    //{
    //    Process[] processes = Process.GetProcesses();
    //    foreach (var item in processes)
    //    {
    //        try
    //        {
    //            Console.WriteLine($"{item.ProcessName} PID: {item.Id}");

    //        }
    //        catch (Exception)
    //        {
    //            Console.WriteLine("Unknown process");
    //        }
    //    }
    //    //var processes = Process.GetProcesses().ToList();
    //    //var processesGroup = processes.GroupBy(p => p.ProcessName).OrderByDescending(p => p.Count());
    //    //foreach (var item in processesGroup)
    //    //{
    //    //    Console.WriteLine($" {item.Key} - {item.Count()}");
    //    //}

    //}
}
