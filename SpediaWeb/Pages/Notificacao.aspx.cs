////-----------------------------------------------------------------------
//// <copyright file="Notificacao.aspx.cs" company="SpediA">
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
    /// Classe responsável por toda a interface de notificacões
    /// </summary>
    public partial class Notificacao : System.Web.UI.Page
    {
        /// <summary> Objeto da biblioteca log4net para registro de log da aplicação </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(Notificacao));

        /// <summary> Lista de usuários da SpediA </summary>
        private List<UsuarioApi> usuarios;

        /// <summary>
        /// Evento de carregamento da página "Notificacao"
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.ObtemNotificacoes();
            }
        }

        /// <summary>
        /// Evento de construção de cada linha das notificações
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void RptNotificacao_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e != null && (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem))
            {
                ItemNotificacao notificacao = (ItemNotificacao)e.Item.DataItem;
                UsuarioApi usuario = this.usuarios.Any(u => u.Id == notificacao.IdUsuario) ? this.usuarios.First(u => u.Id == notificacao.IdUsuario) : null;
                Label lblDescricao = (Label)e.Item.FindControl("LblDescricao");
                Label lblUsuario = (Label)e.Item.FindControl("LblUsuario");
                Label lblHoraNotificacao = (Label)e.Item.FindControl("LblHoraNotificacao");
                Label lblDataNotificacao = (Label)e.Item.FindControl("LblDataNotificacao");

                lblDescricao.Text = notificacao.TextoNotificacao;
                lblUsuario.Text = usuario != null ? usuario.Nome : string.Empty;
                lblHoraNotificacao.Text = notificacao.DataNotificacao.ToString("HH:mm");
                lblDataNotificacao.Text = notificacao.DataNotificacao.ToString("dd/MM/yyyy");
            }
        }

        /// <summary>
        /// Obtém as notificações da conta de uso
        /// </summary>
        private void ObtemNotificacoes()
        {
            Usuario usuario = (Usuario)this.Session[ConstantesGlobais.USUARIO];
            ControleNotificacao controle = GerenciamentoNotificacao.CarregaControleNotificacao(usuario.Id.Value);
            NotificacaoConta notificacaoConta = GerenciamentoNotificacao.ObtemNotificacoes(controle.UltimaNotificacao);

            if (notificacaoConta != null && notificacaoConta.Notificacoes.Count > 0)
            {
                this.usuarios = (List<UsuarioApi>)GerenciamentoUsuario.CarregaUsuariosApi();

                this.RptNotificacao.DataSource = notificacaoConta.Notificacoes.OrderByDescending(n => n.DataNotificacao);
                this.RptNotificacao.DataBind();

                controle.UltimaNotificacao = notificacaoConta.ContinuaEm;

                GerenciamentoNotificacao.AtualizaControleNotificacao(controle);
            }
        }
    }
}