using BaseLibrary.Dtos;
using BaseLibrary.Entities;
using BaseLibrary.Interfaces;

namespace Server.Services
{
    public interface ILaboratoryServices
    {
        Task<IEnumerable<Laboratory>> GetAll();
        Task<Laboratory?> GetById(int laboratoryId);
        Task<Laboratory> Add(LaboratoryDto newLaboratory);
        Task<bool> Update(int id, LaboratoryDto laboratory);
        Task<bool> Delete(int laboratoryId);
    }

    public class LaboratoryServices : ILaboratoryServices
    {
        public IUnitOfWork _unitOfWork;

        public LaboratoryServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Laboratory> Add(LaboratoryDto newLaboratory)
        {
            if (newLaboratory == null)
            {
                return null;
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
            return newLab;
        }

        public async Task<bool> Delete(int laboratoryId)
        {
            var lab = await _unitOfWork.LaboratoryRepository.GetAsync(x => x.Id == laboratoryId);
            if (lab != null)
            {
                _unitOfWork.LaboratoryRepository.Remove(lab);
                await _unitOfWork.CommitAsync();
                return true;
            }
            else 
            { 
                return false; 
            }
        }

        public async Task<IEnumerable<Laboratory>> GetAll()
        {
            var labList = await _unitOfWork.LaboratoryRepository.GetAllAsync();
            return labList;
        }

        public async Task<Laboratory?> GetById(int laboratoryId)
        {
            if (laboratoryId != 0)
            {
                var lab = await _unitOfWork.LaboratoryRepository.GetAsync(x => x.Id == laboratoryId);
                if (lab != null)
                {
                    return lab;
                }
            }
            return null;
        }

        public async Task<bool> Update(int id, LaboratoryDto laboratory)
        {
            if (laboratory == null)
            {
                return false;
            }            

            var lab = await _unitOfWork.LaboratoryRepository.GetAsync(x => x.Id == id);
            if (lab != null)
            {
                lab.LabName = laboratory.LabName;
                lab.OrganizationName = laboratory.OrganizationName;
                lab.Address = laboratory.Address;
                lab.Email = laboratory.Email;
                lab.PhoneNumber = laboratory.PhoneNumber;
                //lab.Logo = laboratory.Logo;
                //lab.LabStatus = laboratory.LabStatus;

                _unitOfWork.LaboratoryRepository.Update(lab);
                await _unitOfWork.CommitAsync();

                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
