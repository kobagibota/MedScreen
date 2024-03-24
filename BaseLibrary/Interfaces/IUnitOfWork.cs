namespace BaseLibrary.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }

        ILaboratoryRepository LaboratoryRepository { get; }
        IQCActionRepository QCActionRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        ILotSupplyRepository LotSupplyRepository { get; }
        ILotTestRepository LotTestRepository { get; }
        IMethodRepository MethodRepository { get; }
        IQCProfileDetailRepository QCProfileDetailRepository { get; }
        IQCProfileRepository QCProfileRepository { get; }
        IQCRepository QCRepository { get; }
        IResultRepository ResultRepository { get; }
        IStandardDetailRepository StandardDetailRepository { get; }
        IStandardRepository StandardRepository { get; }
        IStrainGroupRepository StrainGroupRepository { get; }
        IStrainRepository StrainRepository { get; }
        IStrainTypeRepository StrainTypeRepository { get; }
        ISupplyProfileRepository SupplyProfileRepository { get; }
        ISupplyRepository SupplyRepository { get; }
        ITestProfileRepository TestProfileRepository { get; }
        ITestQCRepository TestQCRepository { get; }
        ITestTypeRepository TestTypeRepository { get; }
        IUseWithRepository UseWithRepository { get; }

        void Commit();
        void Rollback();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
