namespace CQRS.Dtos
{
    public interface IDbSesseon
    {
        void Connect();
        void Rollback();
        void Disconnect();
    }
}