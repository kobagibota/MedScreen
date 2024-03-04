namespace BaseLibrary.Entities
{
    public class TestType
    {
        public int Id { get; set; }
        public required string TypeName { get; set; }
        public string? Unit { get; set; }

        public List<TestQC>? TestQCs { get; set; }
        public List<TestProfile>? TestProfiles { get; set; }
    }
}
