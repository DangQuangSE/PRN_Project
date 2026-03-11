using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FManagement.Entities.QuangND.Entities;
using FManagement.Services.QuangND;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FManagement.MVCWebApp.QuangND.Controllers
{
    [Authorize]
    public class ProductionPlanQuangNdsController : Controller
    {
        private readonly IProductPlanQuangNDService _productPlanQuangNDService;
        private readonly StoreOrderItemQuangNDService _storeOrderItemQuangNDService;
        private readonly ICentralKitchenService _centralKitchenService;

        public ProductionPlanQuangNdsController(
            IProductPlanQuangNDService productPlanQuangNDService,
            StoreOrderItemQuangNDService storeOrderItemQuangNDService,
            ICentralKitchenService centralKitchenService)
        {
            _productPlanQuangNDService = productPlanQuangNDService;
            _storeOrderItemQuangNDService = storeOrderItemQuangNDService;
            _centralKitchenService = centralKitchenService;
        }

        // GET: ProductionPlanQuangNds
        public async Task<IActionResult> Index(string? planStatus, DateOnly? fromDate, DateOnly? toDate, int? productId)
        {
            // Get products for dropdown
            var storeOrderItems = await _storeOrderItemQuangNDService.GetAllAsync();
            var products = storeOrderItems
                .Where(s => s.Product != null)
                .Select(s => s.Product)
                .DistinctBy(p => p.ProductId)
                .ToList();
            ViewData["Products"] = new SelectList(products, "ProductId", "ProductName", productId);
            
            // Preserve filter values for the view
            ViewData["CurrentStatus"] = planStatus;
            ViewData["CurrentFromDate"] = fromDate?.ToString("yyyy-MM-dd");
            ViewData["CurrentToDate"] = toDate?.ToString("yyyy-MM-dd");
            ViewData["CurrentProductId"] = productId;

            List<ProductionPlanQuangNd> items;
            
            // Check if any filter is applied
            if (!string.IsNullOrWhiteSpace(planStatus) || fromDate.HasValue || toDate.HasValue || (productId.HasValue && productId > 0))
            {
                items = await _productPlanQuangNDService.SearchAsync(planStatus, fromDate, toDate, productId);
            }
            else
            {
                items = await _productPlanQuangNDService.GetAllAsync();
            }
            
            return View(items);
        }

        // GET: ProductionPlanQuangNds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productionPlanQuangNd = await _productPlanQuangNDService.GetByIdAysnc(id.Value);
            if (productionPlanQuangNd == null)
            {
                return NotFound();
            }

            return View(productionPlanQuangNd);
        }


        // GET: ProductionPlanQuangNds/Create
        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> Create()
        {
            await PopulateDropdownsAsync();
            return View();
        }


        // POST: ProductionPlanQuangNds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> Create(ProductionPlanQuangNd productionPlanQuangNd)
        {
            if (ModelState.IsValid)
            {
                var result = await _productPlanQuangNDService.CreateAsync(productionPlanQuangNd);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            await PopulateDropdownsAsync(productionPlanQuangNd);
            return View(productionPlanQuangNd);
        }


        // GET: ProductionPlanQuangNds/Edit/5
        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productionPlanQuangNd = await _productPlanQuangNDService.GetByIdAysnc(id.Value);
            if (productionPlanQuangNd == null)
            {
                return NotFound();
            }
            await PopulateDropdownsAsync(productionPlanQuangNd);
            return View(productionPlanQuangNd);
        }

        // POST: ProductionPlanQuangNds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> Edit(ProductionPlanQuangNd productionPlanQuangNd)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    if (int.TryParse(userIdClaim, out int userId))
                    {
                        productionPlanQuangNd.LastModifiedBy = userId;
                    }

                    var result = await _productPlanQuangNDService.UpdateAsync(productionPlanQuangNd);
                    if (result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "Unable to save changes. Please try again.");
                }
            }
            await PopulateDropdownsAsync(productionPlanQuangNd);
            return View(productionPlanQuangNd);
        }

        // GET: ProductionPlanQuangNds/Delete/5
        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productionPlanQuangNd = await _productPlanQuangNDService.GetByIdAysnc(id.Value);
            if (productionPlanQuangNd == null)
            {
                return NotFound();
            }

            return View(productionPlanQuangNd);
        }

        // POST: ProductionPlanQuangNds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _productPlanQuangNDService.DeleteAsync(id);
            if (result == false)
            {
                return RedirectToAction(nameof(Delete), new { id = id });   
            }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Populate dropdown lists for Create and Edit views
        /// </summary>
        private async Task PopulateDropdownsAsync(ProductionPlanQuangNd? productionPlan = null)
        {
            // Get StoreOrderItems with Product info
            var storeOrderItems = await _storeOrderItemQuangNDService.GetAllAsync();
            var storeOrderItemList = storeOrderItems.Select(s => new
            {
                s.OrderItemId,
                DisplayText = $"#{s.OrderItemId} - {s.Product?.ProductName ?? "N/A"} (Qty: {s.QuantityOrdered})"
            }).ToList();
            ViewData["StoreOrderItemId"] = new SelectList(storeOrderItemList, "OrderItemId", "DisplayText", productionPlan?.StoreOrderItemId);

            // Get CentralKitchens
            var kitchens = await _centralKitchenService.GetAllAsync();
            ViewData["KitchenId"] = new SelectList(kitchens, "KitchenId", "KitchenName", productionPlan?.KitchenId);

            // Plan Status options
            var statusList = new List<string> { "Pending", "In Progress", "Completed", "Cancelled" };
            ViewData["PlanStatusList"] = new SelectList(statusList, productionPlan?.PlanStatus);

            // Priority Level options
            var priorityList = new List<SelectListItem>
            {
                new SelectListItem { Value = "0", Text = "Low" },
                new SelectListItem { Value = "1", Text = "Medium" },
                new SelectListItem { Value = "2", Text = "High" }
            };
            ViewData["PriorityLevelList"] = new SelectList(priorityList, "Value", "Text", productionPlan?.PriorityLevel?.ToString());
        }
    }
}
