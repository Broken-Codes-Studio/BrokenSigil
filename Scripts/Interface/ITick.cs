namespace BrokenSigilCollection.Interface;

public interface ITick
{
    public float TickDuration { get; }

    public void Tick();

}
