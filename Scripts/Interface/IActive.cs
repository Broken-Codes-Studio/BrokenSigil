namespace BrokenSigilCollection.Interface;

/// <summary>
/// Interface for handling active state.
/// </summary>
public interface IActive
{
    // Indicates if the object is active.
    public abstract bool Active { get; set; }
}