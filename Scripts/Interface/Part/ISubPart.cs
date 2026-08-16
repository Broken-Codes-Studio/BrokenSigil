namespace BrokenSigilCollection.Interface;

using Godot;
using Godot.Collections;

/// <summary>
/// Interface for 3D subparts location mapping.
/// </summary>
public interface ISubParts3D
{
    /// <summary>
    /// Gets the dictionary mapping subpart names to their 3D locations.
/// </summary>
    public Dictionary<StringName, Vector3> SubPartsLocation { get; }
}

/// <summary>
/// Interface for 2D subparts location mapping.
/// </summary>
public interface ISubParts2D
{
    /// <summary>
    /// Gets the dictionary mapping subpart names to their 2D locations.
/// </summary>
    public Dictionary<StringName, Vector2> SubPartsLocation { get; }
}