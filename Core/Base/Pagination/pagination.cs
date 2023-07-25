using System.Text.Json.Serialization;

namespace Core.Base.Pagination;

public class BasePaginationVM
{
    public int? Page { get; set; }
    public int? PerPage { get; set; }
}

public abstract class GetTotalDB
{
    [JsonIgnore] public int? Total { get; set; }
}

public class ListWithTotal<Type> where Type : GetTotalDB
{
    public int Total => ListData?.FirstOrDefault()?.Total ?? 0;
    public IEnumerable<Type>? ListData { get; set; }
}