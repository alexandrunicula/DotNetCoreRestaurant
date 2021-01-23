using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData restaurantData;
        private readonly IHtmlHelper myHtmlHelper;

        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public EditModel(IRestaurantData restaurantData, IHtmlHelper myHtmlHelper)
        {
            this.restaurantData = restaurantData;
            this.myHtmlHelper = myHtmlHelper;
        }
        public IActionResult OnGet(int? restaurantId)
        {
            Cuisines = myHtmlHelper.GetEnumSelectList<CuisineType>();
            
            if (restaurantId.HasValue)
            {
                Restaurant = restaurantData.GetByID(restaurantId.Value);
            }
            else
            {
                Restaurant = new Restaurant();
            }
           

            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }

            return Page();

        }

        public IActionResult OnPost()
        {

            if (!ModelState.IsValid)
            {
                Cuisines = myHtmlHelper.GetEnumSelectList<CuisineType>();
                return Page();
            }
            if (Restaurant.Id > 0)
            {
               restaurantData.Update(Restaurant);
                TempData["Message"] = "Restaurant Updated !";
            }
            else
            {
                
                restaurantData.Add(Restaurant);
                TempData["Message"] = "Restaurant Saved !";
            }
            
            restaurantData.Commit();
            
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });

        }
    }
}
