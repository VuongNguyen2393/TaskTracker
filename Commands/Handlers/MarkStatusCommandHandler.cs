namespace TaskTracker.Commands.Handlers
{
  public class MarkStatusCommandHandler(ITaskSerice taskSerice) : ICommandHandler
  {
    private readonly ITaskSerice _taskService = taskSerice;
    public void Handle(Command command)
    {
      if (command.Arguments.Length != 2 || !int.TryParse(command.Arguments[0], out int id))
      {
        System.Console.WriteLine("Invalid command.");
        return;
      }
      _taskService.MarkStatus(id, command.Arguments[1]);
    }
  }
}