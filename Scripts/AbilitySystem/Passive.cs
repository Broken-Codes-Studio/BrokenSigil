//TODO: Complete the code
namespace BrokenSigilCollection.Abilities;

using BrokenSigilCollection.Interface;
using Godot;

public abstract partial class Passive : Node, IInitialize, IFunctional, ITarget, ICondition, IReset
{
    public abstract Node Target { get; }

    public abstract void Initialize();
    public abstract void SetTarget(Node target);
    public abstract void Perform(double delta);
    public virtual bool CheckCondition() => true;
    public abstract void Reset();

}
