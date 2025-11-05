namespace App.DataSaving.ConsoleSerialization;

/// <summary>
/// Serializer to console
/// </summary>
/// <param name="visitor">Console visitor</param>
public class ConsoleSerializer(ConsoleSerializationVisitor visitor) : Serializer
{
    /// <summary>
    /// Console visitor
    /// </summary>
    private readonly ConsoleSerializationVisitor _visitor = visitor;

    /// <inheritdoc/>
    public override string Header => "\n";

    /// <inheritdoc/>
    public override string Footer => "\n";

    /// <inheritdoc/>
    public override string Separator => "\n";

    /// <inheritdoc/>
    public override string SerializeObject(ISerializable @object)
    {
        @object.Accept(_visitor);
        return _visitor.Serialize();
    }
}
