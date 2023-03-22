using Application.Common.Models;
using Application.Features.Vendors;
using Ardalis.Specification;

namespace Application.Common.Specification;

public class EntitiesByBaseFilterSpec<T, TResult> : Specification<T, TResult>
{
    public EntitiesByBaseFilterSpec(BaseFilter filter) =>
        Query.SearchBy(filter);

    public EntitiesByBaseFilterSpec(GetvendorsRequest request)
    {
    }
}

public class EntitiesByBaseFilterSpec<T> : Specification<T>
{
    public EntitiesByBaseFilterSpec(BaseFilter filter) =>
        Query.SearchBy(filter);
}