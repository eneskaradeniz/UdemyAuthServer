using SharedLibrary.DTOs;
using System.Linq.Expressions;

namespace UdemyAuthServer.Core.Interfaces.Services
{
    public interface IGenericService<Entity, Dto> where Entity : class where Dto : class
    {
        Task<Response<Dto>> GetByIdAsync(int id);
        Task<Response<IEnumerable<Dto>>> GetAllAsync();
        Task<Response<IEnumerable<Dto>>> Where(Expression<Func<Entity, bool>> predicate);
        Task<Response<Dto>> AddAsync(Dto dto);
        Task<Response<NoDataDto>> Update(Dto entity, int id);
        Task<Response<NoDataDto>> Remove(int id);
    }
}
