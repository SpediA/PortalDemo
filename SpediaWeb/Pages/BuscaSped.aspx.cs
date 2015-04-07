////-----------------------------------------------------------------------
//// <copyright file="BuscaSped.aspx.cs" company="SpediA">
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
    using SpediaLibrary.Util;
    using SpediaWeb.Presentation.Common;

    /// <summary>
    /// Classe responsável pela interface de busca de arquivos SPED
    /// </summary>
    public partial class BuscaSped : System.Web.UI.Page
    {
        #region Constantes

        /// <summary> Representa a quantidade de resultados da busca que serão exibidos por página </summary>
        private const byte RESULTADOS_POR_PAGINA = 20;

        /// <summary> Representa uma mensagem de quando um usuário não foi encontrado </summary>
        private const string USUARIO_NAO_ENCONTRADO = "Usuário não encontrado";

        /// <summary> Representa uma mensagem de sucesso </summary>
        private const string MENSAGEM_SUCESSO_DOWNLOAD = "Download concluído com sucesso!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_DOWNLOAD = "Erro ao tentar realizar o download dos arquivos!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_BUSCA_NENHUM_RESULTADO = "A busca não retornou resultados!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_NENHUM_ARQUIVO_SELECIONADO = "Nenhum arquivo foi selecionado!";

        /// <summary> Representa uma mensagem de sucesso </summary>
        private const string MENSAGEM_SUCESSO_ASSINATURA = "A assinatura dos arquivos foi solicitada!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_ASSINATURA = "O certificado escolhido ou a senha está inválida!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_ASSINATURA_ARQUIVO_REJEITADO = "A assinatura foi solicitada, porém os seguintes itens foram rejeitados: {0}.";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_OBTER_PROPRIEDADES = "Erro ao tentar carregar as propriedades do documento selecionado!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_CERTIFICADO_DIGITAL = "É necessário escolher um arquivo e digitar a senha!";

        /// <summary> Representa o texto para composição dos dados de um participante </summary>
        private const string TEXTO_PARTICIPANTE = "<b>CNPJ:</b> {0} <b>| IE:</b> {1} <b>|</b> {2}";

        /// <summary> Representa o texto para quando um SPED ainda não foi entrega à Sefaz </summary>
        private const string TEXTO_NAO_ENTREGUE = "Não entregue";

        /// <summary> Representa o texto para quando não há tipo de ação do arquivo </summary>
        private const string TEXTO_OUTROS = "Outros";
        
        #endregion

        #region Propriedades privadas

        /// <summary> Objeto da biblioteca log4net para registro de log da aplicação </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(BuscaSped));

        /// <summary> Número da página no resultado da busca </summary>
        private int pagina;

        #endregion

        /// <summary>
        /// Evento de carregamento da página "BuscaSped"
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.DivMensagem.Visible = false;

            if (!this.IsPostBack)
            {
                this.DefineControles();
            }
        }

        #region Comandos

        /// <summary>
        /// Solicita o certificado digital para, então solicitar a assinatura dos arquivos selecionados
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void BtnAssinar_Click(object sender, EventArgs e)
        {
            List<int> arquivosSelecionados = (List<int>)this.ObtemArquivosSelecionados();

            if (arquivosSelecionados.Count <= 0)
            {
                this.DivMensagem.Visible = true;
                this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_ERRO;
                this.LblMensagem.Text = MENSAGEM_ERRO_NENHUM_ARQUIVO_SELECIONADO;
                return;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "CertificadoDigital", "openModalDigitalCertificate();", true);
        }

        /// <summary>
        /// Solicita a assinatura dos arquivos selecionados
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void BtnSolicitarAssinatura_Click(object sender, EventArgs e)
        {
            AssinaturaSped assinatura;
            List<int> arquivosRejeitados;
            List<int> arquivosSelecionados = (List<int>)this.ObtemArquivosSelecionados();

            this.DivMensagem.Visible = true;
            this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_ERRO;

            if (arquivosSelecionados.Count <= 0)
            {
                this.LblMensagem.Text = MENSAGEM_ERRO_NENHUM_ARQUIVO_SELECIONADO;
                return;
            }

            if (!this.FuCertificadoDigital.HasFile || string.IsNullOrEmpty(this.TxtSenhaCertificadoDigital.Text))
            {
                this.LblMensagem.Text = MENSAGEM_ERRO_CERTIFICADO_DIGITAL;
                return;
            }

            try
            {
                assinatura = new AssinaturaSped()
                {
                    Speds = arquivosSelecionados,
                    Certificado = GerenciamentoArquivo.ObtemChaveCertificadoDigital(this.FuCertificadoDigital.FileBytes),
                    SenhaCertificado = this.TxtSenhaCertificadoDigital.Text
                };

                arquivosRejeitados = (List<int>)GerenciamentoArquivo.AssinaSped(assinatura);

                if (arquivosRejeitados.Count <= 0)
                {
                    this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_SUCESSO;
                    this.LblMensagem.Text = MENSAGEM_SUCESSO_ASSINATURA;
                }
                else
                {
                    string arquivos = string.Empty;

                    foreach (int arquivo in arquivosRejeitados)
                    {
                        arquivos += string.IsNullOrEmpty(arquivos) ? arquivo.ToString() : ", " + arquivo.ToString();
                    }

                    this.LblMensagem.Text = string.Format(MENSAGEM_ERRO_ASSINATURA_ARQUIVO_REJEITADO, arquivos);
                }
            }
            catch (Exception ex)
            {
                Log.Info(ex.InnerException == null ? ex.Message : ex.InnerException.ToString());
                this.LblMensagem.Text = MENSAGEM_ERRO_ASSINATURA;
                return;
            }
        }

        /// <summary>
        /// Realiza o download dos arquivos selecionados
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void BtnDownload_Click(object sender, EventArgs e)
        {
            List<int> arquivosSelecionados = (List<int>)this.ObtemArquivosSelecionados();
            string link;

            this.DivMensagem.Visible = true;
            this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_ERRO;

            if (arquivosSelecionados.Count <= 0)
            {
                this.LblMensagem.Text = MENSAGEM_ERRO_NENHUM_ARQUIVO_SELECIONADO;
                return;
            }

            try
            {
                link = GerenciamentoArquivo.ObtemLinkParaDownload(arquivosSelecionados);

                this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_SUCESSO;
                this.LblMensagem.Text = MENSAGEM_SUCESSO_DOWNLOAD;

                Response.Redirect(link, false);
            }
            catch (Exception ex)
            {
                Log.Info(ex.InnerException == null ? ex.Message : ex.InnerException.ToString());
                this.LblMensagem.Text = MENSAGEM_ERRO_DOWNLOAD;
                return;
            }
        }

        #endregion

        /// <summary>
        /// Evento que realiza a busca dos arquivos, baseado nos filtros simples inseridos pelo usuário
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void BtnBusca_Click(object sender, EventArgs e)
        {
            this.MontaFiltroBuscaSimples();
            this.ExecutaBusca();
        }

        /// <summary>
        /// Evento que realiza a busca dos arquivos, baseado nos filtros simples inseridos pelo usuário
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void BtnFiltrarResultados_Click(object sender, EventArgs e)
        {
            this.MontaFiltroBuscaSimples();
            this.ExecutaBusca();
        }

        /// <summary>
        /// Evento que realiza a busca dos arquivos, baseado nos filtros avançados inseridos pelo usuário
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void BtnExecutarBuscaAvancadaTopo_Click(object sender, EventArgs e)
        {
            this.MontaFiltroBuscaAvancada();
            this.ExecutaBusca();
        }

        /// <summary>
        /// Evento que realiza a busca dos arquivos, baseado nos filtros avançados inseridos pelo usuário
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void BtnExecutarBuscaAvancadaBase_Click(object sender, EventArgs e)
        {
            this.MontaFiltroBuscaAvancada();
            this.ExecutaBusca();
        }

        /// <summary>
        /// Evento que formata os resultados da busca e exibe na tela
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void RptResultadoBusca_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e != null && (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem))
            {
                ArquivoSped arquivo = (ArquivoSped)e.Item.DataItem;
                HiddenField hfIdResultadoBusca = (HiddenField)e.Item.FindControl("HfIdResultadoBusca");
                HtmlGenericControl spnSucesso = (HtmlGenericControl)e.Item.FindControl("SpnSucesso");
                HtmlGenericControl spnAdvertencia = (HtmlGenericControl)e.Item.FindControl("SpnAdvertencia");
                HtmlGenericControl spnErro = (HtmlGenericControl)e.Item.FindControl("SpnErro");
                HtmlGenericControl spnRejeitado = (HtmlGenericControl)e.Item.FindControl("SpnRejeitado");
                LinkButton btnPropriedadesArquivo = (LinkButton)e.Item.FindControl("BtnPropriedadesArquivo");
                Label lblTipoEscrituracao = (Label)e.Item.FindControl("LblTipoEscrituracao");
                Label lblFinalidade = (Label)e.Item.FindControl("LblFinalidade");
                Label lblCompetenciaInicio = (Label)e.Item.FindControl("LblCompetenciaInicio");
                Label lblCompetenciaFim = (Label)e.Item.FindControl("LblCompetenciaFim");
                Label lblDataEntrega = (Label)e.Item.FindControl("LblDataEntrega");
                Label lblEntidade = (Label)e.Item.FindControl("LblEntidade");

                hfIdResultadoBusca.Value = arquivo.Id.ToString();

                spnSucesso.Visible = arquivo.StatusPva.HasValue ? arquivo.StatusPva == StatusPva.ValidadoSucesso : false;
                spnAdvertencia.Visible = arquivo.StatusPva.HasValue ? arquivo.StatusPva == StatusPva.ValidadoAdvertencia : false;
                spnErro.Visible = arquivo.StatusPva.HasValue ? arquivo.StatusPva == StatusPva.ValidadoErro : false;
                spnRejeitado.Visible = arquivo.StatusPva.HasValue ? arquivo.StatusPva == StatusPva.Rejeitado : false;
                btnPropriedadesArquivo.CommandArgument = arquivo.Id.ToString();
                lblTipoEscrituracao.Text = Dominio.ValorTextoDe(arquivo.TipoEscrituracao);
                lblFinalidade.Text = arquivo.FinalidadeArquivo.HasValue ? (byte)arquivo.FinalidadeArquivo + " - " + Dominio.ValorTextoDe(arquivo.FinalidadeArquivo) : string.Empty;
                lblCompetenciaInicio.Text = arquivo.Competencia.De.HasValue ? arquivo.Competencia.De.Value.ToString("dd/MM/yyyy") : string.Empty;
                lblCompetenciaFim.Text = arquivo.Competencia.Ate.HasValue ? arquivo.Competencia.Ate.Value.ToString("dd/MM/yyyy") : string.Empty;
                lblDataEntrega.Text = arquivo.DataTransmissaoSefaz.HasValue ? arquivo.DataTransmissaoSefaz.Value.ToString("dd/MM/yyyy") : TEXTO_NAO_ENTREGUE;
                lblEntidade.Text = GerenciamentoEmpresa.MontaTextoParticipante(arquivo.Entidade);
            }
        }

        /// <summary>
        /// Evento que identifica e executa o comando disparado na listagem de resultados da busca
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void RptResultadoBusca_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e != null)
            {
                int idObjeto = Convert.ToInt32(e.CommandArgument);

                if (e.CommandName == "ExibirPropriedades")
                {
                    this.ObtemPropriedadesObjeto(idObjeto);
                }
            }
        }

        /// <summary>
        /// Evento que formata as páginas do resultado da busca e exibe na tela
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void RptPaginacao_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e != null)
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    this.pagina = (int)e.Item.DataItem;
                    LinkButton btnPagina = (LinkButton)e.Item.FindControl("BtnPagina");
                    btnPagina.Text = this.pagina.ToString();
                    btnPagina.CommandArgument = this.pagina.ToString();
                }
                else if (e.Item.ItemType == ListItemType.Header)
                {
                    LinkButton btnPrimeiraPagina = (LinkButton)e.Item.FindControl("BtnPrimeiraPagina");
                    btnPrimeiraPagina.CommandArgument = (this.pagina + 1).ToString();
                }
                else if (e.Item.ItemType == ListItemType.Footer)
                {
                    LinkButton btnUltimaPagina = (LinkButton)e.Item.FindControl("BtnUltimaPagina");
                    btnUltimaPagina.CommandArgument = this.pagina.ToString();
                }
            }
        }

        /// <summary>
        /// Evento que identifica qual comando foi solicitado e executa-o
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void RptPaginacao_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e != null)
            {
                int pagina = 0;

                switch (e.CommandName)
                {
                    case "SelecionaPagina":
                    case "SelecionaPrimeiraPagina":
                    case "SelecionaUltimaPagina":
                        pagina = Convert.ToInt32(e.CommandArgument);
                        break;
                }

                this.ExecutaBusca(pagina);
            }
        }

        /// <summary>
        /// Evento que formata os arquivos encontrados e exibe na tela
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void RptArquivo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e != null && (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem))
            {
                AcaoArquivo acao = (AcaoArquivo)e.Item.DataItem;
                LinkButton btnPropriedadeDownloadArquivo = (LinkButton)e.Item.FindControl("BtnPropriedadeDownloadArquivo");
                TextBox txtPropriedadeDataAcao = (TextBox)e.Item.FindControl("TxtPropriedadeDataAcao");
                string ids = string.Empty;

                foreach (int id in acao.IdArquivos)
                {
                    ids += ids == string.Empty ? id.ToString() : "," + id.ToString();
                }

                txtPropriedadeDataAcao.Text = acao.DataExecucao.HasValue ? acao.DataExecucao.Value.ToString("dd/MM/yyyy HH:mm") : string.Empty;
                btnPropriedadeDownloadArquivo.Text = acao.TipoAcao.HasValue ? Dominio.ValorTextoDe(acao.TipoAcao) : TEXTO_OUTROS;
                btnPropriedadeDownloadArquivo.CommandArgument = ids;
            }
        }

        /// <summary>
        /// Realiza o donwload do arquivo selecionado nas propriedades
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void BtnPropriedadeDownloadArquivo_Click(object sender, EventArgs e)
        {
            try
            {
                string argumento = ((LinkButton)sender).CommandArgument;
                List<string> arquivos = argumento.Split(',').ToList(); 
                List<int> idArquivos = new List<int>();
                string link;
                
                foreach (string arquivo in arquivos)
                {
                    idArquivos.Add(Convert.ToInt32(arquivo));
                }

                link = GerenciamentoArquivo.ObtemLinkParaDownload(idArquivos);

                this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_SUCESSO;
                this.LblMensagem.Text = MENSAGEM_SUCESSO_DOWNLOAD;

                Response.Redirect(link, false);
            }
            catch (Exception ex)
            {
                Log.Info(ex.InnerException == null ? ex.Message : ex.InnerException.ToString());
                this.LblMensagem.Text = MENSAGEM_ERRO_DOWNLOAD;
                return;
            }
        }

        /// <summary>
        /// Define os valores inicias dos controles que forem necessários
        /// </summary>
        private void DefineControles()
        {
            Dictionary<int, string> unidadesFederativas = (Dictionary<int, string>)Dominio.ObtemListaEnum(typeof(UnidadeFederativa));

            unidadesFederativas.Add(0, string.Empty);

            this.DefineControleUnidadeFederativa(unidadesFederativas);
        }

        /// <summary>
        /// Define os valores do controle de unidade federativa do contribuinte
        /// </summary>
        /// <param name="unidadesFederativas">Lista de unidades federativas</param>
        private void DefineControleUnidadeFederativa(Dictionary<int, string> unidadesFederativas)
        {
            this.DdUnidadeFederativaContribuinte.DataValueField = "Key";
            this.DdUnidadeFederativaContribuinte.DataTextField = "Value";
            this.DdUnidadeFederativaContribuinte.DataSource = unidadesFederativas.OrderBy(u => u.Value);
            this.DdUnidadeFederativaContribuinte.DataBind();
            this.DdUnidadeFederativaContribuinte.SelectedValue = "0";
        }

        /// <summary>
        /// Executa a busca de arquivos, baseado nos filtros preenchidos
        /// </summary>
        /// <param name="pagina">Número da página que conterá os resultados da busca</param>
        private void ExecutaBusca(int pagina = 1)
        {
            FiltroBuscaSped filtro = this.ViewState[ConstantesGlobais.FILTRO_BUSCA] != null ? (FiltroBuscaSped)this.ViewState[ConstantesGlobais.FILTRO_BUSCA]
                : new FiltroBuscaSped();
            int inicio = ((pagina - 1) * RESULTADOS_POR_PAGINA) + 1;
            ResultadoBuscaSped resultado = GerenciamentoArquivo.BuscaArquivosSped(filtro, inicio, RESULTADOS_POR_PAGINA);
            List<ArquivoSped> arquivos = (resultado != null && resultado.Resultados != null) ? (List<ArquivoSped>)resultado.Resultados : new List<ArquivoSped>();
            List<int> paginas = new List<int>();

            if (arquivos.Count > 0)
            {
                int quantidadePaginas = (int)Math.Ceiling((double)resultado.Quantidade / (double)RESULTADOS_POR_PAGINA);

                for (int i = 1; i <= quantidadePaginas; i++)
                {
                    paginas.Add(i);
                }

                this.DivAcoes.Visible = true;

                this.pagina = 0;

                this.RptResultadoBusca.Visible = true;
                this.RptResultadoBusca.DataSource = arquivos; ////OrderByDescending(a => a.Competencia).ToList();
                this.RptResultadoBusca.DataBind();

                this.RptPaginacao.Visible = quantidadePaginas > 1;
                this.RptPaginacao.DataSource = paginas;
                this.RptPaginacao.DataBind();
            }
            else
            {
                this.DivMensagem.Visible = true;
                this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_ERRO;
                this.LblMensagem.Text = MENSAGEM_ERRO_BUSCA_NENHUM_RESULTADO;
                this.DivAcoes.Visible = false;
                this.RptResultadoBusca.Visible = false;
                this.RptPaginacao.Visible = false;
                return;
            }
        }

        /// <summary>
        /// Obtem os valores digitados nos campos da pesquisa e monta um objeto FiltroBusca
        /// </summary>
        /// <returns>Objeto filtro da busca</returns>
        private FiltroBuscaSped MontaFiltroBuscaSimples()
        {
            FiltroBuscaSped filtro = new FiltroBuscaSped();
            var recepcionado = Util.MontaDataIntervaloOpcao(this.DdRecepcionado.SelectedValue);

            filtro.TextoLivre = this.TxtBusca.Text;

            filtro.Entidade = new Participante()
            {
                Cnpj = Util.ObtemSomenteDigitos(this.TxtCnpj.Text),
                Ie = Util.ObtemSomenteDigitos(this.TxtInscricaoEstadual.Text)
            };

            foreach (ListItem item in this.CblTipoEscrituracao.Items.Cast<ListItem>().Where(li => li.Selected))
            {
                if (filtro.TipoEscrituracao == null)
                {
                    filtro.TipoEscrituracao = new List<TipoEscrituracao>();
                }

                filtro.TipoEscrituracao.Add((TipoEscrituracao)Convert.ToByte(item.Value));
            }

            foreach (ListItem item in this.CblSituacaoArquivo.Items.Cast<ListItem>().Where(li => li.Selected))
            {
                if (filtro.StatusPva == null)
                {
                    filtro.StatusPva = new List<StatusPva>();
                }

                filtro.StatusPva.Add((StatusPva)Convert.ToByte(item.Value));
            }

            filtro.Recepcionado = recepcionado != null ? (DataIntervalo)recepcionado : null;

            this.ViewState[ConstantesGlobais.FILTRO_BUSCA] = filtro;

            return filtro;
        }

        /// <summary>
        /// Obtem os valores digitados nos campos da pesquisa e monta um objeto FiltroBusca
        /// </summary>
        /// <returns>Objeto filtro da busca</returns>
        private FiltroBuscaSped MontaFiltroBuscaAvancada()
        {
            FiltroBuscaSped filtro = new FiltroBuscaSped();

            var competencia = Util.MontaDataIntervalo(this.TxtCompetenciaInicio.Text, this.TxtCompetenciaFim.Text);
            var periodoEntrega = Util.MontaDataIntervalo(this.TxtPeriodoEntregaInicio.Text, this.TxtPeriodoEntregaFim.Text);
            var dataRecepcionado = Util.MontaDataIntervalo(this.TxtDataRecepcionadoInicio.Text, this.TxtDataRecepcionadoFim.Text);
            var dataValidado = Util.MontaDataIntervalo(this.TxtDataValidadoInicio.Text, this.TxtDataValidadoFim.Text);
            var dataConciliado = Util.MontaDataIntervalo(this.TxtDataConciliadoInicio.Text, this.TxtDataConciliadoFim.Text);
            var dataAssinado = Util.MontaDataIntervalo(this.TxtDataAssinadoInicio.Text, this.TxtDataAssinadoFim.Text);
            var dataTransmitido = Util.MontaDataIntervalo(this.TxtDataTransmitidoInicio.Text, this.TxtDataTransmitidoFim.Text);

            filtro.NomeArquivo = this.TxtNomeArquivo.Text;
            filtro.Competencia = competencia;
            ////filtro.DataEntrega; TODO: Implementar quando houver na API
            filtro.Recepcionado = dataRecepcionado;
            filtro.DataProcessamentoPva = dataValidado;
            ////filtro.DataConciliacao; TODO: Implementar quando houver na API
            filtro.DataAssinatura = dataAssinado;
            filtro.DataTransmissaoSefaz = dataTransmitido;

            foreach (ListItem item in this.CblTipoEscrituracaoAvancada.Items.Cast<ListItem>().Where(li => li.Selected))
            {
                if (filtro.TipoEscrituracao == null)
                {
                    filtro.TipoEscrituracao = new List<TipoEscrituracao>();
                }

                filtro.TipoEscrituracao.Add((TipoEscrituracao)Convert.ToByte(item.Value));
            }

            if (this.RblFinalidade.SelectedValue != null && !string.IsNullOrEmpty(this.RblFinalidade.SelectedValue))
            {
                filtro.FinalidadeArquivo = (FinalidadeOperacaoSped)Convert.ToByte(this.RblFinalidade.SelectedValue);
            }

            filtro.Entidade = new Participante()
            {
                Cnpj = Util.ObtemSomenteDigitos(this.TxtTxtCnpjContribuinte.Text),
                Ie = Util.ObtemSomenteDigitos(this.TxtInscricaoEstadualContribuinte.Text),
                RazaoSocial = this.TxtRazaoSocialContribuinte.Text,
                Uf = this.DdUnidadeFederativaContribuinte.SelectedItem != null ? this.DdUnidadeFederativaContribuinte.SelectedItem.Text : null
            };

            foreach (ListItem item in this.CblSituacaoAvancada.Items.Cast<ListItem>().Where(li => li.Selected))
            {
                if (filtro.StatusPva == null)
                {
                    filtro.StatusPva = new List<StatusPva>();
                }

                filtro.StatusPva.Add((StatusPva)Convert.ToByte(item.Value));
            }

            foreach (ListItem item in this.CblSituacaoSPED.Items.Cast<ListItem>().Where(li => li.Selected))
            {
                if (filtro.StatusSped == null)
                {
                    filtro.StatusSped = new List<StatusSped>();
                }

                filtro.StatusSped.Add((StatusSped)Convert.ToByte(item.Value));
            }

            
            ////filtro.TipoTransmissao

            this.ViewState[ConstantesGlobais.FILTRO_BUSCA] = filtro;

            return filtro;
        }

        /// <summary>
        /// Obtem as propriedades do objeto e exibe
        /// </summary>
        /// <param name="idObjeto">Identificador do objeto</param>
        private void ObtemPropriedadesObjeto(int idObjeto)
        {
            PropriedadeObjeto propriedade;

            try
            {
                propriedade = GerenciamentoArquivo.ObtemPropriedadesObjeto(idObjeto);

                this.DefineControlePropriedades(propriedade);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Propriedades", "openModal();", true);
            }
            catch (Exception ex)
            {
                Log.Info(ex.InnerException == null ? ex.Message : ex.InnerException.ToString());
                this.DivMensagem.Visible = true;
                this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_ERRO;
                this.LblMensagem.Text = MENSAGEM_ERRO_OBTER_PROPRIEDADES;
                return;
            }
        }

        /// <summary>
        /// Define os valores de cada controle de propriedades de arquivo para exibição
        /// </summary>
        /// <param name="propriedade">Propriedades do arquivo</param>
        private void DefineControlePropriedades(PropriedadeObjeto propriedade)
        {
            List<UsuarioApi> usuarios = (List<UsuarioApi>)GerenciamentoUsuario.CarregaUsuariosApi();
            UsuarioApi usuario = usuarios.Exists(u => u.Id == propriedade.IdUsuarioUpload) ? usuarios.First(u => u.Id == propriedade.IdUsuarioUpload) : null;
            List<AcaoArquivo> arquivos = new List<AcaoArquivo>();
            GerenciamentoArquivo.ExtraiDocumentosObjeto(propriedade, ref arquivos);

            this.TxtPropriedadeNomeArquivo.Text = propriedade.NomeOriginal;
            this.TxtPropriedadeCompetenciaInicial.Text = propriedade.DetalheSped.CompetenciaInicial.HasValue ? propriedade.DetalheSped.CompetenciaInicial.Value.ToString("dd/MM/yyyy") : string.Empty;
            this.TxtPropriedadeCompetenciaFinal.Text = propriedade.DetalheSped.CompetenciaFinal.HasValue ? propriedade.DetalheSped.CompetenciaFinal.Value.ToString("dd/MM/yyyy") : string.Empty;
            this.TxtPropriedadeTipoEscrituracao.Text = Dominio.ValorTextoDe(propriedade.DetalheSped.TipoEscrituracao);
            this.TxtPropriedadeFinalidade.Text = propriedade.DetalheSped.FinalidadeArquivo.HasValue ?
                (byte)propriedade.DetalheSped.FinalidadeArquivo + " - " + Dominio.ValorTextoDe(propriedade.DetalheSped.FinalidadeArquivo) : string.Empty;
            this.TxtPropriedadeParticipanteCnpj.Text = Util.FormataCnpj(propriedade.DetalheSped.EntidadeCnpj);
            this.TxtPropriedadeParticipanteInscricaoEstadual.Text = propriedade.DetalheSped.EntidadeIe;
            this.TxtPropriedadeParticipanteRazaoSocial.Text = propriedade.DetalheSped.EntidadeRazaoSocial;
            this.TxtPropriedadeOrigem.Text = usuario != null ? usuario.Nome : USUARIO_NAO_ENCONTRADO;
            this.TxtPropriedadeDataRecepcao.Text = propriedade.DataUpload.ToString("dd/MM/yyyy HH:mm");

            if (propriedade.DetalheSped.DataProcessamentoPva.HasValue)
            {
                this.TxtPropriedadeArquivoValidado.Text = Dominio.ValorTextoDe(propriedade.DetalheSped.StatusPva);
                this.TxtPropriedadeArquivoValidado.Visible = true;
                this.TxtPropriedadeDataArquivoValidado.Text = propriedade.DetalheSped.DataProcessamentoPva.Value.ToString("dd/MM/yyyy HH:mm");
                this.TxtPropriedadeDataArquivoValidado.Visible = true;
            }

            if (propriedade.DetalheSped.DataAssinatura.HasValue)
            {
                this.TxtPropriedadeArquivoAssinadoSucesso.Visible = true;
                this.TxtPropriedadeDataArquivoAssinadoSucesso.Text = propriedade.DetalheSped.DataAssinatura.Value.ToString("dd/MM/yyyy HH:mm");
                this.TxtPropriedadeDataArquivoAssinadoSucesso.Visible = true;
            }

            if (propriedade.DetalheSped.DataTransmissaoSefaz.HasValue)
            {
                this.TxtPropriedadeArquivoTransmitidoSucesso.Visible = true;
                this.TxtPropriedadeDataArquivoTransmitidoSucesso.Text = propriedade.DetalheSped.DataTransmissaoSefaz.Value.ToString("dd/MM/yyyy");
                this.TxtPropriedadeDataArquivoTransmitidoSucesso.Visible = true;
            }

            /*
            this.LblPropriedadeArquivoConciliadoIndiciosErro.Text = propriedade.DetalheSped;
            this.LblPropriedadeDataArquivoConciliadoIndiciosErro.Text = propriedade.DetalheSped..ToString("dd/MM/yyyy");
            */
            
            ////this.LblPropriedadeUsuarioUltimoAcesso = ;
            ////this.LblPropriedadeDataUltimoAcesso = ;

            this.RptArquivo.DataSource = arquivos.OrderBy(a => a.DataExecucao);
            this.RptArquivo.DataBind();
        }

        /// <summary>
        /// Obtém os arquivos, resultados da busca, que foram selecionados pelo usuário
        /// </summary>
        /// <returns>Lista dos arquivos selecionados</returns>
        private IList<int> ObtemArquivosSelecionados()
        {
            List<int> arquivosSelecionados = new List<int>();

            foreach (RepeaterItem item in this.RptResultadoBusca.Items)
            {
                HiddenField hfIdResultadoBusca = (HiddenField)item.FindControl("HfIdResultadoBusca");
                CheckBox cboItemResultadoBusca = (CheckBox)item.FindControl("CboItemResultadoBusca");
                int id;

                if (cboItemResultadoBusca.Checked)
                {
                    id = Convert.ToInt32(hfIdResultadoBusca.Value);
                    arquivosSelecionados.Add(id);
                }
            }

            return arquivosSelecionados;
        }
    }
}