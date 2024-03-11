namespace BaseLibrary.Dtos
{
    public class SupplyDto
    {
        public int Id { get; set; }
        public int MethodId { get; set; }
        public required string SupplyName { get; set; }

        public string? MethodName { get; set; }
    }
}
