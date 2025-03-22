namespace BrokenSigilCollection.Interface;

using Godot;

public interface IPassive<T> : IInitialize, IFunctional, ITarget<T>, ICondition, IReset where T : Node
{
}
