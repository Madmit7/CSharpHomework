using TaskHub.Services;
using TaskHub.Storage;
using TaskHub.UI;

var repository = new TaskRepository();
var fileService = new FileService("tasks.txt");
var taskService = new TaskService(repository, fileService);
var menu = new ConsoleMenu(taskService);
var monitor = new DeadlineMonitor(repository);

monitor.Start();

try
{
    await menu.RunAsync();
}
finally
{
    monitor.Stop();
    fileService.Dispose();
}
