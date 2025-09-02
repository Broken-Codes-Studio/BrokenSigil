namespace BrokenSigilCollection.Interface;

using Godot;
using Godot.Collections;

public interface IPart : IIdentification<ushort>, IPriority<short>, ITag, IType<ushort>
{
    public StringName SlotName { get; }

    public string[] Filters { get; }

    public string[] Incompatibles { get; }

    public Dictionary<StringName, Variant> Blackboard { get; }

}