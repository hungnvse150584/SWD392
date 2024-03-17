using Booking.Application.Catalog.Parties;
using Booking.Application.System.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TotalController : ControllerBase
    {
        private readonly ISystemService _SystemService;
        public TotalController(ISystemService SystemService)
        {
            _SystemService = SystemService;
        }
        [HttpGet("GetTotalCash")]
        public async Task<IActionResult> GetTotalCash()
        {
            try
            {
                double result = await _SystemService.GetTotalCash();
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ và trả về lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("GetTotalPartyBooked")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _SystemService.GetTotalPartyBooked();
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ và trả về lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("GetTotalUser")]
        public async Task<IActionResult> GetTotalUser()
        {
            try
            {
                var result = await _SystemService.GetTotalUser();
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ và trả về lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
