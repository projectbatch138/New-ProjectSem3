using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PayPal.Api;
namespace AirlinesReservationSystem.Models
{
    //get configuration from web.config
    public class PaypalConfiguration
    {
        public readonly static string ClientId;
        public readonly static string ClientSecret;
        static PaypalConfiguration()
        {
            var config = GetConfig();
            ClientId = config["clientId"];
            ClientSecret = config["clientSecret"];
        }

        public static Dictionary<string, string> GetConfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }

        //Create access Token 
        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential(ClientId, ClientSecret, GetConfig()).GetAccessToken();
            return accessToken;

        }


        //Return APIContext obj
        public static APIContext GetAPIContext()
        {
            APIContext apiContext = new APIContext(GetAccessToken())
            {
                Config = GetConfig()
            };
            return apiContext;
        }
    }
}