namespace BrokenSigilCollection.Interface;

using Godot;
using Godot.Collections;

public interface IToggleNodes
{
    // Nodes to disable when the behavior is toggled.
    [Export]
    public Array<Node> DissableNodes { get; }

    /// <summary>
    /// Toggles the script on or off.
    /// </summary>
    /// <param name="Value">True to enable, false to disable.</param>
    public void ToggleNodes(bool Value);
}
