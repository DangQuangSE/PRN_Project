using FManagement.Entities.QuangND.Entities;
using FManagement.Repositories.QuangND;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FManagement.Services.QuangND
{
    public class ProductPlanQuangNDService : IProductPlanQuangNDService
    {
        private readonly ProductionPlanQuangNDRepository _productionPlanQuangNDRepository;
        public ProductPlanQuangNDService() => _productionPlanQuangNDRepository = new ProductionPlanQuangNDRepository();
        public async Task<int> CreateAsync(ProductionPlanQuangNd productionPlanQuangNd)
        {
            try
            {
                return await _productionPlanQuangNDRepository.CreateAsync(productionPlanQuangNd);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteAsync(int id, int? currentUserId = null)
        {
            try
            {
                var item = await _productionPlanQuangNDRepository.GetByIdAsync(id);
                if (item != null && item.PlanId > 0)
                {
                    item.IsDeleted = true;
                    if (currentUserId.HasValue)
                    {
                        item.LastModifiedBy = currentUserId;
                    }
                    var result = await _productionPlanQuangNDRepository.UpdateAsync(item);
                    return result > 0;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }

        public async Task<List<ProductionPlanQuangNd>> GetAllAsync()
        {
            try
            {
                return await _productionPlanQuangNDRepository.GetAllAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<ProductionPlanQuangNd?> GetByIdAsync(int id)
        {
            try
            {
                return await _productionPlanQuangNDRepository.GetByIdAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<ProductionPlanQuangNd>> SearchAsync(
            string? planStatus,
            DateOnly? fromDate,
            DateOnly? toDate,
            int? productId)
        {
            try
            {
                return await _productionPlanQuangNDRepository.SearchAsync(planStatus, fromDate, toDate, productId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<int> UpdateAsync(ProductionPlanQuangNd productionPlanQuangNd)
        {
            try
            {
                return await _productionPlanQuangNDRepository.UpdateAsync(productionPlanQuangNd);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
