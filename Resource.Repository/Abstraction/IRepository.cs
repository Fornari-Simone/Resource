using Resource.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.Repository.Abstraction
{
    public interface IRepository
    {
        Task AddResource(ResourceDb resourceDb, CancellationToken cancellation = default);
        Task<ResourceDb> GetResource(int ID, CancellationToken cancellation = default);
        Task RemoveResource(ResourceDb resourceDb, CancellationToken cancellation = default);
        Task UpdateResource(int delta, int ID, CancellationToken cancellation = default);
        Task<int> SaveChangesAsync(CancellationToken cancellation = default);
        Task DeleteTransactionalOutbox(long ID, CancellationToken cancellation);
        Task<TransactionalOutbox?> GetAllTransactionalOutboxByKey(long ID, CancellationToken cancellation = default);
        Task<IEnumerable<TransactionalOutbox>> GetAllTransactionalOutbox(CancellationToken cancellation);
        Task InsertTransactionalOutbox(TransactionalOutbox transactionalOutbox, CancellationToken cancellation = default);
    }
}
