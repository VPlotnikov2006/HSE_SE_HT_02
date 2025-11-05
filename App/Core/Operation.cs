using App.Core.Context;
using App.DataSaving;

namespace App.Core;

/// <summary>
/// Operation class
/// </summary>
/// <param name="id">Operation Id</param>
/// <param name="link">Operation references</param>
public class Operation(Guid id, OperationLink link) : ISerializable
{
    /// <summary>
    /// Operation Id
    /// </summary>
    public Guid Id { get; private set; } = id;

    /// <summary>
    /// Reference to operation category
    /// </summary>
    public Guid CategoryId { get; set; } = link.CategoryId;

    /// <summary>
    /// Reference to operation account
    /// </summary>
    public Guid BankAccountId { get; set; } = link.BankAccountId;

    /// <summary>
    /// Operation amount
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Operation date & time
    /// </summary>
    public DateTimeOffset Date { get; set; }

    /// <summary>
    /// Operation description
    /// </summary>
    public string? Description { get; set; }


    /// <summary>
    /// Operation constructor
    /// </summary>
    /// <param name="_id">Operation Id</param>
    /// <param name="link">Operation references</param>
    /// <param name="ctx">Operation parameters</param>
    public Operation(Guid _id, OperationLink link, OperationContext ctx) : this(_id, link)
    {
        Amount = ctx.Amount;
        Date = ctx.Date;
        Description = ctx.Description;
    }

    /// <inheritdoc/>
    public void Accept(ISerializationVisitor visitor)
    {
        visitor.Visit(this);
    }
}
