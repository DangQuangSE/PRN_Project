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
                .ThenInclude(s => s.Product)
            .Include(p => p.ProductBatches)
            .Where(p => !p.IsDeleted)
            .ToListAsync();
            return items ?? new List<ProductionPlanQuangNd>();
        }
        public async Task<ProductionPlanQuangNd?> GetByIdAysnc(int id)
        {
            return await _context.ProductionPlanQuangNds
            .Include(p => p.Kitchen)
            .Include(p => p.StoreOrderItem)
                .ThenInclude(s => s.Product)
            .Include(p => p.ProductBatches)
            .FirstOrDefaultAsync(p => p.PlanId == id && !p.IsDeleted);
        }
        public async Task<List<ProductionPlanQuangNd>> SearchAsync(
            string? planStatus,
            DateOnly? fromDate,         
            DateOnly? toDate,
            int? productId)
        {
            return await _context.ProductionPlanQuangNds
                .Include(p => p.Kitchen)
                .Include(p => p.StoreOrderItem)
                    .ThenInclude(s => s.Product)
                .Include(p => p.ProductBatches)
                .Where(p =>
                    !p.IsDeleted &&
                    (string.IsNullOrWhiteSpace(planStatus) || p.PlanStatus == planStatus) &&
                    (!fromDate.HasValue || p.PlanDate >= fromDate.Value) &&
                    (!toDate.HasValue || p.PlanDate <= toDate.Value) &&
                    (!productId.HasValue || productId <= 0 || p.StoreOrderItem.ProductId == productId)
                )
                .ToListAsync();
        }

    }
}
