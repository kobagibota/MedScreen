using BaseLibrary.Dtos;
using BaseLibrary.Entities;
using BaseLibrary.Extentions;
using BaseLibrary.Interfaces;
using BaseLibrary.Respones;

namespace Server.Services
{
    #region Interface

    public interface ISupplyService
    {
        Task<ServiceResponse<IEnumerable<SupplyDto>>> GetAll();
        Task<ServiceResponse<SupplyDto?>> GetById(int supplyId);
        Task<ServiceResponse<SupplyDto>> Add(SupplyDto newSupply);
        Task<ServiceResponse<bool>> Update(int id, SupplyDto supplyDto);
        Task<ServiceResponse<bool>> Delete(int supplyId);
    }

    #endregion

    public class SupplyService : ISupplyService
    {
        #region Private properties

        public IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public SupplyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        public async Task<ServiceResponse<SupplyDto>> Add(SupplyDto request)
        {
            try
            {
                var method = await _unitOfWork.MethodRepository.GetAsync(x => x.Id == request.MethodId);
                if (request == null || method == null)
                {
                    return new ServiceResponse<SupplyDto>
                    {
                        Success = false,
                        Message = $"Thiếu thông tin hoá chất phụ."
                    };
                }

                var newSupply = new Supply
                {
                    SupplyName = request.SupplyName,
                    MethodId = request.MethodId,
                    Method = method
                };

                await _unitOfWork.SupplyRepository.AddAsync(newSupply);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<SupplyDto>
                {
                    Success = true,
                    Data = newSupply.ConvertToDto(),
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
                        errorMessage = "Xảy ra lỗi do trùng tên hoá chất phụ.";
                    }
                }

                return new ServiceResponse<SupplyDto>
                {
                    Success = false,
                    Message = errorMessage
                };
            }
        }

        public async Task<ServiceResponse<bool>> Delete(int supplyIdRequest)
        {
            try
            {
                var supplyEntity = await _unitOfWork.SupplyRepository.GetAsync(x => x.Id == supplyIdRequest);
                if (supplyEntity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Tìm không thấy hoá chất phụ cần xoá!"
                    };
                }

                _unitOfWork.SupplyRepository.Remove(supplyEntity);
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

        public async Task<ServiceResponse<IEnumerable<SupplyDto>>> GetAll()
        {
            try
            {
                var supplyList = await _unitOfWork.SupplyRepository.GetAllAsync(x => x.Method);

                return new ServiceResponse<IEnumerable<SupplyDto>>
                {
                    Success = true,
                    Data = supplyList.ConvertToDto()
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<IEnumerable<SupplyDto>>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy danh sách hoá chất phụ."
                };
            }
        }

        public async Task<ServiceResponse<SupplyDto?>> GetById(int supplyIdRequest)
        {
            try
            {
                var supplyEntity = await _unitOfWork.SupplyRepository.GetAsync(x => x.Id == supplyIdRequest, i => i.Method);
                if (supplyEntity == null)
                {
                    return new ServiceResponse<SupplyDto?>
                    {
                        Success = false,
                        Message = $"Không có hoá chất phụ nào với Id = {supplyIdRequest}."
                    };
                }

                return new ServiceResponse<SupplyDto?>
                {
                    Success = true,
                    Data = supplyEntity.ConvertToDto()
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<SupplyDto?>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy hoá chất phụ."
                };
            }
        }

        public async Task<ServiceResponse<bool>> Update(int id, SupplyDto supplyRequest)
        {
            try
            {
                var supplyEntity = await _unitOfWork.SupplyRepository.GetAsync(x => x.Id == id);
                if (supplyEntity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Không có hoá chất phụ nào với Id = {id}."
                    };
                }

                supplyEntity.SupplyName = supplyRequest.SupplyName;
                supplyEntity.MethodId = supplyRequest.MethodId;

                _unitOfWork.SupplyRepository.Update(supplyEntity);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<bool>
                {
                    Success = true,
                    Message = $"Đã cập nhật thành công.",
                };
            }
            catch (Exception ex)
            {
                string errorMessage = "Xảy ra lỗi trong quá trình cập nhật hoá chất phụ.";

                if (ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlException)
                {
                    int errorCode = sqlException.Number;
                    if (errorCode == 2627 || errorCode == 2601)
                    {
                        errorMessage = "Xảy ra lỗi do trùng tên hoá chất phụ.";
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
