using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameVault.Domain.Models;

namespace GameVault.Application.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> GetAllOrdersByUserAsync(int userId);
        Task<Order?> GetWithItemsAsync(int id);
    }
}
