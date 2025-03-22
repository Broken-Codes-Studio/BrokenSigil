namespace BrokenSigilCollection.Interface;

using Godot;

public interface IReactive<T> : IPassive<T>, ICooldown where T : Node
{
}
