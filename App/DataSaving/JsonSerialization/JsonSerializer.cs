namespace App.DataSaving.JsonSerialization;

public class JsonSerializer(JsonSerializationVisitor visitor) : Serializer
{
    private readonly JsonSerializationVisitor _visitor = visitor;

    public override string Header => "[ ";

    public override string Footer => " ]";

    public override string Separator => ", ";

    public override string SerializeObject(ISerializable @object)
    {
        @object.Accept(_visitor);
        return _visitor.Serialize();
    }
}
