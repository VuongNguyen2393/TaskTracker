using System.Text.Json;
using TaskTracker.Model;

namespace TaskTracker;

public class JSONWriter : Iwriter
{
  public void WriteObject<T>(string filePath, T data)
  {
    var options = new JsonSerializerOptions
    {
      WriteIndented = true
    };

    var serializedData = JsonSerializer.Serialize(data, options);
    File.WriteAllText(filePath, serializedData);
  }

  public List<T> ReadObject<T>(string filePath)
  {
    if (!File.Exists(filePath))
    {
      return new List<T>();
    }

    var json = File.ReadAllText(filePath);

    if (string.IsNullOrWhiteSpace(json))
    {
      return new List<T>();
    }

    return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
  }

  public int GenerateNextId(string filePath)
  {
    if (!File.Exists(filePath))
    {
      return 1;
    }

    var tasks = ReadObject<MyTask>(filePath);
    if (tasks.Count() == 0)
    {
      return 1;
    }
    return tasks.Max(t => t.Id) + 1;
  }
}