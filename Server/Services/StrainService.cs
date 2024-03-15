using BaseLibrary.Dtos;
using BaseLibrary.Entities;
using BaseLibrary.Extentions;
using BaseLibrary.Interfaces;
using BaseLibrary.Respones;

namespace Server.Services
{
    #region Interface

    public interface IStrainService
    {
        Task<ServiceResponse<IEnumerable<StrainDto>>> GetAll();
        Task<ServiceResponse<StrainDto?>> GetById(int strainId);
        Task<ServiceResponse<StrainDto>> Add(StrainDto newStrain);
        Task<ServiceResponse<bool>> Update(int id, StrainDto strainDto);
        Task<ServiceResponse<bool>> Delete(int strainId);
    }

    #endregion

    public class StrainService : IStrainService
    {
        #region Private properties

        public IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public StrainService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        public async Task<ServiceResponse<StrainDto>> Add(StrainDto request)
        {
            try
            {
                var strainGroup = await _unitOfWork.StrainGroupRepository.GetBy(x => x.Id == request.GroupId);
                if (request == null || strainGroup == null)
                {
                    return new ServiceResponse<StrainDto>
                    {
                        Success = false,
                        Message = $"Thiếu thông tin chủng."
                    };
                }

                var newStrain = new Strain
                {
                    StrainName = request.StrainName,
                    GroupId = request.GroupId,
                    StrainGroup = strainGroup
                };

                await _unitOfWork.StrainRepository.Add(newStrain);
                await _unitOfWork.CommitAsync();

                var listStrainType = await _unitOfWork.StrainTypeRepository.GetAll();

                return new ServiceResponse<StrainDto>
                {
                    Success = true,
                    Data = newStrain.ConvertToDto(listStrainType.ToList()),
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
                        errorMessage = "Xảy ra lỗi do trùng tên chủng.";
                    }
                }

                return new ServiceResponse<StrainDto>
                {
                    Success = false,
                    Message = errorMessage
                };
            }
        }

        public async Task<ServiceResponse<bool>> Delete(int strainId)
        {
            try
            {
                var strainEntity = await _unitOfWork.StrainRepository.GetBy(x => x.Id == strainId);
                if (strainEntity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Tìm không thấy chủng cần xoá!"
                    };
                }

                _unitOfWork.StrainRepository.Remove(strainEntity);
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

        public async Task<ServiceResponse<IEnumerable<StrainDto>>> GetAll()
        {
            try
            {
                var strainList = await _unitOfWork.StrainRepository.GetAll(x => x.StrainGroup);
                var strainTypeList = await _unitOfWork.StrainTypeRepository.GetAll();

                return new ServiceResponse<IEnumerable<StrainDto>>
                {
                    Success = true,
                    Data = strainList.ConvertToDto(strainTypeList.ToList())
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<IEnumerable<StrainDto>>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy danh sách chủng."
                };
            }
        }

        public async Task<ServiceResponse<StrainDto?>> GetById(int strainId)
        {
            try
            {
                var strainEntity = await _unitOfWork.StrainRepository.GetBy(x => x.Id == strainId, i => i.StrainGroup);
                if (strainEntity == null)
                {
                    return new ServiceResponse<StrainDto?>
                    {
                        Success = false,
                        Message = $"Không có chủng nào với Id = {strainId}."
                    };
                }

                var listStrainType = await _unitOfWork.StrainTypeRepository.GetAll();

                return new ServiceResponse<StrainDto?>
                {
                    Success = true,
                    Data = strainEntity.ConvertToDto(listStrainType.ToList())
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<StrainDto?>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy chủng."
                };
            }
        }

        public async Task<ServiceResponse<bool>> Update(int id, StrainDto request)
        {
            try
            {
                var strainEntity = await _unitOfWork.StrainRepository.GetBy(x => x.Id == id);
                if (strainEntity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Không có chủng nào với Id = {id}."
                    };
                }

                strainEntity.StrainName = request.StrainName;
                strainEntity.GroupId = request.GroupId;

                _unitOfWork.StrainRepository.Update(strainEntity);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<bool>
                {
                    Success = true,
                    Message = $"Đã cập nhật thành công.",
                };
            }
            catch (Exception ex)
            {
                string errorMessage = "Xảy ra lỗi trong quá trình cập nhật chủng.";

                if (ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlException)
                {
                    int errorCode = sqlException.Number;
                    if (errorCode == 2627 || errorCode == 2601)
                    {
                        errorMessage = "Xảy ra lỗi do trùng tên chủng.";
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
