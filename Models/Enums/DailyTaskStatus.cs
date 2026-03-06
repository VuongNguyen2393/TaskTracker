namespace TaskTracker.Models.Enums
{
  public static class DailyTaskStatus
  {
    public const string Todo = "todo";
    public const string InProgress = "in-progress";
    public const string Done = "done";
    public static readonly List<string> ValidStatus = [Todo, InProgress, Done];
  }
}