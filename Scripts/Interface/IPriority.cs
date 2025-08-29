using System.Numerics;

namespace BrokenSigilCollection.Interface;

/// <summary>
/// Interface to manage priority of an object.
/// </summary>
public interface IPriority<T> where T : ISignedNumber<T>
{
    /// <summary>
    /// Gets or sets the priority of the object.
    /// </summary>
    public T Priority { get; set; }
}
