using System.Web;
using System.Web.Mvc;

namespace Aplicacion_Web_Tienda_Electronicos_SA
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
