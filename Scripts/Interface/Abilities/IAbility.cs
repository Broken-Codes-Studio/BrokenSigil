namespace BrokenSigilCollection.Interface;

using Godot;

public interface IAbility : IPassive
{
    /// <summary>
    /// Checks the input for the ability.
    /// </summary>
    /// <returns>True if the input is valid, otherwise false.</returns>
    public abstract bool CheckInput();
}