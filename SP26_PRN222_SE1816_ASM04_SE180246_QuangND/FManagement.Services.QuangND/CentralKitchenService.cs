using FManagement.Entities.QuangND.Entities;
using FManagement.Repositories.QuangND;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FManagement.Services.QuangND
{
    public class CentralKitchenService : ICentralKitchenService
    {
        private readonly CentralKitchenRepository _centralKitchenRepository;

        public CentralKitchenService() => _centralKitchenRepository ??= new CentralKitchenRepository();

        public async Task<List<CentralKitchen>> GetAllAsync()
        {
            try
            {
                return await _centralKitchenRepository.GetAllAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<CentralKitchen?> GetByIdAsync(int id)
        {
            try
            {
                return await _centralKitchenRepository.GetByIdAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
