//TODO: Complete the code
namespace BrokenSigilCollection.Effects;

using Interface;
using Godot;
using System;

public abstract partial class Effect : Node, IEffect, ITag
{

    #region Properties
    public abstract EffectTypes EffectType { get; }

    public abstract Node Target { get; }

    public abstract float Duration { get; set; }

    public abstract bool Performing { get; protected set; }

    public abstract uint Tags { get; protected set; }
    #endregion

    public abstract void Initialize();

    public abstract void SetTarget(Node target);

    public virtual void Perform(double delta = 0) => applyEffect();

    protected abstract void applyEffect();

    public abstract void Reset();

    public abstract void HardReset();

    public virtual void Remove() => QueueFree();

    public bool ContainsTags(uint tags) => (this.Tags & tags) == tags;

    public void AddTags(uint tags) => this.Tags |= tags;

    public void RemoveTags(uint tags) => this.Tags &= ~tags;

    public void ClearTags() => Tags = 0;

    public bool Equals(ITag other) => this.Tags.Equals(other.Tags);

    public bool IsSimular(ITag other) => (this.Tags & other.Tags) != 0;

}
