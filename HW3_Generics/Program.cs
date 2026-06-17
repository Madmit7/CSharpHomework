using HW3_Generics.Task1;
using HW3_Generics.Task2;

Console.WriteLine("=== Task 1: Repository ===\n");

var productRepo = new Repository<Product>();
productRepo.Add(new Product(1, "Ноутбук", 85000));
productRepo.Add(new Product(2, "Мышь", 1500));
productRepo.Add(new Product(3, "Монитор", 35000));

var userRepo = new Repository<User>();
userRepo.Add(new User(1, "alice"));
userRepo.Add(new User(2, "bob"));

Console.WriteLine($"Продуктов: {productRepo.Count}");
Console.WriteLine($"Найден: {productRepo.GetById(2)}");

var expensive = productRepo.Find(p => p.Price > 10000);
Console.WriteLine("Дороже 10000:");
foreach (var p in expensive)
    Console.WriteLine($"  {p}");

try
{
    productRepo.Add(new Product(1, "Дубликат", 100));
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Ожидаемая ошибка: {ex.Message}");
}

Console.WriteLine("\n=== Task 2: CollectionUtils ===\n");

var numbers = new List<int> { 1, 2, 2, 3, 3, 3, 4 };
Console.WriteLine($"Distinct: [{string.Join(", ", CollectionUtils.Distinct(numbers))}]");

var words = new List<string> { "cat", "dog", "hi", "elephant", "bee", "ant" };
var grouped = CollectionUtils.GroupBy(words, w => w.Length);
Console.WriteLine("GroupBy длина слова:");
foreach (var g in grouped)
    Console.WriteLine($"  {g.Key}: [{string.Join(", ", g.Value)}]");

var dict1 = new Dictionary<string, int> { ["apple"] = 3, ["banana"] = 1 };
var dict2 = new Dictionary<string, int> { ["apple"] = 2, ["cherry"] = 5 };
var merged = CollectionUtils.Merge(dict1, dict2, (a, b) => a + b);
Console.WriteLine("Merge (сумма):");
foreach (var kvp in merged)
    Console.WriteLine($"  {kvp.Key}: {kvp.Value}");

var products = new List<Product>
{
    new(1, "Ноутбук", 85000),
    new(2, "Мышь", 1500),
    new(3, "Монитор", 35000)
};
var mostExpensive = CollectionUtils.MaxBy(products, p => p.Price);
Console.WriteLine($"Самый дорогой: {mostExpensive}");
