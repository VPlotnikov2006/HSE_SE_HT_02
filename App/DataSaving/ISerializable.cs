namespace App.DataSaving;

public interface ISerializable
{
    public void Accept(ISerializationVisitor visitor);
}
