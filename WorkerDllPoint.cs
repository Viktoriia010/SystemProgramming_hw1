using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SystemProgramming;

internal class WorkerDllPoint
{
    [DllImport("Point.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr CreatePointManager();

    [DllImport("Point.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void DestroyPointManager(IntPtr obj);

    [DllImport("Point.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void PointManager_AddPoint(IntPtr obj, int x, int y);

    [DllImport("Point.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void PointManager_RemovePoint(IntPtr obj, int index);

    [DllImport("Point.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern void PointManager_GetPoint(IntPtr obj, int index, ref int x, ref int y);

    [DllImport("Point.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int PointManager_Count(IntPtr obj);
    public void Run()
    {
        IntPtr obj = CreatePointManager();

        PointManager_AddPoint(obj, 10, 20);
        PointManager_AddPoint(obj, 30, 40);
        PointManager_AddPoint(obj, 50, 60);

        int count = PointManager_Count(obj);
        Console.WriteLine($"Count: {count}");

        for (int i = 0; i < count; i++)
        {
            int x = 0, y = 0;
            PointManager_GetPoint(obj, i, ref x, ref y);
            Console.WriteLine($"Point {i}: x = {x}, y = {y}");
        }

        PointManager_RemovePoint(obj, 1);

        count = PointManager_Count(obj);
        Console.WriteLine($"Count: {count}");

        for (int i = 0; i < count; i++)
        {
            int x = 0, y = 0;
            PointManager_GetPoint(obj, i, ref x, ref y);
            Console.WriteLine($"Point {i}: x = {x}, y = {y}");
        }

        DestroyPointManager(obj);
    }
}
