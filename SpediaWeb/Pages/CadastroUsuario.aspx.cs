////-----------------------------------------------------------------------
//// <copyright file="CadastroUsuario.aspx.cs" company="SpediA">
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
    /// Classe responsável por toda a interface do cadastro de usuários
    /// </summary>
    public partial class CadastroUsuario : System.Web.UI.Page
    {
        #region Constantes

        /// <summary> Representa uma mensagem de sucesso </summary>
        private const string MENSAGEM_SUCESSO_INCLUSAO = "Usuário incluído com sucesso!";

        /// <summary> Representa uma mensagem de sucesso </summary>
        private const string MENSAGEM_SUCESSO_EDICAO = "Usuário modificado com sucesso!";

        /// <summary> Representa uma mensagem de sucesso </summary>
        private const string MENSAGEM_SUCESSO_EXCLUSAO = "Usuário excluído com sucesso!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_INCLUSAO = "Erro ao tentar incluir um novo usuário!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_INCLUSAO_API = "Erro ao tentar incluir um novo usuário na API!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_EDICAO = "Erro ao tentar modificar o usuário!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_EDICAO_API = "Erro ao tentar modificar o usuário na API!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_EXCLUSAO = "Erro ao tentar excluir usuário!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_EXCLUSAO_API = "Erro ao tentar excluir usuário na API!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_ENVIO_EMAIL = "O usuário foi criado, porém ocorreu um erro ao tentar enviar o e-mail de boas vindas!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_NOME_VAZIO = "O nome do usuário não pode ser vazio!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_NOME_DUPLICADO = "Já existe um usuário cadastrado com esse nome!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_EMAIL_DUPLICADO = "Já existe um usuário com esse endereço de e-mail cadastrado!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_EMAIL_INVALIDO = "O endereço de e-mail está em um formato inválido!";

        #endregion

        /// <summary> Objeto da biblioteca log4net para registro de log da aplicação </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(CadastroUsuario));

        /// <summary>
        /// Evento de carregamento da página "CadastroUsuario"
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.DivMensagem.Visible = false;

            if (!this.IsPostBack)
            {
                this.DefineControlePerfil();
                this.ReiniciaControles();
            }
        }

        /// <summary>
        /// Evento de construção de cada linha de usuário na tabela
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void RptUsuario_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e != null && (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem))
            {
                Usuario usuario = (Usuario)e.Item.DataItem;
                Label lblNome = (Label)e.Item.FindControl("LblNome");
                Label lblEmail = (Label)e.Item.FindControl("LblEmail");
                Label lblPerfil = (Label)e.Item.FindControl("LblPerfil");
                LinkButton btnEditar = (LinkButton)e.Item.FindControl("BtnEditar");
                LinkButton btnExcluir = (LinkButton)e.Item.FindControl("BtnExcluir");

                lblNome.Text = usuario.Nome;
                lblEmail.Text = usuario.Email;
                lblPerfil.Text = usuario.Perfil > 0  ? Dominio.ValorTextoDe(usuario.Perfil) : string.Empty;
                btnEditar.CommandArgument = usuario.Id.ToString();
                btnExcluir.CommandArgument = usuario.Id.ToString();
            }
        }

        /// <summary>
        /// Evento de execução dos comandos advindos de uma das linhas do controle "RptUsuario"
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void RptUsuario_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            int idUsuario;

            if (e != null)
            {
                idUsuario = Convert.ToInt32(e.CommandArgument);

                switch (e.CommandName)
                {
                    case "Editar": this.EditaUsuario(idUsuario);
                        break;
                    case "Excluir": this.ExcluiUsuario(idUsuario);
                        break;
                }
            }
        }

        /// <summary>
        /// Evento que salva o novo usuário ou as modificações do usuário existente
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void BtnSalvar_Click(object sender, EventArgs e)
        {
            Usuario usuario;
            string argumento = ((LinkButton)sender).CommandArgument;
            int idUsuario;
            string novaSenha;

            if (string.IsNullOrEmpty(argumento)) 
            {
                try
                {
                    novaSenha = Autenticacao.GeraSenhaRandomica();
                    usuario = this.CriaUsuario(novaSenha);

                    if (usuario != null)
                    {
                        GerenciamentoEmail.EnviaEmailNovoUsuario(usuario.Email, usuario.Nome, usuario.Email, novaSenha);
                    }
                }
                catch (Exception ex)
                {
                    Log.Info(ex.InnerException == null ? ex.Message : ex.InnerException.ToString());
                    this.DivMensagem.Visible = true;
                    this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_ERRO;
                    this.LblMensagem.Text = MENSAGEM_ERRO_ENVIO_EMAIL;
                }
            }
            else
            {
                idUsuario = Convert.ToInt32(argumento);

                this.AtualizaUsuario(idUsuario);
            }
        }

        /// <summary>
        /// Carrega as possíveis opções de perfil no Domínio e monta o controle
        /// </summary>
        private void DefineControlePerfil()
        {
            Dictionary<int, string> perfis = (Dictionary<int, string>)Dominio.ObtemListaEnum(typeof(PerfilUsuario));

            perfis.Add(0, string.Empty);

            this.DdPerfil.DataValueField = "Key";
            this.DdPerfil.DataTextField = "Value";
            this.DdPerfil.DataSource = perfis.OrderBy(p => p.Value);
            this.DdPerfil.DataBind();
        }

        /// <summary>
        /// Cria um novo usuário na API e no banco de dados local
        /// </summary>
        /// <param name="novaSenha">Senha gerada para o novo usuário</param>
        /// <returns>Usuário criado</returns>
        private Usuario CriaUsuario(string novaSenha)
        {
            Usuario usuarioLogado = (Usuario)this.Session[ConstantesGlobais.USUARIO];
            Usuario usuario = new Usuario();
            UsuarioApi usuarioApi;
            string novoNome = this.TxtNome.Text;
            string novoEmail = this.TxtEmail.Text;

            this.DivMensagem.Visible = true;
            this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_ERRO;

            if (!this.ValidaPreenchimento(usuario.Id, usuario.IdApi, novoNome, novoEmail))
            {
                return null;
            }

            usuario.Nome = novoNome;
            usuario.Email = novoEmail;
            usuario.Perfil = (PerfilUsuario)Convert.ToByte(this.DdPerfil.SelectedValue);
            usuario.UsuarioSpedia = usuarioLogado.UsuarioSpedia;
            usuario.SenhaSpedia = usuarioLogado.SenhaSpedia;

            usuarioApi = new UsuarioApi() { Nome = usuario.Nome, Email = usuario.Email };

            usuario.IdApi = GerenciamentoUsuario.CriaUsuarioApi(usuarioApi);

            if (!usuario.IdApi.HasValue)
            {
                this.LblMensagem.Text = MENSAGEM_ERRO_INCLUSAO_API;
                return null;
            }


            if (GerenciamentoUsuario.CriaUsuario(usuario, novaSenha) == null)
            {
                this.LblMensagem.Text = MENSAGEM_ERRO_INCLUSAO;
                return null;
            }

            this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_SUCESSO;
            this.LblMensagem.Text = MENSAGEM_SUCESSO_INCLUSAO;

            this.ReiniciaControles();

            return usuario;
        }
        
        /// <summary>
        /// Define os controles de edição com os valores do usuário
        /// </summary>
        /// <param name="idUsuario">Identificador do usuário</param>
        private void EditaUsuario(int idUsuario)
        {
            Usuario usuario = GerenciamentoUsuario.CarregaUsuario(idUsuario);

            this.TxtNome.Text = usuario.Nome;
            this.TxtEmail.Text = usuario.Email;
            this.DdPerfil.SelectedValue = ((byte)usuario.Perfil).ToString();

            this.BtnSalvar.CommandArgument = usuario.Id.ToString();
        }

        /// <summary>
        /// Atualiza as informações do usuário na API e no banco de dados local
        /// </summary>
        /// <param name="idUsuario">Identificador do usuário</param>
        private void AtualizaUsuario(int idUsuario)
        {
            Usuario usuarioLogado = (Usuario)this.Session[ConstantesGlobais.USUARIO];
            Usuario usuario = GerenciamentoUsuario.CarregaUsuario(idUsuario);

            string novoNome = this.TxtNome.Text;
            string novoEmail = this.TxtEmail.Text;

            this.DivMensagem.Visible = true;
            this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_ERRO;

            if (!this.ValidaPreenchimento(usuario.Id, usuario.IdApi, novoNome, novoEmail))
            {
                return;
            }

            usuario.Id = usuario.Id;
            usuario.IdApi = usuario.IdApi;
            usuario.Nome = novoNome;
            usuario.Email = novoEmail;
            usuario.Perfil = (PerfilUsuario)Convert.ToByte(this.DdPerfil.SelectedValue);
            usuario.UsuarioSpedia = usuarioLogado.UsuarioSpedia;
            usuario.SenhaSpedia = usuarioLogado.SenhaSpedia;

            if (!GerenciamentoUsuario.EditaUsuarioApi(usuario))
            {
                this.LblMensagem.Text = MENSAGEM_ERRO_EDICAO_API;
                return;
            }

            if (!GerenciamentoUsuario.AtualizaUsuario(usuario))
            {
                this.LblMensagem.Text = MENSAGEM_ERRO_EDICAO;
                return;
            }

            this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_SUCESSO;
            this.LblMensagem.Text = MENSAGEM_SUCESSO_EDICAO;

            this.ReiniciaControles();
        }

        /// <summary>
        /// Exclui o usuário da API e depois o exclui do banco de dados local
        /// </summary>
        /// <param name="idUsuario">Identificador do usuário</param>
        private void ExcluiUsuario(int idUsuario)
        {
            Usuario usuario = GerenciamentoUsuario.CarregaUsuario(idUsuario);

            this.DivMensagem.Visible = true;
            this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_ERRO;
         
            if (!GerenciamentoUsuario.ExcluiUsuarioApi(usuario.IdApi.Value))
            {
                this.LblMensagem.Text = MENSAGEM_ERRO_EXCLUSAO_API;
                return;
            }

            if (!GerenciamentoUsuario.ExcluiUsuario(usuario))
            {
                this.LblMensagem.Text = MENSAGEM_ERRO_EXCLUSAO;

                return;
            }

            this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_SUCESSO;
            this.LblMensagem.Text = MENSAGEM_SUCESSO_EXCLUSAO;

            this.ReiniciaControles();
        }

        /// <summary>
        /// Valida se existe alguma inconsistência nos campos preenchidos pelo usuário
        /// </summary>
        /// <param name="idUsuario">Identificador do usuário a ser validado</param>
        /// <param name="idApi">Identificador, na API, do usuário a ser validado</param>
        /// <param name="nome">Nome do usuário a ser validado</param>
        /// <param name="email">E-mail do usuário a ser validado</param>
        /// <returns>Indica se está tudo válido ou não</returns>
        private bool ValidaPreenchimento(int? idUsuario, int? idApi, string nome, string email)
        {
            List<UsuarioApi> usuariosApi = (List<UsuarioApi>)GerenciamentoUsuario.CarregaUsuariosApi();
            Usuario lUsuario = GerenciamentoUsuario.CarregaUsuario(nome);

            if (string.IsNullOrEmpty(nome))
            {
                this.LblMensagem.Text = MENSAGEM_ERRO_NOME_VAZIO;
                return false;
            }

            if (!Util.EEmailValido(email))
            {
                this.LblMensagem.Text = MENSAGEM_ERRO_EMAIL_INVALIDO;
                return false;
            }

            if ((lUsuario != null && lUsuario.Id != idUsuario) || usuariosApi.Any(u => u.Nome == nome && u.Id != idApi))
            {
                this.LblMensagem.Text = MENSAGEM_ERRO_NOME_DUPLICADO;
                return false;
            }

            lUsuario = GerenciamentoUsuario.CarregaUsuarioPorEmail(email);

            if ((lUsuario != null && lUsuario.Id != idUsuario) || usuariosApi.Any(u => u.Email == email && u.Id != idApi))
            {
                this.LblMensagem.Text = MENSAGEM_ERRO_EMAIL_DUPLICADO;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Reinicia todos os controles de usuário para o seu estado inicial
        /// </summary>
        private void ReiniciaControles()
        {
            Usuario usuarioLogado = (Usuario)this.Session[ConstantesGlobais.USUARIO];
            List<Usuario> usuarios = (List<Usuario>)GerenciamentoUsuario.CarregaUsuarios(usuarioLogado.UsuarioSpedia);

            this.TxtNome.Text = string.Empty;
            this.TxtEmail.Text = string.Empty;
            this.DdPerfil.SelectedValue = byte.MinValue.ToString();
            this.BtnSalvar.CommandArgument = string.Empty;

            this.RptUsuario.DataSource = usuarios.OrderBy(u => u.Nome);
            this.RptUsuario.DataBind();
        }
    }
}