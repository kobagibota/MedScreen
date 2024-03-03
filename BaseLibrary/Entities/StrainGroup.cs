namespace MQC.BaseLibrary.Entities
{
    public class StrainGroup
    {
        public int Id { get; set; }
        public required string GroupName { get; set; }

        public List<Strain>? Strains { get; set; }
    }
}
