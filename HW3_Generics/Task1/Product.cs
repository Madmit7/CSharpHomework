namespace HW3_Generics.Task1;

public class Product : IEntity
{
    public int Id { get; }
    public string Name { get; }
    public decimal Price { get; }

    public Product(int id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }

    public override string ToString() => $"Product({Id}, {Name}, {Price}₽)";
}
