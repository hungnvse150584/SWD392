using Booking.Application.Catalog.Parties;
using Booking.Application.Catalog.Products;
using BookingSolution.ViewModels.Catalog.Parties;
using BookingSolution.ViewModels.Catalog.Rooms;
using BookingSolution.ViewModels.System.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartiesController : ControllerBase
    {
        private readonly IManagePartyService _managePartyService;
        public PartiesController(IManagePartyService managePartyService)
        {
            _managePartyService = managePartyService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var parties = await _managePartyService.GetAll();
                return Ok(parties);
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ và trả về lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("Search")]
        public async Task<IActionResult> Get([FromQuery] GetPublicPartyPagingRequest request)
        {
            var rooms = await _managePartyService.GetAllPaging(request);
            return Ok(rooms);
        }


        [HttpGet("Get{PartyId}")]
        public async Task<IActionResult> GetById(int PartyId)
        {
            try
            {
                var party = await _managePartyService.GetById(PartyId);
                if (party == null)
                    return BadRequest("Cannot find Party");
                return Ok(party);
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ và trả về lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetPartyDetail")]
        public async Task<IActionResult> GetPartyDetail(int id)         {

            try
            {
                var party = await _managePartyService.GetPartyDetail(id);
                if (party == null)
                    return BadRequest("Cannot find Party");
                return Ok(party);
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ và trả về lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("GetPartyHostHistory")]
        public async Task<IActionResult> GetPartyHostHistory([FromQuery] PartyHistoryRequest request)
        {

            try
            {
                var party = await _managePartyService.PartyHostHistory(request);
                if (party == null)
                    return BadRequest("Cannot find Party");
                return Ok(party);
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ và trả về lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetParentHistory")]
        public async Task<IActionResult> GetParentHistory([FromQuery] PartyHistoryRequest request)
        {

            try
            {
                var party = await _managePartyService.ParentHistory(request);
                if (party == null)
                    return BadRequest("Cannot find Party");
                return Ok(party);
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ và trả về lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] PartyCreateRequest request)
        {
            try
            {
                var RoomId = await _managePartyService.Create(request);
                if (RoomId == 0)
                    return BadRequest();
                
                var room = await _managePartyService.GetById(RoomId);
                if (room == null)
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


        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromForm] PartyUpdateRequest request)
        {
            try
            {
                var affectedRessult = await _managePartyService.Update(request);
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

        [HttpDelete("Delete{roomId}")]
        public async Task<IActionResult> Delete(int roomId)
        {
            try
            {
                var affectedResult = await _managePartyService.Delete(roomId);
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
