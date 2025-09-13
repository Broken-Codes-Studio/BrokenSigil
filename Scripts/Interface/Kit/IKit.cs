namespace BrokenSigilCollection.Interface;

using System.Collections.Generic;
using Godot;

/// <summary>
/// Represents a kit that can construct and hold a collection of items.
/// </summary>
/// <typeparam name="T">Type of item in the kit.</typeparam>
public interface IKit<T> : IConstructable, ICollection<T>
{
    /// <summary>
    /// Checks and adds an item to the kit.
    /// </summary>
    /// <param name="item">The item to check and add.</param>
    public void CheckAdd(T item);
}
