using TaskTracker.Utils;

namespace TaskTracker;

public class UpdateTaskCommandHandler(ITaskSerice taskSerice) : ICommandHandler
{
  private readonly ITaskSerice _taskService = taskSerice;
  public void Handle(Command command)
  {
    if (command.Arguments.Length < 2 || !int.TryParse(command.Arguments[0], out int id))
    {
      ConsoleHelper.PrintError("Invalid update command. Please use format: update <id> <new description>");
      return;
    }
    var newDescription = string.Join(" ", command.Arguments.Skip(1)).Trim('"');

    _taskService.UpdateTask(id, newDescription);
  }
}