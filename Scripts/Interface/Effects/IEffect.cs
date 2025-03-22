using Godot;

namespace BrokenSigilCollection
{
    /// <summary>
    /// Enum representing different types of effects.
    /// </summary>
    public enum EffectTypes : byte
    {
        /// <summary>
        /// No effect.
        /// </summary>
        None = 0,

        /// <summary>
        /// Positive effect that enhances abilities.
        /// </summary>
        Buff = 1,

        /// <summary>
        /// Negative effect that diminishes abilities.
        /// </summary>
        Debuff = 2,
    }
}

namespace BrokenSigilCollection.Interface
{
    /// <summary>
    /// Interface for effects, combining multiple functionalities.
    /// </summary>
    public interface IEffect<T> : IInitialize, ITarget<T>, IFunctional, IDuration, IHardReset, IRemovable where T : Node
    {
        /// <summary>
        /// Gets the type of the effect.
        /// </summary>
        public EffectTypes EffectType { get; }
    }
}