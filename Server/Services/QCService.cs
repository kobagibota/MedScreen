using BaseLibrary.Dtos;
using BaseLibrary.Entities;
using BaseLibrary.Extentions;
using BaseLibrary.Interfaces;
using BaseLibrary.Respones;

namespace Server.Services
{
    #region Interface

    public interface IQCService
    {
        Task<ServiceResponse<IEnumerable<QCDto>>> GetByLabId(int qcId, DateOnly qcDate);
        Task<ServiceResponse<QCDto?>> GetById(int qcId);
        Task<ServiceResponse<QCDto>> Add(QCDto newQC);
        Task<ServiceResponse<bool>> Update(QCDto qcDto);
        Task<ServiceResponse<bool>> Delete(int qcId);
        Task<ServiceResponse<bool>> UpdateAction(int qcId, string action);
        Task<ServiceResponse<bool>> UpdateStatus(int qcId, QCStatus status);

    }

    #endregion

    public class QCService : IQCService
    {
        #region Private properties

        public IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public QCService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        public async Task<ServiceResponse<QCDto>> Add(QCDto request)
        {
            try
            {
                var qcExits = await _unitOfWork.QCRepository.GetBy(x => x.QCProfileId == request.QCProfileId && x.QCDate == request.QCDate);
                if (qcExits != null)
                {
                    return new ServiceResponse<QCDto>
                    {
                        Success = false,
                        Message = $"QC {request.QCName} đã được thực hiện trong ngày {request.QCDate}. \n Mời bạn xem lại."
                    };
                }

                var user = await _unitOfWork.UserRepository.GetBy(x => x.Id == request.UserId);
                var laboretory = await _unitOfWork.LaboratoryRepository.GetBy(x => x.Id == request.LabId);
                var qcProfile = await _unitOfWork.QCProfileRepository.GetBy(x => x.Id == request.QCProfileId);
                if (request == null || laboretory == null || qcProfile == null || user == null)
                {
                    return new ServiceResponse<QCDto>
                    {
                        Success = false,
                        Message = $"Thiếu thông tin QC."
                    };
                }

                var newQC = new QC
                {
                    LabId = request.LabId,
                    UserId = request.UserId,
                    QCProfileId = request.QCProfileId,
                    QCDate = request.QCDate,
                    ReQCId = request.ReQCId,
                    User = user,
                    Laboratory = laboretory,
                    QCProfile = qcProfile
                };

                await _unitOfWork.QCRepository.Add(newQC);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<QCDto>
                {
                    Success = true,
                    Data = newQC.ConvertToDto(),
                    Message = $"Đã thêm thành công!"
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<QCDto>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình thêm."
                };
            }
        }

        public async Task<ServiceResponse<bool>> Delete(int qcId)
        {
            try
            {
                var qcEntity = await _unitOfWork.QCRepository.GetBy(x => x.Id == qcId);
                if (qcEntity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Tìm không thấy QC cần xoá!"
                    };
                }

                _unitOfWork.QCRepository.Remove(qcEntity);
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

        public async Task<ServiceResponse<IEnumerable<QCDto>>> GetByLabId(int labId, DateOnly qcDate)
        {
            try
            {
                var qcList = await _unitOfWork.QCRepository.GetListBy(x => x.LabId == labId && x.QCDate == qcDate, u => u.User, l => l.Laboratory, q => q.QCProfile);

                return new ServiceResponse<IEnumerable<QCDto>>
                {
                    Success = true,
                    Data = qcList.ConvertToDto()
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<IEnumerable<QCDto>>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy danh sách QC."
                };
            }
        }

        public async Task<ServiceResponse<QCDto?>> GetById(int qcId)
        {
            try
            {
                var qcEntity = await _unitOfWork.QCRepository.GetBy(x => x.Id == qcId, u => u.User, l => l.Laboratory, q => q.QCProfile);
                if (qcEntity == null)
                {
                    return new ServiceResponse<QCDto?>
                    {
                        Success = false,
                        Message = $"Không có QC nào với Id = {qcId}."
                    };
                }

                return new ServiceResponse<QCDto?>
                {
                    Success = true,
                    Data = qcEntity.ConvertToDto()
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<QCDto?>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy thông tin QC."
                };
            }
        }

        public async Task<ServiceResponse<bool>> Update(QCDto request)
        {
            try
            {
                var qcEntity = await _unitOfWork.QCRepository.GetBy(x => x.Id == request.Id, u => u.User, l => l.Laboratory, q => q.QCProfile);
                if (qcEntity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Không có QC nào với Id = {request.Id}."
                    };
                }

                qcEntity.QCProfileId = request.QCProfileId;
                qcEntity.QCDate = request.QCDate;
                qcEntity.ReQCId = request.ReQCId;
                qcEntity.QCDate = request.QCDate;

                _unitOfWork.QCRepository.Update(qcEntity);
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
                    Message = $"Xảy ra lỗi trong quá trình cập nhật."
                };
            }
        }

        public async Task<ServiceResponse<bool>> UpdateAction(int qcId, string action)
        {
            try
            {
                var qcEntity = await _unitOfWork.QCRepository.GetBy(x => x.Id == qcId, u => u.User, l => l.Laboratory, q => q.QCProfile);
                if (qcEntity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Không có QC nào với Id = {qcId}."
                    };
                }

                qcEntity.Action = action;

                _unitOfWork.QCRepository.Update(qcEntity);
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
                    Message = $"Xảy ra lỗi trong quá trình cập nhật."
                };
            }
        }

        public async Task<ServiceResponse<bool>> UpdateStatus(int qcId, QCStatus status)
        {
            try
            {
                var qcEntity = await _unitOfWork.QCRepository.GetBy(x => x.Id == qcId, u => u.User, l => l.Laboratory, q => q.QCProfile);
                if (qcEntity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Không có QC nào với Id = {qcId}."
                    };
                }

                qcEntity.Status = status;

                _unitOfWork.QCRepository.Update(qcEntity);
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
                    Message = $"Xảy ra lỗi trong quá trình cập nhật."
                };
            }
        }

        #endregion
    }
}
