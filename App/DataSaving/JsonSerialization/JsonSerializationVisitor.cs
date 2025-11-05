using System.Text;
using App.Core;

namespace App.DataSaving.JsonSerialization;

/// <summary>
/// Visitor to serialize data in json
/// </summary>
public class JsonSerializationVisitor : ISerializationVisitor
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
        serialization.Append("{ ");
        serialization.Append($"\"id\":\"{operation.Id}\"");
        serialization.Append(", ");
        serialization.Append($"\"category_id\":\"{operation.CategoryId}\"");
        serialization.Append(", ");
        serialization.Append($"\"account_id\":\"{operation.BankAccountId}\"");
        serialization.Append(", ");
        serialization.Append($"\"date\":\"{operation.Date:yyyy-MM-ddTHH:mm:ss.fffzzz}\"");
        serialization.Append(", ");
        serialization.Append($"\"amount\":{operation.Amount}");
        if (operation.Description is not null)
        {
            serialization.Append(", ");
            serialization.Append($"\"description\":\"{operation.Description}\"");
        }
        serialization.Append(" }");
    }

    /// <inheritdoc/>
    public void Visit(Category category)
    {
        Reset();
        serialization.Append("{ ");
        serialization.Append($"\"id\":\"{category.Id}\"");
        serialization.Append(", ");
        serialization.Append($"\"name\":\"{category.Name}\"");
        serialization.Append(", ");
        serialization.Append($"\"type\":\"{category.Type}\"");
        serialization.Append(" }");
    }

    /// <inheritdoc/>
    public void Visit(BankAccount account)
    {
        Reset();
        serialization.Append("{ ");
        serialization.Append($"\"id\":\"{account.Id}\"");
        serialization.Append(", ");
        serialization.Append($"\"name\":\"{account.Name}\"");
        serialization.Append(", ");
        serialization.Append($"\"balance\":{account.Balance}");
        serialization.Append(" }");
    }
}
