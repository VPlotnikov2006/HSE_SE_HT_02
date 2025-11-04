using System.Text;

namespace App.DataSaving;

public abstract class Serializer
{
    public abstract string Header { get; }
    public abstract string Footer { get; }
    public abstract string Separator { get; }

    public abstract string SerializeObject(ISerializable @object);

    public string Serialize(IReadOnlyCollection<ISerializable> objects)
    {
        StringBuilder sb = new();
        sb.Append(Header);
        sb.AppendJoin(Separator, objects.Select(SerializeObject));
        sb.Append(Footer);
        return sb.ToString();
    }
}
