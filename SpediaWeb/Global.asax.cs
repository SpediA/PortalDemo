////-----------------------------------------------------------------------
//// <copyright file="Global.asax.cs" company="SpediA">
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
    using System.Web.Routing;
    using System.Web.Security;
    using log4net;
    using NHibernate;
    using NHibernate.Context;
    using SpediaLibrary.Persistence;
    using SpediaLibrary.Persistence.DatabaseMigration;
    using SpediaWeb;

    /// <summary>
    /// Classe Global que trata eventos de aplicação e sessão
    /// </summary>
    public class Global : HttpApplication
    {
        /// <summary> Objeto da biblioteca log4net para registro de log da aplicação </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(Global));

        /// <summary>
        /// Inicia uma nova instância da classe <see cref="Global"/>
        /// </summary>
        public Global()
        {
            this.FabricaSessao = AuxiliarNHibernate.ObtemFabricaSessao();
        }

        /// <summary> Obtém ou define o objeto "fábrica" de sessão </summary>
        private ISessionFactory FabricaSessao { get; set; }

        /// <summary>
        /// Evento de inicialização da aplicação
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        public void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            Log.Info("***Iniciando a aplicação***");

            RotaConfig.RegistraRotas(RouteTable.Routes);
            PacoteConfig.RegistraPacotes(BundleTable.Bundles);

            Log.Info("Iniciando o FluentMigrator");
            ExecutorMigracao.MigraParaMaisRecente();
            Log.Info("Terminado o FluentMigrator");

            Log.Info("***Aplicação iniciada com sucesso***");
        }

        /// <summary>
        /// Evento de inicialização de uma requisição
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        public void Application_BeginRequest(object sender, EventArgs e)
        {
            try
            {
                var sessao = this.FabricaSessao.OpenSession();
                CurrentSessionContext.Bind(sessao);
                sessao.BeginTransaction();
            }
            catch (Exception ex)
            {
                Log.Info(ex.InnerException == null ? ex.Message : ex.InnerException.ToString());
            }
        }

        /// <summary>
        /// Evento de fechamento de uma requisição
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        public void Application_EndRequest(object sender, EventArgs e)
        {
            try
            {
                var sessao = this.FabricaSessao.GetCurrentSession();
                var transacao = sessao.Transaction;
                if (transacao != null && transacao.IsActive)
                {
                    transacao.Commit();
                }

                sessao = CurrentSessionContext.Unbind(this.FabricaSessao);
                sessao.Close();
            }
            catch (Exception ex)
            {
                Log.Info(ex.InnerException == null ? ex.Message : ex.InnerException.ToString());
            }
        }

        /// <summary>
        /// Loga os erros da aplicação
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            Log.Error(ex);
        }
    }
}
