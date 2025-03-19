namespace BrokenSigilCollection.Interface;

using System.Numerics;

/// <summary>
/// Generic Interface for objects that can be stacked.
/// </summary>
public interface IStack<T> where T : IUnsignedNumber<T>
{
    /// <summary>
    /// Gets the current stack count.
    /// </summary>
    public T StackCount { get; }

    /// <summary>
    /// Adds to the stack count.
    /// </summary>
    /// <param name="count">The number of stacks to add. Default is 1.</param>
    public void Stack(T count);
}
