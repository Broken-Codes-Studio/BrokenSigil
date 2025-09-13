using Godot;

namespace BrokenSigilCollection.Interface
{
    /// <summary>
    /// Interface for adding and removing functionality to/from a node.
    /// </summary>
    public interface IFunctionality
    {
        /// <summary>
        /// Adds functionality to the specified target node.
        /// </summary>
        /// <param name="target">The target node.</param>
        public void AddFunctionality(Node target);

        /// <summary>
        /// Removes functionality.
        /// </summary>
        public void RemoveFunctionality();
    }
}