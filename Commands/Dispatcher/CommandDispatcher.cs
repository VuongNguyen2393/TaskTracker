using TaskTracker.Utils;

namespace TaskTracker.Commands.Dispatcher
{
  public class CommandDispatcher(Dictionary<string, ICommandHandler> commandHandlerDict)
  {
    private readonly Dictionary<string, ICommandHandler> _commandHandlerDict = commandHandlerDict;
    public void Dispatch(Command command)
    {
      if (command == null || !_commandHandlerDict.TryGetValue(command.Name, out ICommandHandler? handler))
      {
        ConsoleHelper.PrintError("Invalid command");
        return;
      }
      handler.Handle(command);
    }
  }
}