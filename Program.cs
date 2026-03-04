using TaskTracker.Model;
using Task = TaskTracker.Model.DailyTask;

namespace TaskTracker;

class Program
{
    private ITaskSerice _taskService;
    private bool _stopFlag = false;
    const string FILE_PATH = "Data/tasks.json";
    public Program(ITaskSerice taskService)
    {
        _taskService = taskService;
    }
    static void Main(string[] args)
    {
        var taskRepository = new JsonTaskRepository(FILE_PATH);
        var taskService = new TaskService(taskRepository);
        var program = new Program(taskService);
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
                _taskService.AddTask(input);
                return;
            case "update":
                _taskService.UpdateTask(input);
                return;
            case "delete":
                _taskService.DeleteTask(input);
                return;
            case "list":
                _taskService.ListTasks(input);
                return;
            case "mark-in-progress":
                _taskService.MarkStatus(input, "in-progress");
                return;
            case "mark-done":
                _taskService.MarkStatus(input, "done");
                return;
            case "exit":
                _stopFlag = true;
                return;
            default:
                System.Console.WriteLine("Invalid Command\n");
                return;
        }
    }
}
