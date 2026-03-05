using System.Data.Common;
using TaskTracker.Models;

namespace TaskTracker;

public interface ITaskSerice
{
  void AddTask(string description);
  void UpdateTask(int id, string description);
  void DeleteTask(int id);
  void DisplayAllTasks();
  void DisplayStatusTasks(string status);
  void MarkStatus(int id, string updatedStatus);
  int CreateNextId();
}