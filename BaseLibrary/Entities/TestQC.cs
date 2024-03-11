namespace BaseLibrary.Entities
{
    public class TestQC
    {
        public int Id { get; set; }
        public int TestTypeId { get; set; }        
        public required string TestQCName { get; set; }

        public virtual required TestType TestType { get; set; }

        public List<StandardDetail>? StandardDetails { get; set; }
        public List<LotTest>? LotTests { get; set; }
    }
}
