using TaskHub.Models;
using TaskHub.Storage;

namespace TaskHub.Services;

public class TaskService
{
    private readonly ITaskRepository _repository;
    private readonly FileService _fileService;

    public TaskService(ITaskRepository repository, FileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }

    public void AddTask(string title, string description, 
                        Priority priority, DateTime deadline)
    {
        var task = new TaskItem(
            _repository.NextId(), title, description,
            priority, deadline, Status.New);
        
        _repository.Add(task);
    }

    public List<TaskItem> GetAll() => _repository.GetAll();

    public List<TaskItem> GetDone() =>
        _repository.Find(t => t.Status == Status.Done);

    public List<TaskItem> GetPending() =>
        _repository.Find(t => t.Status != Status.Done);

    public List<TaskItem> GetHighPriority() =>
        _repository.Find(t => t.Priority == Priority.High);

    public List<TaskItem> SearchByTitle(string query) =>
        _repository.Find(t => 
            t.Title.Contains(query, StringComparison.OrdinalIgnoreCase));

    public List<TaskItem> SearchByStatus(Status status) =>
        _repository.Find(t => t.Status == status);

    public List<TaskItem> SearchByPriority(Priority priority) =>
        _repository.Find(t => t.Priority == priority);

    public bool EditTask(int id, string? title, string? description,
                         Priority? priority, Status? status)
    {
        var task = _repository.GetById(id);
        if (task == null) return false;

        if (title != null)       task.Title = title;
        if (description != null) task.Description = description;
        if (priority.HasValue)   task.Priority = priority.Value;
        if (status.HasValue)     task.Status = status.Value;

        _repository.Update(task);
        return true;
    }

    public bool DeleteTask(int id) => _repository.Remove(id);

    public void PrintStats()
    {
        var all = _repository.GetAll();
        Console.WriteLine($"Всего задач: {all.Count}");
        Console.WriteLine($"Выполнено: {all.Count(t => t.Status == Status.Done)}");
        Console.WriteLine($"Просрочено: {all.Count(t => t.IsOverdue())}");
        Console.WriteLine("По приоритетам:");
        foreach (Priority p in Enum.GetValues<Priority>())
            Console.WriteLine($"  {p}: {all.Count(t => t.Priority == p)}");
    }

    public async Task SaveAsync() => 
        await _fileService.SaveAsync(_repository.GetAll());

    public async Task LoadAsync()
    {
        var tasks = await _fileService.LoadAsync();
        foreach (var t in tasks)
            _repository.Add(t);
    }
}
