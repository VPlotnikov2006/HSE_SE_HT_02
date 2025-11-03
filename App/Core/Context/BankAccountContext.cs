namespace App.Core.Context;

public record BankAccountContext(string Name, decimal Balance = 0);