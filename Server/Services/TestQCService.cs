using BaseLibrary.Dtos;
using BaseLibrary.Entities;
using BaseLibrary.Extentions;
using BaseLibrary.Interfaces;
using BaseLibrary.Respones;

namespace Server.Services
{
    #region Interface

    public interface ITestQCService
    {
        Task<ServiceResponse<IEnumerable<TestQCDto>>> GetAll();
        Task<ServiceResponse<TestQCDto?>> GetById(int testQCId);
        Task<ServiceResponse<TestQCDto>> Add(TestQCDto newTestQC);
        Task<ServiceResponse<bool>> Update(int id, TestQCDto testQCDto);
        Task<ServiceResponse<bool>> Delete(int testQCId);
    }

    #endregion

    public class TestQCService : ITestQCService
    {
        #region Private properties

        public IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public TestQCService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region TestQCs

        public async Task<ServiceResponse<TestQCDto>> Add(TestQCDto request)
        {
            try
            {
                var testType = await _unitOfWork.TestTypeRepository.GetAsync(x => x.Id == request.TestTypeId);
                if (request == null || testType == null)
                {
                    return new ServiceResponse<TestQCDto>
                    {
                        Success = false,
                        Message = $"Thiếu thông tin loại thuốc thử."
                    };
                }

                var newTestQC = new TestQC
                {
                    TestQCName = request.TestQCName,
                    TestTypeId = request.TestTypeId,
                    TestType = testType
                };

                await _unitOfWork.TestQCRepository.AddAsync(newTestQC);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<TestQCDto>
                {
                    Success = true,
                    Data = newTestQC.ConvertToDto(),
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
                        errorMessage = "Xảy ra lỗi do trùng tên loại thuốc thử.";
                    }
                }

                return new ServiceResponse<TestQCDto>
                {
                    Success = false,
                    Message = errorMessage
                };
            }
        }

        public async Task<ServiceResponse<bool>> Delete(int testQCIdRequest)
        {
            try
            {
                var testQCEntity = await _unitOfWork.TestQCRepository.GetAsync(x => x.Id == testQCIdRequest);
                if (testQCEntity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Tìm không thấy loại thuốc thử cần xoá!"
                    };
                }

                _unitOfWork.TestQCRepository.Remove(testQCEntity);
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

        public async Task<ServiceResponse<IEnumerable<TestQCDto>>> GetAll()
        {
            try
            {
                var testQCList = await _unitOfWork.TestQCRepository.GetAllAsync(x => x.TestType);

                return new ServiceResponse<IEnumerable<TestQCDto>>
                {
                    Success = true,
                    Data = testQCList.ConvertToDto()
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<IEnumerable<TestQCDto>>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy danh sách loại thuốc thử."
                };
            }
        }

        public async Task<ServiceResponse<TestQCDto?>> GetById(int testQCIdRequest)
        {
            try
            {
                var testQCEntity = await _unitOfWork.TestQCRepository.GetAsync(x => x.Id == testQCIdRequest, i => i.TestType);
                if (testQCEntity == null)
                {
                    return new ServiceResponse<TestQCDto?>
                    {
                        Success = false,
                        Message = $"Không có loại thuốc thử nào với Id = {testQCIdRequest}."
                    };
                }

                return new ServiceResponse<TestQCDto?>
                {
                    Success = true,
                    Data = testQCEntity.ConvertToDto()
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<TestQCDto?>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy loại thuốc thử."
                };
            }
        }

        public async Task<ServiceResponse<bool>> Update(int id, TestQCDto testQCRequest)
        {
            try
            {
                var testQCEntity = await _unitOfWork.TestQCRepository.GetAsync(x => x.Id == id);
                if (testQCEntity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Không có loại thuốc thử nào với Id = {id}."
                    };
                }

                testQCEntity.TestQCName = testQCRequest.TestQCName;
                testQCEntity.TestTypeId = testQCRequest.TestTypeId;

                _unitOfWork.TestQCRepository.Update(testQCEntity);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<bool>
                {
                    Success = true,
                    Message = $"Đã cập nhật thành công.",
                };
            }
            catch (Exception ex)
            {
                string errorMessage = "Xảy ra lỗi trong quá trình cập nhật loại thuốc thử.";

                if (ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlException)
                {
                    int errorCode = sqlException.Number;
                    if (errorCode == 2627 || errorCode == 2601)
                    {
                        errorMessage = "Xảy ra lỗi do trùng tên loại thuốc thử.";
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
