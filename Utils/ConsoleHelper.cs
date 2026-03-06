using TaskTracker.Models.Enums;

namespace TaskTracker.Utils
{
  public static class ConsoleHelper
  {
    public static void PrintError(string message)
    {
      Console.ForegroundColor = ConsoleColor.Red;
      System.Console.WriteLine($"Error: {message}\n");
      Console.ResetColor();
    }

    public static void PrintWarning(string message)
    {
      Console.ForegroundColor = ConsoleColor.Yellow;
      System.Console.WriteLine($"Warning: {message}\n");
      Console.ResetColor();
    }

    public static void PrintSuccess(string message)
    {
      Console.ForegroundColor = ConsoleColor.Green;
      System.Console.WriteLine($"Info: {message}\n");
      Console.ResetColor();
    }

    public static void PrintBanner()
    {
      Console.ForegroundColor = ConsoleColor.DarkMagenta;
      System.Console.WriteLine("=============================");
      System.Console.WriteLine("==  TASK TRACKER PROJECT   ==");
      System.Console.WriteLine("=============================\n");
      Console.ResetColor();
    }

    public static void PrintHeader(string header)
    {
      PrintColor(ConsoleColor.Blue, header);
    }

    public static void PrintDescription(string description)
    {
      Console.ForegroundColor = ConsoleColor.Cyan;
      System.Console.Write(description);
      Console.ResetColor();
    }

    public static void PrintStatus(string status)
    {
      switch (status)
      {
        case DailyTaskStatus.Done:
          PrintColor(ConsoleColor.Green, status);
          return;
        case DailyTaskStatus.InProgress:
          PrintColor(ConsoleColor.Yellow, status);
          return;
        default:
          PrintColor(ConsoleColor.DarkRed, status);
          return;
      }
    }

    public static void PrintColor(ConsoleColor color, string message)
    {
      Console.ForegroundColor = color;
      System.Console.WriteLine(message);
      Console.ResetColor();
    }
  }
}