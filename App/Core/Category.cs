namespace App.Core;

public class Category(Guid _id)
{
    public Guid Id { get; private set; } = _id;
    public required string Name { get; set; }
    public required OperationType Type { get; set; } 
}
