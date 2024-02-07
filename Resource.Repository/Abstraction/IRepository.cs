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
        Task<int> SaveChangesAsync(CancellationToken cancellation = default);
    }
}
