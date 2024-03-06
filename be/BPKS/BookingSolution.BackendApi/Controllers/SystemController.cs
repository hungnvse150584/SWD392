
using Booking.Application.System.Services;
using Microsoft.AspNetCore.Mvc;
using BookingSolution.ViewModels.System.Services;


namespace BookingSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController :  ControllerBase
    {
        private readonly ISystemService _SystemService;

        public SystemController(ISystemService SystemService)
        {
            SystemService = _SystemService;
        }

        [HttpGet("GetAllPartyDetail")]
        public async Task<IActionResult> GetAllPartyDetail()
        {
            try
            {
                var allpartydetail = await _SystemService.GetAllPartyDetail();
                return Ok(allpartydetail);
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ và trả về lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
