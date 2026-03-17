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
            try
            {
                var productionPlan = JsonSerializer.Deserialize<ProductionPlanQuangNd>(dataJson);
                if (productionPlan != null)
                {
                    var userIdClaim = Context.User?.FindFirst("UserId");
                    if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                    {
                        productionPlan.LastModifiedBy = userId;
                    }

                    var result = await _productPlanQuangNDService.CreateAsync(productionPlan);
                    if (result > 0)
                    {
                        // Eager load for broadcast
                        var createdPlan = await _productPlanQuangNDService.GetByIdAysnc(productionPlan.PlanId);
                        
                        // Create safe DTO to avoid circular references
                        var safeItem = new
                        {
                            planId = createdPlan.PlanId,
                            storeOrderItemId = createdPlan.StoreOrderItemId,
                            kitchenId = createdPlan.KitchenId,
                            planDate = createdPlan.PlanDate,
                            startTime = createdPlan.StartTime,
                            endTime = createdPlan.EndTime,
                            planStatus = createdPlan.PlanStatus,
                            priorityLevel = createdPlan.PriorityLevel,
                            totalExpectedQuantity = createdPlan.TotalExpectedQuantity,
                            actualProducedQuantity = createdPlan.ActualProducedQuantity,
                            batchNotes = createdPlan.BatchNotes,
                            lastModifiedBy = createdPlan.LastModifiedBy,
                            isDeleted = createdPlan.IsDeleted,
                            kitchen = new { kitchenName = createdPlan.Kitchen?.KitchenName }
                        };

                        await Clients.All.SendAsync("Receiver_CreateProductionPlan", safeItem);
                    }
                }
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("Error", ex.Message);
            }
        }

        public async Task HubDelete_ProductionPlanQuangNd(int planId)
        {
            try
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
                    if (updateResult > 0)
                    {
                        await Clients.All.SendAsync("Receiver_DeleteProductionPlan", planId);
                    }
                }
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("Error", ex.Message);
            }
        }

        public async Task HubUpdate_ProductionPlanQuangNd(string dataJson)
        {
            try
            {
                var productionPlan = JsonSerializer.Deserialize<ProductionPlanQuangNd>(dataJson);
                if (productionPlan != null)
                {
                    var userIdClaim = Context.User?.FindFirst("UserId");
                    if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                    {
                        productionPlan.LastModifiedBy = userId;
                    }

                    var result = await _productPlanQuangNDService.UpdateAsync(productionPlan);
                    if (result > 0)
                    {
                        // Eager load for broadcast
                        var updatedPlan = await _productPlanQuangNDService.GetByIdAysnc(productionPlan.PlanId);

                        // Create safe DTO to avoid circular references
                        var safeItem = new
                        {
                            planId = updatedPlan.PlanId,
                            storeOrderItemId = updatedPlan.StoreOrderItemId,
                            kitchenId = updatedPlan.KitchenId,
                            planDate = updatedPlan.PlanDate,
                            startTime = updatedPlan.StartTime,
                            endTime = updatedPlan.EndTime,
                            planStatus = updatedPlan.PlanStatus,
                            priorityLevel = updatedPlan.PriorityLevel,
                            totalExpectedQuantity = updatedPlan.TotalExpectedQuantity,
                            actualProducedQuantity = updatedPlan.ActualProducedQuantity,
                            batchNotes = updatedPlan.BatchNotes,
                            lastModifiedBy = updatedPlan.LastModifiedBy,
                            isDeleted = updatedPlan.IsDeleted,
                            kitchen = new { kitchenName = updatedPlan.Kitchen?.KitchenName }
                        };

                        await Clients.All.SendAsync("Receiver_UpdateProductionPlan", safeItem);
                    }
                }
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("Error", ex.Message);
            }
        }
    }
}
