using System;
using System.Collections.Generic;
using System.Data;
using Common.Logging;
using CQRS.Common;
using CQRS.Common.Errors;
using CQRS.Logic.Blos;
using CQRS.Logic.Commands;
using CQRS.Logic.Queries;

namespace CQRS.Logic
{
    /// <summary>
    /// Dispatchering baseCommand execution
    /// </summary>
    public class CommandQueryDispatcher : ICommandQueryDispatcher, ICommandQueryRegistrator
    {
        #region [ Constants ]

        private const string CommandFailedMessage = "Command failed: CommandId[{0}], CommandData[{1}]";
        private const string QueryFailedMessage = "Query failed: QueryId[{0}], QueryData[{1}]";
        private const string NullCommandMessage = "Can't execute baseCommand = null";
        private const string NullQueryMessage = "Can't execute baseQuery = null";
        private const string QueryRegistrationErrorMessage = "Query with id={0} already registered";
        private const string CommandRegistrationErrorMessage = "Command with id={0} already registered";


        #endregion

        #region [ Fields ]

        private static readonly ILog Log = LogManager.GetLogger(typeof(CommandQueryDispatcher).Name);

        private readonly Dictionary<Guid, Func<BaseCommand, IBaseSessionManager, ExecutionResult>> _commandsHandlers =
            new Dictionary<Guid, Func<BaseCommand, IBaseSessionManager, ExecutionResult>>();

        private readonly Dictionary<Guid, Func<BaseQuery, IBaseSessionManager, ExecutionResult>> _queriesHandlers =
            new Dictionary<Guid, Func<BaseQuery, IBaseSessionManager, ExecutionResult>>();

        private readonly IBaseSessionManagerFactory _sessionManagerFactory;

        #endregion

        public CommandQueryDispatcher(IEnumerable<BaseBlo> blos, IBaseSessionManagerFactory sessionManagerFactory)
        {
            _sessionManagerFactory = sessionManagerFactory;
            foreach (var blo in blos)
            {
                blo.RegisterCommandsAndQueries(this);
            }
        }

        /// <summary>
        /// Regiter baseCommand handler
        /// </summary>
        /// <param name="commandId">Command identifier</param>
        /// <param name="commandHandler">Command handler</param>
        public void RegisterCommand<T>(Guid commandId, Func<BaseCommand, IBaseSessionManager, ExecutionResult<T>> commandHandler)
        {
            if (_commandsHandlers.ContainsKey(commandId))
            {
                throw new ApplicationException(CommandRegistrationErrorMessage);
            }

            _commandsHandlers.Add(commandId, commandHandler);
        }

        /// <summary>
        /// Regiter baseQuery handler
        /// </summary>
        /// <param name="queryId">Query identifier</param>
        /// <param name="queryHandler">Query handler</param>
        public void RegisterQuery<T>(Guid queryId, Func<BaseQuery, IBaseSessionManager, ExecutionResult<T>> queryHandler)
        {
            if (_queriesHandlers.ContainsKey(queryId))
            {
                throw new ApplicationException(QueryRegistrationErrorMessage);
            }

            _queriesHandlers.Add(queryId, queryHandler);
        }

        /// <summary>
        /// ExecuteCommand baseCommand 
        /// </summary>
        /// <param name="baseCommand">Command to execute</param>
        /// <returns>Execution result</returns>
        public ExecutionResult<T> ExecuteCommand<T>(BaseCommand baseCommand)
        {
            if (baseCommand == null)
            {
                throw new ArgumentException(NullCommandMessage);
            }

            ExecutionResult<T> result;
            var session = _sessionManagerFactory.GetSession();
            try
            {
                var commandHandler = _commandsHandlers[baseCommand.CommandId];
                session.OpenSession();
                session.BeginTransaction(IsolationLevel.ReadCommitted);
                result = (ExecutionResult<T>)commandHandler(baseCommand, session);
            }
            catch (ValidationException ex)
            {
                session.RollbackTransaction();
                Log.Info(ex);
                throw;
            }
            catch (Exception ex)
            {
                session.RollbackTransaction();
                Log.ErrorFormat(QueryFailedMessage, ex, CommandFailedMessage, baseCommand.CommandId, baseCommand);
                throw;
            }
            finally
            {
                session.CommitTransaction();
                session.CloseSession();
            }
            return result;
        }

        /// <summary>
        /// ExecuteCommand baseQuery 
        /// </summary>
        /// <param name="baseQuery">Query to execute</param>
        /// <returns>Execution result</returns>
        public ExecutionResult<T> ExecuteQuery<T>(BaseQuery baseQuery)
        {
            if (baseQuery == null)
            {
                throw new ArgumentException(NullQueryMessage);
            }

            ExecutionResult<T> result;
            var session = _sessionManagerFactory.GetSession();
            try
            {
                var queryHandler = _queriesHandlers[baseQuery.QueryId];
                session.OpenSession();
                session.BeginTransaction(IsolationLevel.ReadUncommitted);
                result = (ExecutionResult<T>)queryHandler(baseQuery, session);
                //DbExecutionManager.TryExecute();
            }
            catch (ValidationException ex)
            {
                session.RollbackTransaction();
                Log.Info(ex);
                throw;
            }
            catch (Exception ex)
            {
                session.RollbackTransaction();
                Log.ErrorFormat(QueryFailedMessage, ex, baseQuery.QueryId, baseQuery);
                throw;
            }
            finally
            {
                //session.CommitTransaction();
                session.CloseSession();
            }
            return result;
        }
    }
}