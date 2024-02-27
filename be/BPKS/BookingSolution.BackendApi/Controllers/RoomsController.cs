using Booking.Application.Catalog.Rooms;
using BookingSolution.ViewModels.Catalog.Rooms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IPublicRoomService _publicRoomService;
        private readonly IManageRoomService _manageRoomService;
        public RoomsController(IPublicRoomService publicRoomService, IManageRoomService manageProductService)
        {
            _publicRoomService = publicRoomService;
            _manageRoomService = manageProductService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var rooms = await _publicRoomService.GetAll();
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ và trả về lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("public-paging")]
        public async Task<IActionResult> Get([FromQuery] GetPublicRoomPagingRequest request)
        {
            var rooms = await _publicRoomService.GetAllByStyle(request);
            return Ok(rooms);
        }


        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetById(int roomId)
        {
            try
            {
                var rooms = await _manageRoomService.GetById(roomId);
                if (rooms == null)
                    return BadRequest("Cannot find room");
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ và trả về lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoomCreateRequest request)
        {
            try
            {
                var productId = await _manageRoomService.Create(request);
                if (productId == 0)
                    return BadRequest();

                var product = await _manageRoomService.GetById(productId);
                if (product == null)
                    return NotFound();

                // Trả về thông báo thành công và sản phẩm đã tạo
                return Ok("Success");
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ và trả về lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] RoomUpdateRequest request)
        {
            try
            {
                var affectedRessult = await _manageRoomService.Update(request);
                if (affectedRessult == 0)
                    return BadRequest();

                return Ok();
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ và trả về lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{roomId}")]
        public async Task<IActionResult> Delete(int roomId)
        {
            try
            {
                var affectedResult = await _manageRoomService.Delete(roomId);
                if (affectedResult == 0)
                    return BadRequest();
                return Ok();


            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ và trả về lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
