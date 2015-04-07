////-----------------------------------------------------------------------
//// <copyright file="AtributosAdicionais.ashx.cs" company="SpediA">
//// Copyright [2014] [SPEDIA Soluções Tecnológicas Ltda]
//// Licenciado sob Licença Apache, Versão 2.0 (a "Licença"). Você não pode usar este arquivo exceto em conformidade com a Licença.
//// Você pode obter uma cópia da Licença em:
//// http://www.apache.org/licenses/LICENSE-2.0
//// Ao menos que seja exigido por lei aplicável ou com autorização por escrito, todo software distribuído sob a Licença é distribuído "COMO ESTÁ",
//// SEM GARANTIAS OU CONDIÇÕES DE NENHUMA ESPÉCIE, expressas ou implícitas.
//// Veja a Licença no idioma específico que estabelece as permissões e limitações sob a Licença.
//// </copyright>
////-----------------------------------------------------------------------
namespace SpediaWeb.Pages
{
    using System.Collections.Generic;
    using System.Web;
    using SpediaLibrary.Business;
    using SpediaLibrary.Transfer;
    using SpediaLibrary.Util;

    /// <summary>
    /// Classe responsável por obter, através de uma requisição http, os valores dos atributos adicionais
    /// </summary>
    public class AtributosAdicionais : IHttpHandler
    {
        /// <summary>
        /// Obtém um valor que indica se esse Http Handler é reutilizável
        /// </summary>
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Processa a solicitação http
        /// </summary>
        /// <param name="context">Contexto http da solicitação</param>
        public void ProcessRequest(HttpContext context)
        {
            ////context.Response.Write(json);
        }
    }
}