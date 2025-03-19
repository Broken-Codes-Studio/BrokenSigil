namespace BrokenSigilCollection.Interface;

/// <summary>
/// Interface for objects that can perform an action per tick.
/// </summary>
public interface ITick
{
    /// <summary>
    /// Gets the duration of the tick.
    /// </summary>
    public float TickDuration { get; }

    /// <summary>
    /// Performs the tick action.
    /// </summary>
    public void Tick();
}
