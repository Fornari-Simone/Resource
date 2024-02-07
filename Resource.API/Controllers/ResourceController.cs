using Microsoft.AspNetCore.Mvc;
using Resource.Business.Abstraction;
using Resource.Shared;

namespace Resource.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ResourceController : ControllerBase
    {
        private readonly ILogger<ResourceController> _logger;
        private readonly IBusiness _business;
        public ResourceController(IBusiness business, ILogger<ResourceController> logger)
        {
            _business = business;
            _logger = logger;
        }
        [HttpPost(Name = "AddResource")]
        public async Task<ActionResult> AddResource(ResourceDTO ResourceDTO, CancellationToken cancellation = default)
        {
            await _business.AddResource(ResourceDTO, cancellation);
            return Ok("DONE!!!");
        }
        [HttpGet(Name = "GetResource")]
        public async Task<ActionResult<ResourceDTO?>> GetResource(int ID, CancellationToken cancellation = default)
        {
            ResourceDTO? resourceDTO = await _business.GetResource(ID);
            return new JsonResult(resourceDTO);
        }
        [HttpDelete(Name = "RemoveCharRes")]
        public async Task<ActionResult> RemoveResource(int ID, CancellationToken cancellation = default)
        {
            await _business.RemoveResource(ID, cancellation);
            return Ok("DONE!!!");
        }
    }
}
