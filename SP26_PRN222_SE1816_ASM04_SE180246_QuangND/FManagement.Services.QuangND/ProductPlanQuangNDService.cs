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
        public ProductPlanQuangNDService() => _productionPlanQuangNDRepository ??= new ProductionPlanQuangNDRepository();
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

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var item = await _productionPlanQuangNDRepository.GetByIdAysnc(id);
                if (item != null)
                {
                    return await _productionPlanQuangNDRepository.RemoveAsync(item);
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

        public async Task<ProductionPlanQuangNd> GetByIdAysnc(int id)
        {
            try
            {
                return await _productionPlanQuangNDRepository.GetByIdAysnc(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<ProductionPlanQuangNd>> SearchAsync(int id, int quantityOrdered, string PlanStatus)
        {
            try
            {
                return await _productionPlanQuangNDRepository.SearchAsync(id, quantityOrdered, PlanStatus);
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
