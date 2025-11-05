namespace App.Core.Context;

/// <summary>
/// Operation references
/// </summary>
/// <param name="CategoryId">Reference to operation category</param>
/// <param name="BankAccountId">Reference to operation account</param>
public record OperationLink(Guid CategoryId, Guid BankAccountId);
