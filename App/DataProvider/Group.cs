using System.Collections;

namespace App.DataProvider;

public class Group<T>: IEnumerable<Group<T>>
{
    public readonly IEnumerable<Group<T>>? subgroups;
    public readonly T value;

    public Group(T _value)
    {
        subgroups = null;
        value = _value;
    }

    public Group(T _value, IEnumerable<Group<T>> _subgroups)
    {
        value = _value;
        subgroups = _subgroups;
    }

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

    public Group(T _value, IEnumerable<IEnumerable<T>> _paths) : this(_value, _paths.Select(p => new LinkedList<T>(p)))
    {
        
    }

    public IEnumerator<Group<T>> GetEnumerator()
    {
        if (IsLeaf())
        {
            throw new InvalidOperationException("Iterating on leaf value");
        }
        return subgroups!.GetEnumerator();
    }

    public bool IsLeaf()
    {
        return subgroups is null || subgroups.Count() == 0;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
