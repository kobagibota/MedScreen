using MQC.BaseLibrary.Extentions;
using System.ComponentModel.DataAnnotations.Schema;

namespace MQC.BaseLibrary.Entities
{
    public class StandardDetail
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

        [NotMapped]
        public string NormalRange
        {
            get
            {
                return ResultType switch
                {
                    ResultType.Nunmber => Threshold switch
                    {
                        Threshold.Equal => $"{LimitMin} - {LimitMax}",
                        Threshold.GreaterThan => $">{LimitMin}",
                        Threshold.GreaterThanOrEqual => $">={LimitMin}",
                        Threshold.LessThan => $"<{LimitMin}" + (LimitMax.HasValue ? $" - {LimitMax}" : ""),
                        Threshold.LessThanOrEqual => $"<={LimitMin}" + (LimitMax.HasValue ? $" - {LimitMax}" : ""),
                        _ => "",
                    },
                    ResultType.Text => string.IsNullOrEmpty(Normal) ? "" : Normal.ToString(),
                    ResultType.Qualitative => Qualitative == false ? "Âm tính" : "Dương tính",
                    _ => "",
                };
            }
        }
        public ResultType ResultType { get; set; }

        public virtual required Category Category { get; set; }
        public virtual required Method Method { get; set; }
        public virtual required Standard Standard { get; set; }
        public virtual required TestQC TestQC { get; set; }
        public virtual required Strain Strain { get; set; }

        public List<QCProfileDetail>? QCProfileDetails { get; set; }
        public List<Result>? Results { get; set; }
    }
}
