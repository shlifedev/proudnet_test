using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Logger
{
    public static string GetTime()
    {
        return $"[{System.DateTime.Now.ToString("hh:mm:ss")}]";
    }
    public static void Log(string title, string value)
    {
        var org = Console.ForegroundColor;
        Console.Write($"{GetTime()}");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"[{title}] ");
        Console.ForegroundColor = org;
        Console.Write($"{value}\n");
    }
    public static void Log(Object obj, string value)
    {
        string title = title = obj.GetType().Name;
        var org = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"{GetTime()}");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"[{title}] ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"{value}\n");
        Console.ForegroundColor = org;
    }

    public static void Exception(Object obj, string value)
    {
        string title = title = obj.GetType().Name;
        var org = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"{GetTime()}");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"[{title}] ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"{value}\n");
        Console.ForegroundColor = org;
    }

    public static void Error(string title, string value)
    {
        var org = Console.ForegroundColor;
        Console.Write($"{GetTime()}");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"[{title}] ");
        Console.ForegroundColor = org;
        Console.Write($"{value}\n");
    }
    public static void Error(Object obj, string value)
    {
        string title = title = obj.GetType().Name;
        var org = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"{GetTime()}");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"[{title} - Error] ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"{value}\n");
        Console.ForegroundColor = org;
    }
}
