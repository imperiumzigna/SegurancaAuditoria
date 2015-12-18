using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using WebMatrix.WebData;
using Chat.Models;
using System.Linq;

namespace Chat.Filters
{
    public class Conectados : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                UsersContext db = new UsersContext();

                Usuario user = db.Usuarios.FirstOrDefault(x => x.UsuarioNome == filterContext.HttpContext.User.Identity.Name);
                if (user != null)
                {
                    user.Logado = DateTime.Now;
                    db.SaveChanges();
                }
            }

        }
    }
}