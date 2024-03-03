namespace MQC.BaseLibrary.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public required string CategoryName { get; set; }

        public List<StrainType>? StrainTypes { get; set; }
        public List<QCProfile>? QCProfiles { get; set; }
        public List<TestProfile>? TestProfiles { get; set; }
        public List<StandardDetail>? StandardDetails { get; set; }
    }
}
