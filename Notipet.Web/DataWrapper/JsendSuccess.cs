using Utilities;

namespace Notipet.Web.DataWrapper
{
    public class JsendSuccess : JsendWrapper
    {
        public object Data { get; set; }
        public JsendSuccess(object data) : base(Constants.JsendStatus.Success)
        {
            Data = data;
        }
    }
}
