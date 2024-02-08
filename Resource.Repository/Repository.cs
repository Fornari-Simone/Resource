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

        public async Task DeleteTransactionalOutbox(long ID, CancellationToken cancellation = default)
        {
            _context.TransitionalOutboxes.Remove(
                (await GetAllTransactionalOutboxByKey(ID, cancellation)) ??
                throw new ArgumentException($"TransactionalOutbox con ID {ID} non trovato", nameof(ID));
        }

        public async Task<TransitionalOutbox?> GetAllTransactionalOutboxByKey(long ID, CancellationToken cancellation = default)
        {
            return await _context.TransitionalOutboxes.FirstOrDefaultAsync(x => x.ID == ID, cancellation);
        }

        public async Task<IEnumerable<TransitionalOutbox>> GetAllTransactionalOutbox(CancellationToken cancellation = default)
        {
            return await _context.TransitionalOutboxes.ToListAsync(cancellation);
        }

        public async Task<ResourceDb?> GetResource(int ID, CancellationToken cancellation = default)
        {
            return await _context.ResourceDb.FindAsync(ID, cancellation);
        }

        public async Task RemoveResource(ResourceDb resourceDb, CancellationToken cancellation = default)
        {
            _context.ResourceDb.Remove(resourceDb);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellation = default)
        {
            return await _context.SaveChangesAsync(cancellation);
        }
    }
}
