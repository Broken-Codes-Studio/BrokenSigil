namespace BrokenSigilCollection.Abilities;

using Interface;

/// <summary>
/// Abstract base class for passives that repeat an action when they are not on cooldown.
/// </summary>
public abstract partial class Reactive : Passive, ICooldown
{
    /// <summary>
    /// Gets or sets the cooldown duration.
    /// </summary>
    public abstract float Cooldown { get; set; }

    /// <summary>
    /// Gets a value indicating whether the ability is on cooldown.
    /// </summary>
    public abstract bool OnCoolDown { get; }

}
