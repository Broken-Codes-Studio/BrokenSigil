namespace BrokenSigilCollection.Interface;

using System;
using System.Numerics;

/// <summary>
/// Generic interface representing a class with categorization and filtering capabilities.
/// </summary>
public interface IClass<T> : IEquatable<IClass<T>>, ISimuable<IClass<T>> where T : IUnsignedNumber<T>
{
    /// <summary>
    /// Gets the class type of the part as a bitmask, used for categorization or filtering.
    /// </summary>
    public T Class { get; }
}