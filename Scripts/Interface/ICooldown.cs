namespace BrokenSigilCollection.Interface;

/// <summary>
/// Interface for handling cooldown functionality.
/// </summary>
public interface ICooldown
{
    // Current cooldown duration.
    public float Cooldown { get; set; }

    // Indicates if the object is on cooldown.
    public bool OnCoolDown { get; }
}