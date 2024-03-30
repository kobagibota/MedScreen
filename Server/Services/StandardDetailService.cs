using BaseLibrary.Dtos;
using BaseLibrary.Entities;
using BaseLibrary.Extentions;
using BaseLibrary.Interfaces;
using BaseLibrary.Respones;

namespace Server.Services
{
    #region Interface

    public interface IStandardDetailService
    {
        Task<ServiceResponse<IEnumerable<StandardDetailDto>>> GetListByStandardId(int standardId);
        Task<ServiceResponse<StandardDetailDto?>> GetById(int Id);
        Task<ServiceResponse<StandardDetailDto>> Add(StandardDetailDto newStandardDetail);
        Task<ServiceResponse<bool>> Update(StandardDetailDto request);
        Task<ServiceResponse<bool>> Delete(int Id);
    }

    #endregion

    public class StandardDetailService : IStandardDetailService
    {
        #region Private properties

        public IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public StandardDetailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        public async Task<ServiceResponse<StandardDetailDto>> Add(StandardDetailDto request)
        {
            try
            {
                var method = await _unitOfWork.MethodRepository.GetBy(x => x.Id == request.MethodId);
                var category = await _unitOfWork.CategoryRepository.GetBy(x => x.Id == request.CategoryId);
                var standard = await _unitOfWork.StandardRepository.GetBy(x => x.Id == request.StandardId);
                var testQC = await _unitOfWork.TestQCRepository.GetBy(x => x.Id == request.TestQCId);
                var strain = await _unitOfWork.StrainRepository.GetBy(x => x.Id == request.StrainId);

                if (request == null || method == null || category == null || standard == null || testQC == null || strain == null)
                {
                    return new ServiceResponse<StandardDetailDto>
                    {
                        Success = false,
                        Message = $"Thiếu thông tin chi tiết."
                    };
                }

                var newStandardDetail = new StandardDetail
                {
                    StandardId = request.StandardId,
                    MethodId = request.MethodId,
                    CategoryId = request.CategoryId,
                    TestQCId = request.TestQCId,
                    StrainId = request.StrainId,
                    Concentration = request.Concentration,
                    Threshold = request.Threshold,
                    LimitMin = request.LimitMin,
                    LimitMax = request.LimitMax,
                    Normal = request.Normal,
                    Qualitative = request.Qualitative,
                    ResultType = request.ResultType,
                    Standard = standard,
                    Category = category,
                    Method = method,
                    TestQC = testQC,
                    Strain = strain
                };

                await _unitOfWork.StandardDetailRepository.Add(newStandardDetail);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<StandardDetailDto>
                {
                    Success = true,
                    Data = newStandardDetail.ConvertToDto(),
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
                        errorMessage = "Xảy ra lỗi do trùng tên cấu hình QC.";
                    }
                }

                return new ServiceResponse<StandardDetailDto>
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
                var entity = await _unitOfWork.StandardDetailRepository.GetBy(x => x.Id == id);
                if (entity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Tìm không thấy chi tiết cần xoá!"
                    };
                }

                _unitOfWork.StandardDetailRepository.Remove(entity);
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

        public async Task<ServiceResponse<IEnumerable<StandardDetailDto>>> GetListByStandardId(int standardId)
        {
            try
            {
                var list = await _unitOfWork.StandardDetailRepository.GetListBy(
                    x => x.StandardId == standardId,
                    m => m.Method, c => c.Category, s => s.Strain, v => v.Strain, t => t.TestQC);

                return new ServiceResponse<IEnumerable<StandardDetailDto>>
                {
                    Success = true,
                    Data = list.ConvertToDto()
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<IEnumerable<StandardDetailDto>>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy danh sách chi tiết."
                };
            }
        }

        public async Task<ServiceResponse<StandardDetailDto?>> GetById(int id)
        {
            try
            {
                var entity = await _unitOfWork.StandardDetailRepository.GetBy(
                    x => x.Id == id,
                    m => m.Method, c => c.Category, s => s.Strain, v => v.Strain, t => t.TestQC);
                if (entity == null)
                {
                    return new ServiceResponse<StandardDetailDto?>
                    {
                        Success = false,
                        Message = $"Không có chi tiết cần tìm."
                    };
                }

                return new ServiceResponse<StandardDetailDto?>
                {
                    Success = true,
                    Data = entity.ConvertToDto()
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<StandardDetailDto?>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy chi tiết."
                };
            }
        }

        public async Task<ServiceResponse<bool>> Update(StandardDetailDto request)
        {
            try
            {
                var entity = await _unitOfWork.StandardDetailRepository.GetBy(
                    x => x.Id == request.Id,
                    m => m.Method, c => c.Category, s => s.Strain, v => v.Strain, t => t.TestQC);
                if (entity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Không có chi tiết nào cần tìm."
                    };
                }

                entity.MethodId = request.MethodId;
                entity.CategoryId = request.CategoryId;
                entity.TestQCId = request.TestQCId;
                entity.StrainId = request.StrainId;
                entity.Concentration = request.Concentration;
                entity.Threshold = request.Threshold;
                entity.LimitMin = request.LimitMin;
                entity.LimitMax = request.LimitMax;
                entity.Normal = request.Normal;
                entity.Qualitative = request.Qualitative;
                entity.ResultType = request.ResultType;

                _unitOfWork.StandardDetailRepository.Update(entity);
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
                    Message = $"Xảy ra lỗi trong quá trình cập nhật chi tiết."
                };
            }
        }

        #endregion
    }
}
