namespace BrokenSigilCollection.Interface;

using Godot;
using Godot.Collections;

/// <summary>
/// Interface for a part with identification, priority, tags, type, and additional properties.
/// </summary>
public interface IPart : IIdentification<ushort>, IPriority<short>, ITag, IType<ushort>
{
    /// <summary>
    /// Gets the slot name for this part.
    /// </summary>
    public StringName SlotName { get; }

    /// <summary>
    /// Gets the filters applied to this part.
    /// </summary>
    public string[] Filters { get; }

    /// <summary>
    /// Gets the incompatible items for this part.
    /// </summary>
    public string[] Incompatibles { get; }

    /// <summary>
    /// Gets the blackboard dictionary for this part.
    /// </summary>
    public Dictionary<StringName, Variant> Blackboard { get; }
}