namespace BrokenSigilCollection.Abilities;

public abstract partial class Ability : Passive
{
    protected abstract bool checkInput();

    public override bool CheckCondition() => checkInput();

}
