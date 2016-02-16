using System.Web.Http.Routing;

namespace AnApiOfIceAndFire.Models.v1.Mappers
{
    public interface IModelMapper<in TInput, out TOutput>
    {
        TOutput Map(TInput input, UrlHelper urlHelper);
    }
}