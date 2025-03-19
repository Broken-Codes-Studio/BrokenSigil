namespace BrokenSigilCollection.Abilities;

using Interface;

/// <summary>
/// Abstract base class for abilities with a duration.
/// </summary>
public abstract partial class DurationalAbility : CooldownAbility, IDuration
{
    /// <summary>
    /// Gets or sets the duration of the ability.
    /// </summary>
    public abstract float Duration { get; set; }

    /// <summary>
    /// Gets a value indicating whether the ability is currently being performed.
    /// </summary>
    public abstract bool Performing { get; }
}
