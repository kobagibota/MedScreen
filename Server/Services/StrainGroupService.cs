using BaseLibrary.Dtos;
using BaseLibrary.Entities;
using BaseLibrary.Extentions;
using BaseLibrary.Interfaces;
using BaseLibrary.Respones;

namespace Server.Services
{
    #region Interface

    public interface IStrainGroupService
    {
        Task<ServiceResponse<IEnumerable<StrainGroupDto>>> GetAll();
        Task<ServiceResponse<StrainGroupDto?>> GetById(int strainGroupId);
        Task<ServiceResponse<StrainGroupDto>> Add(StrainGroupDto newStrainGroup);
        Task<ServiceResponse<bool>> Update(int id, StrainGroupDto strainGroupDto);
        Task<ServiceResponse<bool>> Delete(int strainGroupId);
    }

    #endregion

    public class StrainGroupService : IStrainGroupService
    {
        #region Private properties

        public IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public StrainGroupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        public async Task<ServiceResponse<StrainGroupDto>> Add(StrainGroupDto request)
        {
            try
            {
                if (request == null)
                {
                    return new ServiceResponse<StrainGroupDto>
                    {
                        Success = false,
                        Message = $"Thiếu thông tin nhóm chủng."
                    };
                }

                var newStrainGroup = new StrainGroup
                {
                    GroupName = request.GroupName
                };

                await _unitOfWork.StrainGroupRepository.Add(newStrainGroup);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<StrainGroupDto>
                {
                    Success = true,
                    Data = newStrainGroup.ConvertToDto(),
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
                        errorMessage = "Xảy ra lỗi do trùng tên nhóm chủng.";
                    }
                }

                return new ServiceResponse<StrainGroupDto>
                {
                    Success = false,
                    Message = errorMessage
                };
            }
        }

        public async Task<ServiceResponse<bool>> Delete(int strainGroupId)
        {
            try
            {
                var strainGroupEntity = await _unitOfWork.StrainGroupRepository.GetBy(x => x.Id == strainGroupId);
                if (strainGroupEntity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Tìm không thấy nhóm chủng cần xoá!"
                    };
                }

                _unitOfWork.StrainGroupRepository.Remove(strainGroupEntity);
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

        public async Task<ServiceResponse<IEnumerable<StrainGroupDto>>> GetAll()
        {
            try
            {
                var strainGroupList = await _unitOfWork.StrainGroupRepository.GetAll();

                return new ServiceResponse<IEnumerable<StrainGroupDto>>
                {
                    Success = true,
                    Data = strainGroupList.ConvertToDto()
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<IEnumerable<StrainGroupDto>>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy danh sách nhóm chủng."
                };
            }
        }

        public async Task<ServiceResponse<StrainGroupDto?>> GetById(int strainGroupId)
        {
            try
            {
                var strainGroupEntity = await _unitOfWork.StrainGroupRepository.GetBy(x => x.Id == strainGroupId);
                if (strainGroupEntity == null)
                {
                    return new ServiceResponse<StrainGroupDto?>
                    {
                        Success = false,
                        Message = $"Không có nhóm chủng nào với Id = {strainGroupId}."
                    };
                }

                return new ServiceResponse<StrainGroupDto?>
                {
                    Success = true,
                    Data = strainGroupEntity.ConvertToDto()
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<StrainGroupDto?>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy nhóm chủng."
                };
            }
        }

        public async Task<ServiceResponse<bool>> Update(int id, StrainGroupDto request)
        {
            try
            {
                var strainGroupEntity = await _unitOfWork.StrainGroupRepository.GetBy(x => x.Id == id);
                if (strainGroupEntity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Không có nhóm chủng nào với Id = {id}."
                    };
                }

                strainGroupEntity.GroupName = request.GroupName;

                _unitOfWork.StrainGroupRepository.Update(strainGroupEntity);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<bool>
                {
                    Success = true,
                    Message = $"Đã cập nhật thành công.",
                };
            }
            catch (Exception ex)
            {
                string errorMessage = "Xảy ra lỗi trong quá trình cập nhật nhóm chủng.";

                if (ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlException)
                {
                    int errorCode = sqlException.Number;
                    if (errorCode == 2627 || errorCode == 2601)
                    {
                        errorMessage = "Xảy ra lỗi do trùng tên nhóm chủng.";
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
