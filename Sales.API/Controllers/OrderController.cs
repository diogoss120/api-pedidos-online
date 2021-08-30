﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales.Domain;
using Sales.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var orders = await orderService.OrderListAsync(true, true);
                if (orders == null) return NoContent();

                return Ok(orders);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro interno ao buscar pedidos, Erro: {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var order = await orderService.GetOrderByIdAsync(id, true, true);
                if (order == null) return NoContent();

                return Ok(order);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro interno ao buscar pedido, Erro: {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Order model)
        {
            try
            {
                var order = await orderService.Add(model);
                if (order == null) return NoContent();

                return Ok(order);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro interno ao tentar cadastrar pedido, Erro: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Order model)
        {
            try
            {
                var order = await orderService.Update(id, model);
                if (order == null) return NoContent();

                return Ok(order);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro interno ao tentar atualizar pedido, Erro: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var order = await orderService.GetOrderByIdAsync(id);
                if (order == null) return NoContent();

                if (await orderService.Delete(id))
                {
                    return Ok("Pedido excluido com sucesso");
                }
                else
                {
                    throw new Exception("Erro inesperado ao tentar excluir pedido");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro interno ao tentar atualizar pedido, Erro: {e.Message}");
            }
        }
    }
}
