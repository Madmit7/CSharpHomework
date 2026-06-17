namespace HW3_Generics.Task1;

public class User : IEntity
{
    public int Id { get; }
    public string Username { get; }

    public User(int id, string username)
    {
        Id = id;
        Username = username;
    }

    public override string ToString() => $"User({Id}, {Username})";
}
