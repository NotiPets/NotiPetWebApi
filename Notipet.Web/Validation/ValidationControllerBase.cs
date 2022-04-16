using System.Reflection;
using Notipet.Data;
using Notipet.Web.DataWrapper;

namespace Notipet.Web.Validation
{
    public abstract class ValidationControllerBase : ControllerBase
    {
        protected async Task<ActionResult<JsendWrapper>?> Validate<T>(T data, ValidationBase validationClass)
        {
            MethodInfo[] methodInfos = validationClass.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (MethodInfo methodInfo in methodInfos)
            {
                var result = await (Task<ActionResult<JsendWrapper>>)methodInfo.Invoke(validationClass, new object[] { data });
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }
    }
}
