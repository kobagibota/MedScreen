using BaseLibrary.Dtos;
using BaseLibrary.Entities;
using BaseLibrary.Extentions;
using BaseLibrary.Interfaces;
using BaseLibrary.Respones;

namespace Server.Services
{
    #region Interface

    public interface IQCProfileService
    {
        Task<ServiceResponse<IEnumerable<QCProfileDto>>> GetListByUserId(string userId);
        Task<ServiceResponse<QCProfileDto?>> GetById(int Id);
        Task<ServiceResponse<QCProfileDto>> Add(QCProfileDto newQCProfile);
        Task<ServiceResponse<bool>> Update(QCProfileDto request);
        Task<ServiceResponse<bool>> Delete(int Id);
    }

    #endregion

    public class QCProfileService : IQCProfileService
    {
        #region Private properties

        public IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public QCProfileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        public async Task<ServiceResponse<QCProfileDto>> Add(QCProfileDto request)
        {
            try
            {
                var method = await _unitOfWork.MethodRepository.GetBy(x => x.Id == request.MethodId);
                var category = await _unitOfWork.CategoryRepository.GetBy(x => x.Id == request.CategoryId);
                if (request == null || method == null || category == null)
                {
                    return new ServiceResponse<QCProfileDto>
                    {
                        Success = false,
                        Message = $"Thiếu thông tin cấu hình QC."
                    };
                }

                var newQCProfile = new QCProfile
                {
                    LabId = request.LabId,
                    CategoryId = request.CategoryId,
                    MethodId = request.MethodId,
                    QCName = request.QCName,
                    Hide = request.Hide,
                    Category = category,
                    Method = method
                };

                await _unitOfWork.QCProfileRepository.Add(newQCProfile);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<QCProfileDto>
                {
                    Success = true,
                    Data = newQCProfile.ConvertToDto(),
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

                return new ServiceResponse<QCProfileDto>
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
                var entity = await _unitOfWork.QCProfileRepository.GetBy(x => x.Id == id);
                if (entity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Tìm không thấy cấu hình QC cần xoá!"
                    };
                }

                _unitOfWork.QCProfileRepository.Remove(entity);
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

        public async Task<ServiceResponse<IEnumerable<QCProfileDto>>> GetListByUserId(string userId)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetBy(x => x.Id.ToString() == userId);
                if (user == null)
                {
                    return new ServiceResponse<IEnumerable<QCProfileDto>>
                    {
                        Success = false,
                        Message = $"Xảy ra lỗi trong quá trình lấy danh sách cấu hình QC."
                    };
                }
                var list = await _unitOfWork.QCProfileRepository.GetListBy(x => x.LabId == user.LabId, m => m.Method, c => c.Category);

                return new ServiceResponse<IEnumerable<QCProfileDto>>
                {
                    Success = true,
                    Data = list.ConvertToDto()
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<IEnumerable<QCProfileDto>>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy danh sách cấu hình QC."
                };
            }
        }

        public async Task<ServiceResponse<QCProfileDto?>> GetById(int id)
        {
            try
            {
                var entity = await _unitOfWork.QCProfileRepository.GetBy(x => x.Id == id, c => c.Category, m => m.Method);
                if (entity == null)
                {
                    return new ServiceResponse<QCProfileDto?>
                    {
                        Success = false,
                        Message = $"Không có cấu hình QC cần tìm."
                    };
                }

                return new ServiceResponse<QCProfileDto?>
                {
                    Success = true,
                    Data = entity.ConvertToDto()
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<QCProfileDto?>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy cấu hình QC."
                };
            }
        }

        public async Task<ServiceResponse<bool>> Update(QCProfileDto request)
        {
            try
            {
                var entity = await _unitOfWork.QCProfileRepository.GetBy(x => x.Id == request.Id);
                if (entity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Không có cấu hình QC nào cần tìm."
                    };
                }

                entity.CategoryId = request.CategoryId;
                entity.MethodId = request.MethodId;
                entity.QCName = request.QCName;
                entity.Hide = request.Hide;

                _unitOfWork.QCProfileRepository.Update(entity);
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
                    Message = $"Xảy ra lỗi trong quá trình cập nhật cấu hình QC."
                };
            }
        }

        #endregion
    }
}
