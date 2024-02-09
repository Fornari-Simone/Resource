using AutoMapper;
using Microsoft.Extensions.Logging;
using Resource.Business.Abstraction;
using Resource.Business.Factory;
using Resource.Repository.Abstraction;
using Resource.Repository.Model;
using Resource.Shared;

namespace Resource.Business
{
    public class Business : IBusiness
    {
        private readonly IRepository _repository;
        private readonly ILogger<Business> _logger;
        private readonly IMapper _mapper;
        public Business(ILogger<Business> logger, IRepository repository, IMapper mapper) 
        { 
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        public async Task AddResource(ResourceDTO resourceDTO, CancellationToken cancellation = default)
        {
            await _repository.AddResource(new ResourceDb
            {
                ID = resourceDTO.ID,
                Name = resourceDTO.Name,
                Grade = resourceDTO.Grade,
                Own = resourceDTO.Own,
                Craftable = resourceDTO.Craftable,
                Material1 = resourceDTO.Material1 == 0? null : resourceDTO.Material1,
                Material2 = resourceDTO.Material2 == 0 ? null : resourceDTO.Material2,
                Material3 = resourceDTO.Material3 == 0 ? null : resourceDTO.Material3,
                Material1Q = resourceDTO.Material1Q,
                Material2Q = resourceDTO.Material2Q,
                Material3Q = resourceDTO.Material3Q,
                LMD = resourceDTO.LMD
            }, cancellation);
            await _repository.SaveChangesAsync(cancellation);

            var resource = _mapper.Map<ResourceDTO>(resourceDTO);
            await _repository.InsertTransactionalOutbox(TransactionalOutboxFactory.CreateInsert(resource), cancellation);
            await _repository.SaveChangesAsync();
        }

        public async Task<ResourceDTO> GetResource(int ID, CancellationToken cancellation = default)
        {
            ResourceDb resourceDb = await _repository.GetResource(ID, cancellation);
            return new ResourceDTO
            {
                ID = resourceDb.ID,
                Name = resourceDb.Name,
                Grade = resourceDb.Grade,
                Own = resourceDb.Own,
                Craftable = resourceDb.Craftable,
                Material1 = resourceDb.Material1,
                Material2 = resourceDb.Material2,
                Material3 = resourceDb.Material3,
                Material1Q = resourceDb.Material1Q,
                Material2Q = resourceDb.Material2Q,
                Material3Q = resourceDb.Material3Q,
                LMD = resourceDb.LMD
            };
        }

        public async Task ModifyOwn(int ID, int delta, CancellationToken cancellation = default)
        {
            await _repository.UpdateResource(ID, delta, cancellation);
            await _repository.SaveChangesAsync();

            var resource = _mapper.Map<ResourceDTO>(await _repository.GetResource(ID, cancellation));
            await _repository.InsertTransactionalOutbox(TransactionalOutboxFactory.CreateUpdate(resource), cancellation);
            await _repository.SaveChangesAsync();
        }

        public async Task RemoveResource(int ID, CancellationToken cancellation = default)
        {
            ResourceDb resourceDb = await _repository.GetResource(ID, cancellation);
            await _repository.RemoveResource(resourceDb, cancellation);
            await _repository.SaveChangesAsync(cancellation);

            var respository = _mapper.Map<ResourceDTO>(resourceDb);
            await _repository.InsertTransactionalOutbox(TransactionalOutboxFactory.CreateDelete(respository), cancellation);
            await _repository.SaveChangesAsync();
        }
    
    }
}
