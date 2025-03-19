namespace BrokenSigilCollection.Interface;

using Godot;

/// <summary>
/// Interface representing a part with a priority.
/// </summary>
public interface IPart : IPriority
{
    /// <summary>
    /// Gets the type of the part as a <see cref="StringName"/>.
    /// Must be implemented by derived classes to define their specific type.
    /// </summary>
    /// <returns>A <see cref="StringName"/> representing the type of the part.</returns>
    public StringName GetPartType();
}