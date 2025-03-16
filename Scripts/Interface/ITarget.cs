using Godot;

namespace BrokenSigilCollection.Interface;
public interface ITarget
{

    public Node Target { get; }

    public void SetTarget(Node target);

}
