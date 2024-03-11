namespace BaseLibrary.Entities
{
    public class QCProfileDetail
    {
        public int QCProfileId { get; set; }
        public int StandardDetailId { get; set; }
        public int? SortOrder { get; set; }

        public virtual required QCProfile QCProfile { get; set; }
        public virtual required StandardDetail StandardDetail { get; set; }
    }
}
