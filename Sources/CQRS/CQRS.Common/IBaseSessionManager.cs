using System.Data;

namespace CQRS.Common
{
    public interface IBaseSessionManager
    {
        /// <summary>
        /// 	Opens current SQL session.
        /// </summary>
        void OpenSession();

        /// <summary>
        /// 	Close current SQL session.
        /// </summary>
        void CloseSession();

        /// <summary>
        /// 	Begins SQL transaction.
        /// </summary>
        void BeginTransaction(IsolationLevel isolationLevel);

        /// <summary>
        /// 	Commits SQL transaction.
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// 	Rollbacks SQL transaction and close session.
        /// </summary>
        void RollbackTransaction();
    }
}