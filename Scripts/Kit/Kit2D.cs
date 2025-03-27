namespace BrokenSigilCollection.Interface;

using Godot;

using System.Collections;
using System.Collections.Generic;

public abstract partial class Kit2D<T> : Node2D, IKit<T> where T : Node
{
    public abstract int Count { get; }
    public bool IsReadOnly => false;

    public abstract bool Contains(T item);
    public abstract void Add(T item);
    public virtual void CheckAdd(T item)
    {
        if (!Contains(item))
        {
            Add(item);
        }
    }
    public abstract bool Remove(T item);
    public abstract void Clear();
    public abstract void Construct();
    public abstract void CopyTo(T[] array, int arrayIndex);
    public abstract IEnumerator<T> GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    public void Reconstruct()
    {
        Clear();
        Construct();
    }

}
