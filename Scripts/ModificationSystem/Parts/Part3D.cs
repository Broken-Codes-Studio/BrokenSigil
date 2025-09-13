namespace BrokenSigilCollection.Modification;

using Godot;
using Godot.Collections;

using System;
using System.Linq;

using BrokenSigilCollection.Interface;

/// <summary>
/// Abstract base class for a 3D part in the modification system.
/// </summary>
public abstract partial class Part3D : Node3D, IPart, ISubParts3D, IFunctionality, IStack<byte>, IRarity
{
    /// <summary>
    /// Unique identifier for the part.
    /// </summary>
    public abstract ushort ID { get; protected set; }
    /// <summary>
    /// Slot name for this part.
    /// </summary>
    public abstract StringName SlotName { get; protected set; }
    /// <summary>
    /// Priority of the part.
    /// </summary>
    public abstract short Priority { get; set; }
    /// <summary>
    /// Tags associated with the part.
    /// </summary>
    public abstract StringName[] Tags { get; protected set; }
    /// <summary>
    /// Type identifier for the part.
    /// </summary>
    public abstract ushort Type { get; protected set; }
    /// <summary>
    /// Rarity of the part.
    /// </summary>
    public abstract RarityEnum Rarity { get; protected set; }
    /// <summary>
    /// Filters for compatibility.
    /// </summary>
    public abstract string[] Filters { get; protected set; }
    /// <summary>
    /// Incompatible identifiers.
    /// </summary>
    public abstract string[] Incompatibles { get; protected set; }
    /// <summary>
    /// Locations of subparts.
    /// </summary>
    public abstract Dictionary<StringName, Vector3> SubPartsLocation { get; protected set; }

    /// <summary>
    /// Adds functionality to the target node.
    /// </summary>
    public abstract void AddFunctionality(Node target);

    /// <summary>
    /// Removes functionality from the part.
    /// </summary>
    public abstract void RemoveFunctionality();

    /// <summary>
    /// Blackboard for storing arbitrary data.
    /// </summary>
    [Export]
    public Dictionary<StringName, Variant> Blackboard { get; protected set; } = new();

    /// <summary>
    /// Stack count for the part.
    /// </summary>
    public byte StackCount { get; protected set; } = 1;

    /// <summary>
    /// Checks if the part contains the specified tag.
    /// </summary>
    public bool ContainsTag(StringName tag) => Tags.Contains(tag);

    /// <summary>
    /// Increases stack count.
    /// </summary>
    public void Stack(byte count) => StackCount = (byte)Mathf.Clamp(StackCount + count, 1, 255);

    /// <summary>
    /// Decreases stack count.
    /// </summary>
    public void RemoveStack(byte count) => StackCount = (byte)Mathf.Clamp(StackCount - count, 1, 255);

    /// <summary>
    /// Clears the stack count to 1.
    /// </summary>
    public void ClearStack() => StackCount = 1;

    /// <summary>
    /// Checks if tags are equal.
    /// </summary>
    public bool Equals(ITag other) => Tags.Equals(other.Tags);

    /// <summary>
    /// Checks if types are equal.
    /// </summary>
    public bool Equals(IType<ushort> other) => Type.Equals(other.Type);

    /// <summary>
    /// Checks if IDs are equal.
    /// </summary>
    public bool Equals(IIdentification<ushort> other) => ID.Equals(other.ID);

    /// <summary>
    /// Checks if types are similar.
    /// </summary>
    public bool IsSimular(IType<ushort> other) => (Type & other.Type) != 0;
}
