using System.Web;
using System.Web.Mvc;

namespace CumulativeProject1_AkshayaDupati
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
