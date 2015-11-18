using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chat.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        //Apenas retorna a página do chat
        public ActionResult Index()
        {

            return View();
        }

        
    }
}
