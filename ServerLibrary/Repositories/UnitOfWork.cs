using BaseLibrary.Interfaces;
using ServerLibrary.Data;

namespace ServerLibrary.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Private member

        private readonly AppDbContext _dbContext;

        private ILaboratoryRepository _laboratoryRepository;
        private IQCActionRepository _qcActionRepository;
        private ICategoryRepository _categoryRepository;
        private ILotSupplyRepository _lotSupplyRepository;
        private ILotTestRepository _lotTestRepository;
        private IMethodRepository _methodRepository;
        private IQCProfileDetailRepository _qCProfileDetailRepository;
        private IQCProfileRepository _qCProfileRepository;
        private IQCRepository _qCRepository;
        private IResultRepository _resultRepository;
        private IStandardDetailRepository _standardDetailRepository;
        private IStandardRepository _standardRepository;
        private IStrainGroupRepository _strainGroupRepository;
        private IStrainRepository _strainRepository;
        private IStrainTypeRepository _strainTypeRepository;
        private ISupplyProfileRepository _supplyProfileRepository;
        private ISupplyRepository _supplyRepository;
        private ITestProfileRepository _testProfileRepository;
        private ITestQCRepository _testQCRepository;
        private ITestTypeRepository _testTypeRepository;
        private IUseWithRepository _useWithRepository;

        #endregion

        #region Contrastor

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Properties

        public ILaboratoryRepository LaboratoryRepository
        {
            get { return _laboratoryRepository ??= new LaboratoryRepository(_dbContext); }
        }

        public IQCActionRepository  QCActionRepository 
        {
            get { return _qcActionRepository ??= new QCActionRepository(_dbContext); }
                
        }

        public ICategoryRepository CategoryRepository
        {
            get { return _categoryRepository ??= new CategoryRepository(_dbContext); }
        }

        public ILotSupplyRepository LotSupplyRepository
        {
            get { return _lotSupplyRepository ??= new LotSupplyRepository(_dbContext); }
        }

        public ILotTestRepository LotTestRepository
        {
            get { return _lotTestRepository ??= new LotTestRepository(_dbContext); }
        }

        public IMethodRepository MethodRepository
        {
            get { return _methodRepository ??= new MethodRepository(_dbContext); }
        }

        public IQCProfileDetailRepository QCProfileDetailRepository
        {
            get { return _qCProfileDetailRepository ??= new QCProfileDetailRepository(_dbContext); }
        }

        public IQCProfileRepository QCProfileRepository
        {
            get { return _qCProfileRepository ??= new QCProfileRepository(_dbContext); }
        }

        public IQCRepository QCRepository
        {
            get { return _qCRepository ??= new QCRepository(_dbContext); }
        }

        public IResultRepository ResultRepository
        {
            get { return _resultRepository ??= new ResultRepository(_dbContext); }
        }

        public IStandardDetailRepository StandardDetailRepository
        {
            get { return _standardDetailRepository ??= new StandardDetailRepository(_dbContext); }
        }

        public IStandardRepository StandardRepository
        {
            get { return _standardRepository ??= new StandardRepository(_dbContext); }
        }

        public IStrainGroupRepository StrainGroupRepository
        {
            get { return _strainGroupRepository ??= new StrainGroupRepository(_dbContext); }
        }

        public IStrainRepository StrainRepository
        {
            get { return _strainRepository ??= new StrainRepository(_dbContext); }
        }

        public IStrainTypeRepository StrainTypeRepository
        {
            get { return _strainTypeRepository ??= new StrainTypeRepository(_dbContext); }
        }

        public ISupplyProfileRepository SupplyProfileRepository
        {
            get { return _supplyProfileRepository ??= new SupplyProfileRepository(_dbContext); }
        }

        public ISupplyRepository SupplyRepository
        {
            get { return _supplyRepository ??= new SupplyRepository(_dbContext); }
        }

        public ITestProfileRepository TestProfileRepository
        {
            get { return _testProfileRepository ??= new TestProfileRepository(_dbContext); }
        }

        public ITestQCRepository TestQCRepository
        {
            get { return _testQCRepository ??= new TestQCRepository(_dbContext); }
        }

        public ITestTypeRepository TestTypeRepository
        {
            get { return _testTypeRepository ??= new TestTypeRepository(_dbContext); }
        }

        public IUseWithRepository UseWithRepository
        {
            get { return _useWithRepository ??= new UseWithRepository(_dbContext); }
        }

        #endregion

        #region Methods

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
        public void Rollback()
        {
            _dbContext.Dispose();
        }

        public async Task RollbackAsync()
        {
            await _dbContext.DisposeAsync();
        }

        #endregion
    }
}
