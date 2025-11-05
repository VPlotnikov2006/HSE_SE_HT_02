namespace App.Core.Context;

/// <summary>
/// Operation parameters
/// </summary>
/// <param name="Amount">Operation amount</param>
/// <param name="Date">Operation date</param>
/// <param name="Description">Operation description (possibly null)</param>
public record OperationContext(decimal Amount, DateTimeOffset Date, string? Description);

