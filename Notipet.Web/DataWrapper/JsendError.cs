using Utilities;

namespace Notipet.Web.DataWrapper
{
    public class JsendError : JsendWrapper
    {
        public string Message { get; set; }

        public JsendError(string message) : base(Constants.JsendStatus.Error)
        {
            Message = message;
        }
    }
}
