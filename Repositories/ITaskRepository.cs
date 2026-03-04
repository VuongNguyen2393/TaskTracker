using TaskTracker.Model;

namespace TaskTracker;

public interface ITaskRepository
{
  List<DailyTask> GetAll();
  DailyTask? GetById(int id);
  void Save(List<DailyTask> data);
  void Add(DailyTask item);
  void Delete(int id);
  void Update(DailyTask updatedData);
}