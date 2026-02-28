using TaskTracker.Model;

namespace TaskTracker;

public interface Iwriter
{
  public void WriteObject<T>(string filePath, T data);
  public List<T> ReadObject<T>(string filePath);
  public int GenerateNextId(string filePath);
}