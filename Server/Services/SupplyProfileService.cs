using BaseLibrary.Dtos;
using BaseLibrary.Entities;
using BaseLibrary.Extentions;
using BaseLibrary.Interfaces;
using BaseLibrary.Respones;

namespace Server.Services
{
    #region Interface

    public interface ISupplyProfileService
    {
        Task<ServiceResponse<IEnumerable<SupplyProfileDto>>> GetByQCProfileId(int qCProfileId);
        Task<ServiceResponse<SupplyProfileDto?>> GetById(int Id);
        Task<ServiceResponse<SupplyProfileDto>> Add(SupplyProfileDto newSupplyProfile);
        Task<ServiceResponse<bool>> Update(SupplyProfileDto request);
        Task<ServiceResponse<bool>> Delete(int Id);
    }

    #endregion

    public class SupplyProfileService : ISupplyProfileService
    {
        #region Private properties

        public IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public SupplyProfileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        public async Task<ServiceResponse<SupplyProfileDto>> Add(SupplyProfileDto request)
        {
            try
            {
                var qCProfile = await _unitOfWork.QCProfileRepository.GetBy(x => x.Id == request.QCProfileId);
                var supply = await _unitOfWork.SupplyRepository.GetBy(x => x.Id == request.SupplyId);
                if (request == null || qCProfile == null || supply == null)
                {
                    return new ServiceResponse<SupplyProfileDto>
                    {
                        Success = false,
                        Message = $"Thiếu thông tin hoá chất phụ."
                    };
                }

                var newSupplyProfile = new SupplyProfile
                {
                    QCProfileId = request.QCProfileId,
                    SupplyId = request.SupplyId,
                    SortOrder = request.SortOrder,
                    InUse = request.InUse,
                    Supply = supply,
                    QCProfile = qCProfile
                };

                await _unitOfWork.SupplyProfileRepository.Add(newSupplyProfile);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<SupplyProfileDto>
                {
                    Success = true,
                    Data = newSupplyProfile.ConvertToDto(),
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
                        errorMessage = "Xảy ra lỗi do trùng loại chủng.";
                    }
                }

                return new ServiceResponse<SupplyProfileDto>
                {
                    Success = false,
                    Message = errorMessage
                };
            }
        }

        public async Task<ServiceResponse<bool>> Delete(int id)
        {
            try
            {
                var entity = await _unitOfWork.SupplyProfileRepository.GetBy(x => x.Id == id);
                if (entity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Tìm không thấy hoá chất phụ cần xoá!"
                    };
                }

                _unitOfWork.SupplyProfileRepository.Remove(entity);
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

        public async Task<ServiceResponse<IEnumerable<SupplyProfileDto>>> GetByQCProfileId(int qCProfileId)
        {
            try
            {
                var list = await _unitOfWork.SupplyProfileRepository.GetListBy(x => x.QCProfileId == qCProfileId, q => q.QCProfile, s => s.Supply);

                return new ServiceResponse<IEnumerable<SupplyProfileDto>>
                {
                    Success = true,
                    Data = list.ConvertToDto()
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<IEnumerable<SupplyProfileDto>>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy danh sách hoá chất phụ."
                };
            }
        }

        public async Task<ServiceResponse<SupplyProfileDto?>> GetById(int id)
        {
            try
            {
                var entity = await _unitOfWork.SupplyProfileRepository.GetBy(x => x.Id == id, q => q.QCProfile, s => s.Supply);
                if (entity == null)
                {
                    return new ServiceResponse<SupplyProfileDto?>
                    {
                        Success = false,
                        Message = $"Không có hoá chất phụ cần tìm."
                    };
                }

                return new ServiceResponse<SupplyProfileDto?>
                {
                    Success = true,
                    Data = entity.ConvertToDto()
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<SupplyProfileDto?>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy hoá chất phụ."
                };
            }
        }

        public async Task<ServiceResponse<bool>> Update(SupplyProfileDto request)
        {
            try
            {
                var entity = await _unitOfWork.SupplyProfileRepository.GetBy(x => x.Id == request.Id);
                if (entity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Không có hoá chất phụ nào cho QC cần tìm."
                    };
                }

                entity.QCProfileId = request.QCProfileId;
                entity.SupplyId = request.SupplyId;
                entity.SortOrder = request.SortOrder;
                entity.InUse = request.InUse;

                _unitOfWork.SupplyProfileRepository.Update(entity);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<bool>
                {
                    Success = true,
                    Message = $"Đã cập nhật thành công.",
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình cập nhật hoá chất phụ cho QC."
                };
            }
        }

        #endregion
    }
}
