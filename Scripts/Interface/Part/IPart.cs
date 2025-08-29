namespace BrokenSigilCollection.Interface;

using Godot;

public interface IPart : IIdentification<ushort>, IPriority<short>, ITag, IType<ushort>
{
    public StringName SlotName { get; }

    public string[] Filters { get; }

    public string[] Incompatibles { get; }
    
}