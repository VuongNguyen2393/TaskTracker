using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography;
using TaskTracker.Models;
using TaskTracker.Models.Enums;

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
    System.Console.WriteLine("Task is added successfully");
  }

  public void UpdateTask(int id, string description)
  {
    var targetTask = _taskRepository.GetById(id);
    if (targetTask == null)
    {
      System.Console.WriteLine("Task is not found\n");
      return;
    }
    targetTask.Description = description;
    targetTask.UpdatedAt = DateTime.Now;
    _taskRepository.Update(targetTask);
    System.Console.WriteLine("Task updated successfully");
  }

  public void DeleteTask(int id)
  {
    var tasks = _taskRepository.GetAll();
    var targetTask = tasks.FirstOrDefault(t => t.Id == id);
    if (targetTask == null)
    {
      System.Console.WriteLine("Task not found");
    }
    _taskRepository.Delete(id);
    System.Console.WriteLine("Task is deleted successfully");
  }

  public void DisplayAllTasks()
  {
    var tasks = _taskRepository.GetAll();
    foreach (var Task in tasks)
    {
      if (tasks.IndexOf(Task) == 0)
      {
        System.Console.WriteLine("\n");
        System.Console.WriteLine(new string('*', 20));
      }
      System.Console.WriteLine($"Id: {Task.Id}");
      System.Console.WriteLine($"Description: {Task.Description}");
      System.Console.WriteLine($"Status: {Task.Status}");
      System.Console.WriteLine($"Created At: {Task.CreatedDate}");
      if (Task.UpdatedAt != null)
      {
        System.Console.WriteLine($"Updated At: {Task.UpdatedAt}\n");
      }
      else
      {
        System.Console.WriteLine("\n");
      }

      if (tasks.IndexOf(Task) == tasks.Count() - 1)
      {
        System.Console.WriteLine($"{new string('*', 20)}\n");
      }

    }
  }

  public void DisplayStatusTasks(string status)
  {
    if (!DailyTaskStatus.ValidStatus.Contains(status.ToLower()))
    {
      System.Console.WriteLine("Invalid Status");
      return;
    }
    var tasks = _taskRepository.GetAll().Where(t => t.Status == status).ToList();
    foreach (var Task in tasks)
    {
      if (tasks.IndexOf(Task) == 0)
      {
        System.Console.WriteLine("\n");
        System.Console.WriteLine(new string('*', 20));
      }
      System.Console.WriteLine($"Id: {Task.Id}");
      System.Console.WriteLine($"Description: {Task.Description}");
      System.Console.WriteLine($"Created At: {Task.CreatedDate}");
      if (Task.UpdatedAt != null)
      {
        System.Console.WriteLine($"Updated At: {Task.UpdatedAt}\n");
      }
      else
      {
        System.Console.WriteLine("\n");
      }
      if (tasks.IndexOf(Task) == tasks.Count() - 1)
      {
        System.Console.WriteLine($"{new string('*', 20)}\n");
      }
    }
  }

  public void MarkStatus(int id, string updatedStatus)
  {
    var targetTask = _taskRepository.GetById(id);
    if (targetTask == null)
    {
      System.Console.WriteLine("Task is not found\n");
      return;
    }
    if (!DailyTaskStatus.ValidStatus.Contains(updatedStatus.ToLower()))
    {
      System.Console.WriteLine("Invalid status.");
      return;
    }
    targetTask.Status = updatedStatus;
    targetTask.UpdatedAt = DateTime.Now;
    _taskRepository.Update(targetTask);
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