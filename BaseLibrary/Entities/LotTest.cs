namespace BaseLibrary.Entities
{
    public class LotTest
    {
        public int Id { get; set; }
        public int TestQCId { get; set; }
        public required string LotNumber { get; set; }
        public required DateTime ExpDate { get; set; }
        public bool Default { get; set; }

        public virtual required TestQC TestQC { get; set; }

        public List<Result>? Results { get; set; }
    }
}
