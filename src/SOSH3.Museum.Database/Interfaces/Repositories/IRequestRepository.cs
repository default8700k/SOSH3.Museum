using SOSH3.Museum.Database.Models;

namespace SOSH3.Museum.Database.Interfaces.Repositories
{
    public interface IRequestRepository
    {
        public Task AddRequestAsync(RequestParams requestParams);
    }
}
