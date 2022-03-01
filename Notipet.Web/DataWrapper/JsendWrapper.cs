namespace Notipet.Web.DataWrapper
{
    public abstract class JsendWrapper
    {
        public string Status { get; set; }

        public JsendWrapper(string status)
        {
            Status = status;
        }
    }
}
