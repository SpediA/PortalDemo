////-----------------------------------------------------------------------
//// <copyright file="CadastroAtributoAdicional.aspx.cs" company="SpediA">
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
    /// Classe responsável por toda a interface do cadastro de atributo adicional e seus valores
    /// </summary>
    public partial class CadastroAtributoAdicional : System.Web.UI.Page
    {
        #region Constantes

        /// <summary> Representa uma mensagem de sucesso </summary>
        private const string MENSAGEM_SUCESSO_INCLUSAO = "Atributo adicional incluído com sucesso!";

        /// <summary> Representa uma mensagem de sucesso </summary>
        private const string MENSAGEM_SUCESSO_ALTERACAO = "Atributo adicional modificado com sucesso!";

        /// <summary> Representa uma mensagem de sucesso </summary>
        private const string MENSAGEM_SUCESSO_EXCLUSAO = "Atributo adicional excluído com sucesso!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_INCLUSAO = "Erro ao tentar incluir um novo atributo adicional!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_ALTERACAO = "Erro ao tentar modificar o atributo adicional!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_EXCLUSAO = "Erro ao tentar excluir o atributo adicional!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_NOME_VAZIO = "O nome do atributo não pode ser vazio!";

        #endregion

        /// <summary> Objeto da biblioteca log4net para registro de log da aplicação </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(CadastroAtributoAdicional));

        /// <summary>
        /// Evento de carregamento da página "CadastroAtributoAdicional"
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.DivMensagem.Visible = false;

            if (!this.IsPostBack)
            {
                this.DivMensagem.Visible = false;
                this.ReiniciaControles();
            }
        }

        /// <summary>
        /// Evento de construção de cada linha de atributo adicional na tabela
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void RptAtributoAdicional_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e != null && (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem))
            {
                AtributoAdicional atributo = (AtributoAdicional)e.Item.DataItem;
                Label lblNome = (Label)e.Item.FindControl("LblNome");
                LinkButton btnEditar = (LinkButton)e.Item.FindControl("btnEditar");
                LinkButton btnExcluir = (LinkButton)e.Item.FindControl("BtnExcluir");
                
                lblNome.Text = atributo.Nome;
                btnEditar.CommandArgument = atributo.Id.ToString();
                btnExcluir.CommandArgument = atributo.Id.ToString();
            }
        }

        /// <summary>
        /// Evento de execução dos comandos advindos de uma das linhas do controle "RptAtributoAdicional"
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void RptAtributoAdicional_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            int idAtributoAdicional;

            if (e != null)
            {
                idAtributoAdicional = Convert.ToInt32(e.CommandArgument);

                switch (e.CommandName)
                {
                    case "Editar": this.EditaAtributoAdicional(idAtributoAdicional);
                        break;
                    case "Excluir": this.ExcluiAtributoAdicional(idAtributoAdicional);
                        break;
                }
            }
        }

        /// <summary>
        /// Evento que salva o atributo adicional e seus valores
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void BtnSalvar_Click(object sender, EventArgs e)
        {
            string argument = ((LinkButton)sender).CommandArgument;
            int? idAtributoAdicional;

            if (string.IsNullOrEmpty(argument))
            {
                idAtributoAdicional = null;
            }
            else
            {
                idAtributoAdicional = Convert.ToInt32(argument);
            }
            
            this.AtualizaAtributoAdicional(idAtributoAdicional);
        }

        /// <summary>
        /// Define os controles de edição com os valores do atributo adicional
        /// </summary>
        /// <param name="idAtributoAdicional">Identificador do atributo adicional</param>
        private void EditaAtributoAdicional(int idAtributoAdicional)
        {
            AtributoAdicional atributo = GerenciamentoAtributoAdicional.ObtemAtributoAdicional(idAtributoAdicional);
            string valores = string.Empty;

            foreach (Parametro valor in atributo.Valores)
            {
                valores += string.IsNullOrEmpty(valores) ? valor.Valor : ConstantesGlobais.SEPARADOR_TAG + valor.Valor;
            }

            this.TxtNome.Text = atributo.Nome;
            this.TxtValores.Text = valores;
            this.BtnSalvar.CommandArgument = idAtributoAdicional.ToString();
        }

        /// <summary>
        /// Altera o atributo adiconal e os seus valores, quando necessário
        /// </summary>
        /// <param name="idAtributoAdicional">Identificador do atributo adicional</param>
        private void AtualizaAtributoAdicional(int? idAtributoAdicional)
        {
            AtributoAdicional atributoAdicional = idAtributoAdicional.HasValue ? 
                GerenciamentoAtributoAdicional.ObtemAtributoAdicional(idAtributoAdicional.Value) : new AtributoAdicional();
            List<string> valoresCarregados = atributoAdicional.Valores != null ? atributoAdicional.Valores.Select(v => v.Valor).ToList() : new List<string>();
            List<string> valoresDigitados = !string.IsNullOrEmpty(this.TxtValores.Text) ? this.TxtValores.Text.Split(ConstantesGlobais.SEPARADOR_TAG).ToList() : new List<string>();
            List<int> valoresExcluidos = atributoAdicional.Valores != null ? atributoAdicional.Valores.Select(v => v.Id.Value).ToList() : new List<int>();
            List<string> valoresAdicionados = new List<string>();
            int idValor;

            foreach (string valor in valoresDigitados)
            {
                if (valoresCarregados.Contains(valor))
                {
                    idValor = atributoAdicional.Valores.First(v => v.Valor == valor).Id.Value;
                    valoresExcluidos.Remove(idValor);
                }
                else
                {
                    valoresAdicionados.Add(valor);
                }
            }

            atributoAdicional.Nome = this.TxtNome.Text;

            this.DivMensagem.Visible = true;
            this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_ERRO;

            if (string.IsNullOrEmpty(atributoAdicional.Nome))
            {
                this.LblMensagem.Text = MENSAGEM_ERRO_NOME_VAZIO;
                return;
            }

            if (!GerenciamentoAtributoAdicional.AtualizaAtributoAdicional(atributoAdicional, valoresAdicionados, valoresExcluidos))
            {
                this.LblMensagem.Text = MENSAGEM_ERRO_ALTERACAO;
                return;
            }

            this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_SUCESSO;
            this.LblMensagem.Text = idAtributoAdicional.HasValue ? MENSAGEM_SUCESSO_ALTERACAO : MENSAGEM_SUCESSO_INCLUSAO;

            this.ReiniciaControles();
        }

        /// <summary>
        /// Exclui o atributo adiconal e todos os seus valores
        /// </summary>
        /// <param name="idAtributoAdicional">Identificador do atributo adicional</param>
        private void ExcluiAtributoAdicional(int idAtributoAdicional)
        {
            AtributoAdicional atributoAdicional = GerenciamentoAtributoAdicional.ObtemAtributoAdicional(idAtributoAdicional);
            List<int> valoresAExcluir = atributoAdicional.Valores != null ? atributoAdicional.Valores.Select(v => v.Id.Value).ToList() : new List<int>();

            this.DivMensagem.Visible = true;
            this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_ERRO;

            if (!GerenciamentoAtributoAdicional.ExcluiAtributoAdicional(idAtributoAdicional, valoresAExcluir))
            {
                this.LblMensagem.Text = MENSAGEM_ERRO_EXCLUSAO;
                return;
            }
            
            this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_SUCESSO;
            this.LblMensagem.Text = MENSAGEM_SUCESSO_EXCLUSAO;

            this.ReiniciaControles();
        }

        /// <summary>
        /// Reinicia todos os controles de atributo adicional para o seu estado inicial
        /// </summary>
        private void ReiniciaControles()
        {
            List<AtributoAdicional> atributos = (List<AtributoAdicional>)GerenciamentoAtributoAdicional.ObtemAtributosAdicionais();

            GerenciamentoAtributoAdicional.AtualizaJsonValoresAtributosAdicionais();

            this.TxtNome.Text = string.Empty;
            this.TxtValores.Text = string.Empty;
            this.BtnSalvar.CommandArgument = string.Empty;

            this.RptAtributoAdicional.DataSource = atributos.OrderBy(u => u.Nome);
            this.RptAtributoAdicional.DataBind();
        }
    }
}