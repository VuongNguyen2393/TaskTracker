namespace TaskTracker;

public class Command
{
  public required string Name { get; set; }
  public required string[] Arguments { get; set; }
}