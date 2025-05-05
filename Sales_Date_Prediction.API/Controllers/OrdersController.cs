using Sales_Date_Prediction.Domain.Interfaces;
using Sales_Date_Prediction.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Sales_Date_Prediction.SharedKernel.Dtos;

namespace Sales_Date_Prediction.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IWriteService<Order> _service;

        public OrdersController(IWriteService<Order> service) => _service = service;

        [HttpGet("GetOrdersBy/{id}")]
        public async Task<ActionResult<PagedResponse<Order>>> GetByParamAsync(int id, int page = 1, int pageSize = 10)
        {
            var (items, total) = await _service.GetPagedAllByParamAsync(id.ToString(), page, pageSize);
            return Ok(new PagedResponse<Order>
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

        [HttpPost("Create")]
        public async Task<ActionResult<ApiResponse<Order>>> CreateAsync(Order dto)
        {
            var items = await _service.CreateAsync(dto);
            return Ok(new ApiResponse<Order>
            {
                Data = items,
                Metadata = new ResponseMetadata(HttpContext.TraceIdentifier, DateTime.UtcNow)
            });
        }
    }
}
