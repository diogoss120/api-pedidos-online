using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales.Domain;
using Sales.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace Sales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var products = await productService.ProductListAsync();
                if (products == null) return NoContent();

                return Ok(products);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar pedidos. Erro: {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var product = await productService.GetProductByIdAsync(id);
                if (product == null) return NotFound("produto não encontrado");

                return Ok(product);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar produto. Erro: {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Product model)
        {
            try
            {
                var product = await productService.Add(model);
                if (product == null) return NoContent();

                return Ok(product);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   $"Erro inesperado ao tentar cadastrar o produto. Erro: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Product model)
        {
            try
            {
                var product = await productService.Update(id, model);
                if (product == null) return BadRequest();

                return Ok(product);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro inesperado ao tentar atualizar o produto. Erro: {e.Message}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = await productService.GetProductByIdAsync(id);
                if (product == null) return NoContent();

                if (await productService.Delete(product.Id))
                {
                    return Ok("Cliente apagado com sucesso");
                }
                else
                {
                    throw new Exception("Ocorreu um problem não específico ao tentar deletar produto.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro inesperado ao tentar apagar o produto. Erro: {e.Message}");
            }
        }
    }
}
