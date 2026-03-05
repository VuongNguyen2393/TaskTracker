namespace TaskTracker.Commands.Handlers
{
  public class ListTaskCommandHandler(ITaskSerice taskSerice) : ICommandHandler
  {
    private readonly ITaskSerice _taskService = taskSerice;
    public void Handle(Command command)
    {
      if (command.Arguments.Length == 0)
      {
        _taskService.DisplayAllTasks();
        return;
      }

      if (command.Arguments.Length > 1)
      {
        System.Console.WriteLine("Invalid command");
        return;
      }

      string status = command.Arguments[0];
      _taskService.DisplayStatusTasks(status);
    }
  }
}