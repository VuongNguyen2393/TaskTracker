namespace TaskTracker;

public interface ICommandHandler
{
  public void Handle(Command command);
}