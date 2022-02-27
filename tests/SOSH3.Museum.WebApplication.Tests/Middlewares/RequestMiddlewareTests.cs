using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using SOSH3.Museum.Database.Interfaces.Repositories;
using SOSH3.Museum.Database.Models;
using SOSH3.Museum.WebApplication.Middlewares;
using SOSH3.Museum.WebApplication.Tests.Middlewares.Exceptions;
using System.Net;
using Xunit;

namespace SOSH3.Museum.WebApplication.Tests.Middlewares
{
    public class RequestMiddlewareTests
    {
        private readonly RequestMiddleware target;

        private readonly Mock<ILogger<RequestMiddleware>> loggerMock = new();
        private readonly Mock<IRequestRepository> requestRepositoryMock = new();

        public RequestMiddlewareTests()
        {
            this.target = new RequestMiddleware((httpContext) => { throw new NextInvokedException(); });
        }

        [Theory]
        [AutoData]
        public async Task InvokeAsync_ShouldBeCorrect(IPAddress remoteIpAddress, string url, string method, string userAgent)
        {
            // setup
            var path = "/" + url;

            var httpContext = new DefaultHttpContext();
            httpContext.Connection.RemoteIpAddress = remoteIpAddress;
            httpContext.Request.Path = path;
            httpContext.Request.Method = method;
            httpContext.Request.Headers["User-Agent"] = userAgent;

            // act
            var action = () => target.InvokeAsync(httpContext, loggerMock.Object, requestRepositoryMock.Object);

            // assert
            await action.Should().ThrowAsync<NextInvokedException>();

            requestRepositoryMock.Verify(x =>
                x.AddRequestAsync(
                    It.Is<RequestParams>(f =>
                        f.Ip == remoteIpAddress &&
                        f.Url == path &&
                        f.Method == method &&
                        f.UserAgent == userAgent
                    )
                ),
                Times.Once
            );

            requestRepositoryMock.VerifyNoOtherCalls();
        }

        [Theory]
        [AutoData]
        public async Task InvokeAsync_AddRequestAsync_Throws_Exception(IPAddress remoteIpAddress, string url, string method, string userAgent, Exception exception)
        {
            // setup
            var path = "/" + url;

            var httpContext = new DefaultHttpContext();
            httpContext.Connection.RemoteIpAddress = remoteIpAddress;
            httpContext.Request.Path = path;
            httpContext.Request.Method = method;
            httpContext.Request.Headers["User-Agent"] = userAgent;

            requestRepositoryMock
                .Setup(x => x.AddRequestAsync(It.IsAny<RequestParams>()))
                .ThrowsAsync(exception);

            // act
            var action = () => target.InvokeAsync(httpContext, loggerMock.Object, requestRepositoryMock.Object);

            // assert
            await action.Should().ThrowAsync<NextInvokedException>();

            requestRepositoryMock.Verify(x => x.AddRequestAsync(It.IsAny<RequestParams>()), Times.Once);
            requestRepositoryMock.VerifyNoOtherCalls();

            loggerMock.Verify(x => x.BeginScope(It.IsAny<object>()), Times.Once);
            loggerMock.Verify(x =>
                x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => true),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception?, string>>((v, t) => true)
                ),
                Times.Once
            );

            loggerMock.VerifyNoOtherCalls();
        }
    }
}
