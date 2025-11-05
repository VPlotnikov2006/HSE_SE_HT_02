using App.Core;

namespace App.DataSaving;

/// <summary>
/// Interface for all serialization visitors
/// </summary>
public interface ISerializationVisitor
{
    /// <summary>
    /// Visit method for operations
    /// </summary>
    /// <param name="operation">Operation</param>
    public void Visit(Operation operation);

    /// <summary>
    /// Visit method for categories
    /// </summary>
    /// <param name="operation">Category</param>
    public void Visit(Category category);

    /// <summary>
    /// Visit method for accounts
    /// </summary>
    /// <param name="operation">Account</param>
    public void Visit(BankAccount account);

    /// <summary>
    /// Resetting method
    /// </summary>
    public void Reset();

    /// <summary>
    /// Serialization method
    /// </summary>
    /// <returns>Serialized visited object</returns>
    public string Serialize();
}
