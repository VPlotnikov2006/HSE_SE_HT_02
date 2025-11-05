using System.Text;
using App.Core;

namespace App.DataSaving.ConsoleSerialization;

public class ConsoleSerializationVisitor : ISerializationVisitor
{
    private readonly StringBuilder serialization = new();

    public void Reset()
    {
        serialization.Clear();
    }

    public string Serialize()
    {
        return serialization.ToString();
    }

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

    public void Visit(Category category)
    {
        Reset();

        serialization.AppendLine("=== Category ===");
        serialization.AppendLine($"Id: {category.Id}");
        serialization.AppendLine($"Name: {category.Name ?? "<no name>"}");
        serialization.AppendLine($"Type: {category.Type}");
    }

    public void Visit(BankAccount account)
    {
        Reset();

        serialization.AppendLine("=== Bank Account ===");
        serialization.AppendLine($"Id: {account.Id}");
        serialization.AppendLine($"Name: {account.Name ?? "<no name>"}");
        serialization.AppendLine($"Balance: {account.Balance}");
    }
}
