namespace BrokenSigilCollection.Modification;

using Godot;
using Godot.Collections;

using System;
using System.Linq;

using BrokenSigilCollection.Interface;

public abstract partial class MeshPart3D : MeshInstance3D, IPart, ISubParts3D, IFunctionality, IStack<byte>, IRarity
{

    public abstract ushort ID { get; protected set; }

    public abstract StringName SlotName { get; protected set; }

    public abstract short Priority { get; set; }

    public abstract StringName[] Tags { get; protected set; }

    public abstract ushort Type { get; protected set; }

    public abstract RarityEnum Rarity { get; protected set; }

    public abstract string[] Filters { get; protected set; }

    public abstract string[] Incompatibles { get; protected set; }

    public abstract Dictionary<StringName, Vector3> SubPartsLocation { get; protected set; }

    public abstract void AddFunctionality(Node target);

    public abstract void RemoveFunctionality();

    [Export]
    public Dictionary<StringName, Variant> Blackboard { get; protected set; } = new();

    public byte StackCount { get; protected set; } = 1;

    public bool ContainsTag(StringName tag) => Tags.Contains(tag);

    public void Stack(byte count) => StackCount = (byte)Mathf.Clamp(StackCount + count, 1, 255);

    public void RemoveStack(byte count) => StackCount = (byte)Mathf.Clamp(StackCount - count, 1, 255);

    public void ClearStack() => StackCount = 1;

    public bool Equals(ITag other) => Tags.Equals(other.Tags);

    public bool Equals(IType<ushort> other) => Type.Equals(other.Type);

    public bool Equals(IIdentification<ushort> other) => ID.Equals(other.ID);

    public bool IsSimular(IType<ushort> other) => (Type & other.Type) != 0;
}
