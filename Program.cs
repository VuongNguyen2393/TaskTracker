using TaskTracker.Model;
using Task = TaskTracker.Model.DailyTask;

namespace TaskTracker;

class Program
{
    private JsonTaskRepository _writer;
    private bool _stopFlag = false;
    const string FILE_PATH = "Data/tasks.json";
    public Program(JsonTaskRepository writer)
    {
        _writer = writer;
    }
    static void Main(string[] args)
    {
        var writer = new JsonTaskRepository(FILE_PATH);
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
        var newTask = new Task()
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
        var targetTask = _writer.GetById(targetId);
        if (targetTask == null)
        {
            System.Console.WriteLine("Task is not found\n");
            return;
        }
        targetTask.Description = splittedInput[1];
        targetTask.UpdatedAt = DateTime.Now;
        _writer.Update(targetTask);
    }

    private void DeleteTask(string input)
    {
        var splittedInput = input.Split();
        var targetId = int.Parse(splittedInput[1]);
        _writer.Delete(targetId);
    }

    private void ListTasks(string input)
    {
        var splittedInput = input.Split();
        var tasks = _writer.GetAll();
        if (splittedInput.Count() == 1)
        {
            DisplayAllTasks(tasks);
        }
        else if (splittedInput.Count() == 2)
        {
            List<DailyTask> targetTasks;
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

    private void DisplayAllTasks(List<DailyTask> tasks)
    {
        foreach (var Task in tasks)
        {
            if (tasks.IndexOf(Task) == 0)
            {
                System.Console.WriteLine("\n");
                System.Console.WriteLine(new string('*', 20));
            }
            System.Console.WriteLine($"Id: {Task.Id}");
            System.Console.WriteLine($"Description: {Task.Description}");
            System.Console.WriteLine($"Status: {Task.Status}");
            System.Console.WriteLine($"Created At: {Task.CreatedDate}");
            if (Task.UpdatedAt != null)
            {
                System.Console.WriteLine($"Updated At: {Task.UpdatedAt}\n");
            }
            else
            {
                System.Console.WriteLine("\n");
            }

            if (tasks.IndexOf(Task) == tasks.Count() - 1)
            {
                System.Console.WriteLine($"{new string('*', 20)}\n");
            }

        }
    }

    private void DisplayStatusTasks(List<DailyTask> tasks)
    {
        foreach (var Task in tasks)
        {
            if (tasks.IndexOf(Task) == 0)
            {
                System.Console.WriteLine("\n");
                System.Console.WriteLine(new string('*', 20));
            }
            System.Console.WriteLine($"Id: {Task.Id}");
            System.Console.WriteLine($"Description: {Task.Description}");
            System.Console.WriteLine($"Created At: {Task.CreatedDate}");
            if (Task.UpdatedAt != null)
            {
                System.Console.WriteLine($"Updated At: {Task.UpdatedAt}\n");
            }
            else
            {
                System.Console.WriteLine("\n");
            }
            if (tasks.IndexOf(Task) == tasks.Count() - 1)
            {
                System.Console.WriteLine($"{new string('*', 20)}\n");
            }
        }
    }

    private void MarkStatus(string input, string updatedStatus)
    {
        var splittedInput = input.Split();
        var targetId = int.Parse(splittedInput[1]);
        var targetTask = _writer.GetById(targetId);
        if (targetTask == null)
        {
            System.Console.WriteLine("Task is not found\n");
            return;
        }
        targetTask.Status = updatedStatus;
        targetTask.UpdatedAt = DateTime.Now;
        _writer.Update(targetTask);
    }

    private int CreateNextId()
    {
        var tasks = _writer.GetAll();
        if (tasks.Count == 0)
        {
            return 1;
        }
        return tasks.Max(t => t.Id) + 1;
    }

}
