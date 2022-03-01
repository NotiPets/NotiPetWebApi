using Utilities;

namespace Notipet.Web.DataWrapper
{
    public class JsendFail : JsendWrapper
    {
        public object Data { get; set; }

        public JsendFail(object data) : base(Constants.JsendStatus.Fail)
        {
            Data = data;
        }
    }
}
