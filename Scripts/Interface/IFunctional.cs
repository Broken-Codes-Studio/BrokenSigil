namespace BrokenSigilCollection.Interface;

/// <summary>
/// Interface for performing functionality.
/// </summary>
public interface IFunctional
{
    /// <summary>
    /// Method to perform an action.
    /// </summary>
    /// <param name="delta">The time delta since the last call.</param>
    public void Perform(double delta);
}