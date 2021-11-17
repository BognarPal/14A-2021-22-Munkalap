using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace munkalap.WebAPI.Controllers
{
    public static class Extensions
    {
        public static ActionResult Run(this ControllerBase controller,  Func<ActionResult> function)
        {
            try
            {
                return function();
            }
            catch (KeyNotFoundException)
            {
                return controller.StatusCode(501, new
                {
                    ErrorMessage = "Nem létező azonosító"
                });
            }
            catch (Exception ex)
            {
                return controller.BadRequest(new
                {
#if DEBUG
                    ErrorMessage = ex.Message,
                    StackTrace = ex.StackTrace
#else
                    ErrorMessage = "Váratlan hiba"
#endif
                });
            }
        }
    }
}
