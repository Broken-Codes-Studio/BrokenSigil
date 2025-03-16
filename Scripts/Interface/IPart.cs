using Godot;

namespace BrokenSigilCollection.Interface;

public interface IPart : IPriority
{
    /// <summary>
    /// Gets the type of the part as a <see cref="StringName"/>.
    /// Must be implemented by derived classes to define their specific type.
    /// </summary>
    /// <returns>A <see cref="StringName"/> representing the type of the part.</returns>
    public StringName GetPartType();

}