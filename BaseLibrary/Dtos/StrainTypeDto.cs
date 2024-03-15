namespace BaseLibrary.Dtos
{
    public class StrainTypeDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int StrainId { get; set; }
        public bool InUse { get; set; }

        public string? CategoryName { get; set; }
        public string? StrainName { get; set; }
    }
}
