namespace MQC.BaseLibrary.Entities
{
    public class StrainType
    {
        public int CategoryId { get; set; }
        public int StrainId { get; set; }
        public bool InUse { get; set; }

        public virtual required Category Category { get; set; }
        public virtual required Strain Strain { get; set; }
    }
}
