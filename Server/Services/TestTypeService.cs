using BaseLibrary.Dtos;
using BaseLibrary.Entities;
using BaseLibrary.Extentions;
using BaseLibrary.Interfaces;
using BaseLibrary.Respones;

namespace Server.Services
{
    #region Interface

    public interface ITestTypeService
    {
        Task<ServiceResponse<IEnumerable<TestTypeDto>>> GetAll();
        Task<ServiceResponse<TestTypeDto?>> GetById(int testTypeId);
        Task<ServiceResponse<TestTypeDto>> Add(TestTypeDto newTestType);
        Task<ServiceResponse<bool>> Update(int id, TestTypeDto testTypeDto);
        Task<ServiceResponse<bool>> Delete(int testTypeId);
    }

    #endregion

    public class TestTypeService : ITestTypeService
    {
        #region Private properties

        public IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public TestTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region TestTypes

        public async Task<ServiceResponse<TestTypeDto>> Add(TestTypeDto request)
        {
            try
            {
                if (request == null)
                {
                    return new ServiceResponse<TestTypeDto>
                    {
                        Success = false,
                        Message = $"Thiếu thông tin loại thuốc thử."
                    };
                }

                var newTestType = new TestType
                {
                    TypeName = request.TypeName,
                    Unit = request.Unit
                };

                await _unitOfWork.TestTypeRepository.AddAsync(newTestType);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<TestTypeDto>
                {
                    Success = true,
                    Data = newTestType.ConvertToDto(),
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

                return new ServiceResponse<TestTypeDto>
                {
                    Success = false,
                    Message = errorMessage
                };
            }
        }

        public async Task<ServiceResponse<bool>> Delete(int testTypeIdRequest)
        {
            try
            {
                var testTypeEntity = await _unitOfWork.TestTypeRepository.GetAsync(x => x.Id == testTypeIdRequest);
                if (testTypeEntity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Tìm không thấy loại thuốc thử cần xoá!"
                    };
                }

                _unitOfWork.TestTypeRepository.Remove(testTypeEntity);
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

        public async Task<ServiceResponse<IEnumerable<TestTypeDto>>> GetAll()
        {
            try
            {
                var testTypeList = await _unitOfWork.TestTypeRepository.GetAllAsync();

                return new ServiceResponse<IEnumerable<TestTypeDto>>
                {
                    Success = true,
                    Data = testTypeList.ConvertToDto()
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<IEnumerable<TestTypeDto>>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy danh sách loại thuốc thử."
                };
            }
        }

        public async Task<ServiceResponse<TestTypeDto?>> GetById(int testTypeIdRequest)
        {
            try
            {
                var testTypeEntity = await _unitOfWork.TestTypeRepository.GetAsync(x => x.Id == testTypeIdRequest);
                if (testTypeEntity == null)
                {
                    return new ServiceResponse<TestTypeDto?>
                    {
                        Success = false,
                        Message = $"Không có loại thuốc thử nào với Id = {testTypeIdRequest}."
                    };
                }

                return new ServiceResponse<TestTypeDto?>
                {
                    Success = true,
                    Data = testTypeEntity.ConvertToDto()
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<TestTypeDto?>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy loại thuốc thử."
                };
            }
        }

        public async Task<ServiceResponse<bool>> Update(int id, TestTypeDto categoryRequest)
        {
            try
            {
                var testTypeEntity = await _unitOfWork.TestTypeRepository.GetAsync(x => x.Id == id);
                if (testTypeEntity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Không có loại thuốc thử nào với Id = {id}."
                    };
                }

                testTypeEntity.TypeName = categoryRequest.TypeName;
                testTypeEntity.Unit = categoryRequest.Unit;

                _unitOfWork.TestTypeRepository.Update(testTypeEntity);
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
