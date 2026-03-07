using FManagement.Entities.QuangND.Entities;
using FManagement.Repositories.QuangND;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FManagement.Services.QuangND
{
    public class StoreOrderItemQuangNDService
    {
        private readonly StoreOrderItemQuangNDRepository _storeOrderItemQuangNDRepository;
        public StoreOrderItemQuangNDService() => _storeOrderItemQuangNDRepository ??= new StoreOrderItemQuangNDRepository();
        public async Task<List<StoreOrderItemQuangNd>> GetAllAsync()
        {
            try
            {
                return await _storeOrderItemQuangNDRepository.GetAllAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
