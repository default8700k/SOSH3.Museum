using AutoFixture.Xunit2;
using Microsoft.EntityFrameworkCore;
using SOSH3.Museum.Database.Models;
using SOSH3.Museum.Database.Repositories;
using Xunit;

namespace SOSH3.Museum.Database.Tests.Repositories
{
    public class RequestRepositoryTests
    {
        private readonly RequestRepository target;

        private readonly InMemoryDbContext dbContext = new();

        public RequestRepositoryTests()
        {
            this.target = new RequestRepository(dbContext);
        }

        [Theory]
        [AutoData]
        public async Task AddRequestAsync_ShouldBeCorrect(RequestParams requestParams)
        {
            // setup
            // nothing

            // act
            await target.AddRequestAsync(requestParams);

            // assert
            await dbContext.Requests.SingleAsync(x =>
                x.Ip == requestParams.Ip &&
                x.Url == requestParams.Url &&
                x.Method == requestParams.Method &&
                x.UserAgent == requestParams.UserAgent &&
                x.DateTime == requestParams.DateTime
            );
        }
    }
}
