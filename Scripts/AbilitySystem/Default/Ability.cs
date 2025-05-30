namespace BrokenSigilCollection.Abilities;

using Godot;

/// <summary>
/// Abstract base class for all abilities in the game.
/// </summary>
[Icon("uid://brfe5un54pbh2")]
public abstract partial class Ability : Passive
{
    /// <summary>
    /// Checks the input for the ability.
    /// </summary>
    /// <returns>True if the input is valid, otherwise false.</returns>
    public abstract bool CheckInput();

    /// <summary>
    /// Checks the condition for the ability.
    /// </summary>
    /// <returns>True if the condition is met, otherwise false.</returns>
    public override bool CheckCondition() => CheckInput();
}
