namespace TaskTracker;

public class DeleteTaskCommandHandler(ITaskSerice taskSerice) : ICommandHandler
{
  private readonly ITaskSerice _taskService = taskSerice;
  public void Handle(Command command)
  {
    if (command.Arguments.Length != 1 || !int.TryParse(command.Arguments[0], out int id))
    {
      System.Console.WriteLine("Invalid Command.");
      return;
    }
    _taskService.DeleteTask(id);
  }
}