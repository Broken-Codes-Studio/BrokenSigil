namespace BrokenSigilCollection.Modification;

using Godot;
using System;

using BrokenSigilCollection.Interface;
using System.Collections.Generic;

public abstract partial class ModificationKit : Kit3D<IPart>, IIdentification<ushort>, IType<ushort>
{
    public abstract ushort ID { get; protected set; }
    public abstract ushort Type { get; protected set; }
    protected abstract Dictionary<StringName, IPart> parts { get; set; }
    protected abstract Dictionary<StringName, Slot3D> slots { get; set; }
    protected Dictionary<string, Variant> blackboard { get; set; } = new();
    protected List<string> blackList { get; set; } = new();

    public bool Equals(IType<ushort> other) => Type.Equals(other.Type);

    public bool Equals(IIdentification<ushort> other) => ID.Equals(other.ID);

    public bool IsSimular(IType<ushort> other) => (Type & other.Type) != 0;
}
