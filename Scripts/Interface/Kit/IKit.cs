namespace BrokenSigilCollection.Interface;

using System.Collections.Generic;
using Godot;

public interface IKit<T> : IConstructable, ICollection<T> where T : Node
{

    public void CheckAdd(T item);

}
