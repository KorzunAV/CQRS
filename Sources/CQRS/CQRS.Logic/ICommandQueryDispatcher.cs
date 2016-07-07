using CQRS.Logic.Commands;
using CQRS.Logic.Queries;

namespace CQRS.Logic
{
    /// <summary>
    /// Interface for baseCommand dispatchering
    /// </summary>
    public interface ICommandQueryDispatcher
    {
        /// <summary>
        /// Eхeсute baseCommand
        /// </summary>
        /// <param name="baseCommand">Command to execute</param>
        /// <returns>Command execution result</returns>
        ExecutionResult<T> ExecuteCommand<T>(BaseCommand baseCommand);
        
        /// <summary>
        /// Eхeсute baseQuery
        /// </summary>
        /// <param name="baseQuery">Query to execute</param>
        /// <returns>Query execution result</returns>
        ExecutionResult<T> ExecuteQuery<T>(BaseQuery baseQuery);
    }
}
