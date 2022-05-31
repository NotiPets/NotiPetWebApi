using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notipet.Domain;

namespace Utilities
{
    public static class Constants
    {
        public static class SignalR
        {
            public const string DefaultMessage = "Muerte a Xamarin! atte. Amel";
        }
        public static class JsendStatus
        {
            public const string Success = "success";
            public const string Fail = "fail";
            public const string Error = "error";
        }

        public static class ControllerTextResponse
        {
            public const string Error = "¡Oops! Parece que algo pasó...";
        }

        public static class BusinessDefault
        {
            public const int Id = 1;
            public const string BusinessName = "Notipet";
            public const string Rnc = "Hola";
            public const string Phone = "8099999999";
            public const string Email = "notipetapp@gmail.com";
            public const string Address1 = "Donde hay un pais en el mundo";
            public const string Address2 = "Donde hay un pais en el mundo";
            public const string City = "Santo Domingo";
            public const string Province = "Santo Domingo";
            public const string PictureUrl = "url";
            public const double Latitude = 18.487704815310526;
            public const double Longitude = -69.93023348311469;
        }
    }
}
