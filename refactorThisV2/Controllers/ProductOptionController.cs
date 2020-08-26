using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RefactorThisV2.Service.Exceptions;
using RefactorThisV2.Service.ProductOptions.Commands;
using RefactorThisV2.Service.ProductOptions.Models;
using RefactorThisV2.Service.ProductOptions.Queries;

namespace RefactorThisV2.Controllers
{
    [ApiController]
    [Route("/products")]
    public class ProductOptionController : BaseController
    {
        [HttpGet("{id}/options")]
        public async Task<IActionResult> GetProductOptions(Guid id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetProductOptionsQuery(id)));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("{id}/options/{optionId}")]
        public async Task<IActionResult> GetProductOption(Guid id, Guid optionId)
        {
            try
            {
                return Ok(await Mediator.Send(new GetProductOptionQuery(id, optionId)));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("{id}/options")]
        public async Task<IActionResult> CreateProductOption(Guid id, [FromBody] CreateProductOptionCommand command)
        {
            try
            {
                if (id != command.ProductId)
                {
                    return BadRequest("Product id in route not match product id in payload");
                }

                return Ok(await Mediator.Send(command));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("{id}/options/{optionId}")]
        public async Task<IActionResult> UpdateProductOption(Guid id, Guid optionId,
            [FromBody] ProductOptionDto productOptionModel)
        {
            try
            {
                if (optionId != productOptionModel.Id)
                {
                    return BadRequest("Product Option id not match id in payload");
                }

                return Ok(await Mediator.Send(new UpdateProductOptionCommand
                {
                    Id = optionId,
                    ProductId = id,
                    Description = productOptionModel.Description,
                    Name = productOptionModel.Name
                }));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id}/options/{optionId}")]
        public async Task<IActionResult> DeleteProductOption(Guid id, Guid optionId)
        {
            try
            {
                return Ok(await Mediator.Send(new DeleteProductOptionCommand(id: optionId, productId: id)));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
