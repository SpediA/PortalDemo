////-----------------------------------------------------------------------
//// <copyright file="MapaProducaoSped.aspx.cs" company="SpediA">
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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using log4net;
    using SpediaLibrary.Business;
    using SpediaLibrary.Transfer;

    /// <summary>
    /// Classe responsável por toda a interface do mapa de produção de SPED
    /// </summary>
    public partial class MapaProducaoSped : System.Web.UI.Page
    {
        #region Propriedades Privadas

        /// <summary> Objeto da biblioteca log4net para registro de log da aplicação </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(MapaProducaoSped));

        /// <summary> Lista com todos os valores do mapa de produção </summary>
        private List<ItemProducao> mapa;

        /// <summary> Lista das empresas cadastradas </summary>
        private List<Empresa> empresas;

        #endregion

        /// <summary>
        /// Evento de carregamento da página "MapaProducaoSped"
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.DefineMapaProducao();
            }
        }

        /// <summary>
        /// Obtem os dados do mapa de produção e constrói a tela
        /// </summary>
        private void DefineMapaProducao()
        {
            this.mapa = (List<ItemProducao>)GerenciamentoConta.CarregaMapaProducao(TipoArquivo.Efd);

            if (this.mapa != null && this.mapa.Count > 0)
            {
                this.empresas = (List<Empresa>)GerenciamentoEmpresa.CarregaEmpresas();

                this.RptMapa.DataSource = this.mapa.OrderBy(m => m.IdEmpresa).ThenBy(m => m.TipoArquivo).Select(m => m.IdEmpresa).Distinct();
                this.RptMapa.DataBind();
            }
        }
    }
}