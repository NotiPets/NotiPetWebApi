using Notipet.Data;

namespace Notipet.Web.Validation
{
    public abstract class ValidationBase : ControllerBase
    {
        protected readonly NotiPetBdContext _context;

        public ValidationBase(NotiPetBdContext context)
        {
            _context = context;
        }
    }
}
