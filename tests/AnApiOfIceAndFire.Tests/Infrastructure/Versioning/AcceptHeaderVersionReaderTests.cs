using AnApiOfIceAndFire.Infrastructure.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Primitives;
using Xunit;

namespace AnApiOfIceAndFire.Tests.Infrastructure.Versioning
{
    public class AcceptHeaderVersionReaderTests
    {
        [Fact]
        public void RequestWithoutAcceptHeader_WhenReadingVersion_NullIsReturned()
        {
            var sut = new AcceptHeaderVersionReader();

            var version = sut.Read(new DefaultHttpRequest(new DefaultHttpContext()));

            Assert.Null(version);
        }

        [Fact]
        public void AcceptHeaderWithIncorrectMediaType_WhenReadingVersion_NullIsReturned()
        {
            var sut = new AcceptHeaderVersionReader();
            var request = new DefaultHttpRequest(new DefaultHttpContext());
            request.Headers.Add("Accept", new StringValues("application/json"));

            var version = sut.Read(request);

            Assert.Null(version);
        }

        [Fact]
        public void AcceptHeaderWithCorrectMediaTypeButIncorrectParameter_ReadingVersion_NullIsReturned()
        {
            var sut = new AcceptHeaderVersionReader();
            var request = new DefaultHttpRequest(new DefaultHttpContext());
            request.Headers.Add("Accept", new StringValues($"{AcceptHeaderVersionReader.MediaType}; wrong=abc"));

            var version = sut.Read(request);

            Assert.Null(version);
        }

        [Fact]
        public void CorrectHeaderButWithoutVersion_WhenReadingVersion_NullIsReturned()
        {
            var sut = new AcceptHeaderVersionReader();
            var request = new DefaultHttpRequest(new DefaultHttpContext());
            request.Headers.Add("Accept", new StringValues($"{AcceptHeaderVersionReader.MediaType}; {AcceptHeaderVersionReader.MediaTypeParameter}="));

            var version = sut.Read(request);

            Assert.Null(version);
        }

        [Fact]
        public void CorrectHeader_WhenReadingVersion_VersionIsReturned()
        {
            var sut = new AcceptHeaderVersionReader();
            var request = new DefaultHttpRequest(new DefaultHttpContext());
            request.Headers.Add("Accept", new StringValues($"{AcceptHeaderVersionReader.MediaType}; {AcceptHeaderVersionReader.MediaTypeParameter}=1"));

            var version = sut.Read(request);

            Assert.Equal("1", version);
        }
    }
}