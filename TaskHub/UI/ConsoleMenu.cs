using TaskHub.Models;
using TaskHub.Services;

namespace TaskHub.UI;

public class ConsoleMenu
{
    private readonly TaskService _service;

    public ConsoleMenu(TaskService service)
    {
        _service = service;
    }

    public async Task RunAsync()
    {
        await _service.LoadAsync();
        Console.WriteLine("TaskHub запущен. Данные загружены.");

        while (true)
        {
            PrintMenu();
            string choice = Console.ReadLine()!.Trim();

            switch (choice)
            {
                case "1": CreateTask(); break;
                case "2": ViewTasks(); break;
                case "3": EditTask(); break;
                case "4": DeleteTask(); break;
                case "5": SearchTasks(); break;
                case "6": _service.PrintStats(); break;
                case "7": await _service.SaveAsync(); 
                          Console.WriteLine("Сохранено."); break;
                case "0":
                    await _service.SaveAsync();
                    Console.WriteLine("До свидания!");
                    return;
                default:
                    Console.WriteLine("Неизвестная команда.");
                    break;
            }
        }
    }

    private void PrintMenu()
    {
        Console.WriteLine("\n--- TaskHub ---");
        Console.WriteLine("1. Создать задачу");
        Console.WriteLine("2. Просмотр задач");
        Console.WriteLine("3. Редактировать задачу");
        Console.WriteLine("4. Удалить задачу");
        Console.WriteLine("5. Поиск задач");
        Console.WriteLine("6. Статистика");
        Console.WriteLine("7. Сохранить");
        Console.WriteLine("0. Выход");
        Console.Write("Выбор: ");
    }

    private void CreateTask()
    {
        Console.Write("Название: ");
        string title = Console.ReadLine()!;

        Console.Write("Описание: ");
        string description = Console.ReadLine()!;

        Priority priority = ReadEnum<Priority>("Приоритет (Low/Medium/High)");
        
        Console.Write("Дедлайн (дд.мм.гггг): ");
        DateTime deadline = DateTime.ParseExact(
            Console.ReadLine()!, "dd.MM.yyyy", null);

        _service.AddTask(title, description, priority, deadline);
        Console.WriteLine("Задача создана.");
    }

    private void ViewTasks()
    {
        Console.WriteLine("\n1. Все  2. Выполненные  3. Невыполненные  4. Высокий приоритет");
        Console.Write("Выбор: ");
        string choice = Console.ReadLine()!;

        var tasks = choice switch
        {
            "1" => _service.GetAll(),
            "2" => _service.GetDone(),
            "3" => _service.GetPending(),
            "4" => _service.GetHighPriority(),
            _   => _service.GetAll()
        };

        PrintTasks(tasks);
    }

    private void EditTask()
    {
        Console.Write("Id задачи: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Некорректный Id.");
            return;
        }

        Console.Write("Новое название (Enter — пропустить): ");
        string? title = NullIfEmpty(Console.ReadLine());

        Console.Write("Новое описание (Enter — пропустить): ");
        string? description = NullIfEmpty(Console.ReadLine());

        Console.Write("Приоритет Low/Medium/High (Enter — пропустить): ");
        Priority? priority = TryParseEnum<Priority>(Console.ReadLine());

        Console.Write("Статус New/InProgress/Done (Enter — пропустить): ");
        Status? status = TryParseEnum<Status>(Console.ReadLine());

        bool ok = _service.EditTask(id, title, description, priority, status);
        Console.WriteLine(ok ? "Обновлено." : "Задача не найдена.");
    }

    private void DeleteTask()
    {
        Console.Write("Id задачи: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Некорректный Id.");
            return;
        }

        Console.WriteLine(_service.DeleteTask(id) ? "Удалено." : "Не найдено.");
    }

    private void SearchTasks()
    {
        Console.WriteLine("1. По названию  2. По статусу  3. По приоритету");
        Console.Write("Выбор: ");
        string choice = Console.ReadLine()!;

        List<TaskItem> results = choice switch
        {
            "1" => SearchByTitle(),
            "2" => SearchByStatus(),
            "3" => SearchByPriority(),
            _   => new List<TaskItem>()
        };

        PrintTasks(results);
    }

    private List<TaskItem> SearchByTitle()
    {
        Console.Write("Запрос: ");
        return _service.SearchByTitle(Console.ReadLine()!);
    }

   private List<TaskItem> SearchByStatus()
    {
        Status status = ReadEnum<Status>("Статус (New/InProgress/Done)");
        return _service.SearchByStatus(status);
    }

    private List<TaskItem> SearchByPriority()
    {
        Priority priority = ReadEnum<Priority>("Приоритет (Low/Medium/High)");
        return _service.SearchByPriority(priority);
    }

    private static void PrintTasks(List<TaskItem> tasks)
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("Задач нет.");
            return;
        }

        foreach (var t in tasks)
        {
            Console.ForegroundColor = t.IsOverdue() 
                ? ConsoleColor.Red 
                : ConsoleColor.White;
            Console.WriteLine(t);
        }

        Console.ResetColor();
    }

    private static TEnum ReadEnum<TEnum>(string prompt) where TEnum : struct, Enum
    {
        while (true)
        {
            Console.Write($"{prompt}: ");
            if (Enum.TryParse<TEnum>(Console.ReadLine(), ignoreCase: true, out var result))
                return result;
            Console.WriteLine("Некорректное значение, попробуйте снова.");
        }
    }

    private static TEnum? TryParseEnum<TEnum>(string? input) where TEnum : struct, Enum =>
        Enum.TryParse<TEnum>(input, ignoreCase: true, out var result) ? result : null;

    private static string? NullIfEmpty(string? s) =>
        string.IsNullOrWhiteSpace(s) ? null : s;
}
