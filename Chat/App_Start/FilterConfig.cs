using System.Web;
using System.Web.Mvc;
using Chat.Filters;
namespace Chat
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            
            filters.Add(new Conectados());
        }
    }
}