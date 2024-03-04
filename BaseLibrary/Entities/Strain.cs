namespace BaseLibrary.Entities
{
    public class Strain
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public required string StrainName { get; set; }

        public virtual required StrainGroup StrainGroup { get; set; }

        public List<StrainType>? StrainTypes { get; set; }
        public List<StandardDetail>? StandardDetails { get; set; }
    }
}
