using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BurgerProject.Repositories;
using AutoMapper;
using BurgerProject.DTOs;
using BurgerProject.Model;

namespace BurgerProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersAPIController : ControllerBase
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IMapper _mapper;

        public OrdersAPIController(IOrderRepo orderRepo, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _mapper = mapper;
            
        }


        [HttpGet]

        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderRepo.GetAllOrderAsync();
            var orderDTOs = _mapper.Map<IEnumerable<OrderDTO>>(orders);

            return Ok(orderDTOs);

        }


        [HttpGet("{id}")]

        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderRepo.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            var orderDTO = _mapper.Map<OrderDTO>(order);
            return Ok(orderDTO);




        }


        [HttpPost]

        public async Task<IActionResult> CreateOrder([FromBody] OrderDTO orderDTO)
        {
            if (orderDTO == null)
            {
                return BadRequest("Order Data is required");
            }

            var order = _mapper.Map<Order>(orderDTO);
            await _orderRepo.AddOrderAsync(order);

            var createdOrderDTO = _mapper.Map<OrderDTO>(order);

            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, createdOrderDTO);

        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderDTO orderDTO)
        {
            if (orderDTO == null || id == orderDTO.Id)
            {
                return BadRequest("Order data is Incorrect");
            }

            var existingorder = await _orderRepo.GetOrderByIdAsync(id);
            if (existingorder == null)
            {
                return NotFound();
            }

            var order = _mapper.Map<Order>(orderDTO);
            await _orderRepo.UpdateOrderAsync(order);

            return NoContent();
        }


        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteOrder(int id )
        {
            var existingorder = await _orderRepo.GetOrderByIdAsync(id);
            if (existingorder == null)
            {
                return NotFound();
            }

            await _orderRepo.DeleteOrderAsync(id);
            return NoContent();
        }

    }
}
