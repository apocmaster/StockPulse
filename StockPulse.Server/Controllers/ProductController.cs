using Microsoft.AspNetCore.Mvc;
using StockPulse.Server.Dtos;
using StockPulse.Server.Mappings;
using StockPulse.Server.Services;

namespace StockPulse.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet("echo", Name = "Echo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> Echo()
        {
            return await Task.FromResult<string>("I heard you");
        }
       
        [HttpGet("{id}", Name = "Get product by Id")]
        [ProducesResponseType<ProductDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProductById(Guid id)
        {
            var product = await productService.GetById(id);
            if (!product.IsSuccess)
            {
                return NotFound();
            }
            return Ok(product.Value!.ToDto());
        }

        [HttpPost]
        [ProducesResponseType<ProductDto>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductDto>> AddProduct([FromBody] CreateProductDto createProductDto)
        {

            var result = await productService.Add(createProductDto);

            if(!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return CreatedAtAction(
                nameof(GetProductById), new { id = result.Value!.Id }, result.Value.ToDto()
            );
        }
    }
}
