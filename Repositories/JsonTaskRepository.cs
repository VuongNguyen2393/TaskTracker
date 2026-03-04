using System.Text.Json;
using TaskTracker.Model;

namespace TaskTracker;

public class JsonTaskRepository : ITaskTracker
{
  private string _filePath;
  public JsonTaskRepository(string filePath)
  {
    _filePath = filePath;
    if (!File.Exists(_filePath))
    {
      File.WriteAllText(_filePath, "[]");
    }
    ;
  }

  public List<DailyTask> GetAll()
  {
    var json = File.ReadAllText(_filePath);
    return JsonSerializer.Deserialize<List<DailyTask>>(json) ?? new List<DailyTask>();
  }

  public DailyTask? GetById(int id)
  {
    var tasks = GetAll();
    return tasks.FirstOrDefault(t => t.Id == id);
  }
  public void Save(List<DailyTask> data)
  {
    var options = new JsonSerializerOptions
    {
      WriteIndented = true
    };

    var serializedData = JsonSerializer.Serialize(data, options);
    File.WriteAllText(_filePath, serializedData);
  }

  public void Add(DailyTask item)
  {
    var json = GetAll();
    json.Add(item);
    Save(json);
  }

  public void Delete(int id)
  {
    var tasks = GetAll();
    var targetTask = tasks.FirstOrDefault(t => t.Id == id);
    if (targetTask == null)
    {
      System.Console.WriteLine("Task not found");
      return;
    }
    tasks.Remove(targetTask);
    Save(tasks);
  }

  public void Update(DailyTask updatedData)
  {
    var tasks = GetAll();
    var idxTask = tasks.FindIndex(t => t.Id == updatedData.Id);
    if (idxTask < 0)
    {
      System.Console.WriteLine("Task not found");
      return;
    }
    tasks[idxTask] = updatedData;
    Save(tasks);
  }
}