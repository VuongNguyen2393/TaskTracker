using TaskTracker.Models.Enums;

namespace TaskTracker.Models
{
  public class DailyTask
  {
    public DailyTask()
    {
      CreatedDate = DateTime.Now;
      Status = DailyTaskStatus.Todo;
    }
    public int Id { get; set; }
    public string? Description { get; set; }
    public string Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedAt { get; set; }
  }
}