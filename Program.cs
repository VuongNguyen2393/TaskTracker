using TaskTracker.Model;

namespace TaskTracker;

class Program
{
    private static JSONWriter _writer;
    const string FILE_PATH = "JsonStoredFile.json";
    public Program(JSONWriter writer)
    {
        _writer = writer;
    }
    static void Main(string[] args)
    {
        var writer = new JSONWriter(FILE_PATH);
        var program = new Program(writer);
        program.Run();
    }

    void Run()
    {
        System.Console.WriteLine("What do you want to do?");

        //Read the input
        var inputStr = Console.ReadLine();

        //Extract the command
        var command = ExtractCommand(inputStr);

        //Route the command to the method
        RouteCommand(command, inputStr);

    }

    private static string ExtractCommand(string input)
    {
        return input.Split()[0].ToLower();
    }

    private static void RouteCommand(string command, string input)
    {
        switch (command)
        {
            case "add":
                AddTask(input);
                return;
            case "update":
                return;
            case "delete":
                return;
            case "list":
                return;
            case "mark-in-progress":
                return;
            case "mark-done":
                return;
            default:
                System.Console.WriteLine("Invalid Command");
                return;
        }
    }

    private static void AddTask(string input)
    {
        var splittedInput = input.Split('"');
        if (splittedInput.Length != 3)
        {
            System.Console.WriteLine("Invalid Command");
            return;
        }
        var description = splittedInput[1];
        var newTask = new MyTask()
        {
            Id = CreateNextId(),
            Description = description
        };
        _writer.Add(newTask);
    }

    private static int CreateNextId()
    {
        var tasks = _writer.GetAll<MyTask>();
        if (tasks.Count == 0)
        {
            return 1;
        }
        return tasks.Max(t => t.Id) + 1;
    }

}
