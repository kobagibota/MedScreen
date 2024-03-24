namespace BaseLibrary.Dtos
{
    public class SupplyProfileDto
    {
        public int Id { get; set; }
        public int SupplyId { get; set; }
        public int QCProfileId { get; set; }
        public int? SortOrder { get; set; }
        public bool InUse { get; set; }

        public string? SupplyName { get; set; }
        public string? QCProfileName { get; set; }
    }
}
