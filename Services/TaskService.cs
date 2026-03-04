using TaskTracker.Model;

namespace TaskTracker;

public class TaskService : ITaskSerice
{
  ITaskRepository _taskRepository;
  public TaskService(ITaskRepository taskRepository)
  {
    _taskRepository = taskRepository;
  }
  public void AddTask(string input)
  {
    var splittedInput = input.Split('"');
    if (splittedInput.Length != 3)
    {
      System.Console.WriteLine("Invalid Command\n");
      return;
    }
    var description = splittedInput[1];
    var newTask = new DailyTask()
    {
      Id = CreateNextId(),
      Description = description
    };
    _taskRepository.Add(newTask);
  }

  public void UpdateTask(string input)
  {
    var splittedInput = input.Split('"');
    var targetId = int.Parse(splittedInput[0].Split()[1]);
    var targetTask = _taskRepository.GetById(targetId);
    if (targetTask == null)
    {
      System.Console.WriteLine("Task is not found\n");
      return;
    }
    targetTask.Description = splittedInput[1];
    targetTask.UpdatedAt = DateTime.Now;
    _taskRepository.Update(targetTask);
  }

  public void DeleteTask(string input)
  {
    var splittedInput = input.Split();
    var targetId = int.Parse(splittedInput[1]);
    _taskRepository.Delete(targetId);
  }

  public void ListTasks(string input)
  {
    var splittedInput = input.Split();
    var tasks = _taskRepository.GetAll();
    if (splittedInput.Count() == 1)
    {
      DisplayAllTasks(tasks);
    }
    else if (splittedInput.Count() == 2)
    {
      List<DailyTask> targetTasks;
      switch (splittedInput[1].ToLower())
      {
        case "done":
          targetTasks = tasks.Where(t => t.Status == "done").ToList();
          DisplayStatusTasks(targetTasks);
          return;
        case "todo":
          targetTasks = tasks.Where(t => t.Status == "todo").ToList();
          DisplayStatusTasks(targetTasks);
          return;
        case "in-progress":
          targetTasks = tasks.Where(t => t.Status == "in-progress").ToList();
          DisplayStatusTasks(targetTasks);
          return;
        default:
          System.Console.WriteLine("Invalid command\n");
          return;
      }
    }
    else
    {
      System.Console.WriteLine("Invalid command\n");
    }
  }

  public void DisplayAllTasks(List<DailyTask> tasks)
  {
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

  public void DisplayStatusTasks(List<DailyTask> tasks)
  {
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

  public void MarkStatus(string input, string updatedStatus)
  {
    var splittedInput = input.Split();
    var targetId = int.Parse(splittedInput[1]);
    var targetTask = _taskRepository.GetById(targetId);
    if (targetTask == null)
    {
      System.Console.WriteLine("Task is not found\n");
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