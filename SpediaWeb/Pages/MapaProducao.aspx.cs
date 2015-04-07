////-----------------------------------------------------------------------
//// <copyright file="MapaProducao.aspx.cs" company="SpediA">
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
    using SpediaWeb.Presentation.Common;

    /// <summary>
    /// Classe responsável por toda a interface do mapa de produção de DFe
    /// </summary>
    public partial class MapaProducao : System.Web.UI.Page
    {
        #region Propriedades Privadas

        /// <summary> Objeto da biblioteca log4net para registro de log da aplicação </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(MapaProducao));

        /// <summary> Lista com todos os valores do mapa de produção </summary>
        private List<ItemProducao> mapa;

        /// <summary> Lista das empresas cadastradas </summary>
        private List<Empresa> empresas;

        #endregion

        /// <summary>
        /// Evento de carregamento da página "MapaProducao"
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
        /// Evento de construção de cada linha do mapa de produção
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void RptMapa_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e != null && (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem))
            {
                int idEmpresa = Convert.ToInt32(e.Item.DataItem);
                LinkButton btnEmpresa = (LinkButton)e.Item.FindControl("BtnEmpresa");
                LinkButton btnTipoProprios = (LinkButton)e.Item.FindControl("BtnTipoProprios");
                LinkButton btnTipoTerceiros = (LinkButton)e.Item.FindControl("BtnTipoTerceiros");
                LinkButton btnDocumentoInvalidoProprios = (LinkButton)e.Item.FindControl("BtnDocumentoInvalidoProprios");
                LinkButton btnDocumentoValidoProprios = (LinkButton)e.Item.FindControl("BtnDocumentoValidoProprios");
                LinkButton btnDocumentoInvalidoTerceiros = (LinkButton)e.Item.FindControl("BtnDocumentoInvalidoTerceiros");
                LinkButton btnDocumentoValidoTerceiros = (LinkButton)e.Item.FindControl("BtnDocumentoValidoTerceiros");
                Empresa empresa = this.empresas.Any(m => m.Id == idEmpresa) ? this.empresas.First(m => m.Id == idEmpresa) : null;

                btnEmpresa.Text = empresa != null ? empresa.RazaoSocial : idEmpresa.ToString();
                btnEmpresa.CommandArgument = empresa.Cnpj;

                btnTipoProprios.CommandArgument = TipoOperacao.Emitente.ToString();
                btnTipoTerceiros.CommandArgument = TipoOperacao.Destinatario.ToString();

                btnDocumentoInvalidoProprios.Text = this.mapa.Where(m => m.IdEmpresa == idEmpresa).Where(m => m.Quantidades.First().Key == TipoOperacao.Emitente)
                    .Sum(m => m.Quantidades.Values.First().QuantidadeInvalido).ToString();
                btnDocumentoInvalidoProprios.CommandArgument = StatusArquivo.Invalido.ToString();

                btnDocumentoValidoProprios.Text = this.mapa.Where(m => m.IdEmpresa == idEmpresa).Where(m => m.Quantidades.First().Key == TipoOperacao.Emitente)
                    .Sum(m => m.Quantidades.Values.First().QuantidadeValido).ToString();
                btnDocumentoValidoProprios.CommandArgument = StatusArquivo.Valido.ToString();
                
                btnDocumentoInvalidoTerceiros.Text = this.mapa.Where(m => m.IdEmpresa == idEmpresa).Where(m => m.Quantidades.First().Key == TipoOperacao.Destinatario)
                    .Sum(m => m.Quantidades.Values.First().QuantidadeInvalido).ToString();
                btnDocumentoInvalidoTerceiros.CommandArgument = StatusArquivo.Invalido.ToString();

                btnDocumentoValidoTerceiros.Text = this.mapa.Where(m => m.IdEmpresa == idEmpresa).Where(m => m.Quantidades.First().Key == TipoOperacao.Destinatario)
                    .Sum(m => m.Quantidades.Values.First().QuantidadeValido).ToString();
                btnDocumentoValidoTerceiros.CommandArgument = StatusArquivo.Valido.ToString();
            }
        }

        /// <summary>
        /// Evento de execução dos comandos advindos de uma das linhas do controle "RptMapa"
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void RptMapa_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e != null)
            {
                LinkButton btnEmpresa = (LinkButton)e.Item.FindControl("BtnEmpresa");
                LinkButton btnTipoProprios = (LinkButton)e.Item.FindControl("BtnTipoProprios");
                LinkButton btnTipoTerceiros = (LinkButton)e.Item.FindControl("BtnTipoTerceiros");
                FiltroBusca filtroMapa = new FiltroBusca();
                int month = DateTime.Now.Month;
                int year = DateTime.Now.Year;

                filtroMapa.Recepcionado = new DataIntervalo()
                {
                    De = Convert.ToDateTime("1/" + month + "/" + year),
                    Ate = Convert.ToDateTime(DateTime.DaysInMonth(year, month) + "/" + month + "/" + year)
                };

                filtroMapa.Participante = new Participante()
                {
                    Cnpj = btnEmpresa.CommandArgument
                };

                if (e.CommandName == "SelecaoTipoProprios" || e.CommandName == "SelecaoTipoTerceiros")
                {
                    filtroMapa.OperacaoFiscal = new OperacaoFiscal()
                    {
                        TipoEmissao = e.CommandName == "SelecaoTipoProprios" ? TipoEmissao.Propria : TipoEmissao.Terceiros
                    };
                }

                if (e.CommandName == "SelecaoInvalido" || e.CommandName == "SelecaoValido")
                {
                    filtroMapa.OperacaoFiscal = new OperacaoFiscal()
                    {
                        TipoEmissao = btnTipoProprios != null ? TipoEmissao.Propria : TipoEmissao.Terceiros
                    };

                    filtroMapa.DocumentoFiscal = new DocumentoFiscal()
                    {
                        JuridicamenteValido = e.CommandName == "SelecaoValido"
                    };
                }

                this.Session[ConstantesGlobais.FILTRO_BUSCA] = filtroMapa;

                Response.Redirect("Busca.aspx");
            }
        }

        /// <summary>
        /// Obtem os dados do mapa de produção e constrói a tela
        /// </summary>
        private void DefineMapaProducao()
        {
            this.mapa = (List<ItemProducao>)GerenciamentoConta.CarregaMapaProducao(TipoArquivo.Nfe);

            if (this.mapa != null && this.mapa.Count > 0)
            {
                this.empresas = (List<Empresa>)GerenciamentoEmpresa.CarregaEmpresas();

                this.RptMapa.DataSource = this.mapa.OrderBy(m => m.IdEmpresa).ThenBy(m => m.TipoArquivo).Select(m => m.IdEmpresa).Distinct();
                this.RptMapa.DataBind();
            }
        }
    }
}