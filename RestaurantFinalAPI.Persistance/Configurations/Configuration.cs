using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Persistance.Configurations
{
    public static class Configuration
    {
        //Microsoft.Extensions.Configuration ve Microsoft.Extensions.Configuration.Json frameworkleri ile appsetting oxu
        static public string ConnectionStringForRestaurantApiDB
        {
            get
            {                                
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../RestaurantFinalAPI/RestaurantFinalAPI"));
                Console.WriteLine();
                configurationManager.AddJsonFile("appsettings.json");

                return configurationManager.GetConnectionString("RestaurantApiDB");
            }
        }
    }
}
