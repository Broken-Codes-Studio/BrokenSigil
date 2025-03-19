namespace BrokenSigilCollection.Interface;

using Godot;

/// <summary>
/// Generic interface for objects that can have a target.
/// </summary>
public interface ITarget<T> where T : Node
{
    /// <summary>
    /// Gets the current target.
    /// </summary>
    public T Target { get; }

    /// <summary>
    /// Sets the target.
    /// </summary>
    /// <param name="target">The target to set.</param>
    public void SetTarget(T target);
}
