namespace Taav.Contracts.Interfaces;

public interface UnitOfWork
{
    Task Begin();
    Task Commit();
    Task Rollback();
    Task CommitPartial();
    Task Complete();
}