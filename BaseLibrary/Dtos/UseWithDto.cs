namespace BaseLibrary.Dtos
{
    public class UseWithDto
    {
        public int Id { get; set; }
        public int QCId { get; set; }
        public int SupplyId { get; set; }
        public int LotSupplyId { get; set; }

        public string? SupplyName { get; set; }
        public string? LotNumber { get; set; }
        public DateOnly? ExpDate { get; set; }
    }
}
