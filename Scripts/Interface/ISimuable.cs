namespace BrokenSigilCollection.Interface;

/// <summary>
/// Interface for objects that can be compared for similarity.
/// </summary>
/// <typeparam name="T">The type of object to compare.</typeparam>
public interface ISimuable<T>
{
    /// <summary>
    /// Determines whether the current object is similar to another object of the same type.
    /// </summary>
    /// <param name="other">The object to compare with the current object.</param>
    /// <returns>true if the current object is similar to the other parameter; otherwise, false.</returns>
    public bool IsSimular(T other);
}
