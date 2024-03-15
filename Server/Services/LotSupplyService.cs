using BaseLibrary.Dtos;
using BaseLibrary.Entities;
using BaseLibrary.Extentions;
using BaseLibrary.Interfaces;
using BaseLibrary.Respones;

namespace Server.Services
{
    #region Interface

    public interface ILotSupplyService
    {
        Task<ServiceResponse<IEnumerable<LotSupplyDto>>> GetAll();
        Task<ServiceResponse<LotSupplyDto?>> GetById(int lotSupplyId);
        Task<ServiceResponse<LotSupplyDto>> Add(LotSupplyDto newLotSupply);
        Task<ServiceResponse<bool>> Update(int id, LotSupplyDto lotSupplyDto);
        Task<ServiceResponse<bool>> Delete(int lotSupplyId);

        Task<ServiceResponse<bool>> SetDefault(int lotSupplyId);
    }

    #endregion

    public class LotSupplyService: ILotSupplyService
    {
        #region Private properties

        public IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public LotSupplyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        public async Task<ServiceResponse<LotSupplyDto>> Add(LotSupplyDto request)
        {
            try
            {
                var supply = await _unitOfWork.SupplyRepository.GetBy(x => x.Id == request.SupplyId);
                if (request == null || supply == null)
                {
                    return new ServiceResponse<LotSupplyDto>
                    {
                        Success = false,
                        Message = $"Thiếu thông tin lô hoá chất phụ."
                    };
                }

                var newLotSupply = new LotSupply
                {
                    LotNumber = request.LotNumber,
                    ExpDate = request.ExpDate,
                    SupplyId= request.SupplyId,
                    Supply = supply
                };

                await _unitOfWork.LotSupplyRepository.Add(newLotSupply);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<LotSupplyDto>
                {
                    Success = true,
                    Data = newLotSupply.ConvertToDto(),
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
                        errorMessage = "Xảy ra lỗi do trùng số lô hoá chất phụ.";
                    }
                }

                return new ServiceResponse<LotSupplyDto>
                {
                    Success = false,
                    Message = errorMessage
                };
            }
        }

        public async Task<ServiceResponse<bool>> Delete(int lotSupplyIdRequest)
        {
            try
            {
                var lotSupplyEntity = await _unitOfWork.LotSupplyRepository.GetBy(x => x.Id == lotSupplyIdRequest);
                if (lotSupplyEntity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Tìm không thấy lô hoá chất phụ cần xoá!"
                    };
                }

                _unitOfWork.LotSupplyRepository.Remove(lotSupplyEntity);
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

        public async Task<ServiceResponse<IEnumerable<LotSupplyDto>>> GetAll()
        {
            try
            {
                var lotSupplyList = await _unitOfWork.LotSupplyRepository.GetAll(x => x.Supply);

                return new ServiceResponse<IEnumerable<LotSupplyDto>>
                {
                    Success = true,
                    Data = lotSupplyList.ConvertToDto()
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<IEnumerable<LotSupplyDto>>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy danh sách lô hoá chất phụ."
                };
            }
        }

        public async Task<ServiceResponse<LotSupplyDto?>> GetById(int lotSupplyId)
        {
            try
            {
                var lotSupplyEntity = await _unitOfWork.LotSupplyRepository.GetBy(x => x.Id == lotSupplyId, i => i.Supply);
                if (lotSupplyEntity == null)
                {
                    return new ServiceResponse<LotSupplyDto?>
                    {
                        Success = false,
                        Message = $"Không có lô hoá chất phụ nào với Id = {lotSupplyId}."
                    };
                }

                return new ServiceResponse<LotSupplyDto?>
                {
                    Success = true,
                    Data = lotSupplyEntity.ConvertToDto()
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<LotSupplyDto?>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy lô hoá chất phụ."
                };
            }
        }

        public async Task<ServiceResponse<bool>> SetDefault(int lotSupplyId)
        {
            try
            {
                var lotSupplyDefault = await _unitOfWork.LotSupplyRepository.GetBy(x => x.Id == lotSupplyId);
                if (lotSupplyDefault == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Không có lô hoá chất phụ nào với Id = {lotSupplyId}."
                    };
                }

                var lotSupplyList = await _unitOfWork.LotSupplyRepository.GetListBy(x => x.Id != lotSupplyId && x.Default == true);
                lotSupplyList.ToList().ForEach(x => x.Default = false);
                
                lotSupplyDefault.Default = true;
                _unitOfWork.LotSupplyRepository.Update(lotSupplyDefault);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<bool>
                {
                    Success = true,
                    Message = $"Đã đặt thành mặc định thành công.",
                };
            }
            catch (Exception)
            {                
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình đặt mặc định cho lô hoá chất phụ."
                };
            }
        }

        public async Task<ServiceResponse<bool>> Update(int id, LotSupplyDto request)
        {
            try
            {
                var lotSupplyEntity = await _unitOfWork.LotSupplyRepository.GetBy(x => x.Id == id);
                if (lotSupplyEntity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Không có lô hoá chất phụ nào với Id = {id}."
                    };
                }

                lotSupplyEntity.LotNumber = request.LotNumber;
                lotSupplyEntity.ExpDate=request.ExpDate;
                lotSupplyEntity.SupplyId = request.SupplyId;

                _unitOfWork.LotSupplyRepository.Update(lotSupplyEntity);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<bool>
                {
                    Success = true,
                    Message = $"Đã cập nhật thành công.",
                };
            }
            catch (Exception ex)
            {
                string errorMessage = "Xảy ra lỗi trong quá trình cập nhật lô hoá chất phụ.";

                if (ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlException)
                {
                    int errorCode = sqlException.Number;
                    if (errorCode == 2627 || errorCode == 2601)
                    {
                        errorMessage = "Xảy ra lỗi do trùng tên lô hoá chất phụ.";
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
