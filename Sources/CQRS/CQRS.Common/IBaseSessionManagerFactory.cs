namespace CQRS.Common
{
    public interface IBaseSessionManagerFactory
    {
        IBaseSessionManager GetSession();
    }
}