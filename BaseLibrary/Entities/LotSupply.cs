namespace BaseLibrary.Entities
{
    public class LotSupply
    {
        public int Id { get; set; }
        public int SupplyId { get; set; }
        public required string LotNumber { get; set; }
        public required DateTime ExpDate { get; set; }
        public bool Default { get; set; }

        public virtual required Supply Supply { get; set; }

        public List<UseWith>? UseWiths { get; set; }
    }
}
