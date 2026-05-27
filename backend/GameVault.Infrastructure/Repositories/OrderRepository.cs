using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameVault.Application.Interfaces.Repositories;
using GameVault.Domain.Models;
using GameVault.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameVault.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext appDbContext) : base(appDbContext) { }
        
        public async Task<List<Order>> GetAllOrdersByUserAsync(int userId)
        {
            return await _appDbContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Game)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        public Task<Order?> GetWithItemsAsync(int id)
        {
            return _appDbContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Game)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
