using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Serialization;
using Chat.Models;

namespace Chat.Controllers
{
    [Authorize]
    public class ChatApiController : Controller
    {
        UsersContext db = new UsersContext();


        //Envia uma mensagem para um usuário
        [HttpPost]
        public ActionResult Send(string conv, string mensagem)
        {
            if (conv == null || mensagem == null)
            {
                return HttpNotFound();
            }

            //Proíbe usuário de enviar mensagem para ele mesmo
            if (conv == User.Identity.Name)
            {
                return HttpNotFound();
            }

            Usuario usuario = db.Usuarios.FirstOrDefault(x => x.UsuarioNome == conv);

            //Nome da conversa não é um usuário
            //Implementar lógica de grupos
            if (usuario == null)
            {
                return HttpNotFound();
            }

            Conversa conversa = db.Conversas.FirstOrDefault(x => x.Participantes.FirstOrDefault(y => y.UsuarioNome == User.Identity.Name) != null && x.Participantes.FirstOrDefault(z => z.UsuarioNome == conv) != null);

            if (conversa == null)
            {
                conversa = new Conversa();
                db.Conversas.Add(conversa);

                Usuario eu = db.Usuarios.FirstOrDefault(x => x.UsuarioNome == User.Identity.Name);
                conversa.Participantes.Add(eu);
                conversa.Participantes.Add(usuario);
            }

            Mensagem m = new Mensagem(User.Identity.Name, mensagem, DateTime.Now.Ticks);
            conversa.Mensagens.Add(m);
            conversa.BroadCast(m);
            db.SaveChanges();

            return new HttpStatusCodeResult(200);
        }


        //Recebe novas mensagens do usuário logado (em formato JSON)
        public ActionResult Receive()
        {
            Usuario eu = db.Usuarios.FirstOrDefault(x => x.UsuarioNome == User.Identity.Name);

            object json = eu.Mensagens.Select(x => new MensagemJson(x.MensagemId, x.GetAutor(User.Identity.Name), x.Conversa.GetNomeConversa(eu.UsuarioNome), x.Conteudo, new DateTime(x.Hora).ToString("hh:mm dd/MM/yyyy"))).ToList();

            //Descomentar essa linha para limpar histórico
            eu.Mensagens.Clear();

            db.SaveChanges();
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logados() {
            if(!User.Identity.IsAuthenticated){
            return HttpNotFound();
            }
            //Usuario eu = db.Usuarios.FirstOrDefault(x => x.UsuarioNome == User.Identity.Name);
            IEnumerable<Usuario> logados = db.Usuarios.Where(x=> (DateTime.Now - x.Logado).Minutes < 1);
            object json = logados.Select(x => new Logados(x.UsuarioNome));
            return Json(json,JsonRequestBehavior.AllowGet);
        }
    }

}
