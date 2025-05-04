namespace BrokenSigilCollection.Abilities;

using Interface;
using Godot;

/// <summary>
/// Abstract base class for passive abilities.
/// </summary>
[Icon("uid://ccg8mxdjapg7i")]
public abstract partial class Passive : Node, IPassive<Node>
{
    [Export]
    public ProcessType processType { get; protected set; } = ProcessType.Frame;
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

    public override void _Process(double delta)
    {
        if (processType is ProcessType.Frame)
            Perform(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (processType is ProcessType.Physics)
            Perform(delta);
    }

    /// <summary>
    /// Performs the passive ability.
    /// </summary>
    /// <param name="delta">The delta time.</param>
    public virtual bool Perform(double delta)
    {
        if (CheckCondition())
        {
            performAction(delta);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Performs the ability.
    /// </summary>
    /// <param name="delta">The frame time.</param>
    protected abstract void performAction(double delta);

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
