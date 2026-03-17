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
    public class CentralKitchenRepository : GenericRepository<CentralKitchen>
    {
        public CentralKitchenRepository() { }
        public CentralKitchenRepository(FranchiseManagementContext context) => _context = context;

        public async Task<List<CentralKitchen>> GetAllAsync()
        {
            return await _context.CentralKitchens
                .Where(k => k.OperatingStatus == "Active" || k.OperatingStatus == "Maintenance")
                .ToListAsync();
        }

        public async Task<CentralKitchen?> GetByIdAsync(int id)
        {
            return await _context.CentralKitchens
                .FirstOrDefaultAsync(k => k.KitchenId == id);
        }
    }
}
