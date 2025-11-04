using App.Core.Context;
using App.DataSaving;

namespace App.Core;

public class Category(Guid id): ISerializable
{
    public Guid Id { get; private set; } = id;
    public string? Name { get; set; }
    public OperationType Type { get; set; }

    public Category(Guid _id, CategoryContext ctx) : this(_id)
    {
        Name = ctx.Name;
        Type = ctx.Type;
    }
    
    public void Accept(ISerializationVisitor visitor)
    {
        visitor.Visit(this);
    }
}
