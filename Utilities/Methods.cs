using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Notipet.Domain;

namespace Utilities
{
    public static class Methods
    {
        public static Business GetNotipetBusiness()
        {
            return new Business
            {
                Id = Constants.BusinessDefault.Id,
                BusinessName = Constants.BusinessDefault.BusinessName,
                Rnc = Constants.BusinessDefault.Rnc,
                Phone = Constants.BusinessDefault.Phone,
                Email = Constants.BusinessDefault.Email,
                Address1 = Constants.BusinessDefault.Address1,
                Address2 = Constants.BusinessDefault.Address2,
                City = Constants.BusinessDefault.City,
                Province = Constants.BusinessDefault.Province,
                PictureUrl = Constants.BusinessDefault.PictureUrl,
                Latitude = Constants.BusinessDefault.Latitude,
                Longitude = Constants.BusinessDefault.Longitude
            };
        }
        public static string GetConnectionString()
        {
            if (Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING") != null)
            {
                return Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING").ToString();
            }
            else
            {
                throw new Exception("No connection string ");
            }
        }

        public static bool IsDevelopment() => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

        public static string ComputeSha256Hash(string toHash)
        {
            string hashed = String.Empty;
            foreach (var hashByte in SHA256.HashData(Encoding.UTF8.GetBytes(toHash)))
            {
                hashed += hashByte.ToString();
            }
            return hashed;
        }
    }
}
