////-----------------------------------------------------------------------
//// <copyright file="Busca.aspx.cs" company="SpediA">
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
    using System.IO;
    using System.Linq;
    using System.Net;
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
    /// Classe responsável pela interface de busca de arquivos DF-e
    /// </summary>
    public partial class Busca : System.Web.UI.Page
    {
        #region Constantes

        /// <summary> Representa a quantidade de resultados da busca que serão exibidos por página </summary>
        private const byte RESULTADOS_POR_PAGINA = 20;

        /// <summary> Representa uma mensagem de quando um usuário não foi encontrado </summary>
        private const string USUARIO_NAO_ENCONTRADO = "Usuário não encontrado";

        /// <summary> Representa uma mensagem de sucesso </summary>
        private const string MENSAGEM_SUCESSO_CONSULTA_SEFAZ = "Consulta na SEFAZ foi solicitada!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_CONSULTA_SEFAZ_ARQUIVO_PROBLEMA = "A consulta na SEFAZ foi solicitada, porém os seguintes itens apresentaram inconsistência: {0}.";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_CONSULTA_SEFAZ = "Erro ao tentar solicitar a consulta na SEFAZ!";

        /// <summary> Representa uma mensagem de sucesso </summary>
        private const string MENSAGEM_SUCESSO_DOWNLOAD = "Download concluído com sucesso!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_DOWNLOAD = "Erro ao tentar realizar o download dos arquivos!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_BUSCA_NENHUM_RESULTADO = "A busca não retornou resultados!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_NENHUM_ARQUIVO_SELECIONADO = "Nenhum arquivo foi selecionado!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_OBTER_PROPRIEDADES = "Erro ao tentar carregar as propriedades do documento selecionado!";

        /// <summary> Representa uma situação de integridade do arquivo </summary>
        private const string TEXTO_COM_PROTOCOLO_AUTORIZACAO = "Arquivo com Protocolo de Autorização";

        /// <summary> Representa uma situação de integridade do arquivo </summary>
        private const string TEXTO_SEM_PROTOCOLO_AUTORIZACAO = "Arquivo sem Protocolo de Autorização";

        /// <summary> Representa uma situação de integridade do arquivo </summary>
        private const string TEXTO_ASSINATURA_DIGITAL_VALIDA = "Arquivo com Assinatura Digital Válida";

        /// <summary> Representa uma situação de integridade do arquivo </summary>
        private const string TEXTO_ASSINATURA_DIGITAL_INVALIDA = "Arquivo com Assinatura Digital Inválida";

        /// <summary> Representa uma situação de integridade do arquivo </summary>
        private const string TEXTO_NFE_CANCELADA = "Arquivo Cancelado";

        #endregion

        #region Propriedades privadas

        /// <summary> Objeto da biblioteca log4net para registro de log da aplicação </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(Global));

        /// <summary> Número da página no resultado da busca </summary>
        private int pagina;

        #endregion

        /// <summary>
        /// Evento de carregamento da página "BuscaDfe"
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.DivMensagem.Visible = false;

            if (!this.IsPostBack)
            {
                FiltroBusca filtroBusca = (FiltroBusca)this.Session[ConstantesGlobais.FILTRO_BUSCA];

                this.DefineControles();

                if (filtroBusca != null)
                {
                    this.TxtCnpj.Text = Util.FormataCnpj(filtroBusca.Participante.Cnpj);

                    if (filtroBusca.OperacaoFiscal != null && filtroBusca.OperacaoFiscal.TipoEmissao.HasValue)
                    {
                        this.RblTipoEmissao.SelectedValue = ((byte)filtroBusca.OperacaoFiscal.TipoEmissao.Value).ToString();
                    }

                    if (filtroBusca.DocumentoFiscal != null && filtroBusca.DocumentoFiscal.JuridicamenteValido.HasValue)
                    {
                        this.RblSituacaoDocumento.SelectedValue = filtroBusca.DocumentoFiscal.JuridicamenteValido.Value ? ((byte)StatusArquivo.Valido).ToString() : ((byte)StatusArquivo.Invalido).ToString();
                    }

                    this.Session[ConstantesGlobais.FILTRO_BUSCA] = null;
                    this.ViewState[ConstantesGlobais.FILTRO_BUSCA] = filtroBusca;
                    this.ExecutaBusca();
                }
            }
        }

        #region Comandos
        
        /// <summary>
        /// Realiza a consulta dos arquivos selecionados junto a secretaria da fazenda
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void BtnConsultarSefaz_Click(object sender, EventArgs e)
        {
            List<int> arquivosSelecionados = (List<int>)this.ObtemArquivosSelecionados();
            List<int> arquivosComProblema;

            this.DivMensagem.Visible = true;
            this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_ERRO;

            if (arquivosSelecionados.Count <= 0)
            {
                this.LblMensagem.Text = MENSAGEM_ERRO_NENHUM_ARQUIVO_SELECIONADO;
                return;
            }

            try
            {
                arquivosComProblema = (List<int>)GerenciamentoArquivo.ConsultaSefaz(arquivosSelecionados);

                if (arquivosComProblema.Count <= 0)
                {
                    this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_SUCESSO;
                    this.LblMensagem.Text = MENSAGEM_SUCESSO_CONSULTA_SEFAZ;
                }
                else
                {
                    string arquivos = string.Empty;

                    foreach (int arquivo in arquivosComProblema)
                    {
                        arquivos += string.IsNullOrEmpty(arquivos) ? arquivo.ToString() : "," + arquivo.ToString();
                    }

                    this.LblMensagem.Text = string.Format(MENSAGEM_ERRO_CONSULTA_SEFAZ_ARQUIVO_PROBLEMA, arquivos);
                }
            }
            catch (Exception ex)
            {
                Log.Info(ex.InnerException == null ? ex.Message : ex.InnerException.ToString());
                this.LblMensagem.Text = MENSAGEM_ERRO_CONSULTA_SEFAZ;
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

            if (arquivosSelecionados == null || arquivosSelecionados.Count <= 0)
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
                Arquivo arquivo = (Arquivo)e.Item.DataItem;
                HiddenField hfIdResultadoBusca = (HiddenField)e.Item.FindControl("HfIdResultadoBusca");
                HtmlGenericControl spnValido = (HtmlGenericControl)e.Item.FindControl("SpnValido");
                HtmlGenericControl spnInvalido = (HtmlGenericControl)e.Item.FindControl("SpnInvalido");
                LinkButton btnPropriedadesArquivo = (LinkButton)e.Item.FindControl("BtnPropriedadesArquivo");
                Label lblDataEmissao = (Label)e.Item.FindControl("LblDataEmissao");
                Label lblDataRecepcao = (Label)e.Item.FindControl("LblDataRecepcao");
                Label lblNumero = (Label)e.Item.FindControl("LblNumero");
                Label lblSerie = (Label)e.Item.FindControl("LblSerie");
                Label lblValorTotal = (Label)e.Item.FindControl("LblValorTotal");
                Label lblEmissor = (Label)e.Item.FindControl("LblEmissor");
                Label lblDestinatario = (Label)e.Item.FindControl("LblDestinatario");

                hfIdResultadoBusca.Value = arquivo.IdArquivo.ToString();

                spnValido.Visible = arquivo.StatusArquivo.HasValue ? arquivo.StatusArquivo == StatusArquivo.Valido : false;
                spnInvalido.Visible = arquivo.StatusArquivo.HasValue ? arquivo.StatusArquivo == StatusArquivo.Invalido : false;
                btnPropriedadesArquivo.CommandArgument = arquivo.IdArquivo.ToString();
                lblDataEmissao.Text = arquivo.DataEmissao.HasValue ? arquivo.DataEmissao.Value.ToString("dd/MM/yyyy") : string.Empty;
                lblDataRecepcao.Text = arquivo.DataUpload.HasValue ? arquivo.DataUpload.Value.ToString("dd/MM/yyyy") : string.Empty;
                lblNumero.Text = arquivo.Numero.ToString();
                lblSerie.Text = arquivo.Serie.ToString();
                lblValorTotal.Text = arquivo.ValorTotal.HasValue ? arquivo.ValorTotal.Value.ToString("F2") : string.Empty;
                lblEmissor.Text = GerenciamentoEmpresa.MontaTextoParticipante(arquivo.Emitente);
                lblDestinatario.Text = GerenciamentoEmpresa.MontaTextoParticipante(arquivo.Destinatario);
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
                    LinkButton btnUltimaPagina  = (LinkButton)e.Item.FindControl("BtnUltimaPagina");
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
        /// Executa o download do arquivo
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void BtnPropriedadeDownload_Click(object sender, EventArgs e)
        {
            this.DivMensagem.Visible = true;
            this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_ERRO;

            try
            {
                int idArquivo = Convert.ToInt32(((LinkButton)sender).CommandArgument);
                string link = GerenciamentoArquivo.ObtemLinkParaDownload(new List<int>() { idArquivo });
                
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

            this.DefineControleUnidadeFederativaEmissor(unidadesFederativas);
            this.DefineControleUnidadeFederativaDestinatario(unidadesFederativas);
        }

        /// <summary>
        /// Define os valores do controle de unidade federativa do emissor
        /// </summary>
        /// <param name="unidadesFederativas">Lista de unidades federativas</param>
        private void DefineControleUnidadeFederativaEmissor(Dictionary<int, string> unidadesFederativas)
        {
            this.DdUnidadeFederativaEmissor.DataValueField = "Key";
            this.DdUnidadeFederativaEmissor.DataTextField = "Value";
            this.DdUnidadeFederativaEmissor.DataSource = unidadesFederativas.OrderBy(u => u.Value);
            this.DdUnidadeFederativaEmissor.DataBind();
            this.DdUnidadeFederativaEmissor.SelectedValue = "0";
        }

        /// <summary>
        /// Define os valores do controle de unidade federativa do destinatário
        /// </summary>
        /// <param name="unidadesFederativas">Lista de unidades federativas</param>
        private void DefineControleUnidadeFederativaDestinatario(Dictionary<int, string> unidadesFederativas)
        {
            this.DdUnidadeFederativaDestinatario.DataValueField = "Key";
            this.DdUnidadeFederativaDestinatario.DataTextField = "Value";
            this.DdUnidadeFederativaDestinatario.DataSource = unidadesFederativas.OrderBy(u => u.Value);
            this.DdUnidadeFederativaDestinatario.DataBind();
            this.DdUnidadeFederativaDestinatario.SelectedValue = "0";
        }

        /// <summary>
        /// Executa a busca de arquivos, baseado nos filtros preenchidos
        /// </summary>
        /// <param name="pagina">Número da página que conterá os resultados da busca</param>
        private void ExecutaBusca(int pagina = 1)
        {
            FiltroBusca filtro = this.ViewState[ConstantesGlobais.FILTRO_BUSCA] != null ? (FiltroBusca)this.ViewState[ConstantesGlobais.FILTRO_BUSCA] : new FiltroBusca();
            int inicio = ((pagina - 1) * RESULTADOS_POR_PAGINA) + 1;
            ResultadoBusca resultado = GerenciamentoArquivo.BuscaArquivosDfe(filtro, inicio, RESULTADOS_POR_PAGINA);
            List<Arquivo> arquivos = (resultado != null && resultado.Resultados != null) ? (List<Arquivo>)resultado.Resultados : new List<Arquivo>();
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
                this.RptResultadoBusca.DataSource = arquivos.OrderByDescending(a => a.DataEmissao).ToList();
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
        private FiltroBusca MontaFiltroBuscaSimples()
        {
            FiltroBusca filtro = new FiltroBusca();
            var recepcionado = Util.MontaDataIntervaloOpcao(this.DdRecepcionado.SelectedValue);

            if (!string.IsNullOrEmpty(this.TxtBusca.Text))
            {
                filtro.TextoLivre = this.TxtBusca.Text;
            }

            filtro.Participante = new Participante()
            {
                Cnpj = Util.ObtemSomenteDigitos(this.TxtCnpj.Text),
                Ie = Util.ObtemSomenteDigitos(this.TxtInscricaoEstadual.Text)
            };

            if (this.RblTipoEmissao.SelectedItem != null)
            {
                filtro.OperacaoFiscal = new OperacaoFiscal()
                {
                    TipoEmissao = (TipoEmissao)Convert.ToByte(this.RblTipoEmissao.SelectedValue)
                };
            }

            if (this.RblSituacaoDocumento.SelectedItem != null)
            {
                filtro.DocumentoFiscal = new DocumentoFiscal()
                {
                    JuridicamenteValido = (StatusArquivo)Convert.ToByte(this.RblSituacaoDocumento.SelectedValue) == StatusArquivo.Valido
                };
            }

            filtro.Recepcionado = recepcionado != null ? (DataIntervalo)recepcionado : null;
 
            this.ViewState[ConstantesGlobais.FILTRO_BUSCA] = filtro;

            return filtro;
        }

        /// <summary>
        /// Obtem os valores digitados nos campos da pesquisa e monta um objeto FiltroBusca
        /// </summary>
        /// <returns>Objeto filtro da busca</returns>
        private FiltroBusca MontaFiltroBuscaAvancada()
        {
            FiltroBusca filtro = new FiltroBusca();
            var numeroDocumento = Util.MontaValorIntervalo(this.TxtNumeroDocumentoInicio.Text, this.TxtNumeroDocumentoFim.Text);
            var serieDocumento = Util.MontaValorIntervalo(this.TxtSerieDocumentoInicio.Text, this.TxtSerieDocumentoFim.Text);
            var valorDocumento = Util.MontaValorIntervalo(this.TxtValorDocumentoInicio.Text, this.TxtValorDocumentoFim.Text);
            var dataEmissao = Util.MontaDataIntervalo(this.TxtDataEmissaoInicio.Text, this.TxtDataEmissaoFim.Text);
            var dataEntradaSaida = Util.MontaDataIntervalo(this.TxtDataEntradaSaidaInicio.Text, this.TxtDataEntradaSaidaFim.Text);
            var recepcionado = Util.MontaDataIntervalo(this.TxtDocumentoRecepcionadoInicio.Text, this.TxtDocumentoRecepcionadoFim.Text);
            var ultimaConsultaSefaz = Util.MontaDataIntervalo(this.TxtDataUltimaConsultaInicio.Text, this.TxtDataUltimaConsultaFim.Text);

            filtro.OperacaoFiscal = new OperacaoFiscal()
            {
                ChaveAcesso = this.TxtChaveAcesso.Text,
                FinalidadeOperacao = this.RblFinalidadeOperacao.SelectedValue != null ? this.RblFinalidadeOperacao.SelectedValue : null,
                Numero = numeroDocumento != null ? (ValorIntervalo)numeroDocumento : null,
                Serie = serieDocumento != null ? (ValorIntervalo)serieDocumento : null,
                ValorTotal = valorDocumento != null ? (ValorIntervalo)valorDocumento : null,
                DataEmissao = dataEmissao != null ? (DataIntervalo)dataEmissao : null,
                DataSaidaEntrada = dataEntradaSaida != null ? (DataIntervalo)dataEntradaSaida : null,
                NaturezaOperacao = this.TxtNaturezaOperacao.Text,
            };

            if (this.RblTipoOperacaoAvancada.SelectedItem != null && string.IsNullOrEmpty(this.RblTipoOperacaoAvancada.SelectedValue))
            {
                filtro.OperacaoFiscal.TipoOperacao = (TipoOperacaoDfe)Convert.ToByte(this.RblTipoOperacaoAvancada.SelectedValue);
            }

            if (this.RblTipoEmissaoAvancada.SelectedItem != null && string.IsNullOrEmpty(this.RblTipoEmissaoAvancada.SelectedValue))
            {
                filtro.OperacaoFiscal.TipoEmissao = (TipoEmissao)Convert.ToByte(this.RblTipoEmissaoAvancada.SelectedValue);
            }

            filtro.Emitente = new Participante()
            {
                Cnpj = Util.ObtemSomenteDigitos(this.TxtCnpjEmissor.Text),
                RazaoSocial = this.TxtRazaoSocialEmissor.Text,
                Ie = Util.ObtemSomenteDigitos(this.TxtInscricaoEstadualEmissor.Text),
                Uf = this.DdUnidadeFederativaEmissor.SelectedItem != null ? this.DdUnidadeFederativaEmissor.SelectedItem.Text : null
            };

            filtro.Destinatario = new Participante()
            {
                Cnpj = Util.ObtemSomenteDigitos(this.TxtCnpjDestinatario.Text),
                RazaoSocial = this.TxtRazaoSocialDestinatario.Text,
                Ie = Util.ObtemSomenteDigitos(this.TxtInscricaoEstadualDestinatario.Text),
                Uf = this.DdUnidadeFederativaDestinatario.SelectedItem != null ? this.DdUnidadeFederativaDestinatario.SelectedItem.Text : null
            };

            filtro.DocumentoFiscal = new DocumentoFiscal();

            filtro.DocumentoFiscal.TemCartaCorrecao = this.TvSituacaoDocumentoSefaz.Nodes[0].ChildNodes[0].Checked;

            filtro.DocumentoFiscal.CodigoStatusSefaz = this.ObtemStatusSefazSelecionados();

            if (this.TvIntegridadeXml.Nodes[0].Checked ^ this.TvIntegridadeXml.Nodes[1].Checked)
            {
                filtro.DocumentoFiscal.JuridicamenteValido = this.TvIntegridadeXml.Nodes[1].Checked;
            }

            if (this.TvIntegridadeXml.Nodes[0].ChildNodes[0].Checked)
            {
                filtro.DocumentoFiscal.ProtocoloAutorizacaoPresente = false;
            }

            if (this.TvIntegridadeXml.Nodes[0].ChildNodes[1].Checked)
            {
                filtro.DocumentoFiscal.AssinaturaValida = false;
            }

            filtro.Produto = new ProdutoServico()
            {
                CodigoInterno = this.TxtCodigoProdutoServico.Text,
                Descricao = this.TxtDescricaoProdutoServico.Text,
                Ncm = this.TxtCodigoNcmNbs.Text,
                Ean = this.TxtCodigoEan.Text,
                Nfci = this.TxtControleFci.Text,
                Cfop = this.TxtCfop.Text
            };

            filtro.Recepcionado = recepcionado != null ? (DataIntervalo)recepcionado : null;

            filtro.UltimaConsultaSefaz = ultimaConsultaSefaz != null ? (DataIntervalo)ultimaConsultaSefaz : null;

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
            string linkDownloadArquivo = GerenciamentoArquivo.ObtemLinkParaDownload(new List<int>() { propriedade.IdArquivo });

            UsuarioApi usuario = usuarios.Exists(u => u.Id == propriedade.IdUsuarioUpload) ? usuarios.First(u => u.Id == propriedade.IdUsuarioUpload) : null;
            
            this.TxtPropriedadeChaveAcesso.Text = propriedade.DetalheDfe.Chave;
            this.TxtPropriedadeTipoDocumento.Text = Dominio.ValorTextoDe(propriedade.TipoArquivo);
            this.TxtPropriedadeNumeroDocumento.Text = propriedade.DetalheDfe.Numero.ToString();
            this.TxtPropriedadeDataEmissao.Text = propriedade.DetalheDfe.DataEmissao.Value.ToString("dd/MM/yyyy");

            this.TxtPropriedadeEmissorCnpj.Text = Util.FormataCnpj(propriedade.DetalheDfe.EmitenteCnpj);
            this.TxtPropriedadeEmissorInscricaoEstadual.Text = propriedade.DetalheDfe.EmitenteIe;
            this.TxtPropriedadeEmissorRazaoSocial.Text = propriedade.DetalheDfe.EmitenteRazaoSocial;

            this.TxtPropriedadeDestinatarioCnpj.Text = Util.FormataCnpj(propriedade.DetalheDfe.DestinatarioCnpj);
            this.TxtPropriedadeDestinatarioInscricaoEstadual.Text = propriedade.DetalheDfe.DestinatarioIe;
            this.TxtPropriedadeDestinatarioRazaoSocial.Text = propriedade.DetalheDfe.DestinatarioRazaoSocial;

            this.TxtPropriedadeOrigem.Text = usuario != null ? usuario.Nome : USUARIO_NAO_ENCONTRADO;
            this.TxtPropriedadeDataRecepcao.Text = propriedade.DataUpload.ToString("dd/MM/yyyy HH:mm");
            this.TxtPropriedadeSituacaoSefaz.Text = propriedade.DetalheDfe.StatusSefaz;
            this.TxtPropriedadeDataSituacaoSefaz.Text = propriedade.DetalheDfe.DataStatusSefaz.HasValue ? propriedade.DetalheDfe.DataStatusSefaz.Value.ToString("dd/MM/yyyy HH:mm") : string.Empty;
            this.TxtPropriedadeIntegridadeArquivoProtocoloAutorizacao.Text = (propriedade.DetalheDfe.EProtocoloPresente.HasValue && propriedade.DetalheDfe.EProtocoloPresente.Value) ? TEXTO_COM_PROTOCOLO_AUTORIZACAO : TEXTO_SEM_PROTOCOLO_AUTORIZACAO;
            this.TxtPropriedadeIntegridadeArquivoAssinaturaDigital.Text = (propriedade.DetalheDfe.EAssinaturaValida.HasValue && propriedade.DetalheDfe.EAssinaturaValida.Value) ? TEXTO_ASSINATURA_DIGITAL_VALIDA : TEXTO_ASSINATURA_DIGITAL_INVALIDA;
            this.TxtPropriedadeDataIntegridadeArquivoAssinaturaDigital.Text = propriedade.DetalheDfe.DataStatusSefaz.HasValue ? propriedade.DetalheDfe.DataStatusSefaz.Value.ToString("dd/MM/yyyy HH:mm") : string.Empty;
            this.TxtPropriedadeDataIntegridadeArquivoProtocoloAutorizacao.Text = propriedade.DetalheDfe.DataStatusSefaz.HasValue ? propriedade.DetalheDfe.DataStatusSefaz.Value.ToString("dd/MM/yyyy HH:mm") : string.Empty;
            ////TxtPropriedadeDataManifestacaoDestinatario.Text = ;
            ////TxtPropriedadeUsuarioUltimoAcesso.Text = ;
            ////TxtPropriedadeDataUltimoAcesso.Text = ;

            this.BtnPropriedadeDownload.CommandArgument = propriedade.IdArquivo.ToString();
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

        /// <summary>
        /// Percorre o controle de situações do documento na Sefaz e retorna os valores selecionados
        /// </summary>
        /// <returns>Lista dos valores selecionados</returns>
        private List<string> ObtemStatusSefazSelecionados()
        {
            List<string> listaSelecionados = null;
            string selecionados = string.Empty;

            foreach (TreeNode no in this.TvSituacaoDocumentoSefaz.Nodes)
            {
                foreach (TreeNode noFilho in no.ChildNodes)
                {
                    if (noFilho.Checked && !string.IsNullOrEmpty(noFilho.Value))
                    {
                        selecionados += string.IsNullOrEmpty(selecionados) ? noFilho.Value : "," + noFilho.Value;
                    }
                }

                if (no.Checked)
                {
                    selecionados += string.IsNullOrEmpty(selecionados) ? no.Value : "," + no.Value;
                }
            }

            if (!string.IsNullOrEmpty(selecionados))
            {
                listaSelecionados = selecionados.Split(',').ToList();
            }

            return listaSelecionados;
        }
    }
}