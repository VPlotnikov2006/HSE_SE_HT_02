using System.Text;
using App.Core;

namespace App.DataSaving.JsonSerialization;

public class JsonSerializationVisitor : ISerializationVisitor
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
