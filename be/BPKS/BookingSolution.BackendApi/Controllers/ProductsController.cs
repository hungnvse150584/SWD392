using Booking.Application.Catalog.Products;
using BookingSolution.ViewModels.Catalog.Products;
using Firebase.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.IO;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Firebase.Auth.Providers;
using BookingSolution.ViewModels.Catalog.Rooms;


namespace BookingSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        
      
        public ProductsController(IProductService productService)
        {
            
            _productService = productService;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetAll")]
        //[Authorize]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await _productService.GetAll();
                return Ok(products);
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ và trả về lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("public-paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetManageProductPagingRequest request)
        {
            var products = await _productService.GetProductsPaging(request);
            return Ok(products);
        }


        [HttpGet("{productId}")]
        public async Task<IActionResult> GetById(int productId)
        {
            try
            {
                var products = await _productService.GetById(productId);
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

        //[Authorize(Roles = "1")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            try
            {
                var productId = await _productService.Create(request);
                if (productId == 0)
                    return BadRequest();

                var product = await _productService.GetById(productId);
                if (product == null)
                    return NotFound();

                // Trả về thông báo thành công và sản phẩm đã tạo
                return CreatedAtAction(nameof(GetById), new { id = productId }, product);
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ và trả về lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            try
            {
                var affectedRessult = await _productService.Update(request);
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

        [HttpPut("UpdateQuantity")]
        public async Task<IActionResult> UpdateQuantity(AddProductRequest request)
        {
            try
            {
                var affectedRessult = await _productService.OrderProductQuanity(request);
                if (affectedRessult == null)
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
                var affectedResult = await _productService.Delete(productId);
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
