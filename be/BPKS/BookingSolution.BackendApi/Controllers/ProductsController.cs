using Booking.Application.Catalog.Products;
using BookingSolution.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IPublicProductService _publicProductService;
        private readonly IManageProductService _manageProductService;
        public ProductsController(IPublicProductService publicProductService, IManageProductService manageProductService)
        {
            _publicProductService = publicProductService;
            _manageProductService = manageProductService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await _publicProductService.GetAll();
                return Ok(products);
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ và trả về lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("public-paging")]
        public async Task<IActionResult> Get([FromQuery] GetPublicProductPagingRequest request)
        {
            var products = await _publicProductService.GetAllByStyle(request);
            return Ok(products);
        }


        [HttpGet("{productId}")]
        public async Task<IActionResult> GetById(int productId)
        {
            try
            {
                var products = await _manageProductService.GetById(productId);
                if (products == null)
                    return BadRequest("Cannot find product");
                return Ok(products);
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ và trả về lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

<<<<<<< HEAD
        [HttpPost]







        //[Authorize(Roles = "1")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
=======





   
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateRequest request)

>>>>>>> 8dee79aba3dc08f0b50fef06c28bb3d587a8c95e
        {
            try
            {
                var productId = await _manageProductService.Create(request);
                if (productId == 0)
                    return BadRequest();

                var product = await _manageProductService.GetById(productId);
<<<<<<< HEAD
                return CreatedAtAction(nameof(GetById), new { id = productId }, product);
                //return Ok("ok");
=======

                return CreatedAtAction(nameof(GetById), new { id = productId }, product);
>>>>>>> 8dee79aba3dc08f0b50fef06c28bb3d587a8c95e

                if (product == null)
                    return NotFound();

                // Trả về thông báo thành công và sản phẩm đã tạo
                return Ok("Success");
<<<<<<< HEAD
=======

>>>>>>> 8dee79aba3dc08f0b50fef06c28bb3d587a8c95e
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ và trả về lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "1")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductUpdateRequest request)
        {
            try
            {
                var affectedRessult = await _manageProductService.Update(request);
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

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            try
            {
                var affectedResult = await _manageProductService.Delete(productId);
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
