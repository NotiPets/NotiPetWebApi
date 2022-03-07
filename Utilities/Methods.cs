using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class Methods
    {
        public static string GetConnectionString()
        {
            if (Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING") != null)
            {
                return Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING").ToString();
            }
            else
            {
                throw new Exception("No connection string");
            }
        }

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
