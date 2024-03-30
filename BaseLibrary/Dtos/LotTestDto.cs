namespace BaseLibrary.Dtos
{
    public class LotTestDto
    {
        public int Id { get; set; }
        public int TestQCId { get; set; }
        public required string LotNumber { get; set; }
        public required DateOnly ExpDate { get; set; }
        public bool Default { get; set; }

        public string? TestQCName { get; set; }
    }
}
