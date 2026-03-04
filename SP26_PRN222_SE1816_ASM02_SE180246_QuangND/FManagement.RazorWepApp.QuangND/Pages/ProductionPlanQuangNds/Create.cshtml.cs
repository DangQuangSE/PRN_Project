using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FManagement.Entities.QuangND.Entities;
using FManagement.Services.QuangND;

namespace FManagement.RazorWepApp.QuangND.Pages.ProductionPlanQuangNds
{
    
    public class CreateModel : PageModel
    {

        private readonly IProductPlanQuangNDService _productPlanQuangNDService;
        private readonly StoreOrderItemQuangNDService _storeOrderItemQuangNDService;
        private readonly ICentralKitchenService _centralKitchenService;

        public CreateModel(IProductPlanQuangNDService productionPlanSV, 
            StoreOrderItemQuangNDService storeOrderSV,
            ICentralKitchenService centralKitchenService)
        {
            _productPlanQuangNDService = productionPlanSV;
            _storeOrderItemQuangNDService = storeOrderSV;
            _centralKitchenService = centralKitchenService;
        }


        public async Task<IActionResult> OnGetAsync()
        {
            await PopulateDropdownsAsync();
            return Page();
        }

        [BindProperty]
        public ProductionPlanQuangNd ProductionPlanQuangNd { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync(ProductionPlanQuangNd);
                return Page();
            }
            var resullt = await _productPlanQuangNDService.CreateAsync(ProductionPlanQuangNd);
            if (resullt > 0)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                await PopulateDropdownsAsync(ProductionPlanQuangNd);
                return Page();
            }
        }

        private async Task PopulateDropdownsAsync(ProductionPlanQuangNd? plan = null)
        {
            var storeOrderItems = await _storeOrderItemQuangNDService.GetAllAsync();
            var storeOrderItemList = storeOrderItems.Select(s => new
            {
                s.OrderItemId,
                DisplayText = $"#{s.OrderItemId} - {s.Product?.ProductName ?? "N/A"} (Qty: {s.QuantityOrdered})"
            }).ToList();
            ViewData["StoreOrderItemId"] = new SelectList(storeOrderItemList, "OrderItemId", "DisplayText", plan?.StoreOrderItemId);

            var kitchens = await _centralKitchenService.GetAllAsync();
            ViewData["KitchenId"] = new SelectList(kitchens, "KitchenId", "KitchenName", plan?.KitchenId);

            var statusList = new List<string> { "Pending", "In Progress", "Completed", "Cancelled" };
            ViewData["PlanStatusList"] = new SelectList(statusList, plan?.PlanStatus);

            var priorityList = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "Low" },
                new SelectListItem { Value = "1", Text = "Medium" },
                new SelectListItem { Value = "2", Text = "High" }
            };
            ViewData["PriorityLevelList"] = new SelectList(priorityList, "Value", "Text", plan?.PriorityLevel?.ToString());
        }
    }
}
