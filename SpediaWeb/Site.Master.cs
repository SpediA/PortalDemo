////-----------------------------------------------------------------------
//// <copyright file="Site.Master.cs" company="SpediA">
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
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using log4net;
    using SpediaLibrary.Business;
    using SpediaLibrary.Transfer;
    using SpediaWeb.Pages;
    using SpediaWeb.Presentation.Common;

    /// <summary>
    /// Classe responsável pela página template do portal
    /// </summary>
    public partial class SiteMaster : MasterPage
    {
        #region Constantes

        /// <summary> Representa o nome de uma página </summary>
        private const string PAGINA_BUSCA = "Busca";

        /// <summary> Representa o nome de uma página </summary>
        private const string PAGINA_MAPA_PRODUCAO = "MapaProducao";

        /// <summary> Representa uma mensagem de sucesso </summary>
        private const string MENSAGEM_SUCESSO_UPLOAD = "O upload foi realizado com sucesso!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_UPLOAD = "Erro ao tentar fazer o upload do arquivo!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_FALTA_ARQUIVO_UPLOAD = "É necessário escolher um arquivo para realizar o upload!";

        #endregion

        /// <summary> Objeto da biblioteca log4net para registro de log da aplicação </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(SiteMaster));

        /// <summary>
        /// Objeto que representa um usuário
        /// </summary>
        private Usuario usuario;

        /// <summary>
        /// Evento de inicialização da página Site.Master (página template)
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            this.usuario = (Usuario)Session[ConstantesGlobais.USUARIO];

            if (this.usuario == null)
            {
                Response.Redirect("~/Pages/Logon");
            }
        }

        /// <summary>
        /// Evento de carregamento da página Site.Master (página template)
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string page = Path.GetFileName(Request.Url.AbsolutePath);

            this.VerificaNotificacao();

            this.LblUsuario.Text = this.usuario.Email;
            this.DivMensagemUpload.Visible = false;

            this.BtnVisualizarNFe.Visible = page == PAGINA_BUSCA;
            this.BtnMapaProducao.Visible = (page == PAGINA_BUSCA) || (page == PAGINA_MAPA_PRODUCAO);
            this.BtnBusca.Visible = (page == PAGINA_BUSCA) || (page == PAGINA_MAPA_PRODUCAO);

            if (this.IsPostBack && this.FuArquivo.HasFile)
            {
                this.ProcessaUpload();
            }
        }

        /// <summary>
        /// Finaliza a sessão do usuário
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void BtnSair_Click(object sender, EventArgs e)
        {
            this.Session[ConstantesGlobais.USUARIO] = null;
            Response.Redirect("Logon");
        }

        /// <summary>
        /// Realiza o upload do arquivo selecionado
        /// </summary>
        private void ProcessaUpload()
        {
            Parametro parametroUpload;
            string enderecoCompleto;
            string nomeArquivo;

            try
            {
                nomeArquivo = this.FuArquivo.FileName;

                enderecoCompleto = GerenciamentoArquivo.ObtemEnderecoArquivo(this.usuario.Id.Value, nomeArquivo);

                parametroUpload = GerenciamentoArquivo.ObtemParametrosUpload(enderecoCompleto.Split('\\').Last());

                if (parametroUpload != null && !string.IsNullOrEmpty(parametroUpload.Valor))
                {
                    this.FuArquivo.SaveAs(enderecoCompleto);

                    GerenciamentoArquivo.ExecutaUploadApi(parametroUpload.Valor, enderecoCompleto);

                    GerenciamentoArquivo.InformaUploadConcluido(parametroUpload.Id.Value);

                    this.DivMensagemUpload.Visible = true;
                    this.DivMensagemUpload.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_SUCESSO;
                    this.LblMensagemUpload.Text = MENSAGEM_SUCESSO_UPLOAD;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                this.DivMensagemUpload.Visible = true;
                this.DivMensagemUpload.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_ERRO;
                this.LblMensagemUpload.Text = MENSAGEM_ERRO_UPLOAD;
            }
        }

        /// <summary>
        /// Verifica se há novas notificações para informar no ícone de notificações
        /// </summary>
        private void VerificaNotificacao()
        {
            ControleNotificacao controle = GerenciamentoNotificacao.CarregaControleNotificacao(this.usuario.Id.Value);
            NotificacaoConta notificacaoConta = GerenciamentoNotificacao.ObtemNotificacoes(controle.UltimaNotificacao);

            if (notificacaoConta != null && notificacaoConta.Notificacoes.Count > 0)
            {
                this.LblNumeroNotificacoes.Text = notificacaoConta.Notificacoes.Count.ToString();
                this.LblNumeroNotificacoes.Visible = true;
            }
        }
    }
}