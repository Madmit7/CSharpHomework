using TaskHub.Models;

namespace TaskHub.Storage;

public interface ITaskRepository
{
    void Add(TaskItem task);
    bool Remove(int id);
    TaskItem? GetById(int id);
    List<TaskItem> GetAll();
    List<TaskItem> Find(Predicate<TaskItem> predicate);
    void Update(TaskItem task);
    int NextId();
}
