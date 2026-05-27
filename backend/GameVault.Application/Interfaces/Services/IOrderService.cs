using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameVault.Application.Dtos.Order;

namespace GameVault.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetOrdersByUserIdAsync(int userId);
        Task<OrderDto?> GetOrderByIdAsync(int orderId);
        Task<OrderDto> CreateOrderAsync(int userId, CreateOrderDto createOrderDto);
    }
}
