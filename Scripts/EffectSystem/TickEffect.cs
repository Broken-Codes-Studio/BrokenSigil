namespace BrokenSigilCollection.Effects;

using Interface;

public abstract partial class TickEffect : Effect, ITick, ICondition
{
    public abstract float TickDuration { get; }

    public virtual bool CheckCondition() => true;

    public abstract void Tick();

}
