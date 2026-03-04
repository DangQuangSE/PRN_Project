using FManagement.Entities.QuangND.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FManagement.Services.QuangND
{
    public interface IProductPlanQuangNDService
    {
        // Query methods:
        Task<List<ProductionPlanQuangNd>> GetAllAsync();
        Task<ProductionPlanQuangNd> GetByIdAysnc(int id);
        Task<List<ProductionPlanQuangNd>> SearchAsync(int id, int quantityOrdered, string PlanStatus);
        // Mutation methods:
        Task<int> CreateAsync(ProductionPlanQuangNd productionPlanQuangNd);
        Task<int> UpdateAsync(ProductionPlanQuangNd productionPlanQuangNd);
        Task<bool> DeleteAsync(int id);
    }
}
