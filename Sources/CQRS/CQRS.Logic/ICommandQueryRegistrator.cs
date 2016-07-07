using System;
using CQRS.Common;
using CQRS.Logic.Commands;
using CQRS.Logic.Queries;

namespace CQRS.Logic
{
    /// <summary>
    /// Interface for command registration 
    /// </summary>
    public interface ICommandQueryRegistrator
    {
        /// <summary>
        /// Register command
        /// </summary>
        /// <param name="commandId">Command identifier</param>
        /// <param name="commandHandler">Command execution handler</param>
        void RegisterCommand<T>(Guid commandId, Func<BaseCommand, IBaseSessionManager, ExecutionResult<T>> commandHandler);

        /// <summary>
        /// Register query
        /// </summary>
        /// <param name="queryId">Query identifier</param>
        /// <param name="queryHandler">Query execution handler</param>
        void RegisterQuery<T>(Guid queryId, Func<BaseQuery, IBaseSessionManager, ExecutionResult<T>> queryHandler);
    }
}
