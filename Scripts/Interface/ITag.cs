using System;
using System.Diagnostics.Tracing;

namespace BrokenSigilCollection.Interface;

public interface ITag : IEquatable<ITag>, ISimuable<ITag>
{
    public uint Tags { get; }

    public bool ContainsTags(uint tags);
    public void AddTags(uint tags);
    public void RemoveTags(uint tags);
    public void ClearTags();

}
