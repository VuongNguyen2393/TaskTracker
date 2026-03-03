using TaskTracker.Model;

namespace TaskTracker;

class Program
{
    private JSONWriter _writer;
    private bool _stopFlag = false;
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
        while (!_stopFlag)
        {
            System.Console.WriteLine("What do you want to do?\n");

            //Read the input
            var inputStr = Console.ReadLine();

            //Extract the command
            string command;
            if (inputStr != null)
            {
                command = ExtractCommand(inputStr);
            }
            else
            {
                return;
            }

            //Route the command to the method
            RouteCommand(command, inputStr);
        }
    }

    private string ExtractCommand(string input)
    {
        return input.Split()[0].ToLower();
    }

    private void RouteCommand(string command, string input)
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
            case "exit":
                _stopFlag = true;
                return;
            default:
                System.Console.WriteLine("Invalid Command\n");
                return;
        }
    }

    private void AddTask(string input)
    {
        var splittedInput = input.Split('"');
        if (splittedInput.Length != 3)
        {
            System.Console.WriteLine("Invalid Command\n");
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

    private void UpdateTask(string input)
    {
        var splittedInput = input.Split('"');
        var targetId = int.Parse(splittedInput[0].Split()[1]);
        var targetTask = _writer.GetById<MyTask>(t => t.Id == targetId);
        if (targetTask == null)
        {
            System.Console.WriteLine("Task is not found\n");
            return;
        }
        targetTask.Description = splittedInput[1];
        targetTask.UpdatedAt = DateTime.Now;
        _writer.Update<MyTask>(t => t.Id == targetId, targetTask);
    }

    private void DeleteTask(string input)
    {
        var splittedInput = input.Split();
        var targetId = int.Parse(splittedInput[1]);
        _writer.Delete<MyTask>(t => t.Id == targetId);
    }

    private void ListTasks(string input)
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
                    System.Console.WriteLine("Invalid command\n");
                    return;
            }
        }
        else
        {
            System.Console.WriteLine("Invalid command\n");
        }
    }

    private void DisplayAllTasks(List<MyTask> tasks)
    {
        foreach (var myTask in tasks)
        {
            if (tasks.IndexOf(myTask) == 0)
            {
                System.Console.WriteLine("\n");
                System.Console.WriteLine(new string('*', 20));
            }
            System.Console.WriteLine($"Id: {myTask.Id}");
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
                System.Console.WriteLine($"{new string('*', 20)}\n");
            }

        }
    }

    private void DisplayStatusTasks(List<MyTask> tasks)
    {
        foreach (var myTask in tasks)
        {
            if (tasks.IndexOf(myTask) == 0)
            {
                System.Console.WriteLine("\n");
                System.Console.WriteLine(new string('*', 20));
            }
            System.Console.WriteLine($"Id: {myTask.Id}");
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
                System.Console.WriteLine($"{new string('*', 20)}\n");
            }
        }
    }

    private void MarkStatus(string input, string updatedStatus)
    {
        var splittedInput = input.Split();
        var targetId = int.Parse(splittedInput[1]);
        var targetTask = _writer.GetById<MyTask>(t => t.Id == targetId);
        if (targetTask == null)
        {
            System.Console.WriteLine("Task is not found\n");
            return;
        }
        targetTask.Status = updatedStatus;
        targetTask.UpdatedAt = DateTime.Now;
        _writer.Update<MyTask>(t => t.Id == targetId, targetTask);
    }

    private int CreateNextId()
    {
        var tasks = _writer.GetAll<MyTask>();
        if (tasks.Count == 0)
        {
            return 1;
        }
        return tasks.Max(t => t.Id) + 1;
    }

}
