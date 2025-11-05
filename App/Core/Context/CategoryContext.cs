namespace App.Core.Context;

/// <summary>
/// Category parameters
/// </summary>
/// <param name="Name">Category name</param>
/// <param name="Type">Category type (Income/Expense)</param>
public record CategoryContext(string Name, OperationType Type);
