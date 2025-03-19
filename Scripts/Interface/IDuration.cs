namespace BrokenSigilCollection.Interface;

/// <summary>
/// Interface to manage duration-related events and properties.
/// </summary>
public interface IDuration
{
    /// <summary>
    /// Gets or sets the current duration.
    /// </summary>
    public float Duration { get; set; }

    /// <summary>
    /// Indicates if the object is currently performing.
    /// </summary>
    public bool Performing { get; }
}