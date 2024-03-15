using BaseLibrary.Dtos;
using BaseLibrary.Entities;
using BaseLibrary.Extentions;
using BaseLibrary.Interfaces;
using BaseLibrary.Respones;

namespace Server.Services
{
    #region Interface

    public interface IMethodService
    {
        Task<ServiceResponse<IEnumerable<MethodDto>>> GetAll();
        Task<ServiceResponse<MethodDto?>> GetById(int methodId);
        Task<ServiceResponse<MethodDto>> Add(MethodDto newmethod);
        Task<ServiceResponse<bool>> Update(int id, MethodDto methodDto);
        Task<ServiceResponse<bool>> Delete(int methodId);
    }

    #endregion

    public class MethodService : IMethodService
    {
        #region Private properties

        public IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public MethodService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        public async Task<ServiceResponse<MethodDto>> Add(MethodDto request)
        {
            try
            {
                if (request == null)
                {
                    return new ServiceResponse<MethodDto>
                    {
                        Success = false,
                        Message = $"Thiếu thông tin phương pháp."
                    };
                }

                var newMethod = new Method
                {
                    MethodName = request.MethodName
                };

                await _unitOfWork.MethodRepository.Add(newMethod);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<MethodDto>
                {
                    Success = true,
                    Data = newMethod.ConvertToDto(),
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
                        errorMessage = "Xảy ra lỗi do trùng tên phương pháp.";
                    }
                }

                return new ServiceResponse<MethodDto>
                {
                    Success = false,
                    Message = errorMessage
                };
            }
        }

        public async Task<ServiceResponse<bool>> Delete(int methodId)
        {
            try
            {
                var methodEntity = await _unitOfWork.MethodRepository.GetBy(x => x.Id == methodId);
                if (methodEntity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Tìm không thấy phương pháp cần xoá!"
                    };
                }

                _unitOfWork.MethodRepository.Remove(methodEntity);
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

        public async Task<ServiceResponse<IEnumerable<MethodDto>>> GetAll()
        {
            try
            {
                var methodList = await _unitOfWork.MethodRepository.GetAll();

                return new ServiceResponse<IEnumerable<MethodDto>>
                {
                    Success = true,
                    Data = methodList.ConvertToDto()
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<IEnumerable<MethodDto>>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy danh sách phương pháp."
                };
            }
        }

        public async Task<ServiceResponse<MethodDto?>> GetById(int methodId)
        {
            try
            {
                var methodEntity = await _unitOfWork.MethodRepository.GetBy(x => x.Id == methodId);
                if (methodEntity == null)
                {
                    return new ServiceResponse<MethodDto?>
                    {
                        Success = false,
                        Message = $"Không có phương pháp nào với Id = {methodId}."
                    };
                }

                return new ServiceResponse<MethodDto?>
                {
                    Success = true,
                    Data = methodEntity.ConvertToDto()
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<MethodDto?>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy phương pháp."
                };
            }
        }

        public async Task<ServiceResponse<bool>> Update(int id, MethodDto methodRequest)
        {
            try
            {
                var methodEntity = await _unitOfWork.MethodRepository.GetBy(x => x.Id == id);
                if (methodEntity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Không có phương pháp nào với Id = {id}."
                    };
                }

                methodEntity.MethodName = methodRequest.MethodName;

                _unitOfWork.MethodRepository.Update(methodEntity);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<bool>
                {
                    Success = true,
                    Message = $"Đã cập nhật thành công.",
                };
            }
            catch (Exception ex)
            {
                string errorMessage = "Xảy ra lỗi trong quá trình cập nhật phương pháp.";

                if (ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlException)
                {
                    int errorCode = sqlException.Number;
                    if (errorCode == 2627 || errorCode == 2601)
                    {
                        errorMessage = "Xảy ra lỗi do trùng phương pháp.";
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
