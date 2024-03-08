using BaseLibrary.Entities;
using BaseLibrary.Interfaces;
using BaseLibrary.Respones;

namespace Server.Services
{
    #region Interface

    public interface IQCActionServices
    {
        Task<ServiceResponse<IEnumerable<QCAction>>> GetAll();
        Task<ServiceResponse<QCAction?>> GetById(int qcActionId);
        Task<ServiceResponse<QCAction>> Add(QCAction newQCAction);
        Task<ServiceResponse<bool>> Update(int id, QCAction qcAction);
        Task<ServiceResponse<bool>> Delete(int qcActionId);
    }

    #endregion

    public class QCActionServices : IQCActionServices
    {
        #region Private properties

        public IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        public QCActionServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        public async Task<ServiceResponse<QCAction>> Add(QCAction qcActionRequest)
        {
            try
            {
                if (qcActionRequest == null)
                {
                    return new ServiceResponse<QCAction>
                    {
                        Success = false,
                        Message = $"Thiếu thông tin hành động khắc phục."
                    };
                }

                var newQCAction = new QCAction
                {
                    ActionName = qcActionRequest.ActionName
                };

                await _unitOfWork.QCActionRepository.AddAsync(newQCAction);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<QCAction>
                {
                    Success = true,
                    Data = newQCAction,
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
                        errorMessage = "Xảy ra lỗi do trùng hành động khắc phục.";
                    }
                }

                return new ServiceResponse<QCAction>
                {
                    Success = false,
                    Message = errorMessage
                };
            }
        }

        public async Task<ServiceResponse<bool>> Delete(int qcActionIdRequest)
        {
            try
            {
                var qCActionEntity = await _unitOfWork.QCActionRepository.GetAsync(x => x.Id == qcActionIdRequest);
                if (qCActionEntity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Tìm không thấy hành động khắc phục cần xoá!"
                    };
                }

                _unitOfWork.QCActionRepository.Remove(qCActionEntity);
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

        public async Task<ServiceResponse<IEnumerable<QCAction>>> GetAll()
        {
            try
            {
                var qcActionList = await _unitOfWork.QCActionRepository.GetAllAsync();
                return new ServiceResponse<IEnumerable<QCAction>>
                {
                    Success = true,
                    Data = qcActionList
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<IEnumerable<QCAction>>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy danh sách hành động khắc phục."
                };
            }
        }

        public async Task<ServiceResponse<QCAction?>> GetById(int qcActionIdRequest)
        {
            try
            {
                var qcActionEntity = await _unitOfWork.QCActionRepository.GetAsync(x => x.Id == qcActionIdRequest);
                if (qcActionEntity == null)
                {
                    return new ServiceResponse<QCAction?>
                    {
                        Success = false,
                        Message = $"Không có hành động khắc phục nào với Id = {qcActionIdRequest}."
                    };
                }

                return new ServiceResponse<QCAction?>
                {
                    Success = false,
                    Data = qcActionEntity
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<QCAction?>
                {
                    Success = false,
                    Message = $"Xảy ra lỗi trong quá trình lấy hành động khắc phục."
                };
            }
        }

        public async Task<ServiceResponse<bool>> Update(int id, QCAction qcActionRequest)
        {
            try
            {
                var qcActionEntity = await _unitOfWork.QCActionRepository.GetAsync(x => x.Id == id);
                if (qcActionEntity == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = $"Không có phòng xét nghiệm nào với Id = {id}."
                    };
                }

                qcActionEntity.ActionName = qcActionRequest.ActionName;

                _unitOfWork.QCActionRepository.Update(qcActionEntity);
                await _unitOfWork.CommitAsync();

                return new ServiceResponse<bool>
                {
                    Success = true,
                    Message = $"Đã cập nhật thành công.",
                };
            }
            catch (Exception ex)
            {
                string errorMessage = "Xảy ra lỗi trong quá trình cập nhật hành động khắc phục.";

                if (ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlException)
                {
                    int errorCode = sqlException.Number;
                    if (errorCode == 2627 || errorCode == 2601)
                    {
                        errorMessage = "Xảy ra lỗi do trùng hành động khắc phục.";
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
