using System.Text;
using App.Core;

namespace App.DataSaving.ConsoleSerialization;

/// <summary>
/// Visitor to serialize data in console
/// </summary>
public class ConsoleSerializationVisitor : ISerializationVisitor
{
    /// <summary>
    /// Object to store serialized data
    /// </summary>
    private readonly StringBuilder serialization = new();

    /// <inheritdoc/>
    public void Reset()
    {
        serialization.Clear();
    }

    /// <inheritdoc/>
    public string Serialize()
    {
        return serialization.ToString();
    }

    /// <inheritdoc/>
    public void Visit(Operation operation)
    {
        Reset();

        serialization.AppendLine("=== Operation ===");
        serialization.AppendLine($"Id: {operation.Id}");
        serialization.AppendLine($"Bank Account Id: {operation.BankAccountId}");
        serialization.AppendLine($"Category Id: {operation.CategoryId}");
        serialization.AppendLine($"Amount: {operation.Amount}");
        serialization.AppendLine($"Date: {operation.Date:yyyy-MM-dd HH:mm:ss}");
        serialization.AppendLine($"Description: {operation.Description ?? "<no description>"}");
    }

    /// <inheritdoc/>
    public void Visit(Category category)
    {
        Reset();

        serialization.AppendLine("=== Category ===");
        serialization.AppendLine($"Id: {category.Id}");
        serialization.AppendLine($"Name: {category.Name ?? "<no name>"}");
        serialization.AppendLine($"Type: {category.Type}");
    }

    /// <inheritdoc/>
    public void Visit(BankAccount account)
    {
        Reset();

        serialization.AppendLine("=== Bank Account ===");
        serialization.AppendLine($"Id: {account.Id}");
        serialization.AppendLine($"Name: {account.Name ?? "<no name>"}");
        serialization.AppendLine($"Balance: {account.Balance}");
    }
}
