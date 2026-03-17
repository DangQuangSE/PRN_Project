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
    public class StoreOrderItemQuangNDRepository : GenericRepository<StoreOrderItemQuangNd>
    {
        public StoreOrderItemQuangNDRepository() { }
        public StoreOrderItemQuangNDRepository(FranchiseManagementContext context) => _context = context;

        public async Task<List<StoreOrderItemQuangNd>> GetAllAsync()
        {
            return await _context.StoreOrderItemQuangNds
                .Include(s => s.Product)
                .Include(s => s.Order)
                .ToListAsync();
        }
    }
}
