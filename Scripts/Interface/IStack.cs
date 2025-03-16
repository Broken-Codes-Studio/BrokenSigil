namespace BrokenSigilCollection.Interface;

public interface IStack
{
    public byte StackCount { get; }
    public void Stack(byte count = 1);
}
