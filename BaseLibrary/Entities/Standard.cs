namespace BaseLibrary.Entities
{
    public class Standard
    {
        public int Id { get; set; }
        public required string StandardName { get; set; }

        public List<StandardDetail>? StandardDetails { get; set; }
    }
}
