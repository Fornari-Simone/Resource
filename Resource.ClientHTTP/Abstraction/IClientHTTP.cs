using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.ClientHTTP.Abstraction
{
    public interface IClientHTTP
    {
        Task<IActionResult> ModifyOwn(int ID, int delta, CancellationToken cancellation = default);
    }
}
