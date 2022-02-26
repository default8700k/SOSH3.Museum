using SOSH3.Museum.Database.Interfaces.Repositories;
using SOSH3.Museum.Database.Models;

namespace SOSH3.Museum.Database.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly AppDbContext dbContext;

        public RequestRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddRequestAsync(RequestParams requestParams)
        {
            var model = new RequestDbModel
            {
                Ip = requestParams.Ip,
                Url = requestParams.Url,
                Method = requestParams.Method,
                UserAgent = requestParams.UserAgent,
                DateTime = requestParams.DateTime
            };

            await dbContext.Requests.AddAsync(model);
            await dbContext.SaveChangesAsync();
        }
    }
}
