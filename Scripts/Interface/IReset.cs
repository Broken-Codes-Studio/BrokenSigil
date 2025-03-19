namespace BrokenSigilCollection.Interface;

/// <summary>
/// Interface for objects that can be reset.
/// </summary>
public interface IReset
{
    /// <summary>
    /// Resets the object.
    /// </summary>
    public void Reset();
}

/// <summary>
/// Interface for objects that can be hard reset.
/// </summary>
public interface IHardReset : IReset
{
    /// <summary>
    /// Performs a hard reset on the object.
    /// </summary>
    public void HardReset();
}