using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Core.Base.Pagination;

public class BasePaginationVM
{
    [Required]
    [DefaultValue(1)]
    [Range(1, int.MaxValue, ErrorMessage = "شماره صفحه باید بزرگتر از {1} باشد")]
    public int Page { get; set; }
    [Required]
    [DefaultValue(10)]
    [Range(1, int.MaxValue, ErrorMessage = "تعداد آیتم های صفحه باید بزرگتر از {1} باشد")]
    public int? Limit { get; set; }
}

public abstract class GetTotalDB
{
    [JsonIgnore] public int? Total { get; set; }
}

public class ListWithTotal<TType> where TType : GetTotalDB
{
    public int Total { get { return ListData.FirstOrDefault()?.Total ?? 0; } }
    public IEnumerable<TType> ListData { get; set; }
}