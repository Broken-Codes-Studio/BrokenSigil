namespace BrokenSigilCollection.Interface;

/// <summary>
/// Interface for construction and reconstruction functionality.
/// </summary>
public interface IConstructable
{
    /// <summary>
    /// Method to construct the object.
    /// </summary>
    public void Construct();

    /// <summary>
    /// Method to reconstruct the object.
    /// </summary>
    public void Reconstruct();
}
