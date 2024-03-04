namespace BaseLibrary.Entities
{
    public class Supply
    {
        public int Id { get; set; }
        public int MethodId { get; set; }
        public int? SortOrder { get; set; }
        public required string SupplyName { get; set; }

        public virtual required Method Method { get; set; }

        public List<SupplyProfile>? SupplyProfiles { get; set; }
        public List<UseWith>? UseWiths { get; set; }
        public List<LotSupply>? LotSupplies { get; set; }
    }
}
