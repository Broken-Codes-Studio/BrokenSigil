namespace BrokenSigilCollection.Abilities;

using Interface;

public abstract partial class DurationalReactive : Reactive, IDuration
{
    public abstract float Duration { get; set; }
    public abstract bool Performing { get; }

}
