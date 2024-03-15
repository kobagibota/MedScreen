using BaseLibrary.Dtos;
using BaseLibrary.Entities;
using BaseLibrary.Extentions;
using BaseLibrary.Interfaces;
using BaseLibrary.Respones;

namespace Server.Services
{
    #region Interface

    public interface IStandardService
    {
        Task<ServiceResponse<IEnumerable<StandardDto>>> GetAll();
        Task<ServiceResponse<StandardDto?>> GetById(int standardId);
        Task<ServiceResponse<StandardDto>> Add(StandardDto newstandard);
        Task<ServiceResponse<bool>> Update(int id, StandardDto standardDto);
        Task<ServiceResponse<bool>> Delete(int standardId);
    }

    #endregion

    public class StandardService: IStandardService
    {
        #region Private properties

        public IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public StandardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        public async Task<ServiceResponse<StandardDto>> Add(StandardDto request)
        {
            try
            {
                if (request == null)
                {
                    return new ServiceResponse<StandardDto>
                    {
                        Success = false,
                        Message = $"Thiếu thông tin chuẩn phiên giải."
                    };
                }

                var newStandard = new Standard
                {
                    StandardName = request.StandardName
                };

                await _unitOfWork.StandardRepository.Add(newStandard);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<StandardDto>
                {
                    Success = true,
                    Data = newStandard.ConvertToDto(),
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
                        errorMessage = "Xảy ra lỗi do trùng tên chuẩn phiên giải.";
                    }
                }

                return new ServiceResponse<StandardDto>
                {
                    Success = false,
                    Message = errorMessage
                };
            }
        }

        public async Task<ServiceResponse<bool>> Delete(int standardId)
        {
            try
            {
                var standardEntity = await _unitOfWork.StandardRepository.GetBy(x => x.Id == standardId);
                if (standardEntity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Tìm không thấy chuẩn phiên giải cần xoá!"
                    };
                }

                _unitOfWork.StandardRepository.Remove(standardEntity);
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

        public async Task<ServiceResponse<IEnumerable<StandardDto>>> GetAll()
        {
            try
            {
                var standardList = await _unitOfWork.StandardRepository.GetAll();

                return new ServiceResponse<IEnumerable<StandardDto>>
                {
                    Success = true,
                    Data = standardList.ConvertToDto()
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<IEnumerable<StandardDto>>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy danh sách chuẩn phiên giải."
                };
            }
        }

        public async Task<ServiceResponse<StandardDto?>> GetById(int standardId)
        {
            try
            {
                var standardEntity = await _unitOfWork.StandardRepository.GetBy(x => x.Id == standardId);
                if (standardEntity == null)
                {
                    return new ServiceResponse<StandardDto?>
                    {
                        Success = false,
                        Message = $"Không có chuẩn phiên giải nào với Id = {standardId}."
                    };
                }

                return new ServiceResponse<StandardDto?>
                {
                    Success = true,
                    Data = standardEntity.ConvertToDto()
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<StandardDto?>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy chuẩn phiên giải."
                };
            }
        }

        public async Task<ServiceResponse<bool>> Update(int id, StandardDto request)
        {
            try
            {
                var standardEntity = await _unitOfWork.StandardRepository.GetBy(x => x.Id == id);
                if (standardEntity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Không có chuẩn phiên giải nào với Id = {id}."
                    };
                }

                standardEntity.StandardName = request.StandardName;

                _unitOfWork.StandardRepository.Update(standardEntity);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<bool>
                {
                    Success = true,
                    Message = $"Đã cập nhật thành công.",
                };
            }
            catch (Exception ex)
            {
                string errorMessage = "Xảy ra lỗi trong quá trình cập nhật chuẩn phiên giải.";

                if (ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlException)
                {
                    int errorCode = sqlException.Number;
                    if (errorCode == 2627 || errorCode == 2601)
                    {
                        errorMessage = "Xảy ra lỗi do trùng tên chuẩn phiên giải.";
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
