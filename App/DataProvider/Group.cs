using System.Collections;

namespace App.DataProvider;

/// <summary>
/// Composite pattern, stores nested lists of values
/// </summary>
/// <typeparam name="T"></typeparam>
public class Group<T> : IEnumerable<Group<T>>
{
    /// <summary>
    /// Nested values
    /// </summary>
    public readonly IEnumerable<Group<T>>? subgroups;

    /// <summary>
    /// Main value
    /// </summary>
    public readonly T value;

    /// <summary>
    /// Constructor from single value
    /// </summary>
    /// <param name="_value">Value</param>
    public Group(T _value)
    {
        subgroups = null;
        value = _value;
    }

    /// <summary>
    /// Constructor with nested groups
    /// </summary>
    /// <param name="_value">Value</param>
    /// <param name="_subgroups">Nested groups</param>
    public Group(T _value, IEnumerable<Group<T>> _subgroups)
    {
        value = _value;
        subgroups = _subgroups;
    }

    /// <summary>
    /// Constructor from root paths
    /// </summary>
    /// <param name="_value">Value</param>
    /// <param name="_paths">Root paths (will be grouped by first element)</param>
    public Group(T _value, IEnumerable<LinkedList<T>> _paths)
    {
        value = _value;
        subgroups = [.. _paths
            .Where(p => p.Count > 0)
            .GroupBy(p => p.First!.Value)
            .Select(g =>
            {
                var groupName = g.Key;
                var trimmed = g.Select(p =>
                    {
                        p.RemoveFirst();
                        return p;
                    }
                ).Where(p => p.Count > 0).ToArray();
                return new Group<T>(groupName, trimmed);
            })];
    }

    /// <summary>
    /// Constructor from root paths
    /// </summary>
    /// <param name="_value">Value</param>
    /// <param name="_paths">Root paths (will be grouped by first element)</param>
    public Group(T _value, IEnumerable<IEnumerable<T>> _paths) : this(_value, _paths.Select(p => new LinkedList<T>(p)))
    {

    }

    /// <inheritdoc/>
    public IEnumerator<Group<T>> GetEnumerator()
    {
        if (IsLeaf())
        {
            throw new InvalidOperationException("Iterating on leaf value");
        }
        return subgroups!.GetEnumerator();
    }

    /// <summary>
    /// Check is there any nested groups
    /// </summary>
    /// <returns><see cref="true"/> if there is no nested groups, <see cref="false"/> otherwise</returns>
    public bool IsLeaf()
    {
        return subgroups is null || subgroups.Count() == 0;
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
