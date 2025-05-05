using Microsoft.AspNetCore.Mvc;
using Sales_Date_Prediction.Domain.Interfaces;
using Sales_Date_Prediction.SharedKernel.Dtos;

namespace Sales_Date_Prediction.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ReadController<TEntity> : ControllerBase where TEntity : DtoBase
    {
        private readonly IReadService<TEntity> _service;

        protected ReadController(IReadService<TEntity> service) => _service = service;

        [HttpGet("GetAll")]
        public async Task<ActionResult<PagedResponse<TEntity>>> GetAll(int page = 1, int pageSize = 10, string filter = "")
        {
            var (items, total) = await _service.GetPagedAllAsync(page, pageSize, filter);
            return Ok(new PagedResponse<TEntity>
            {
                Data = items,
                Metadata = new ResponseMetadata
                {
                    TotalItems = total,
                    Page = page,
                    PageSize = pageSize,
                    TraceId = HttpContext.TraceIdentifier,
                    Timestamp = DateTime.UtcNow
                }
            });
        }
    }
}
