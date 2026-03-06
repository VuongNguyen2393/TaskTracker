using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography;
using TaskTracker.Models;
using TaskTracker.Models.Enums;
using TaskTracker.Utils;

namespace TaskTracker;

public class TaskService : ITaskSerice
{
  ITaskRepository _taskRepository;
  public TaskService(ITaskRepository taskRepository)
  {
    _taskRepository = taskRepository;
  }
  public void AddTask(string description)
  {
    var newTask = new DailyTask()
    {
      Id = CreateNextId(),
      Description = description
    };
    _taskRepository.Add(newTask);
    ConsoleHelper.PrintSuccess("Task is added successfully");
  }

  public void UpdateTask(int id, string description)
  {
    var targetTask = _taskRepository.GetById(id);
    if (targetTask == null)
    {
      ConsoleHelper.PrintWarning("Task is not found");
      return;
    }
    targetTask.Description = description;
    targetTask.UpdatedAt = DateTime.Now;
    _taskRepository.Update(targetTask);
    ConsoleHelper.PrintSuccess("Task updated successfully");

  }

  public void DeleteTask(int id)
  {
    var tasks = _taskRepository.GetAll();
    var targetTask = tasks.FirstOrDefault(t => t.Id == id);
    if (targetTask == null)
    {
      ConsoleHelper.PrintWarning("Task is not found");
      return;
    }
    _taskRepository.Delete(id);
    ConsoleHelper.PrintSuccess("Task is deleted successfully");

  }

  public void DisplayAllTasks()
  {
    var dailyTasks = _taskRepository.GetAll();

    ConsoleHelper.PrintHeader($"{"ID",-4} {"Description",-30} Status");
    System.Console.WriteLine(new string('=', 60));
    foreach (var dailyTask in dailyTasks)
    {
      System.Console.Write($"{dailyTask.Id,-4}");
      ConsoleHelper.PrintDescription($"{dailyTask.Description,-30}");
      ConsoleHelper.PrintStatus(dailyTask.Status);
    }
    System.Console.WriteLine();
  }

  public void DisplayStatusTasks(string status)
  {
    if (!DailyTaskStatus.ValidStatus.Contains(status.ToLower()))
    {
      ConsoleHelper.PrintError("Invalid Status");
      return;
    }
    var dailyTasks = _taskRepository.GetAll().Where(t => t.Status == status).ToList();
    ConsoleHelper.PrintHeader($"{"ID",-4} {"Description",-30} {"CreatedDatetime",-30} UpdatedDatetime");
    System.Console.WriteLine(new string('=', 90));
    foreach (var dailyTask in dailyTasks)
    {
      System.Console.Write($"{dailyTask.Id,-4}");
      ConsoleHelper.PrintDescription($"{dailyTask.Description,-30}");
      System.Console.WriteLine($"{dailyTask.CreatedDate,-30} {dailyTask.UpdatedAt}");
    }
    System.Console.WriteLine();
  }

  public void MarkStatus(int id, string updatedStatus)
  {
    var targetTask = _taskRepository.GetById(id);
    if (targetTask == null)
    {
      ConsoleHelper.PrintWarning("Task is not found");
      return;
    }
    if (!DailyTaskStatus.ValidStatus.Contains(updatedStatus.ToLower()))
    {
      ConsoleHelper.PrintError("Invalid Status");
      return;
    }
    targetTask.Status = updatedStatus;
    targetTask.UpdatedAt = DateTime.Now;
    _taskRepository.Update(targetTask);
    ConsoleHelper.PrintSuccess("Update task successfully");
  }

  public int CreateNextId()
  {
    var tasks = _taskRepository.GetAll();
    if (tasks.Count == 0)
    {
      return 1;
    }
    return tasks.Max(t => t.Id) + 1;
  }
}