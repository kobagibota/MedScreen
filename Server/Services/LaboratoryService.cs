using BaseLibrary.Dtos;
using BaseLibrary.Entities;
using BaseLibrary.Extentions;
using BaseLibrary.Interfaces;
using BaseLibrary.Respones;

namespace Server.Services
{
    #region Interface

    public interface ILaboratoryService
    {
        Task<ServiceResponse<IEnumerable<Laboratory>>> GetAll();
        Task<ServiceResponse<Laboratory?>> GetById(int laboratoryId);
        Task<ServiceResponse<Laboratory>> Add(LaboratoryDto newLaboratory);
        Task<ServiceResponse<bool>> Update(int id, LaboratoryDto laboratory);
        Task<ServiceResponse<bool>> Delete(int laboratoryId);

        Task<ServiceResponse<bool>> ChangeStatus(int id, LabStatus status);
    }

    #endregion

    public class LaboratoryService : ILaboratoryService
    {
        #region Private properties

        public IUnitOfWork _unitOfWork;

        #endregion
        
        #region Constructor

        public LaboratoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        public async Task<ServiceResponse<Laboratory>> Add(LaboratoryDto newLaboratory)
        {
            try
            {
                if (newLaboratory == null)
                {
                    return new ServiceResponse<Laboratory>
                    {
                        Success = false,
                        Message = $"Thiếu thông tin phòng xét nghiệm."
                    };
                }

                var newLab = new Laboratory
                {
                    LabName = newLaboratory.LabName,
                    OrganizationName = newLaboratory.OrganizationName,
                    Address = newLaboratory.Address,
                    Email = newLaboratory.Email,
                    PhoneNumber = newLaboratory.PhoneNumber,
                    Logo = newLaboratory.Logo
                };

                await _unitOfWork.LaboratoryRepository.AddAsync(newLab);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<Laboratory>
                {
                    Success = true,
                    Data = newLab,
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
                        errorMessage = "Xảy ra lỗi do trùng tên tổ chức.";
                    }
                }
                
                return new ServiceResponse<Laboratory>
                {
                    Success = false,
                    Message = errorMessage
                };
            }
            
        }

        public async Task<ServiceResponse<bool>> ChangeStatus(int id, LabStatus status)
        {
            try
            {
                var lab = await _unitOfWork.LaboratoryRepository.GetAsync(x => x.Id == id);
                if (lab == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Không có phòng xét nghiệm nào với Id = {id}."
                    };
                }

                lab.LabStatus = status;

                _unitOfWork.LaboratoryRepository.Update(lab);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<bool>
                {
                    Success = true,
                    Message = $"Đã cập nhật trạng thái thành công.",
                };
            }
            catch (Exception)
            {                
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình cập nhật trạng thái."
                };
            }
        }

        public async Task<ServiceResponse<bool>> Delete(int laboratoryId)
        {
            try
            {
                var lab = await _unitOfWork.LaboratoryRepository.GetAsync(x => x.Id == laboratoryId);
                if (lab == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Tìm không thấy phòng xét nghiệm cần xoá!"
                    };
                }

                _unitOfWork.LaboratoryRepository.Remove(lab);
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

        public async Task<ServiceResponse<IEnumerable<Laboratory>>> GetAll()
        {
            try
            {
                var labList = await _unitOfWork.LaboratoryRepository.GetAllAsync();
                return new ServiceResponse<IEnumerable<Laboratory>>
                {
                    Success = true,
                    Data = labList
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<IEnumerable<Laboratory>>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy danh sách phòng xét nghiệm."
                };
            }
        }

        public async Task<ServiceResponse<Laboratory?>> GetById(int laboratoryId)
        {
            try
            {
                var lab = await _unitOfWork.LaboratoryRepository.GetAsync(x => x.Id == laboratoryId);
                if (lab == null)
                {
                    return new ServiceResponse<Laboratory?>
                    {
                        Success = false,
                        Message = $"Không có phòng xét nghiệm nào với Id = {laboratoryId}."
                    };
                }

                return new ServiceResponse<Laboratory?>
                {
                    Success = false,
                    Data = lab
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<Laboratory?>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình thêm."
                };
            }
        }

        public async Task<ServiceResponse<bool>> Update(int id, LaboratoryDto laboratory)
        {
            try
            {
                var lab = await _unitOfWork.LaboratoryRepository.GetAsync(x => x.Id == id);
                if (lab == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Không có phòng xét nghiệm nào với Id = {id}."
                    };
                }

                lab.LabName = laboratory.LabName;
                lab.OrganizationName = laboratory.OrganizationName;
                lab.Address = laboratory.Address;
                lab.Email = laboratory.Email;
                lab.PhoneNumber = laboratory.PhoneNumber;
                //lab.Logo = laboratory.Logo;
                //lab.LabStatus = laboratory.LabStatus;

                _unitOfWork.LaboratoryRepository.Update(lab);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<bool>
                {
                    Success = true,
                    Message = $"Đã cập nhật thành công.",
                };
            }
            catch (Exception ex)
            {
                string errorMessage = "Xảy ra lỗi trong quá trình cập nhật.";

                if (ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlException)
                {
                    int errorCode = sqlException.Number;
                    if (errorCode == 2627 || errorCode == 2601)
                    {
                        errorMessage = "Xảy ra lỗi do trùng tên tổ chức.";
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
