////-----------------------------------------------------------------------
//// <copyright file="Logon.aspx.cs" company="SpediA">
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
    /// Classe responsável pela página de acesso ao portal
    /// </summary>
    public partial class Logon : System.Web.UI.Page
    {
        #region Constantes

        /// <summary> Representa uma mensagem de sucesso </summary>
        private const string MENSAGEM_ERRO_CREDENCIAIS = "Usuário ou senha inválida!";

        /// <summary> Representa uma mensagem de sucesso </summary>
        private const string MENSAGEM_SUCESSO_RECUPERACAO_SENHA = "Senha enviada com sucesso para o e-mail {0}!";

        /// <summary> Representa uma mensagem de sucesso </summary>
        private const string MENSAGEM_ERRO_USUARIO_INEXISTENTE = "Não existe usuário cadastrado com o e-mail {0}!";

        /// <summary> Representa uma mensagem de sucesso </summary>
        private const string MENSAGEM_ERRO_ENVIO_EMAIL = "Erro ao tentar enviar e-mail para {0}!";

        #endregion

        /// <summary> Objeto da biblioteca log4net para registro de log da aplicação </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(Logon));

        /// <summary>
        /// Evento de carregamento da página "Logon"
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.TxtLogin.Focus();
            }
        }

        /// <summary>
        /// Realiza a verificação do usuário e a senha
        /// Se estiverem corretos, permite o acesso a área interna do sistema, caso contrário exibe mensagem de erro
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void BtnEntrar_Click(object sender, EventArgs e)
        {
            Usuario usuario;
            string login = this.TxtLogin.Text;
            string senha = this.TxtSenha.Text;

            usuario = GerenciamentoUsuario.CarregaUsuario(login, senha);

            if (usuario != null)
            {
                this.Session[ConstantesGlobais.USUARIO] = usuario;
                Response.Redirect("~/Pages/MapaProducao");
            }
            else
            {
                this.DivMensagem.Visible = true;
                this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_ERRO;
                this.LblMensagem.Text = MENSAGEM_ERRO_CREDENCIAIS;
            }
        }

        /// <summary>
        /// Envia uma nova senha para o usuário que esqueceu sua senha.
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void BtnEnviarSenha_Click(object sender, EventArgs e)
        {
            Usuario usuario = GerenciamentoUsuario.CarregaUsuarioPorEmail(this.TxtEmail.Text);
            string novaSenha = Autenticacao.GeraSenhaRandomica();

            this.DivMensagem.Visible = true;
            this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_ERRO;

            if (usuario == null)
            {
                this.LblMensagem.Text = string.Format(MENSAGEM_ERRO_USUARIO_INEXISTENTE, this.TxtEmail.Text);
                return;
            }

            try
            {
                usuario.Senha = Autenticacao.ObtemSHA1Hash(novaSenha);

                GerenciamentoUsuario.AtualizaUsuario(usuario);

                GerenciamentoEmail.EnviaEmailRecuperacaoSenha(usuario.Email, usuario.Nome, usuario.Email, novaSenha);
            }
            catch (Exception ex)
            {
                Log.Info(ex.InnerException == null ? ex.Message : ex.InnerException.ToString());
                this.LblMensagem.Text = string.Format(MENSAGEM_ERRO_ENVIO_EMAIL, usuario.Email);
                return;
            }

            this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_SUCESSO;
            this.LblMensagem.Text = string.Format(MENSAGEM_SUCESSO_RECUPERACAO_SENHA, usuario.Email);
        }
    }
}