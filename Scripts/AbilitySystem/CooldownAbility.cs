namespace BrokenSigilCollection.Abilities;

using Interface;

/// <summary>
/// Abstract base class for abilities with a cooldown.
/// </summary>
public abstract partial class CooldownAbility : Ability, ICooldown
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
