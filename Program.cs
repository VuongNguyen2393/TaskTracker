using Microsoft.VisualBasic;
using TaskTracker.Commands.Dispatcher;
using TaskTracker.Commands.Handlers;
using TaskTracker.Models;
using TaskTracker.Utils;
using Task = TaskTracker.Models.DailyTask;

namespace TaskTracker;

class Program
{
    const string FILE_PATH = "Data/tasks.json";
    static void Main(string[] args)
    {
        var parser = new CommandParser();
        var jsonTaskRepository = new JsonTaskRepository(FILE_PATH);
        var taskService = new TaskService(jsonTaskRepository);
        var commandDict = new Dictionary<string, ICommandHandler>
        {
            {"add", new AddTaskCommandHandler(taskService)},
            {"update", new UpdateTaskCommandHandler(taskService)},
            {"delete", new DeleteTaskCommandHandler(taskService)},
            {"list", new ListTaskCommandHandler(taskService)},
            {"mark", new MarkStatusCommandHandler(taskService)}
        };
        var dispatcher = new CommandDispatcher(commandDict);
        ConsoleHelper.PrintBanner();
        while (true)
        {
            Console.Write("> ");
            var input = Console.ReadLine();
            if (input == null)
            {
                ConsoleHelper.PrintError("Invalid command");
                return;
            }
            var command = parser.Parse(input);
            if (command == null)
            {
                ConsoleHelper.PrintError("Invalid command");
                return;
            }
            dispatcher.Dispatch(command);
        }
    }
}
