namespace BaseLibrary.Dtos
{
    public class StrainDto
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public required string StrainName { get; set; }
        public string? GroupName { get; set; }
    }
}
