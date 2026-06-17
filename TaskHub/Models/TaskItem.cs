namespace TaskHub.Models;

public class TaskItem
{
    public int Id { get; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public DateTime Deadline { get; set; }
    public Status Status { get; set; }

    public TaskItem(int id, string title, string description,
                    Priority priority, DateTime deadline, Status status)
    {
        Id = id;
        Title = title;
        Description = description;
        Priority = priority;
        Deadline = deadline;
        Status = status;
    }

    public bool IsOverdue() =>
        Status != Status.Done && Deadline < DateTime.Now;

    public override string ToString() =>
        $"[{Id}] {Title} | {Priority} | {Status} | до {Deadline:dd.MM.yyyy}";
}
