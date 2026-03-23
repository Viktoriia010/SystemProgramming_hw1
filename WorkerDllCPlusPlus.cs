using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SystemProgramming;

internal class WorkerDllCPlusPlus
{
    [DllImport("Calculator.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr CreateCalculatorObject();

    [DllImport("Calculator.dll", CallingConvention = CallingConvention.Cdecl)]

    public static extern void DeleteCalculatorObject(IntPtr obj);

    [DllImport("Calculator.dll", CallingConvention = CallingConvention.Cdecl)]

    public static extern int Add(IntPtr obj, int a, int b);
    public void Run()
    {
        IntPtr objCalc = CreateCalculatorObject();
        Console.WriteLine(Add(objCalc, 4, 6));
        DeleteCalculatorObject(objCalc);
    }
}
