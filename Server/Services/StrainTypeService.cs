using BaseLibrary.Dtos;
using BaseLibrary.Entities;
using BaseLibrary.Extentions;
using BaseLibrary.Interfaces;
using BaseLibrary.Respones;

namespace Server.Services
{
    #region Interface

    public interface IStrainTypeService
    {
        Task<ServiceResponse<IEnumerable<StrainTypeDto>>> GetByStrainId(int strainId);
        Task<ServiceResponse<StrainTypeDto?>> GetById(int Id);
        Task<ServiceResponse<StrainTypeDto>> Add(StrainTypeDto newStrainType);
        Task<ServiceResponse<bool>> Update(StrainTypeDto request);
        Task<ServiceResponse<bool>> Delete(int Id);
    }

    #endregion

    public class StrainTypeService : IStrainTypeService
    {
        #region Private properties

        public IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public StrainTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        public async Task<ServiceResponse<StrainTypeDto>> Add(StrainTypeDto request)
        {
            try
            {
                var strain = await _unitOfWork.StrainRepository.GetBy(x => x.Id == request.StrainId);
                var category = await _unitOfWork.CategoryRepository.GetBy(x => x.Id == request.CategoryId);
                if (request == null || strain == null || category == null)
                {
                    return new ServiceResponse<StrainTypeDto>
                    {
                        Success = false,
                        Message = $"Thiếu thông tin loại chủng."
                    };
                }

                var newStrainType = new StrainType
                {
                    StrainId = request.StrainId,
                    CategoryId = request.CategoryId,
                    Strain = strain,
                    Category = category
                };

                await _unitOfWork.StrainTypeRepository.Add(newStrainType);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<StrainTypeDto>
                {
                    Success = true,
                    Data = newStrainType.ConvertToDto(),
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

                return new ServiceResponse<StrainTypeDto>
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
                var entity = await _unitOfWork.StrainTypeRepository.GetBy(x => x.Id == id);
                if (entity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Tìm không thấy loại chủng cần xoá!"
                    };
                }

                _unitOfWork.StrainTypeRepository.Remove(entity);
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

        public async Task<ServiceResponse<IEnumerable<StrainTypeDto>>> GetByStrainId(int strainId)
        {
            try
            {
                var list = await _unitOfWork.StrainTypeRepository.GetListBy(x => x.StrainId == strainId, s => s.Strain, c => c.Category);

                return new ServiceResponse<IEnumerable<StrainTypeDto>>
                {
                    Success = true,
                    Data = list.ConvertToDto()
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<IEnumerable<StrainTypeDto>>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy danh sách loại chủng."
                };
            }
        }

        public async Task<ServiceResponse<StrainTypeDto?>> GetById(int id)
        {
            try
            {
                var entity = await _unitOfWork.StrainTypeRepository.GetBy(x => x.Id == id, c => c.Category, s => s.Strain);
                if (entity == null)
                {
                    return new ServiceResponse<StrainTypeDto?>
                    {
                        Success = false,
                        Message = $"Không có loại chủng cần tìm."
                    };
                }

                return new ServiceResponse<StrainTypeDto?>
                {
                    Success = true,
                    Data = entity.ConvertToDto()
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<StrainTypeDto?>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy loại chủng."
                };
            }
        }

        public async Task<ServiceResponse<bool>> Update(StrainTypeDto request)
        {
            try
            {
                var entity = await _unitOfWork.StrainTypeRepository.GetBy(x => x.StrainId == request.StrainId && x.CategoryId == request.CategoryId);
                if (entity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Không có loại chủng nào cần tìm."
                    };
                }

                entity.StrainId = request.StrainId;
                entity.CategoryId = request.CategoryId;
                entity.InUse = request.InUse;

                _unitOfWork.StrainTypeRepository.Update(entity);
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
                    Message = $"Xảy ra lỗi trong quá trình cập nhật loại chủng."
                };
            }
        }

        #endregion
    }
}
