using Microsoft.AspNetCore.Mvc;

namespace AnApiOfIceAndFire.Models
{
    public interface IModelMapper<in TFrom, out TTo>
    {
        TTo Map(TFrom from, IUrlHelper urlHelper);
    }
}