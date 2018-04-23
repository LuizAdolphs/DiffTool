namespace DiffProject.Controllers.V1
{
    using DiffProject.Infrastructure.V1;
    using DiffProject.Models.V1;
    using Microsoft.AspNetCore.Mvc;
	using System.Threading.Tasks;

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DiffController : Controller
    {
        readonly IHashStrategy _hashStrategy;
        readonly ICache _cache;

        public DiffController(IHashStrategy hashStrategy, ICache cache)
        {
            this._hashStrategy = hashStrategy;
            this._cache = cache;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var cachedLeft = await _cache.GetAsync(id + SideEnum.Left.ToString());
            var cachedRight = await _cache.GetAsync(id + SideEnum.Right.ToString());

            return new JsonResult(new DataCompare(cachedLeft.Value, cachedRight.Value).Compare());
        }

        [HttpPost("{id}/left")]
        public async Task<IActionResult> Left(string id, [FromBody]string value)
        {
            await PersistInCache(id, SideEnum.Left, value);

            return Ok();
        }

        [HttpPost("{id}/right")]
        public async Task<IActionResult> Right(string id, [FromBody]string value)
        {
            await PersistInCache(id, SideEnum.Right, value);

            return Ok();
        }

        [HttpPost("compare")]
        public IActionResult Compare([FromBody]DataCompare compare)
        {
            return new JsonResult(compare.Compare());
        }

        async Task PersistInCache(string id, SideEnum side, string value)
        {
            await this._cache.AddAsync(id + side.ToString(), new Data(id, side, value, this._hashStrategy));
        }
    }
}
