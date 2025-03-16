namespace BrokenSigilCollection
{
    public enum EffectTypes : byte
    {
        None = 0,
        Buff = 1,
        Debuff = 2,
    }
}

namespace BrokenSigilCollection.Interface
{

    public interface IEffect : IInitialize, ITarget, IFunctional, IDuration, IHardReset, IRemovable
    {
        public EffectTypes EffectType { get; }
    }
}