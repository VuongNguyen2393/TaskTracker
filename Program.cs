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
                UpdateTask(input);
                return;
            case "delete":
                DeleteTask(input);
                return;
            case "list":
                ListTasks(input);
                return;
            case "mark-in-progress":
                MarkStatus(input, "in-progress");
                return;
            case "mark-done":
                MarkStatus(input, "done");
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

    private static void UpdateTask(string input)
    {
        var splittedInput = input.Split('"');
        var targetId = int.Parse(splittedInput[0].Split()[1]);
        var targetTask = _writer.GetById<MyTask>(t => t.Id == targetId);
        if (targetTask == null)
        {
            System.Console.WriteLine("Task is not found");
            return;
        }
        targetTask.Description = splittedInput[1];
        targetTask.UpdatedAt = DateTime.Now;
        _writer.Update<MyTask>(t => t.Id == targetId, targetTask);
    }

    private static void DeleteTask(string input)
    {
        var splittedInput = input.Split();
        var targetId = int.Parse(splittedInput[1]);
        _writer.Delete<MyTask>(t => t.Id == targetId);
    }

    private static void ListTasks(string input)
    {
        var splittedInput = input.Split();
        var tasks = _writer.GetAll<MyTask>();
        if (splittedInput.Count() == 1)
        {
            DisplayAllTasks(tasks);
        }
        else if (splittedInput.Count() == 2)
        {
            List<MyTask> targetTasks;
            switch (splittedInput[1].ToLower())
            {
                case "done":
                    targetTasks = tasks.Where(t => t.Status == "done").ToList();
                    DisplayStatusTasks(targetTasks);
                    return;
                case "todo":
                    targetTasks = tasks.Where(t => t.Status == "todo").ToList();
                    DisplayStatusTasks(targetTasks);
                    return;
                case "in-progress":
                    targetTasks = tasks.Where(t => t.Status == "in-progress").ToList();
                    DisplayStatusTasks(targetTasks);
                    return;
                default:
                    System.Console.WriteLine("Invalid command");
                    return;
            }
        }
        else
        {
            System.Console.WriteLine("Invalid command");
        }
    }

    private static void DisplayAllTasks(List<MyTask> tasks)
    {
        foreach (var myTask in tasks)
        {
            if (tasks.IndexOf(myTask) == 0)
            {
                System.Console.WriteLine("\n");
                System.Console.WriteLine(new string('*', 20));
            }
            System.Console.WriteLine($"Description: {myTask.Description}");
            System.Console.WriteLine($"Status: {myTask.Status}");
            System.Console.WriteLine($"Created At: {myTask.CreatedDate}");
            if (myTask.UpdatedAt != null)
            {
                System.Console.WriteLine($"Updated At: {myTask.UpdatedAt}\n");
            }
            else
            {
                System.Console.WriteLine("\n");
            }

            if (tasks.IndexOf(myTask) == tasks.Count() - 1)
            {
                System.Console.WriteLine(new string('*', 20));
            }

        }
    }

    private static void DisplayStatusTasks(List<MyTask> tasks)
    {
        foreach (var myTask in tasks)
        {
            if (tasks.IndexOf(myTask) == 0)
            {
                System.Console.WriteLine("\n");
                System.Console.WriteLine(new string('*', 20));
            }
            System.Console.WriteLine($"Description: {myTask.Description}");
            System.Console.WriteLine($"Created At: {myTask.CreatedDate}");
            if (myTask.UpdatedAt != null)
            {
                System.Console.WriteLine($"Updated At: {myTask.UpdatedAt}\n");
            }
            else
            {
                System.Console.WriteLine("\n");
            }
            if (tasks.IndexOf(myTask) == tasks.Count() - 1)
            {
                System.Console.WriteLine(new string('*', 20));
            }
        }
    }

    private static void MarkStatus(string input, string updatedStatus)
    {
        var splittedInput = input.Split();
        var targetId = int.Parse(splittedInput[1]);
        var targetTask = _writer.GetById<MyTask>(t => t.Id == targetId);
        if (targetTask == null)
        {
            System.Console.WriteLine("Task is not found");
            return;
        }
        targetTask.Status = updatedStatus;
        targetTask.UpdatedAt = DateTime.Now;
        _writer.Update<MyTask>(t => t.Id == targetId, targetTask);
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
