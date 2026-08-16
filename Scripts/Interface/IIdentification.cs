namespace BrokenSigilCollection.Interface
{
    using System;
    using System.Numerics;

    /// <summary>
    /// Interface for identification with a generic ID type.
    /// </summary>
    /// <typeparam name="T">The type of the ID.</typeparam>
    public interface IIdentification<T> : IEquatable<IIdentification<T>> where T : IUnsignedNumber<T>
    {
        /// <summary>
        /// Gets the ID value.
        /// </summary>
        public T ID { get; }
    }
}