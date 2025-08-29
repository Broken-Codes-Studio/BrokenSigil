namespace BrokenSigilCollection.Interface
{
    
    using System;
    using System.Numerics;

    public interface IIdentification<T> : IEquatable<IIdentification<T>> where T : IUnsignedNumber<T>
    {
        public T ID { get; }
    }
}