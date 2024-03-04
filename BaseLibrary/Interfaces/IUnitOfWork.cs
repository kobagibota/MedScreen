namespace BaseLibrary.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ILaboratoryRepository LaboratoryRepository { get; }

        void Commit();
        void Rollback();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
