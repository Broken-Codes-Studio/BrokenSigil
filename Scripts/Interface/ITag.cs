namespace BrokenSigilCollection.Interface;

using System;
using System.Numerics;


/// <summary>
/// Generic interface for objects that can manage tags.
/// </summary>
public interface ITag<T> : IEquatable<ITag<T>>, ISimuable<ITag<T>> where T : IUnsignedNumber<T>
{
    /// <summary>
    /// Gets the tags associated with the object.
    /// </summary>
    public T Tags { get; }

    /// <summary>
    /// Determines whether the object contains the specified tags.
    /// </summary>
    /// <param name="tags">The tags to check for.</param>
    /// <returns>true if the object contains the specified tags; otherwise, false.</returns>
    public bool ContainsTags(T tags);

    /// <summary>
    /// Adds the specified tags to the object.
    /// </summary>
    /// <param name="tags">The tags to add.</param>
    public void AddTags(T tags);

    /// <summary>
    /// Removes the specified tags from the object.
    /// </summary>
    /// <param name="tags">The tags to remove.</param>
    public void RemoveTags(T tags);

    /// <summary>
    /// Clears all tags from the object.
    /// </summary>
    public void ClearTags();
}
