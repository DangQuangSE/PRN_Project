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

            var result = await _productPlanQuangNDService.CreateAsync(productionPlan);

            await Clients.All.SendAsync("Receiver_CreateProductionPlan", productionPlan);
        }

        public async Task HubDelete_ProductionPlanQuangNd(int planId)
        {
            var result = await _productPlanQuangNDService.DeleteAsync(planId);

            await Clients.All.SendAsync("Receiver_DeleteProductionPlan", planId);
        }

        public async Task HubUpdate_ProductionPlanQuangNd(string dataJson)
        {
            var productionPlan = JsonSerializer.Deserialize<ProductionPlanQuangNd>(dataJson);

            var result = await _productPlanQuangNDService.UpdateAsync(productionPlan);

            await Clients.All.SendAsync("Receiver_UpdateProductionPlan", productionPlan);
        }
    }
}
