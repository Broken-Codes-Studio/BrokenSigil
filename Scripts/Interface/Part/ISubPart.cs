namespace BrokenSigilCollection.Interface;

using System.Collections.Generic;
using Godot;

public interface ISubParts3D
{
    public Dictionary<StringName, Vector3[]> SubPartsLocations { get; }
}

public interface ISubParts2D
{
    public Dictionary<StringName, Vector2[]> SubPartsLocations { get; }
}