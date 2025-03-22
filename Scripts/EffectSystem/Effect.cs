namespace BrokenSigilCollection.Effects;

using Interface;
using Godot;

/// <summary>
/// Abstract base class for all effects in the game.
/// </summary>
public abstract partial class Effect : Node, IEffect<Node>, ITag<uint>
{
    #region Properties
    [Export]
    public ProcessType processType { get; protected set; } = ProcessType.None;
    /// <summary>
    /// Gets the type of the effect.
    /// </summary>
    public abstract EffectTypes EffectType { get; }

    /// <summary>
    /// Gets the target node of the effect.
    /// </summary>
    public abstract Node Target { get; }

    /// <summary>
    /// Gets or sets the duration of the effect.
    /// </summary>
    public abstract float Duration { get; set; }

    /// <summary>
    /// Gets a value indicating whether the effect is currently being performed.
    /// </summary>
    public abstract bool Performing { get; protected set; }

    /// <summary>
    /// Gets or sets the tags associated with the effect.
    /// </summary>
    public abstract uint Tags { get; protected set; }
    #endregion

    /// <summary>
    /// Initializes the effect.
    /// </summary>
    public abstract void Initialize();

    /// <summary>
    /// Sets the target node for the effect.
    /// </summary>
    /// <param name="target">The target node.</param>
    public abstract void SetTarget(Node target);

    public override void _Process(double delta)
    {
        if (processType is ProcessType.Frame)
            Perform(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (processType is ProcessType.Physics)
            Perform(delta);
    }


    /// <summary>
    /// Performs the effect.
    /// </summary>
    /// <param name="delta">The delta time.</param>
    public virtual bool Perform(double delta = 0)
    {
        applyEffect();
        return true;
    }

    /// <summary>
    /// Applies the effect.
    /// </summary>
    protected abstract void applyEffect();

    /// <summary>
    /// Resets the effect.
    /// </summary>
    public abstract void Reset();

    /// <summary>
    /// Hard resets the effect.
    /// </summary>
    public abstract void HardReset();

    /// <summary>
    /// Removes the effect.
    /// </summary>
    public virtual void Remove() => QueueFree();

    /// <summary>
    /// Checks if the effect contains the specified tags.
    /// </summary>
    /// <param name="tags">The tags to check.</param>
    /// <returns>True if the effect contains the tags, otherwise false.</returns>
    public bool ContainsTags(uint tags) => (this.Tags & tags) == tags;

    /// <summary>
    /// Adds the specified tags to the effect.
    /// </summary>
    /// <param name="tags">The tags to add.</param>
    public void AddTags(uint tags) => this.Tags |= tags;

    /// <summary>
    /// Removes the specified tags from the effect.
    /// </summary>
    /// <param name="tags">The tags to remove.</param>
    public void RemoveTags(uint tags) => this.Tags &= ~tags;

    /// <summary>
    /// Clears all tags from the effect.
    /// </summary>
    public void ClearTags() => Tags = 0;

    /// <summary>
    /// Checks if the effect is equal to another effect based on tags.
    /// </summary>
    /// <param name="other">The other effect to compare.</param>
    /// <returns>True if the effects are equal, otherwise false.</returns>
    public bool Equals(ITag<uint> other) => this.Tags.Equals(other.Tags);

    /// <summary>
    /// Checks if the effect is similar to another effect based on tags.
    /// </summary>
    /// <param name="other">The other effect to compare.</param>
    /// <returns>True if the effects are similar, otherwise false.</returns>
    public bool IsSimular(ITag<uint> other) => (this.Tags & other.Tags) != 0;
}
