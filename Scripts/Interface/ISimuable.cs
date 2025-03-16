using Godot;
using System;

namespace BrokenSigilCollection.Interface;

public interface ISimuable<T>
{
    public bool IsSimular(T other);
}
