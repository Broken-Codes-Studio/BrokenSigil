namespace BrokenSigilCollection.Abilities;

using BrokenSigilCollection.Interface;
using Godot;

/// <summary>
/// Abstract base class for passive abilities.
/// </summary>
public abstract partial class Passive : Node, IInitialize, IFunctional, ITarget<Node>, ICondition, IReset
{
    /// <summary>
    /// Gets the target node of the passive ability.
    /// </summary>
    public abstract Node Target { get; }

    /// <summary>
    /// Initializes the passive ability.
    /// </summary>
    public abstract void Initialize();

    /// <summary>
    /// Sets the target node for the passive ability.
    /// </summary>
    /// <param name="target">The target node.</param>
    public abstract void SetTarget(Node target);

    /// <summary>
    /// Performs the passive ability.
    /// </summary>
    /// <param name="delta">The delta time.</param>
    public abstract void Perform(double delta);

    /// <summary>
    /// Checks the condition for the passive ability.
    /// </summary>
    /// <returns>True if the condition is met, otherwise false.</returns>
    public virtual bool CheckCondition() => true;

    /// <summary>
    /// Resets the passive ability.
    /// </summary>
    public abstract void Reset();
}
