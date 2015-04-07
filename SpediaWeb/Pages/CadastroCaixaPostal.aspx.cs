////-----------------------------------------------------------------------
//// <copyright file="CadastroCaixaPostal.aspx.cs" company="SpediA">
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
    using SpediaLibrary.Util;
    using SpediaWeb.Presentation.Common;

    /// <summary>
    /// Classe responsável por toda a interface do cadastro de caixas postais
    /// </summary>
    public partial class CadastroCaixaPostal : System.Web.UI.Page
    {
        #region Constantes

        /// <summary> Representa uma mensagem de sucesso </summary>
        private const string MENSAGEM_SUCESSO_INCLUSAO = "Caixa postal incluída com sucesso!";

        /// <summary> Representa uma mensagem de sucesso </summary>
        private const string MENSAGEM_SUCESSO_EDICAO = "Caixa postal modificada com sucesso!";

        /// <summary> Representa uma mensagem de sucesso </summary>
        private const string MENSAGEM_SUCESSO_EXCLUSAO = "Caixa postal excluída com sucesso!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_INCLUSAO = "Erro ao tentar incluir uma nova caixa postal!";

        /// <summary> Representa uma mensagem de sucesso </summary>
        private const string MENSAGEM_ERRO_EDICAO = "Erro ao tentar modificar a caixa postal!";

        /// <summary> Representa uma mensagem de sucesso </summary>
        private const string MENSAGEM_ERRO_EXCLUSAO = "Erro ao tentar excluir a caixa postal!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_NOME_VAZIO = "O nome da caixa postal não pode ser vazio!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_EMAIL_INVALIDO = "O endereço de e-mail está em um formato inválido!";

        #endregion

        /// <summary> Objeto da biblioteca log4net para registro de log da aplicação </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(CadastroCaixaPostal));

        /// <summary>
        /// Evento de carregamento da página "CadastroCaixaPostal"
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.DivMensagem.Visible = false;
                this.ReiniciaControles();
            }
        }

        /// <summary>
        /// Evento de construção de cada linha de caixa postal na tabela
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void RptCaixaPostal_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e != null && (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem))
            {
                CaixaPostal caixaPostal = (CaixaPostal)e.Item.DataItem;
                Label lblNomeCaixaPostal = (Label)e.Item.FindControl("LblNomeCaixaPostal");
                Label lblEnderecoEmail = (Label)e.Item.FindControl("LblEnderecoEmail");
                Label lblEnderecoServidor = (Label)e.Item.FindControl("LblEnderecoServidor");
                LinkButton btnEditar = (LinkButton)e.Item.FindControl("BtnEditar");
                LinkButton btnExcluir = (LinkButton)e.Item.FindControl("BtnExcluir");

                lblNomeCaixaPostal.Text = caixaPostal.NomeCaixaPostal;
                lblEnderecoEmail.Text = caixaPostal.EnderecoEmail;
                lblEnderecoServidor.Text = caixaPostal.EnderecoServidor;
                btnEditar.CommandArgument = caixaPostal.Id.ToString();
                btnExcluir.CommandArgument = caixaPostal.Id.ToString();
            }
        }

        /// <summary>
        /// Evento de execução dos comandos advindos de uma das linhas do controle "RptCaixaPostal"
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void RptCaixaPostal_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            int idCaixaPostal;

            if (e != null)
            {
                idCaixaPostal = Convert.ToInt32(e.CommandArgument);

                switch (e.CommandName)
                {
                    case "Editar": this.EditaCaixaPostal(idCaixaPostal);
                        break;
                    case "Excluir": this.ExcluiCaixaPostal(idCaixaPostal);
                        break;
                }
            }
        }

        /// <summary>
        /// Evento que salva a nova caixa postal ou as modificações das caixas postais existente
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void BtnSalvar_Click(object sender, EventArgs e)
        {
            string argumento = ((LinkButton)sender).CommandArgument;
            int? idCaixaPostal;

            if (string.IsNullOrEmpty(argumento))
            {
                idCaixaPostal = null;
            }
            else
            {
                idCaixaPostal = Convert.ToInt32(argumento);
            }

            this.AtualizaCaixaPostal(idCaixaPostal);
        }

        /// <summary>
        /// Define os controles de edição com os valores da caixa postal
        /// </summary>
        /// <param name="idCaixaPostal">Identificador da caixa postal</param>
        private void EditaCaixaPostal(int idCaixaPostal)
        {
            List<CaixaPostal> caixasPostais = (List<CaixaPostal>)this.ViewState[ConstantesGlobais.CAIXAS_POSTAIS];
            CaixaPostal caixaPostal = caixasPostais.First(c => c.Id == idCaixaPostal);

            this.TxtNomeCaixaPostal.Text = caixaPostal.NomeCaixaPostal;
            this.TxtEnderecoEmail.Text = caixaPostal.EnderecoEmail;
            this.TxtEnderecoServidor.Text = caixaPostal.EnderecoServidor;
            this.TxtPorta.Text = caixaPostal.Porta;
            this.BtnSalvar.CommandArgument = caixaPostal.Id.ToString();
        }

        /// <summary>
        /// Atualiza as informações da caixa postal
        /// </summary>
        /// <param name="idCaixaPostal">Identificador da caixa postal</param>
        private void AtualizaCaixaPostal(int? idCaixaPostal)
        {
            List<CaixaPostal> caixasPostais = (List<CaixaPostal>)this.ViewState[ConstantesGlobais.CAIXAS_POSTAIS];
            CaixaPostal caixaPostal = idCaixaPostal.HasValue ? caixasPostais.First(c => c.Id == idCaixaPostal) : new CaixaPostal();

            caixaPostal.NomeCaixaPostal = this.TxtNomeCaixaPostal.Text;
            caixaPostal.EnderecoEmail = this.TxtEnderecoEmail.Text;
            caixaPostal.EnderecoServidor = this.TxtEnderecoServidor.Text;
            caixaPostal.Porta = this.TxtPorta.Text;
            caixaPostal.Senha = this.TxtSenha.Text;

            this.DivMensagem.Visible = true;
            this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_ERRO;

            if (string.IsNullOrEmpty(caixaPostal.NomeCaixaPostal))
            {
                this.LblMensagem.Text = MENSAGEM_ERRO_NOME_VAZIO;
                return;
            }

            if (string.IsNullOrEmpty(caixaPostal.EnderecoEmail) || !Util.EEmailValido(caixaPostal.EnderecoEmail))
            {
                this.LblMensagem.Text = MENSAGEM_ERRO_EMAIL_INVALIDO;
                return;
            }

            if (!GerenciamentoCaixaPostal.AtualizaCaixaPostal(caixaPostal))
            {
                this.LblMensagem.Text = caixaPostal.Id.HasValue ? MENSAGEM_ERRO_EDICAO : MENSAGEM_ERRO_INCLUSAO;
                return;
            }

            this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_SUCESSO;
            this.LblMensagem.Text = caixaPostal.Id.HasValue ? MENSAGEM_SUCESSO_EDICAO : MENSAGEM_SUCESSO_INCLUSAO;

            this.ReiniciaControles();
        }

        /// <summary>
        /// Exclui a caixa postal
        /// </summary>
        /// <param name="idCaixaPostal">Identificador da caixa postal</param>
        private void ExcluiCaixaPostal(int idCaixaPostal)
        {
            this.DivMensagem.Visible = true;
            this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_ERRO;

            if (!GerenciamentoCaixaPostal.ExcluiCaixaPostal(idCaixaPostal))
            {
                this.LblMensagem.Text = MENSAGEM_ERRO_EXCLUSAO;
                return;
            }

            this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_SUCESSO;
            this.LblMensagem.Text = MENSAGEM_SUCESSO_EXCLUSAO;

            this.ReiniciaControles();
        }

        /// <summary>
        /// Reinicia todos os controles de caixa postal para o seu estado inicial
        /// </summary>
        private void ReiniciaControles()
        {
            List<CaixaPostal> caixasPostais = (List<CaixaPostal>)GerenciamentoCaixaPostal.CarregaCaixasPostais();
            this.ViewState[ConstantesGlobais.CAIXAS_POSTAIS] = caixasPostais;

            this.TxtNomeCaixaPostal.Text = string.Empty;
            this.TxtEnderecoEmail.Text = string.Empty;
            this.TxtEnderecoServidor.Text = string.Empty;
            this.TxtPorta.Text = string.Empty;
            this.BtnSalvar.CommandArgument = string.Empty;

            this.RptCaixaPostal.DataSource = caixasPostais.OrderBy(u => u.NomeCaixaPostal);
            this.RptCaixaPostal.DataBind();
        }
    }
}