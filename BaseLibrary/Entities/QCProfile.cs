namespace MQC.BaseLibrary.Entities
{
    public class QCProfile
    {
        public int Id { get; set; }
        public int LabId { get; set; }
        public int MethodId { get; set; }
        public int CategoryId { get; set; }
        public required string QCName { get; set; }
        public bool Hide { get; set; }

        public virtual required Laboratory Laboratory { get; set; }
        public virtual required Method Method { get; set; }
        public virtual required Category Category { get; set; }

        public List<QC>? QCs { get; set; }
        public List<QCProfileDetail>? QCProfileDetails { get; set; }
        public List<SupplyProfile>? SupplyProfiles { get; set; }
    }
}
