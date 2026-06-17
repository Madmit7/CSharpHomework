using TaskHub.Models;

namespace TaskHub.Services;

public class FileService : IDisposable
{
    private readonly string _filePath;
    private bool _disposed;

    public FileService(string filePath)
    {
        _filePath = filePath;
    }

    public async Task SaveAsync(List<TaskItem> tasks)
    {
        var lines = tasks.Select(t =>
            $"{t.Id}|{t.Title}|{t.Description}|{t.Priority}|{t.Deadline:O}|{t.Status}");
        
        await File.WriteAllLinesAsync(_filePath, lines);
    }

    public async Task<List<TaskItem>> LoadAsync()
    {
        var tasks = new List<TaskItem>();

        if (!File.Exists(_filePath))
            return tasks;

        var lines = await File.ReadAllLinesAsync(_filePath);

        foreach (var line in lines)
        {
            var parts = line.Split('|');
            if (parts.Length != 6) continue;

            tasks.Add(new TaskItem(
                id:          int.Parse(parts[0]),
                title:       parts[1],
                description: parts[2],
                priority:    Enum.Parse<Priority>(parts[3]),
                deadline:    DateTime.Parse(parts[4]),
                status: Enum.Parse<Status>(parts[5])
            ));
        }

        return tasks;
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
    }
}
