namespace UdemyAuthServer.Core.Interfaces.UnitOfWorks
{
    public interface IUnitOfWork
    {
        void Commit();

        Task CommitAsync();
    }
}
