using System.Text.Json.Serialization;

namespace Core.Base.Pagination;

public class BasePaginationVM
{
    public int? Page { get; set; }
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