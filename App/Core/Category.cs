using App.Core.Context;
using App.DataSaving;

namespace App.Core;

/// <summary>
/// Category class
/// </summary>
/// <param name="id">Category Id</param>
public class Category(Guid id) : ISerializable
{
    /// <summary>
    /// Category Id
    /// </summary>
    public Guid Id { get; private set; } = id;

    /// <summary>
    /// Category name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Category type
    /// </summary>
    public OperationType Type { get; set; }


    /// <summary>
    /// Category constructor
    /// </summary>
    /// <param name="_id">Category Id</param>
    /// <param name="ctx">Category parameters</param>
    public Category(Guid _id, CategoryContext ctx) : this(_id)
    {
        Name = ctx.Name;
        Type = ctx.Type;
    }

    /// <inheritdoc/>
    public void Accept(ISerializationVisitor visitor)
    {
        visitor.Visit(this);
    }
}
