using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Configurations.ReadAppsetting
{
    public static class Configuration
    {
        //Microsoft.Extensions.Configuration ve Microsoft.Extensions.Configuration.Json frameworkleri ile appsetting oxu
        static public string? GetTokenAudience
        {
            get
            {
                //C:\Users\user\source\repos\RestaurantFinalAPI\RestaurantFinalAPI.sln - sollution
                //C:\Users\user\source\repos\RestaurantFinalAPI\RestaurantFinalAPI\appsettings.json - appsetting
                //C:\Users\user\source\repos\RestaurantFinalAPI\RestaurantFinalAPI
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Directory.GetCurrentDirectory());
                configurationManager.AddJsonFile("appsettings.json");

                return configurationManager.GetSection("Token")["Audience"];
            }
        }

        static public string? GetTokenIssure
        {
            get
            {
                //C:\Users\user\source\repos\RestaurantFinalAPI\RestaurantFinalAPI.sln - sollution
                //C:\Users\user\source\repos\RestaurantFinalAPI\RestaurantFinalAPI\appsettings.json - appsetting
                //C:\Users\user\source\repos\RestaurantFinalAPI\RestaurantFinalAPI
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Directory.GetCurrentDirectory());
                configurationManager.AddJsonFile("appsettings.json");

                return configurationManager.GetSection("Token")["Issure"];
            }
        }

        static public string? GetTokenSecurityKey
        {
            get
            {
                //C:\Users\user\source\repos\RestaurantFinalAPI\RestaurantFinalAPI.sln - sollution
                //C:\Users\user\source\repos\RestaurantFinalAPI\RestaurantFinalAPI\appsettings.json - appsetting
                //C:\Users\user\source\repos\RestaurantFinalAPI\RestaurantFinalAPI
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Directory.GetCurrentDirectory());
                configurationManager.AddJsonFile("appsettings.json");

                return configurationManager.GetSection("Token")["SecurityKey"];
            }
        }

        static public string? GetSeqServerUrl
        {
            get
            {
                //C:\Users\user\source\repos\RestaurantFinalAPI\RestaurantFinalAPI.sln - sollution
                //C:\Users\user\source\repos\RestaurantFinalAPI\RestaurantFinalAPI\appsettings.json - appsetting
                //C:\Users\user\source\repos\RestaurantFinalAPI\RestaurantFinalAPI
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Directory.GetCurrentDirectory());
                configurationManager.AddJsonFile("appsettings.json");

                return configurationManager.GetSection("Seq")["ServerUrl"];
            }
        }


    }
}
