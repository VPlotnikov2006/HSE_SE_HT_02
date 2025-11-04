using App.Core.Context;
using App.DataSaving;

namespace App.Core;

public class Operation(Guid id, OperationLink link): ISerializable
{
    public Guid Id { get; private set; } = id;
    public Guid CategoryId { get; set; } = link.CategoryId;
    public Guid BankAccountId { get; set; } = link.BankAccountId;
    public decimal Amount { get; set; }
    public DateTimeOffset Date { get; set; }
    public string? Description { get; set; }

    public Operation(Guid _id, OperationLink link, OperationContext ctx) : this(_id, link)
    {
        Amount = ctx.Amount;
        Date = ctx.Date;
        Description = ctx.Description;
    }

    public void Accept(ISerializationVisitor visitor)
    {
        visitor.Visit(this);
    }
}
