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
        Task DeleteTransactionalOutbox(long ID, CancellationToken cancellation);
        Task<TransitionalOutbox?> GetAllTransactionalOutboxByKey(long ID, CancellationToken cancellation = default);
        Task<IEnumerable<TransitionalOutbox>> GetAllTransactionalOutbox(CancellationToken cancellation);
        Task<ResourceDb> GetResource(int ID, CancellationToken cancellation = default);
        Task RemoveResource(ResourceDb resourceDb, CancellationToken cancellation = default);
        Task<int> SaveChangesAsync(CancellationToken cancellation = default);
    }
}
