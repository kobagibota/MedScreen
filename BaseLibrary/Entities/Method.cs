namespace MQC.BaseLibrary.Entities
{
    public class Method
    {
        public int Id { get; set; }
        public required string MethodName { get; set; }

        public List<QCProfile>? QCProfiles { get; set; }
        public List<TestProfile>? TestProfiles { get; set; }
        public List<StandardDetail>? StandardDetails { get; set; }
        public List<Supply>? Supplies { get; set; }
    }
}
