using BaseLibrary.Dtos;
using BaseLibrary.Entities;
using BaseLibrary.Extentions;
using BaseLibrary.Interfaces;
using BaseLibrary.Respones;

namespace Server.Services
{
    #region Interface

    public interface IUseWithService
    {
        Task<ServiceResponse<IEnumerable<UseWithDto>>> GetByQCId(int qcId);
        Task<ServiceResponse<UseWithDto?>> GetById(int useWithId);
        Task<ServiceResponse<UseWithDto>> Add(UseWithDto newUseWith);
        Task<ServiceResponse<bool>> Update(UseWithDto useWithDto);
        Task<ServiceResponse<bool>> Delete(int useWithId);
    }

    #endregion

    public class UseWithService : IUseWithService
    {
        #region Private properties

        public IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public UseWithService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        public async Task<ServiceResponse<UseWithDto>> Add(UseWithDto request)
        {
            try
            {
                var qc = await _unitOfWork.QCRepository.GetBy(x => x.Id == request.QCId);
                var supply = await _unitOfWork.SupplyRepository.GetBy(x => x.Id == request.SupplyId);
                var lotSupply = await _unitOfWork.LotSupplyRepository.GetBy(x => x.Id == request.LotSupplyId);
                if (request == null || supply == null || lotSupply == null || qc == null)
                {
                    return new ServiceResponse<UseWithDto>
                    {
                        Success = false,
                        Message = $"Thiếu thông tin hoá chất phụ đi kèm."
                    };
                }

                var newUseWith = new UseWith
                {
                    QCId = request.QCId,
                    SupplyId = request.SupplyId,
                    LotSupplyId = request.LotSupplyId,
                    QC = qc,
                    Supply = supply,
                    LotSupply = lotSupply
                };

                await _unitOfWork.UseWithRepository.Add(newUseWith);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<UseWithDto>
                {
                    Success = true,
                    Data = newUseWith.ConvertToDto(),
                    Message = $"Đã thêm thành công!"
                };
            }
            catch (Exception ex)
            {
                string errorMessage = "Xảy ra lỗi trong quá trình thêm.";

                if (ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlException)
                {
                    int errorCode = sqlException.Number;
                    if (errorCode == 2627 || errorCode == 2601)
                    {
                        errorMessage = "Xảy ra lỗi do trùng hoá chất phụ.";
                    }
                }

                return new ServiceResponse<UseWithDto>
                {
                    Success = false,
                    Message = errorMessage
                };
            }
        }

        public async Task<ServiceResponse<bool>> Delete(int useWithId)
        {
            try
            {
                var useWithEntity = await _unitOfWork.UseWithRepository.GetBy(x => x.Id == useWithId);
                if (useWithEntity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Tìm không thấy hoá chất phụ cần xoá!"
                    };
                }

                _unitOfWork.UseWithRepository.Remove(useWithEntity);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<bool>
                {
                    Success = true,
                    Message = $"Đã xoá thành công!"
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình xoá."
                };
            }
        }

        public async Task<ServiceResponse<IEnumerable<UseWithDto>>> GetByQCId(int qcId)
        {
            try
            {
                var useWithList = await _unitOfWork.UseWithRepository.GetListBy(x => x.QCId == qcId, s => s.Supply, l => l.LotSupply, q => q.QC);

                return new ServiceResponse<IEnumerable<UseWithDto>>
                {
                    Success = true,
                    Data = useWithList.ConvertToDto()
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<IEnumerable<UseWithDto>>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy danh sách hoá chất phụ đi kèm."
                };
            }
        }

        public async Task<ServiceResponse<UseWithDto?>> GetById(int useWithId)
        {
            try
            {
                var useWithEntity = await _unitOfWork.UseWithRepository.GetBy(x => x.Id == useWithId, s => s.Supply, l => l.LotSupply, q => q.QC);
                if (useWithEntity == null)
                {
                    return new ServiceResponse<UseWithDto?>
                    {
                        Success = false,
                        Message = $"Không có hoá chất phụ đi kèm nào với Id = {useWithId}."
                    };
                }

                return new ServiceResponse<UseWithDto?>
                {
                    Success = true,
                    Data = useWithEntity.ConvertToDto()
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<UseWithDto?>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy thông tin hoá chất phụ đi kèm."
                };
            }
        }

        public async Task<ServiceResponse<bool>> Update(UseWithDto request)
        {
            try
            {
                var useWithEntity = await _unitOfWork.UseWithRepository.GetBy(x => x.Id == request.Id, s => s.Supply, l => l.LotSupply, q => q.QC);
                if (useWithEntity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Không có hoá chất phụ đi kèm nào với Id = {request.Id}."
                    };
                }

                useWithEntity.LotSupplyId = request.LotSupplyId;
                useWithEntity.SupplyId = request.SupplyId;

                _unitOfWork.UseWithRepository.Update(useWithEntity);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<bool>
                {
                    Success = true,
                    Message = $"Đã cập nhật thành công.",
                };
            }
            catch (Exception ex)
            {
                string errorMessage = "Xảy ra lỗi trong quá trình cập nhật hoá chất phụ đi kèm.";

                if (ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlException)
                {
                    int errorCode = sqlException.Number;
                    if (errorCode == 2627 || errorCode == 2601)
                    {
                        errorMessage = "Xảy ra lỗi do trùng tên hoá chất phụ đi kèm.";
                    }
                }

                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = errorMessage
                };
            }
        }

        #endregion
    }
}
