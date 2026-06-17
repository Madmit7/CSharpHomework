while (true)
{
    Console.WriteLine("\nВведите первое число (или 'exit' для выхода):");
    string firstInput = Console.ReadLine()!;
    
    if (firstInput.ToLower() == "exit")
        break;

    Console.WriteLine("Введите второе число:");
    string secondInput = Console.ReadLine()!;

    if (!double.TryParse(firstInput, out double a) || 
        !double.TryParse(secondInput, out double b))
    {
        Console.WriteLine("Ошибка: введите корректные числа.");
        continue;
    }

    Console.WriteLine("Выберите операцию (+, -, *, /):");
    string operation = Console.ReadLine()!;

    try
    {
        double result = operation switch
        {
            "+" => a + b,
            "-" => a - b,
            "*" => a * b,
            "/" => b == 0 
                ? throw new DivideByZeroException("Деление на ноль!") 
                : a / b,
            _ => throw new ArgumentException($"Неизвестная операция: {operation}")
        };

        Console.WriteLine($"Результат: {a} {operation} {b} = {result}");
    }
    catch (DivideByZeroException ex)
    {
        Console.WriteLine($"Ошибка: {ex.Message}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Ошибка: {ex.Message}");
    }
}

Console.WriteLine("Программа завершена.");

