using TaskHub.Models;

namespace TaskHub.Storage;

public class TaskRepository : ITaskRepository
{
    private readonly Dictionary<int, TaskItem> _tasks = new();
    private int _nextId = 1;

    public void Add(TaskItem task) => _tasks[task.Id] = task;

    public bool Remove(int id) => _tasks.Remove(id);

    public TaskItem? GetById(int id) =>
        _tasks.TryGetValue(id, out var task) ? task : null;

    public List<TaskItem> GetAll() => _tasks.Values.ToList();

    public List<TaskItem> Find(Predicate<TaskItem> predicate)
    {
        var result = new List<TaskItem>();
        foreach (var task in _tasks.Values)
            if (predicate(task))
                result.Add(task);
        return result;
    }

    public void Update(TaskItem task)
    {
        if (_tasks.ContainsKey(task.Id))
            _tasks[task.Id] = task;
    }

    public int NextId() => _nextId++;
}
