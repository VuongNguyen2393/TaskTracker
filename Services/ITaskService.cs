using TaskTracker.Model;

namespace TaskTracker;

public interface ITaskSerice
{
  void AddTask(string input);
  void UpdateTask(string input);
  void DeleteTask(string input);
  void ListTasks(string input);
  void DisplayAllTasks(List<DailyTask> tasks);
  void DisplayStatusTasks(List<DailyTask> tasks);
  void MarkStatus(string input, string updatedStatus);
  int CreateNextId();
}