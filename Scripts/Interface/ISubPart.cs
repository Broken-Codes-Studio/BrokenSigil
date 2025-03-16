using System.Collections.Generic;
using Godot;

namespace BrokenSigilCollection.Interface;

//TODO: Need an overhaul.

/// <summary>
/// Defines a contract for objects that manage subparts represented as a collection of 3D vectors.
/// </summary>
public interface ISubPart
{
    /// Retrieves a collection of subparts associated with the Node.
    /// Each subpart is represented as a key-value pair, where the key is a <see cref="StringName"/> 
    /// and the value is an array of <see cref="Vector3"/> positions.
    /// </summary>
    /// <returns>A dictionary containing part type (as <see cref="StringName"/>) and their 3D positions.</returns>
    public Dictionary<StringName, Vector3[]> GetSubparts();
}
