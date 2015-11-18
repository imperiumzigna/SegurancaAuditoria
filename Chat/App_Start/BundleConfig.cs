using System.Web;
using System.Web.Optimization;

namespace Chat
{
    public class BundleConfig
    {
        // Para mais informações sobre Bundling, visite http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/chat").Include(
                        "~/Scripts/chat.js"));

            

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use a versão em desenvolvimento do Modernizr para desenvolver e aprender com ela. Após isso, quando você estiver
            // pronto para produção, use a ferramenta de compilação em http://modernizr.com para selecionar somente os testes que você precisa.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/").Include(
                        "~/Content/boostrap.css",
                        "~/Content/bootstrap.min.css",
                        "~/Content/materialize/css/materialize.css",
                        "~/Content/font-awesome.css",
                        "~/Content/font-awesome.min.css",
                        "~/Content/style.css"
                        ));
        }
    }
}