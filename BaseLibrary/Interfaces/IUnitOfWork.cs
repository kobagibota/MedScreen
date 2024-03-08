namespace BaseLibrary.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ILaboratoryRepository LaboratoryRepository { get; }
        IQCActionRepository QCActionRepository { get; }

        void Commit();
        void Rollback();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
