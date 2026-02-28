namespace TaskTracker.Model
{
  public class MyTask
  {
    public MyTask()
    {
      CreatedDate = DateTime.Now;
      Status = "todo";
    }
    public int Id { get; set; }
    public string? Description { get; set; }
    public string Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedAt { get; set; }
  }
}