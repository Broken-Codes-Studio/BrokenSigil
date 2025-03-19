namespace BrokenSigilCollection.Interface;

/// <summary>
/// Interface for checking conditions.
/// </summary>
public interface ICondition
{
    /// <summary>
    /// Method to check if a condition is met.
    /// </summary>
    /// <returns>True if the condition is met, otherwise false.</returns>
    public bool CheckCondition();
}
