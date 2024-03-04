using BaseLibrary.Extentions;

namespace BaseLibrary.Entities
{
    public class Result
    {
        public int Id { get; set; }
        public int QCId { get; set; }
        public int StandardDetailId { get; set; }
        public int? LotTestId { get; set; }
        public string? QCResult { get; set; }
        public Evaluate Evaluate { get; set; }
        public DateTime LastUpdated { get; set; }
        public Guid UpdatedByUserId { get; set; }

        public virtual required QC QC { get; set; }
        public virtual required StandardDetail StandardDetail { get; set; }
        public virtual required LotTest LotTest { get; set; }
        public virtual required AppUser User { get; set; }
    }
}
