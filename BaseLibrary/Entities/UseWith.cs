namespace BaseLibrary.Entities
{
    public class UseWith
    {
        public int Id { get; set; }
        public int QCId { get; set; }
        public int SupplyId { get; set; }
        public int LotSupplyId { get; set; }

        public virtual required QC QC { get; set; }
        public virtual required Supply Supply { get; set; }
        public virtual required LotSupply LotSupply { get; set; }
    }
}
