namespace BrokenSigilCollection.Modification;

using Godot;

using BrokenSigilCollection.Interface;

public abstract class Slot : IPriority<sbyte>
{

    public readonly StringName Name;
    public readonly StringName[] Dependencies;
    public readonly bool Main;

    public bool Filled { get; set; }
    public sbyte Priority { get; set; } = 0;

    public Slot(StringName name, StringName[] dependencies, bool main, sbyte priority = 0)
    {
        this.Name = name;
        this.Dependencies = dependencies;
        this.Main = main;

        this.Priority = priority;
    }
}

public class Slot3D : Slot
{
    public readonly Vector3 Position;

    public Slot3D(StringName name, StringName[] dependencies, Vector3 position, bool main, sbyte priority = 0) : base(name, dependencies, main, priority)
    {
        this.Position = position;
    }
}

public class Slot2D : Slot
{
    public readonly Vector2 Position;

    public Slot2D(StringName name, StringName[] dependencies, Vector2 position, bool main, sbyte priority = 0) : base(name, dependencies, main, priority)
    {
        this.Position = position;
    }
}