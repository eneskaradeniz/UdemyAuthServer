using Microsoft.EntityFrameworkCore;
using SharedLibrary.DTOs;
using System.Linq.Expressions;
using UdemyAuthServer.Core.Interfaces.Repositories;
using UdemyAuthServer.Core.Interfaces.Services;
using UdemyAuthServer.Core.Interfaces.UnitOfWorks;
using UdemyAuthServer.Service.Mapping;

namespace UdemyAuthServer.Service.Services
{
    public class GenericService<Entity, Dto> : IGenericService<Entity, Dto> where Entity : class where Dto : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Entity> _repository;

        public GenericService(IUnitOfWork unitOfWork, IGenericRepository<Entity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<Response<Dto>> AddAsync(Dto dto)
        {
            var newEntity = ObjectMapper.Mapper.Map<Entity>(dto);
            await _repository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();

            var newDto = ObjectMapper.Mapper.Map<Dto>(newEntity);
            return Response<Dto>.Success(newDto, 200);
        }

        public async Task<Response<IEnumerable<Dto>>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            var dtos = ObjectMapper.Mapper.Map<IEnumerable<Dto>>(entities);
            return Response<IEnumerable<Dto>>.Success(dtos, 200);
        }

        public async Task<Response<Dto>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return Response<Dto>.Fail("Id not found", 404, true);
            }

            var dto = ObjectMapper.Mapper.Map<Dto>(entity);
            return Response<Dto>.Success(dto, 200);
        }

        public async Task<Response<NoDataDto>> Remove(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return Response<NoDataDto>.Fail("Id not found", 404, true);
            }

            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            return Response<NoDataDto>.Success(204);
        }

        public async Task<Response<NoDataDto>> Update(Dto dto, int id)
        {
            var entity = _repository.GetByIdAsync(id).Result;
            if (entity == null)
            {
                return Response<NoDataDto>.Fail("Id not found", 404, true);
            }

            var updateEntity = ObjectMapper.Mapper.Map<Entity>(dto);
            _repository.Update(updateEntity);
            await _unitOfWork.CommitAsync();
            return Response<NoDataDto>.Success(204);
        }

        public Task<Response<NoDataDto>> Update(Entity entity, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<IEnumerable<Dto>>> Where(Expression<Func<Entity, bool>> predicate)
        {
            var list = _repository.Where(predicate);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<Dto>>(await list.ToListAsync());

            return Response<IEnumerable<Dto>>.Success(mapped, 200);
        }

    }
}
