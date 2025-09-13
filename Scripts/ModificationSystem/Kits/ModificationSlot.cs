namespace BrokenSigilCollection.Modification;

using Godot;

using BrokenSigilCollection.Interface;

/// <summary>
/// Abstract base class for a modification slot.
/// </summary>
public abstract class Slot : IPriority<sbyte>
{
    /// <summary>
    /// Name of the slot.
    /// </summary>
    public readonly StringName Name;
    /// <summary>
    /// Dependencies for this slot.
    /// </summary>
    public readonly StringName[] Dependencies;
    /// <summary>
    /// Indicates if this is a main slot.
    /// </summary>
    public readonly bool Main;

    /// <summary>
    /// Indicates if the slot is filled.
    /// </summary>
    public bool Filled { get; set; }
    /// <summary>
    /// Priority of the slot.
    /// </summary>
    public sbyte Priority { get; set; } = 0;

    /// <summary>
    /// Constructs a slot with the given parameters.
    /// </summary>
    public Slot(StringName name, StringName[] dependencies, bool main, sbyte priority = 0)
    {
        this.Name = name;
        this.Dependencies = dependencies;
        this.Main = main;

        this.Priority = priority;
    }
}

/// <summary>
/// 3D slot with position.
/// </summary>
public class Slot3D : Slot
{
    /// <summary>
    /// Position of the slot in 3D space.
    /// </summary>
    public readonly Vector3 Position;

    /// <summary>
    /// Constructs a 3D slot.
    /// </summary>
    public Slot3D(StringName name, StringName[] dependencies, Vector3 position, bool main, sbyte priority = 0) : base(name, dependencies, main, priority)
    {
        this.Position = position;
    }
}

/// <summary>
/// 2D slot with position.
/// </summary>
public class Slot2D : Slot
{
    /// <summary>
    /// Position of the slot in 2D space.
    /// </summary>
    public readonly Vector2 Position;

    /// <summary>
    /// Constructs a 2D slot.
    /// </summary>
    public Slot2D(StringName name, StringName[] dependencies, Vector2 position, bool main, sbyte priority = 0) : base(name, dependencies, main, priority)
    {
        this.Position = position;
    }
}