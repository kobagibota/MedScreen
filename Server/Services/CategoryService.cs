using BaseLibrary.Dtos;
using BaseLibrary.Entities;
using BaseLibrary.Extentions;
using BaseLibrary.Interfaces;
using BaseLibrary.Respones;

namespace Server.Services
{
    #region Interface

    public interface ICategoryService
    {
        Task<ServiceResponse<IEnumerable<CategoryDto>>> GetAll();
        Task<ServiceResponse<CategoryDto?>> GetById(int CategoryId);
        Task<ServiceResponse<CategoryDto>> Add(CategoryDto newCategory);
        Task<ServiceResponse<bool>> Update(int id, CategoryDto category);
        Task<ServiceResponse<bool>> Delete(int categoryId);
    }

    #endregion

    public class CategoryService : ICategoryService
    {
        #region Private properties

        public IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        public async Task<ServiceResponse<CategoryDto>> Add(CategoryDto request)
        {
            try
            {
                if (request == null)
                {
                    return new ServiceResponse<CategoryDto>
                    {
                        Success = false,
                        Message = $"Thiếu thông tin loại test."
                    };
                }

                var newCategory = new Category
                {
                    CategoryName = request.CategoryName
                };

                await _unitOfWork.CategoryRepository.AddAsync(newCategory);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<CategoryDto>
                {
                    Success = true,
                    Data = newCategory.ConvertToDto(),
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
                        errorMessage = "Xảy ra lỗi do trùng loại test.";
                    }
                }

                return new ServiceResponse<CategoryDto>
                {
                    Success = false,
                    Message = errorMessage
                };
            }
        }

        public async Task<ServiceResponse<bool>> Delete(int categoryIdRequest)
        {
            try
            {
                var CategoryEntity = await _unitOfWork.CategoryRepository.GetAsync(x => x.Id == categoryIdRequest);
                if (CategoryEntity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Tìm không thấy loại test cần xoá!"
                    };
                }

                _unitOfWork.CategoryRepository.Remove(CategoryEntity);
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

        public async Task<ServiceResponse<IEnumerable<CategoryDto>>> GetAll()
        {
            try
            {
                var categoryList = await _unitOfWork.CategoryRepository.GetAllAsync();

                return new ServiceResponse<IEnumerable<CategoryDto>>
                {
                    Success = true,
                    Data = categoryList.ConvertToDto()
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<IEnumerable<CategoryDto>>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy danh sách loại test."
                };
            }
        }

        public async Task<ServiceResponse<CategoryDto?>> GetById(int categoryIdRequest)
        {
            try
            {
                var categoryEntity = await _unitOfWork.CategoryRepository.GetAsync(x => x.Id == categoryIdRequest);
                if (categoryEntity == null)
                {
                    return new ServiceResponse<CategoryDto?>
                    {
                        Success = false,
                        Message = $"Không có loại test nào với Id = {categoryIdRequest}."
                    };
                }

                return new ServiceResponse<CategoryDto?>
                {
                    Success = true,
                    Data = categoryEntity.ConvertToDto()
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<CategoryDto?>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy loại test."
                };
            }
        }

        public async Task<ServiceResponse<bool>> Update(int id, CategoryDto categoryRequest)
        {
            try
            {
                var categoryEntity = await _unitOfWork.CategoryRepository.GetAsync(x => x.Id == id);
                if (categoryEntity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Không có loại test nào với Id = {id}."
                    };
                }

                categoryEntity.CategoryName = categoryRequest.CategoryName;

                _unitOfWork.CategoryRepository.Update(categoryEntity);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<bool>
                {
                    Success = true,
                    Message = $"Đã cập nhật thành công.",
                };
            }
            catch (Exception ex)
            {
                string errorMessage = "Xảy ra lỗi trong quá trình cập nhật loại test.";

                if (ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlException)
                {
                    int errorCode = sqlException.Number;
                    if (errorCode == 2627 || errorCode == 2601)
                    {
                        errorMessage = "Xảy ra lỗi do trùng loại test.";
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
