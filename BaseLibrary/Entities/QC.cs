using BaseLibrary.Extentions;

namespace BaseLibrary.Entities
{
    public class QC
    {
        public int Id { get; set; }
        public int LabId { get; set; }
        public Guid UserId { get; set; }
        public int QCProfileId { get; set; }
        public required DateTime QCDate { get; set; }
        public int? ReQCId { get; set; }
        public string? Action { get; set; }
        public QCStatus Status { get; set; } = QCStatus.New;
        public DateTime DateCreated { get; set; }

        public virtual required Laboratory Laboratory { get; set; }
        public virtual required AppUser User { get; set; }
        public virtual required QCProfile QCProfile { get; set; }
        public virtual QC? ReQC { get; set; }

        public List<UseWith>? UseWiths { get; set; }
        public List<Result>? Results { get; set; }
    }
}
