using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameVault.Application.Dtos.Order;
using GameVault.Application.Interfaces.Repositories;
using GameVault.Application.Interfaces.Services;
using GameVault.Domain.Models;

namespace GameVault.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IGameRepository _gameRepository;

        public OrderService(IOrderRepository orderRepository, IGameRepository gameRepository)
        {
            _orderRepository = orderRepository;
            _gameRepository = gameRepository;
        }

        public async Task<List<OrderDto>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _orderRepository.GetAllOrdersByUserAsync(userId);
            return orders.Select(MapToDto).ToList();
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetWithItemsAsync(orderId);
            return order == null ? null : MapToDto(order);
        }

        public async Task<OrderDto> CreateOrderAsync(int userId, CreateOrderDto createOrderDto)
        {
            if (createOrderDto.GameIds == null || !createOrderDto.GameIds.Any())
            {
                throw new ArgumentException("An order must contain at least one game.");
            }

            var orderItems = new List<OrderItem>();
            decimal total = 0;

            foreach (var gameId in createOrderDto.GameIds)
            {
                var game = await _gameRepository.GetByIdAsync(gameId);
                if (game == null)
                {
                    throw new KeyNotFoundException($"Game with ID {gameId} not found");
                }

                var orderItem = new OrderItem
                {
                    GameId = game.Id,
                    Price = game.Price
                };

                orderItems.Add(orderItem);
                total += game.Price;
            }

            var order = new Order
            {
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                TotalPrice = total,
                OrderItems = orderItems
            };

            var createdOrder = await _orderRepository.AddAsync(order);

            var fullyLoadedOrder = await _orderRepository.GetWithItemsAsync(createdOrder.Id);
            return MapToDto(fullyLoadedOrder!);
        }

        private static OrderDto MapToDto(Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                TotalPrice = order.TotalPrice,
                Items = order.OrderItems.Select(MapItemToDto).ToList()
            };
        }

        private static OrderItemDto MapItemToDto(OrderItem item)
        {
            return new OrderItemDto
            {
                GameId = item.GameId,
                GameTitle = item.Game?.Title ?? string.Empty,
                CoverImageUrl = item.Game?.CoverImageUrl ?? string.Empty,
                Price = item.Price
            };
        }
    }
}
