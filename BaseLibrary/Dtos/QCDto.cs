using BaseLibrary.Entities;
using BaseLibrary.Extentions;

namespace BaseLibrary.Dtos
{
    public class QCDto
    {
        public int Id { get; set; }
        public int LabId { get; set; }
        public Guid UserId { get; set; }
        public int QCProfileId { get; set; }
        public required DateOnly QCDate { get; set; }
        public int? ReQCId { get; set; }
        public string? Action { get; set; }
        public QCStatus Status { get; set; } = QCStatus.New;
        public DateTime DateCreated { get; set; }

        public required string UserFullName { get; set; }
        public required string QCName { get; set; }
        public virtual QC? ReQC { get; set; }
    }
}
