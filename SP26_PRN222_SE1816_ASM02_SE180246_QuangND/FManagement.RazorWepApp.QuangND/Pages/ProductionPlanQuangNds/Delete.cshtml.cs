using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FManagement.Entities.QuangND.Entities;
using FManagement.Services.QuangND;

namespace FManagement.RazorWepApp.QuangND.Pages.ProductionPlanQuangNds
{
    public class DeleteModel : PageModel
    {
        private readonly IProductPlanQuangNDService _productPlanQuangNDService;

        public DeleteModel(IProductPlanQuangNDService productionPlanSV)
        {
            _productPlanQuangNDService = productionPlanSV;
        }

        [BindProperty]
        public ProductionPlanQuangNd ProductionPlanQuangNd { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productionplanquangnd = await _productPlanQuangNDService.GetByIdAysnc(id.Value);

            if (productionplanquangnd == null)
            {
                return NotFound();
            }
            else
            {
                ProductionPlanQuangNd = productionplanquangnd;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planToSoftDelete = await _productPlanQuangNDService.GetByIdAysnc(id.Value);
            if (planToSoftDelete != null)
            {
                planToSoftDelete.IsDeleted = true;
                
                var userIdClaim = User.FindFirst("UserId");
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    planToSoftDelete.LastModifiedBy = userId;
                }

                var updateResult = await _productPlanQuangNDService.UpdateAsync(planToSoftDelete);
                if (updateResult > 0)
                {
                    return RedirectToPage("./Index");
                }
            }

            return Page();
        }
    }
}
