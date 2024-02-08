using Resource.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.Business.Abstraction
{
    public interface IBusiness
    {
        Task AddResource(ResourceDTO resourceDTO, CancellationToken cancellation = default);
        Task<ResourceDTO> GetResource(int ID, CancellationToken cancellation = default);
        Task RemoveResource(int ID, CancellationToken cancellation = default);
        Task ModifyOwn(int ID, int delta, CancellationToken cancellation = default);

    }
}
