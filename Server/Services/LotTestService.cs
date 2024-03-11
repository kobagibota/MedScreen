using BaseLibrary.Dtos;
using BaseLibrary.Entities;
using BaseLibrary.Extentions;
using BaseLibrary.Interfaces;
using BaseLibrary.Respones;

namespace Server.Services
{
    #region Interface

    public interface ILotTestService
    {
        Task<ServiceResponse<IEnumerable<LotTestDto>>> GetAll();
        Task<ServiceResponse<LotTestDto?>> GetById(int lotTestId);
        Task<ServiceResponse<LotTestDto>> Add(LotTestDto newLotTest);
        Task<ServiceResponse<bool>> Update(int id, LotTestDto lotTestDto);
        Task<ServiceResponse<bool>> Delete(int lotTestId);

        Task<ServiceResponse<bool>> SetDefault(int lotTestId);
    }

    #endregion

    public class LotTestService: ILotTestService
    {
        #region Private properties

        public IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public LotTestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        public async Task<ServiceResponse<LotTestDto>> Add(LotTestDto request)
        {
            try
            {
                var testQC = await _unitOfWork.TestQCRepository.GetAsync(x => x.Id == request.TestQCId);
                if (request == null || testQC == null)
                {
                    return new ServiceResponse<LotTestDto>
                    {
                        Success = false,
                        Message = $"Thiếu thông tin lô hoá chất."
                    };
                }

                var newLotTest = new LotTest
                {
                    LotNumber = request.LotNumber,
                    ExpDate = request.ExpDate,
                    TestQCId= request.TestQCId,
                    TestQC = testQC
                };

                await _unitOfWork.LotTestRepository.AddAsync(newLotTest);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<LotTestDto>
                {
                    Success = true,
                    Data = newLotTest.ConvertToDto(),
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
                        errorMessage = "Xảy ra lỗi do trùng số lô hoá chất.";
                    }
                }

                return new ServiceResponse<LotTestDto>
                {
                    Success = false,
                    Message = errorMessage
                };
            }
        }

        public async Task<ServiceResponse<bool>> Delete(int lotTestIdRequest)
        {
            try
            {
                var lotTestEntity = await _unitOfWork.LotTestRepository.GetAsync(x => x.Id == lotTestIdRequest);
                if (lotTestEntity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Tìm không thấy lô hoá chất cần xoá!"
                    };
                }

                _unitOfWork.LotTestRepository.Remove(lotTestEntity);
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

        public async Task<ServiceResponse<IEnumerable<LotTestDto>>> GetAll()
        {
            try
            {
                var lotTestList = await _unitOfWork.LotTestRepository.GetAllAsync(x => x.TestQC);

                return new ServiceResponse<IEnumerable<LotTestDto>>
                {
                    Success = true,
                    Data = lotTestList.ConvertToDto()
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<IEnumerable<LotTestDto>>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy danh sách lô hoá chất."
                };
            }
        }

        public async Task<ServiceResponse<LotTestDto?>> GetById(int lotTestIdRequest)
        {
            try
            {
                var lotTestEntity = await _unitOfWork.LotTestRepository.GetAsync(x => x.Id == lotTestIdRequest, i => i.TestQC);
                if (lotTestEntity == null)
                {
                    return new ServiceResponse<LotTestDto?>
                    {
                        Success = false,
                        Message = $"Không có lô hoá chất nào với Id = {lotTestIdRequest}."
                    };
                }

                return new ServiceResponse<LotTestDto?>
                {
                    Success = true,
                    Data = lotTestEntity.ConvertToDto()
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<LotTestDto?>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy lô hoá chất."
                };
            }
        }

        public async Task<ServiceResponse<bool>> SetDefault(int lotTestId)
        {
            try
            {
                var lotTestDefault = await _unitOfWork.LotTestRepository.GetAsync(x => x.Id == lotTestId);
                if (lotTestDefault == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Không có lô hoá chất nào với Id = {lotTestId}."
                    };
                }

                var lotTestList = await _unitOfWork.LotTestRepository.FindAsync(x => x.Id != lotTestId && x.Default == true);
                lotTestList.ToList().ForEach(x => x.Default = false);
                
                lotTestDefault.Default = true;
                _unitOfWork.LotTestRepository.Update(lotTestDefault);
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
                    Message = $"Xảy ra lỗi trong quá trình đặt mặc định cho lô hoá chất."
                };
            }
        }

        public async Task<ServiceResponse<bool>> Update(int id, LotTestDto lotTestRequest)
        {
            try
            {
                var lotTestEntity = await _unitOfWork.LotTestRepository.GetAsync(x => x.Id == id);
                if (lotTestEntity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Không có lô hoá chất nào với Id = {id}."
                    };
                }

                lotTestEntity.LotNumber = lotTestRequest.LotNumber;
                lotTestEntity.ExpDate=lotTestRequest.ExpDate;
                lotTestEntity.TestQCId = lotTestRequest.TestQCId;

                _unitOfWork.LotTestRepository.Update(lotTestEntity);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<bool>
                {
                    Success = true,
                    Message = $"Đã cập nhật thành công.",
                };
            }
            catch (Exception ex)
            {
                string errorMessage = "Xảy ra lỗi trong quá trình cập nhật lô hoá chất.";

                if (ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlException)
                {
                    int errorCode = sqlException.Number;
                    if (errorCode == 2627 || errorCode == 2601)
                    {
                        errorMessage = "Xảy ra lỗi do trùng tên lô hoá chất.";
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
