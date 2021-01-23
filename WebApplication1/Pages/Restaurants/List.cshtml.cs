using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OdeToFood.Core;
using OdeToFood.Data;

namespace WebApplication1.Pages.Restaurants
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration config;
        private readonly IRestaurantData restaurantData;
        
        public string Message { get; set; }
        public IEnumerable<Restaurant> Restaurants { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public ListModel(IConfiguration config, 
                         IRestaurantData restaurantData )
        {
            this.config = config;
            this.restaurantData = restaurantData;
        }

               
        public void OnGet(string SearchTerm)
        {
            //This way I am setting the value of the searchbox to be the string that i have searched before pressing search
            //SearchTerm = searchTerm;

            Message = config["Message"];
            Restaurants = restaurantData.GetRestaurantsByName(SearchTerm);

        }
    }
}
