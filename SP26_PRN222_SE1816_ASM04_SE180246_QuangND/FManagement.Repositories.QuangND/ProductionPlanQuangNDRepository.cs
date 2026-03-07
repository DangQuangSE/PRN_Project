using FManagement.Entities.QuangND.Entities;
using FManagement.Repositories.QuangND.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FManagement.Repositories.QuangND
{
    public class ProductionPlanQuangNDRepository : GenericRepository<ProductionPlanQuangNd>
    {
        public ProductionPlanQuangNDRepository() { }
        public ProductionPlanQuangNDRepository(FranchiseManagementContext context) => _context = context;
        public async Task<List<ProductionPlanQuangNd>> GetAllAsync()
        {
            var items = await _context.ProductionPlanQuangNds
            .Include(p => p.Kitchen)
            .Include(p => p.StoreOrderItem)
            .Include(p => p.ProductBatches).ToListAsync();
            return items ?? new List<ProductionPlanQuangNd>();
        }
        public async Task<ProductionPlanQuangNd> GetByIdAysnc(int id)
        {
            var item = await _context.ProductionPlanQuangNds
            .Include(p => p.Kitchen)
            .Include(p => p.StoreOrderItem)
            .Include(p => p.ProductBatches)
            .FirstOrDefaultAsync(p => p.PlanId == id);
            return item ?? new ProductionPlanQuangNd();
        }
        public async Task<List<ProductionPlanQuangNd>> SearchAsync(int id, int quantityOrdered, string? planStatus)
        {
            return await _context.ProductionPlanQuangNds
                .Include(p => p.Kitchen)
                .Include(p => p.StoreOrderItem)
                .Include(p => p.ProductBatches)
                .Where(p =>
                    (id <= 0 || p.PlanId == id) &&
                    (quantityOrdered <= 0 || (p.StoreOrderItem != null && p.StoreOrderItem.QuantityOrdered == quantityOrdered)) &&
                    (string.IsNullOrWhiteSpace(planStatus) || (p.PlanStatus != null && p.PlanStatus.Contains(planStatus)))
                )
                .ToListAsync();
        }

    }
}
