using TaskHub.Storage;

namespace TaskHub.Services;

public class DeadlineMonitor
{
    private readonly ITaskRepository _repository;
    private readonly CancellationTokenSource _cts = new();

    public DeadlineMonitor(ITaskRepository repository)
    {
        _repository = repository;
    }

    public void Start()
    {
        Task.Run(() => MonitorLoop(_cts.Token));
    }

    public void Stop() => _cts.Cancel();

    private async Task MonitorLoop(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromSeconds(30), token).ContinueWith(_ => { });

            if (token.IsCancellationRequested) break;

            var overdue = _repository.Find(t => t.IsOverdue());

            if (overdue.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n⚠ Просроченных задач: {overdue.Count}");
                foreach (var t in overdue)
                    Console.WriteLine($"  ! {t}");
                Console.ResetColor();
            }
        }
    }
}
