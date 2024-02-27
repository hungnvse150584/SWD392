using Booking.Application.Catalog.Parties;
using Booking.Application.Catalog.Products;
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
        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    var products = await _publicProductService.GetAll();
        //    return Ok(products);

    } 
}
