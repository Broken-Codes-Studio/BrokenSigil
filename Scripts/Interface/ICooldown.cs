namespace BrokenSigilCollection.Interface;

/// <summary>
/// Interface for handling cooldown functionality.
/// </summary>
public interface ICooldown
{
    /// <summary>
    /// Gets or sets the current cooldown duration.
    /// </summary>
    public float Cooldown { get; set; }

    /// <summary>
    /// Indicates if the object is on cooldown.
    /// </summary>
    public bool OnCoolDown { get; }
}