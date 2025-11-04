namespace App.Core.Context;

public record OperationContext(decimal Amount, DateTimeOffset Date, string? Description);

