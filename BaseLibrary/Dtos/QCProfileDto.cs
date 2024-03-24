namespace BaseLibrary.Dtos
{
    public class QCProfileDto
    {
        public int Id { get; set; }
        public int LabId { get; set; }
        public int MethodId { get; set; }
        public int CategoryId { get; set; }
        public required string QCName { get; set; }
        public bool Hide { get; set; }

        public string? LaboratoryName { get; set; }
        public string? MethodName { get; set; }
        public string? CategoryName { get; set; }
    }
}
