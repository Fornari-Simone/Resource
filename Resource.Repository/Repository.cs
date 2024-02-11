using Microsoft.EntityFrameworkCore;
using Resource.Repository.Abstraction;
using Resource.Repository.Model;

namespace Resource.Repository
{
    public class Repository : IRepository
    {
        private ResourceDbContext _context;
        public Repository(ResourceDbContext context)
        {
            _context = context;
        }
        public async Task AddResource(ResourceDb resourceDb, CancellationToken cancellation = default)
        {
            await _context.AddAsync(resourceDb, cancellation);
        }
        public async Task<ResourceDb?> GetResource(int ID, CancellationToken cancellation = default)
        {
            return await _context.ResourceDb.FindAsync(ID, cancellation);
        }

        public async Task RemoveResource(ResourceDb resourceDb, CancellationToken cancellation = default)
        {
            _context.ResourceDb.Remove(resourceDb);
        }
        public async Task UpdateResource(int delta, int ID, CancellationToken cancellation = default)
        {
            ResourceDb? resourceDb = await this.GetResource(ID, cancellation);
            if (resourceDb == null)
            {
                return;
            }
            if (delta < 0 && resourceDb.Own < delta)
            {
                resourceDb.Own = 0;
            }
            resourceDb.Own = resourceDb.Own - delta;
        }
        
        public async Task DeleteTransactionalOutbox(long ID, CancellationToken cancellation = default)
        {
            _context.TransactionalOutboxes.Remove(
                (await GetAllTransactionalOutboxByKey(ID, cancellation)) ??
                throw new ArgumentException($"TransactionalOutbox con ID {ID} non trovato", nameof(ID)));
        }

        public async Task<TransactionalOutbox?> GetAllTransactionalOutboxByKey(long ID, CancellationToken cancellation = default)
        {
            return await _context.TransactionalOutboxes.FirstOrDefaultAsync(x => x.ID == ID, cancellation);
        }

        public async Task<IEnumerable<TransactionalOutbox>> GetAllTransactionalOutbox(CancellationToken cancellation = default)
        {
            return await _context.TransactionalOutboxes.ToListAsync(cancellation);
        }

        public async Task InsertTransactionalOutbox(TransactionalOutbox transactionalOutbox, CancellationToken cancellation = default)
        {
            await _context.AddAsync(transactionalOutbox, cancellation);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellation = default)
        {
            return await _context.SaveChangesAsync(cancellation);
        }

    }
}
