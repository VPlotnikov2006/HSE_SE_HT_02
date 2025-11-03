using App.Core.Context;

namespace App.Core;

public class Operation(Guid id, OperationLink link)
{
    public Guid Id { get; private set; } = id;
    public Guid CategoryId { get; private set; } = link.CategoryId;
    public Guid BankAccountId { get; private set; } = link.BankAccountId;
    public required decimal Amount { get; set; }
    public required DateTime Date { get; set; }
    public string? Description { get; set; }

    public Operation(Guid _id, OperationLink link, OperationContext ctx) : this(_id, link)
    {
        Amount = ctx.Amount;
        Date = ctx.Date;
        Description = ctx.Description;
    }
}
