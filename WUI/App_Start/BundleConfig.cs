using System.Web;
using System.Web.Optimization;

namespace WUI
{
    public class BundleConfig
    {
        // Pour plus d’informations sur le Bundling, accédez à l’adresse http://go.microsoft.com/fwlink/?LinkId=254725 (en anglais)
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;

            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Utilisez la version de développement de Modernizr pour développer et apprendre. Puis, lorsque vous êtes
            // prêt pour la production, utilisez l’outil de génération sur http://modernizr.com pour sélectionner uniquement les tests dont vous avez besoin.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            // CSS général de mon site
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            // CSS de Bootstrap
            bundles.Add(new StyleBundle("~/bundles/Bootstrap/css")
                .Include("~/Content/Bootstrap/bootstrap.css")
                .Include("~/Content/Bootstrap/bootstrap-theme.css")
                .Include("~/Content/Bootstrap/bootstrap-theme.css.map")
                .Include("~/Content/Bootstrap/bootstrap.css.map")
                );

            // JS de Bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap/js")
                .Include("~/Scripts/Bootstrap/bootstrap.js"));


            bundles.Add(new StyleBundle("~/Content/Category/css")
                .Include("~/Content/Category.css"));

            bundles.Add(new ScriptBundle("~/bundles/Race/js")
                .Include("~/Scripts/Race/Race.js"));


            bundles.Add(new StyleBundle("~/Content/Race/css")
                .Include("~/Content/Race.css"));
        }
    }
}