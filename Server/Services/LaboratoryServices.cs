using BaseLibrary.Dtos;
using BaseLibrary.Entities;
using BaseLibrary.Interfaces;
using BaseLibrary.Respones;

namespace Server.Services
{
    public interface ILaboratoryServices
    {
        Task<ServiceResponse<IEnumerable<Laboratory>>> GetAll();
        Task<ServiceResponse<Laboratory?>> GetById(int laboratoryId);
        Task<ServiceResponse<Laboratory>> Add(LaboratoryDto newLaboratory);
        Task<ServiceResponse<bool>> Update(int id, LaboratoryDto laboratory);
        Task<ServiceResponse<bool>> Delete(int laboratoryId);
    }

    public class LaboratoryServices : ILaboratoryServices
    {
        public IUnitOfWork _unitOfWork;

        public LaboratoryServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

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
            catch (Exception)
            {

                return new ServiceResponse<Laboratory>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình thêm."
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
                    Message = $"Xảy ra lỗi trong quá trình thêm."
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
                    Message = $"Xảy ra lỗi trong quá trình thêm."
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
            catch (Exception)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình thêm."
                };
            }
        }
    }
}
