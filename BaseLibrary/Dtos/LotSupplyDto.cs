namespace BaseLibrary.Dtos
{
    public class LotSupplyDto
    {
        public int Id { get; set; }
        public int SupplyId { get; set; }
        public required string LotNumber { get; set; }
        public required DateOnly ExpDate { get; set; }
        public bool Default { get; set; }
        public string? SupplyName { get; set; }
    }
}
