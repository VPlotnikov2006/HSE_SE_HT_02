using System.Text;

namespace App.DataSaving;

/// <summary>
/// Base serializer class
/// </summary>
public abstract class Serializer
{
    /// <summary>
    /// String to put in the header
    /// </summary>
    public abstract string Header { get; }

    /// <summary>
    /// String to put in the footer
    /// </summary>
    public abstract string Footer { get; }

    /// <summary>
    /// String to separate objects
    /// </summary>
    public abstract string Separator { get; }


    /// <summary>
    /// Single object serialization method
    /// </summary>
    /// <param name="object">Object to serialize</param>
    /// <returns>Serialized object</returns>
    public abstract string SerializeObject(ISerializable @object);

    /// <summary>
    /// Method to serialize collection
    /// </summary>
    /// <param name="objects">Object collection</param>
    /// <returns>Serialized version of collection</returns>
    public virtual string Serialize(IReadOnlyCollection<ISerializable> objects)
    {
        StringBuilder sb = new();
        sb.Append(Header);
        sb.AppendJoin(Separator, objects.Select(SerializeObject));
        sb.Append(Footer);
        return sb.ToString();
    }
}
