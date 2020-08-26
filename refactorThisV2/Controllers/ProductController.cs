using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RefactorThisV2.Service.Exceptions;
using RefactorThisV2.Service.Products.Commands;
using RefactorThisV2.Service.Products.Queries;

namespace RefactorThisV2.Controllers
{
    [ApiController]
    [Route("/products")]
    public class ProductController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetProducts(string name)
        {
            return string.IsNullOrEmpty(name)
                ? Ok(await Mediator.Send(new GetAllProductsQuery()))
                : Ok(await Mediator.Send(new GetAllProductsByNameQuery(name)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetProductQuery(id)));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            var productDto = await Mediator.Send(command);

            return CreatedAtAction("GetProduct", new { id = productDto.Id }, productDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Product id in route not match id in payload");
            }

            try
            {
                var productDto = await Mediator.Send(command);

                return CreatedAtAction("GetProduct", new { id = productDto.Id }, productDto);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                return Ok(await Mediator.Send(new DeleteProductCommand(id)));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
