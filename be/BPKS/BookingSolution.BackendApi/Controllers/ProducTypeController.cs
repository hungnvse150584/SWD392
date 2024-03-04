using Booking.Application.Catalog.ProductTypes;
using Booking.Application.Catalog.Rooms;
using BookingSolution.ViewModels.Catalog.Products;
using BookingSolution.ViewModels.Catalog.ProductType;
using BookingSolution.ViewModels.Catalog.Rooms;
using Microsoft.AspNetCore.Mvc;

namespace BookingSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducTypeController : Controller
    {
        private readonly IProductTypeService _productTypeService;
       
        public ProducTypeController(IProductTypeService productTypeService)
        {
            _productTypeService = productTypeService;
           
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var productTypes = await _productTypeService.GetAll();
                return Ok(productTypes);
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ và trả về lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
      


        [HttpGet("get{ProductTypeId}")]
        public async Task<IActionResult> GetById(int ProductTypeId)
        {
            try
            {
                var rooms = await _productTypeService.GetById(ProductTypeId);
                if (rooms == null)
                    return BadRequest("Cannot find ProductType");
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ và trả về lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("create")]

        public async Task<IActionResult> Create([FromBody] ProductTypeCreateRequest request)
        {
            try
            {
                var Id = await _productTypeService.Create(request);
                if (Id == 0)
                    return BadRequest();

                var product = await _productTypeService.GetById(Id);
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


        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ProductTypeUpdateRequest request)
        {
            try
            {
                var affectedRessult = await _productTypeService.Update(request);
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

        [HttpDelete("Delete{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var affectedResult = await _productTypeService.Delete(Id);
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
