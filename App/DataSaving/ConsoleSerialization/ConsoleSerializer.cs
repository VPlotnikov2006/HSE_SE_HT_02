namespace App.DataSaving.ConsoleSerialization;

public class ConsoleSerializer(ConsoleSerializationVisitor visitor) : Serializer
{
    private readonly ConsoleSerializationVisitor _visitor = visitor;

    public override string Header => "\n";

    public override string Footer => "\n";

    public override string Separator => "\n";

    public override string SerializeObject(ISerializable @object)
    {
        @object.Accept(_visitor);
        return _visitor.Serialize();
    }
}
