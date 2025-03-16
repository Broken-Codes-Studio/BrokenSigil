using System;

namespace BrokenSigilCollection.Interface;

public interface IClass : IEquatable<IClass>, ISimuable<IClass>
{
    /// <summary>
    /// Gets the class type of the part as a bitmask, used for categorization or filtering.
    /// </summary>
    public uint Class { get; }

}