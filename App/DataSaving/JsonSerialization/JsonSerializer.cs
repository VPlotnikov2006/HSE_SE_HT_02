namespace App.DataSaving.JsonSerialization;

/// <summary>
/// Serializer to json
/// </summary>
/// <param name="visitor">Json visitor</param>
public class JsonSerializer(JsonSerializationVisitor visitor) : Serializer
{
    /// <summary>
    /// Json visitor
    /// </summary>
    private readonly JsonSerializationVisitor _visitor = visitor;

    /// <inheritdoc/>
    public override string Header => "[ ";

    /// <inheritdoc/>
    public override string Footer => " ]";

    /// <inheritdoc/>
    public override string Separator => ", ";

    /// <inheritdoc/>
    public override string SerializeObject(ISerializable @object)
    {
        @object.Accept(_visitor);
        return _visitor.Serialize();
    }
}
