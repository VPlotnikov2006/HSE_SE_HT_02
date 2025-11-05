namespace App.DataSaving;

/// <summary>
/// Interface for Serializable objects
/// </summary>
public interface ISerializable
{
    /// <summary>
    /// Method to implement Visitor pattern
    /// </summary>
    /// <param name="visitor">Visitor</param>
    public void Accept(ISerializationVisitor visitor);
}
