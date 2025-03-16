using System.Collections.Generic;
using Godot;

//TODO: Need an overhaul.

namespace BrokenSigilCollection
{

    /// <summary>
    /// Represents the types of operations that can be performed on statistics.
    /// </summary>
    public enum StatisticOperation
    {
        /// <summary>
        /// Add the two statistic values together.
        /// </summary>
        Add,
        /// <summary>
        /// Multiply the two statistic values together.
        /// </summary>
        Multiply,
        /// <summary>
        /// Subtract the two statistic values from eachother.
        /// </summary>
        Subtract,
    }

    /// <summary>
    /// Represents a single statistic, including its value and the operation associated with it.
    /// </summary>
    public struct Statistic
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Statistic"/> struct.
        /// </summary>
        /// <param name="value">The numeric value of the statistic.</param>
        /// <param name="operation">The operation to be applied when combining this statistic with another.</param>
        public Statistic(float value, StatisticOperation operation)
        {
            this.Value = value;
            this.Operation = operation;
        }

        /// <summary>
        /// Value of the statistic.
        /// </summary>
        public float Value { get; private set; }
        /// <summary>
        /// Operation associated with the statistic.
        /// </summary>
        public StatisticOperation Operation { get; private set; }

        /// <summary>
        /// Performs the specified operation on two statistic float values.
        /// </summary>
        /// <param name="a">The first statistic value.</param>
        /// <param name="b">The second statistic value.</param>
        /// <param name="operation">The operation to perform.</param>
        /// <returns>The result of applying the operation on the two statistic values.</returns>
        public static float Operate(float a, float b, StatisticOperation operation) => operation switch
        {
            StatisticOperation.Add => a + b,
            StatisticOperation.Multiply => a * b,
            StatisticOperation.Subtract => a / b,
            _ => a,
        };

    }
}

namespace BrokenSigilCollection.Interface
{

    /// <summary>
    /// Defines a contract for objects that provide a collection of statistics.
    /// </summary>
    public interface IStatistic
    {
        /// <summary>
        /// Retrieves a dictionary of statistics, where the key is a <see cref="StringName"/>
        /// and the value is a list of associated <see cref="Statistic"/> objects.
        /// </summary>
        /// <returns>A dictionary mapping keys to lists of statistics.</returns>
        public Dictionary<StringName, List<Statistic>> GetStatistic();
    }
}
