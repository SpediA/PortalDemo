////-----------------------------------------------------------------------
//// <copyright file="RotaConfig.cs" company="SpediA">
//// Copyright [2014] [SPEDIA Solu��es Tecnol�gicas Ltda]
//// Licenciado sob Licen�a Apache, Vers�o 2.0 (a "Licen�a"). Voc� n�o pode usar este arquivo exceto em conformidade com a Licen�a.
//// Voc� pode obter uma c�pia da Licen�a em:
//// http://www.apache.org/licenses/LICENSE-2.0
//// Ao menos que seja exigido por lei aplic�vel ou com autoriza��o por escrito, todo software distribu�do sob a Licen�a � distribu�do "COMO EST�",
//// SEM GARANTIAS OU CONDI��ES DE NENHUMA ESP�CIE, expressas ou impl�citas.
//// Veja a Licen�a no idioma espec�fico que estabelece as permiss�es e limita��es sob a Licen�a.
//// </copyright>
////-----------------------------------------------------------------------
namespace SpediaWeb
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Routing;
    using Microsoft.AspNet.FriendlyUrls;

    /// <summary>
    /// Classe de configura��o de rotas
    /// </summary>
    public static class RotaConfig
    {
        /// <summary>
        /// Registra as rotas para que as urls fiquem mais amig�veis (sem exten��es)
        /// </summary>
        /// <param name="rotas">Cole��o de rotas</param>
        public static void RegistraRotas(RouteCollection rotas)
        {
            var configuracoes = new FriendlyUrlSettings();
            configuracoes.AutoRedirectMode = RedirectMode.Permanent;
            rotas.EnableFriendlyUrls(configuracoes);
        }
    }
}
