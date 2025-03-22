namespace BrokenSigilCollection.Interface;

using Godot;

public interface IAbility<T> : IPassive<T> where T : Node
{
    /// <summary>
    /// Checks the input for the ability.
    /// </summary>
    /// <returns>True if the input is valid, otherwise false.</returns>
    public abstract bool CheckInput();
}