namespace BrokenSigilCollection
{
    /// <summary>
    /// Enum for process types.
    /// </summary>
    public enum ProcessType : byte
    {
        None = 0,
        Frame = 1,
        Physics = 2,
    }
}

namespace BrokenSigilCollection.Interface
{
    /// <summary>
    /// Interface for performing functionality.
    /// </summary>
    public interface IFunctional
    {
        // The process type for the function.
        public ProcessType processType { get; }
        /// <summary>
        /// Method to perform an action based on the provided time delta.
        /// </summary>
        /// <param name="delta">The time delta since the last call.</param>
        /// <returns>True if the action was performed successfully, otherwise false.</returns>
        public bool Perform(double delta);
    }
}