namespace TaskTracker;

public class AddTaskCommandHandler(ITaskSerice taskSerice) : ICommandHandler
{
  private readonly ITaskSerice _taskService = taskSerice;
  public void Handle(Command command)
  {
    if (command.Arguments.Length == 0)
    {
      System.Console.WriteLine("Missing description for the task");
      return;
    }

    var description = string.Join(" ", command.Arguments).Trim('"');
    _taskService.AddTask(description);
  }
}