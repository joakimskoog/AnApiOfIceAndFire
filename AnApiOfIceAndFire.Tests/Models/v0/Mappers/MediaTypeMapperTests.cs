using System.Diagnostics.CodeAnalysis;
using System.Web.Http.Routing;
using AnApiOfIceAndFire.Domain.Models;
using AnApiOfIceAndFire.Models.v0.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnApiOfIceAndFire.Tests.Models.v0.Mappers
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class MediaTypeMapperTests
    {
        [TestMethod]
        public void GivenThatMediaTypeIsHardback_WhenMapping_ThenReturnedMediaTypeIsHardback()
        {
            var mapper = new MediaTypeMapper();

            var mediaType = mapper.Map(MediaType.Hardback, new UrlHelper());

            Assert.AreEqual(AnApiOfIceAndFire.Models.v0.MediaType.Hardback, mediaType);
        }

        [TestMethod]
        public void GivenThatMediaTypeIsHardcover_WhenMapping_ThenReturnedMediaTypeIsHardcover()
        {
            var mapper = new MediaTypeMapper();

            var mediaType = mapper.Map(MediaType.Hardcover, new UrlHelper());

            Assert.AreEqual(AnApiOfIceAndFire.Models.v0.MediaType.Hardcover, mediaType);
        }

        [TestMethod]
        public void GivenThatMediaTypeIsGraphicNovel_WhenMapping_ThenReturnedMediaTypeIsGraphicNovel()
        {
            var mapper = new MediaTypeMapper();

            var mediaType = mapper.Map(MediaType.GraphicNovel, new UrlHelper());

            Assert.AreEqual(AnApiOfIceAndFire.Models.v0.MediaType.GraphicNovel, mediaType);
        }

        [TestMethod]
        public void GivenThatMediaTypeIsPaperback_WhenMapping_ThenReturnedMediaTypeIsPaperback()
        {
            var mapper = new MediaTypeMapper();

            var mediaType = mapper.Map(MediaType.Paperback, new UrlHelper());

            Assert.AreEqual(AnApiOfIceAndFire.Models.v0.MediaType.Paperback, mediaType);
        }
    }
}