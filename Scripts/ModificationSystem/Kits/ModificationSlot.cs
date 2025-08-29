namespace BrokenSigilCollection.Modification;

using Godot;

public abstract class Slot
{

    public readonly StringName Name;
    public readonly StringName[] Dependencies;

    public bool Filled { get; set; }
    public bool Main { get; private set; }

    public Slot(StringName name, StringName[] dependencies, bool main)
    {
        this.Name = name;
        this.Dependencies = dependencies;
        this.Main = main;
    }
}

public class Slot3D : Slot
{
    public readonly Vector3 Position;

    public Slot3D(StringName name, StringName[] dependencies, Vector3 position, bool main) : base(name, dependencies, main)
    {
        this.Position = position;
    }
}

public class Slot2D : Slot
{
    public readonly Vector2 Position;

    public Slot2D(StringName name, StringName[] dependencies, Vector2 position, bool main) : base(name, dependencies, main)
    {
        this.Position = position;
    }
}