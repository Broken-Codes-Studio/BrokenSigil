namespace BrokenSigilCollection.Interface;

using Godot;
using Godot.Collections;

public interface ISubParts3D
{
    public Dictionary<StringName, Vector3> SubPartsLocation { get; }
}

public interface ISubParts2D
{
    public Dictionary<StringName, Vector2> SubPartsLocation { get; }
}