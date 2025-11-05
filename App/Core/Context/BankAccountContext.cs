namespace App.Core.Context;

/// <summary>
/// Bank Account parameters
/// </summary>
/// <param name="Name">Account name</param>
/// <param name="Balance">Initial balance</param>
public record BankAccountContext(string Name, decimal Balance = 0);