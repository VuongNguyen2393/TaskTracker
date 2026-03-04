using TaskTracker.Model;

namespace TaskTracker;

public interface ITaskTracker
{
  public List<DailyTask> GetAll();
  public DailyTask? GetById(int id);
  public void Save(List<DailyTask> data);
  public void Add(DailyTask item);
  public void Delete(int id);
  public void Update(DailyTask updatedData);
}