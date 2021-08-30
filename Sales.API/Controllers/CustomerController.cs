using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales.Services.Contracts;
using Sales.Services.DTOs;
using System;
using System.Threading.Tasks;

namespace Sales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;
        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var customers = await customerService.CustomerListAsync();
                if (customers == null) return NoContent();

                return Ok(customers);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar clientes. Erro: {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var customer = await customerService.GetCustomerByIdAsync(id);
                if (customer == null) return NoContent();

                return Ok(customer);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar o cliente. Erro: {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(CustomerDto model)
        {
            try
            {
                var customer = await customerService.Add(model);
                if (customer == null) return NoContent();

                return Ok(customer);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar salvar o cliente. Erro: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CustomerDto model)
        {
            try
            {
                var customer = await customerService.Update(id, model);
                if (customer == null) return NoContent();

                return Ok(customer);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar o cliente. Erro: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var customer = await customerService.GetCustomerByIdAsync(id);
                if (customer == null) return NoContent();

                if(await customerService.Delete(customer.Id))
                {
                    return Ok("Cliente apagado com sucesso");
                }
                else
                {
                    throw new Exception("Ocorreu um problem não específico ao tentar deletar o cliente.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar excluir o cliente no banco de dados. Erro: {e.Message}");
            }
        }
    }
}
