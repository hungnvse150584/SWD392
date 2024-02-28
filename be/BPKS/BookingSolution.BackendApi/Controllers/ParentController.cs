using Booking.Application.Catalog.Parents;
using Booking.Application.Catalog.Products;
using BookingSolution.ViewModels.Catalog.Parents;
using BookingSolution.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        private readonly IPublicParentService _publicParentService;
        private readonly IManageParentService _manageParentService;
        public ParentController(IPublicParentService publicParentService, IManageParentService manageParentService)
        {
            _publicParentService = publicParentService;
            _manageParentService = manageParentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var parent = await _publicParentService.GetAll();
                return Ok(parent);
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ và trả về lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("public-paging")]
        public async Task<IActionResult> Get([FromQuery] GetPublicParentPagingRequest request)
        {
            var parent = await _publicParentService.GetAllByStatus(request);
            return Ok(parent);
        }
        [HttpGet("{parentId}")]
        public async Task<IActionResult> GetById(int parentId)
        {
            try
            {
                var parent = await _manageParentService.GetById(parentId);
                if (parent == null)
                    return BadRequest("Cannot find product");
                return Ok(parent);
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ và trả về lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ParentCreateRequest request)
        {
            try
            {
                var parentId = await _manageParentService.Create(request);
                if (parentId == 0)
                    return BadRequest();

                var parent = await _manageParentService.GetById(parentId);
                if (parent == null)
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
        public async Task<IActionResult> Update([FromBody] ParentUpdateRequest request)
        {
            try
            {
                var affectedRessult = await _manageParentService.Update(request);
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
        [HttpDelete("{parentId}")]
        public async Task<IActionResult> Delete(int parentId)
        {
            try
            {
                var affectedResult = await _manageParentService.Delete(parentId);
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

        [HttpGet("Sort")]
        public async Task<IActionResult> GetSortedParents([FromQuery] string sortOrder = "asc")
        {
            var parents = await _publicParentService.GetParentsSortedByNameAsync(sortOrder);
            return Ok(parents);
        }


    }

}
