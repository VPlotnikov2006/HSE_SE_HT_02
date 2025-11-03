namespace App.Core;

public class Operation(Guid _id, Guid account_id, Guid category_id)
{
    public Guid Id { get; private set; } = _id;
    public Guid CategoryId { get; private set; } = category_id;
    public Guid BankAccountId { get; private set; } = account_id;
    public required decimal Amount { get; set; }
    public required DateTime Date { get; set; }
    public string? Desctiption { get; set; }
}
