using HW2_Cars.Factory;

while (true)
{
    Console.Write("\nВведите марку автомобиля или 'done' для остановки: ");
    string input = Console.ReadLine()!.Trim();

    if (input.ToLower() == "done")
        break;

    if (!CarFactory.TryParse(input, out var carType))
    {
        Console.WriteLine($"Неизвестная марка: {input}. Доступны: Tesla, BMW, Toyota, Ford");
        continue;
    }

    var car = CarFactory.Create(carType);
    Console.WriteLine(car.GetDescription());
}
