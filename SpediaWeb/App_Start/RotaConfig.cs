////-----------------------------------------------------------------------
//// <copyright file="RotaConfig.cs" company="SpediA">
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
    using System.Web;
    using System.Web.Routing;
    using Microsoft.AspNet.FriendlyUrls;

    /// <summary>
    /// Classe de configuração de rotas
    /// </summary>
    public static class RotaConfig
    {
        /// <summary>
        /// Registra as rotas para que as urls fiquem mais amigáveis (sem extenções)
        /// </summary>
        /// <param name="rotas">Coleção de rotas</param>
        public static void RegistraRotas(RouteCollection rotas)
        {
            var configuracoes = new FriendlyUrlSettings();
            configuracoes.AutoRedirectMode = RedirectMode.Permanent;
            rotas.EnableFriendlyUrls(configuracoes);
        }
    }
}
