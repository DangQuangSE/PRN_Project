using Microsoft.AspNetCore.SignalR;
using FManagement.Services.QuangND;
using FManagement.Entities.QuangND.Entities;
using System.Text.Json;

namespace FManagement.RazorWepApp.QuangND.Hubs
{
    public class ProductionPlanHub : Hub
    {
        private readonly IProductPlanQuangNDService _productPlanQuangNDService;

        public ProductionPlanHub(IProductPlanQuangNDService productPlanQuangNDService)
        {
            _productPlanQuangNDService = productPlanQuangNDService;
        }

        public async Task HubCreate_ProductionPlanQuangNd(string dataJson)
        {
            var productionPlan = JsonSerializer.Deserialize<ProductionPlanQuangNd>(dataJson);

            var userIdClaim = Context.User?.FindFirst("UserId");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                productionPlan.LastModifiedBy = userId;
            }

            var result = await _productPlanQuangNDService.CreateAsync(productionPlan);

            await Clients.All.SendAsync("Receiver_CreateProductionPlan", productionPlan);
        }

        public async Task HubDelete_ProductionPlanQuangNd(int planId)
        {
            var planToSoftDelete = await _productPlanQuangNDService.GetByIdAysnc(planId);
            if (planToSoftDelete != null)
            {
                planToSoftDelete.IsDeleted = true;
                var userIdClaim = Context.User?.FindFirst("UserId");
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    planToSoftDelete.LastModifiedBy = userId;
                }
                var updateResult = await _productPlanQuangNDService.UpdateAsync(planToSoftDelete);
            }

            await Clients.All.SendAsync("Receiver_DeleteProductionPlan", planId);
        }

        public async Task HubUpdate_ProductionPlanQuangNd(string dataJson)
        {
            var productionPlan = JsonSerializer.Deserialize<ProductionPlanQuangNd>(dataJson);

            var userIdClaim = Context.User?.FindFirst("UserId");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                productionPlan.LastModifiedBy = userId;
            }

            var result = await _productPlanQuangNDService.UpdateAsync(productionPlan);

            await Clients.All.SendAsync("Receiver_UpdateProductionPlan", productionPlan);
        }
    }
}
