using App.Core;

namespace App.DataSaving;

public interface ISerializationVisitor
{
    public void Visit(Operation operation);
    public void Visit(Category category);
    public void Visit(BankAccount account);

    public void Reset();
    public string Serialize();
}
