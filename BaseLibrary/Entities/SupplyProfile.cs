namespace MQC.BaseLibrary.Entities
{
    public class SupplyProfile
    {
        public int SupplyId { get; set; }
        public int QCProfileId { get; set; }
        public bool InUse { get; set; }

        public virtual required Supply Supply { get; set; }
        public virtual required QCProfile QCProfile { get; set; }
    }
}
