namespace MQC.BaseLibrary.Entities
{
    public class TestProfile
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int TestTypeId { get; set; }
        public int MethodId { get; set; }

        public virtual required Category Category { get; set; }
        public virtual required TestType TestType { get; set; }
        public virtual required Method Method { get; set; }
    }
}
