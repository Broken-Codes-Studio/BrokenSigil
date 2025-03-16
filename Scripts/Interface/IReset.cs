namespace BrokenSigilCollection.Interface;

public interface IReset
{
    public void Reset();
}

public interface IHardReset : IReset
{
    public void HardReset();
}