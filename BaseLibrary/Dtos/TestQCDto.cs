namespace BaseLibrary.Dtos
{
    public class TestQCDto
    {
        public int Id { get; set; }
        public int TestTypeId { get; set; }
        public required string TestQCName { get; set; }

        public string? TestTypeName { get; set; }
    }
}
