using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
        Restaurant GetRestaurantById(int id);
    }
    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> restaurants;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant {Id = 1, Name = "Domino`s", Location="Portishead", Cuisine= CuisineType.Italian },
                new Restaurant { Id = 2, Name = "Tika Tika", Location = "Yatton", Cuisine = CuisineType.Indian},
                new Restaurant { Id = 3, Name = "Hey Fluffy", Location = "Bristol", Cuisine = CuisineType.Mexican}
            };
        }

        public Restaurant GetRestaurantById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }
        public IEnumerable<Restaurant> GetRestaurantsByName(string name=null)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
            //var test = restaurants.Where(x => x.Name.StartsWith(name) || x.Name.Length >= 0)
            //                      .OrderBy(x => x.Id);
            //return test;
        }

    }
}
