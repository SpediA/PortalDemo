////-----------------------------------------------------------------------
//// <copyright file="PacoteConfig.cs" company="SpediA">
//// Copyright [2014] [SPEDIA Soluções Tecnológicas Ltda]
//// Licenciado sob Licença Apache, Versão 2.0 (a "Licença"). Você não pode usar este arquivo exceto em conformidade com a Licença.
//// Você pode obter uma cópia da Licença em:
//// http://www.apache.org/licenses/LICENSE-2.0
//// Ao menos que seja exigido por lei aplicável ou com autorização por escrito, todo software distribuído sob a Licença é distribuído "COMO ESTÁ",
//// SEM GARANTIAS OU CONDIÇÕES DE NENHUMA ESPÉCIE, expressas ou implícitas.
//// Veja a Licença no idioma específico que estabelece as permissões e limitações sob a Licença.
//// </copyright>
////-----------------------------------------------------------------------
namespace SpediaWeb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Optimization;

    /// <summary>
    /// Classe de configuração de pacotes
    /// </summary>
    public class PacoteConfig
    {
        /// <summary>
        /// Registra os pacotes (bundles)
        /// Para maiores informações sobre "bundling", visite http://go.microsoft.com/fwlink/?LinkId=301862
        /// </summary>
        /// <param name="pacotes">Coleção de pacotes</param>
        public static void RegistraPacotes(BundleCollection pacotes)
        {
            pacotes.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            pacotes.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/bootstrap-hover-dropdown.js",
                "~/Scripts/bootstrap-tagsinput.js"));

            pacotes.Add(new ScriptBundle("~/bundles/fileinput").Include(
                "~/Scripts/fileinput.js"));

            pacotes.Add(new ScriptBundle("~/bundles/typeahead").Include(
                "~/Scripts/typeahead.bundle.js"));

            pacotes.Add(new StyleBundle("~/styles/bootstrap")
                .Include("~/Content/Styles/bootstrap.css", new CssRewriteUrlTransform())
                .Include("~/Content/Styles/bootstrap-theme.css", new CssRewriteUrlTransform())
                .Include("~/Content/Styles/bootstrap-tagsinput.css", new CssRewriteUrlTransform()));

            pacotes.Add(new StyleBundle("~/styles/spedia")
                .Include("~/Content/Styles/site.css", new CssRewriteUrlTransform()));

            pacotes.Add(new StyleBundle("~/styles/fileinput")
                .Include("~/Content/Styles/bootstrap-fileinput/css/fileinput.css", new CssRewriteUrlTransform()));
        }
    }
}