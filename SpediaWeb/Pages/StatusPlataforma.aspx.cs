////-----------------------------------------------------------------------
//// <copyright file="StatusPlataforma.aspx.cs" company="SpediA">
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
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using log4net;
    using SpediaLibrary.Business;
    using SpediaLibrary.Transfer;

    /// <summary>
    /// Classe responsável por toda a interface de status da plataforma
    /// </summary>
    public partial class StatusPlataforma : System.Web.UI.Page
    {
        /// <summary> Objeto da biblioteca log4net para registro de log da aplicação </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(StatusPlataforma));

        /// <summary>
        /// Evento de carregamento da página "StatusPlataforma"
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.ObtemStatusPlataforma();
            }
        }

        /// <summary>
        /// Evento de construção de cada linha de status da plataforma
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void RptPlataforma_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e != null && (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem))
            {
                StatusModulo statusModulo = (StatusModulo)e.Item.DataItem;
                StatusModuloData dataStatus = (statusModulo.Datas != null && statusModulo.Datas.Count > 0) ? 
                    statusModulo.Datas.OrderByDescending(d => d.DataConsulta).First() : null;
                Label lblDescricao = (Label)e.Item.FindControl("LblDescricao");
                Label lblDataStatus = (Label)e.Item.FindControl("LblDataStatus");
                HtmlGenericControl divStatusOk = (HtmlGenericControl)e.Item.FindControl("DivStatusOk");
                HtmlGenericControl divStatusAtencao = (HtmlGenericControl)e.Item.FindControl("DivStatusAtencao");
                HtmlGenericControl divStatusErro = (HtmlGenericControl)e.Item.FindControl("DivStatusErro");

                if (dataStatus != null)
                {
                    lblDescricao.Text = statusModulo.Modulo;
                    lblDataStatus.Text = dataStatus.DataConsulta.ToString("dd/MM/yyyy");
                    divStatusOk.Visible = dataStatus.StatusModulo == (byte)TipoStatusPlataforma.Ok;
                    divStatusAtencao.Visible = dataStatus.StatusModulo == (byte)TipoStatusPlataforma.Atencao;
                    divStatusErro.Visible = dataStatus.StatusModulo == (byte)TipoStatusPlataforma.Erro;
                }
            }
        }

        /// <summary>
        /// Obtém os status de cada módulo da plataforma
        /// </summary>
        private void ObtemStatusPlataforma()
        {
            List<StatusModulo> status = (List<StatusModulo>)GerenciamentoPlataforma.ObtemStatusPlataforma();

            this.RptPlataforma.DataSource = status;
            this.RptPlataforma.DataBind();
        }
    }
}