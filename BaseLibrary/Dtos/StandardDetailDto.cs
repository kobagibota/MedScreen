using BaseLibrary.Extentions;

namespace BaseLibrary.Dtos
{
    public class StandardDetailDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int MethodId { get; set; }
        public int StandardId { get; set; }
        public int TestQCId { get; set; }
        public int StrainId { get; set; }
        public string? Concentration { get; set; }
        public Threshold Threshold { get; set; }
        public float? LimitMin { get; set; }
        public float? LimitMax { get; set; }
        public string? Normal { get; set; }
        public bool? Qualitative { get; set; }
        public required string NormalRange { get; set; }
        public ResultType ResultType { get; set; }

        public string? CategoryName { get; set; }
        public string? MethodName { get; set; }
        public string? StandardName { get; set; }
        public string? TestQCName { get; set; }
        public string? StrainName { get; set; }
    }
}
