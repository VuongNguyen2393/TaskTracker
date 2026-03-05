namespace TaskTracker;

public class CommandParser
{
  public Command? Parse(string input)
  {
    if (string.IsNullOrWhiteSpace(input))
    {
      return null;
    }
    var parts = input.Split(" ", 2);
    var commandName = parts[0].ToLower();
    var commandArgs = parts.Length > 1 ? parts[1].Split(" ") : Array.Empty<string>();
    return new Command()
    {
      Name = commandName,
      Arguments = commandArgs
    };
  }
}