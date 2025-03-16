namespace BrokenSigilCollection.Abilities;

using Interface;

public abstract partial class Reactive : Passive, ICooldown
{
    public abstract float Cooldown { get; set; }
    public abstract bool OnCoolDown { get; }

}
