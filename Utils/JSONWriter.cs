using System.Text.Json;
using TaskTracker.Model;

namespace TaskTracker;

public class JSONWriter
{
  private string _filePath;
  public JSONWriter(string filePath)
  {
    _filePath = filePath;
    if (!File.Exists(_filePath))
    {
      File.WriteAllText(_filePath, "[]");
    }
    ;
  }

  public List<T> GetAll<T>()
  {
    var json = File.ReadAllText(_filePath);
    return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
  }

  public T? GetById<T>(Func<T, bool> predicate)
  {
    var tasks = GetAll<T>();
    return tasks.FirstOrDefault(predicate);
  }
  public void SaveAll<T>(List<T> data)
  {
    var options = new JsonSerializerOptions
    {
      WriteIndented = true
    };

    var serializedData = JsonSerializer.Serialize(data, options);
    File.WriteAllText(_filePath, serializedData);
  }

  public void Add<T>(T item)
  {
    var json = GetAll<T>();
    json.Add(item);
    SaveAll<T>(json);
  }

  public void Delete<T>(Func<T, bool> predicate)
  {
    var tasks = GetAll<T>();
    var targetTask = tasks.FirstOrDefault(predicate);
    if (targetTask == null)
    {
      System.Console.WriteLine("Task not found");
      return;
    }
    tasks.Remove(targetTask);
    SaveAll(tasks);
  }

  public void Update<T>(Func<T, bool> prediate, T updatedData)
  {
    var tasks = GetAll<T>();
    var idxTask = tasks.FindIndex(t => prediate(t));
    if (idxTask < 0)
    {
      System.Console.WriteLine("Task not found");
      return;
    }
    tasks[idxTask] = updatedData;
    SaveAll<T>(tasks);
  }
}