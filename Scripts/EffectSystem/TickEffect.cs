namespace BrokenSigilCollection.Effects;

using Interface;

/// <summary>
/// Abstract base class for effects that tick over time.
/// </summary>
public abstract partial class TickEffect : Effect, ITick, ICondition
{
    /// <summary>
    /// Gets the duration between ticks.
    /// </summary>
    public abstract float TickDuration { get; set; }

    /// <summary>
    /// Checks the condition for the tick effect.
    /// </summary>
    /// <returns>True if the condition is met, otherwise false.</returns>
    public virtual bool CheckCondition() => true;

    /// <summary>
    /// Performs a tick of the effect.
    /// </summary>
    public abstract void Tick();
}
