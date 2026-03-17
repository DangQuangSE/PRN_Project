using FManagement.Entities.QuangND.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FManagement.Services.QuangND
{
    public interface ICentralKitchenService
    {
        Task<List<CentralKitchen>> GetAllAsync();
        Task<CentralKitchen?> GetByIdAsync(int id);
    }
}
