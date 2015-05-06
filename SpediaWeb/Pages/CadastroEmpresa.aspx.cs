////-----------------------------------------------------------------------
//// <copyright file="CadastroEmpresa.aspx.cs" company="SpediA">
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
    /// Classe responsável por toda a interface do cadastro de empresas
    /// </summary>
    public partial class CadastroEmpresa : System.Web.UI.Page
    {
        #region Constantes

        /// <summary> Representa uma mensagem de sucesso </summary>
        private const string MENSAGEM_SUCESSO_INCLUSAO = "Empresa incluída com sucesso!";

        /// <summary> Representa uma mensagem de sucesso </summary>
        private const string MENSAGEM_SUCESSO_EDICAO = "Empresa modificada com sucesso!";

        /// <summary> Representa uma mensagem de sucesso </summary>
        private const string MENSAGEM_SUCESSO_EXCLUSAO = "Empresa excluída com sucesso!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_INCLUSAO = "Erro ao tentar incluir uma nova empresa!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_EDICAO = "Erro ao tentar modificar a empresa!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_EXCLUSAO = "Erro ao tentar excluir empresa!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_NOME_VAZIO = "A razão social da empresa não pode ser vazia!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_CNPJ_VAZIO = "O CNPJ da empresa não pode ser vazio!";

        /// <summary> Representa uma mensagem de erro </summary>
        private const string MENSAGEM_ERRO_CNPJ_DUPLICADO = "Já existe uma empresa cadastrada com esse CNPJ!";

        #endregion

        #region Atributos da classe

        /// <summary> Objeto da biblioteca log4net para registro de log da aplicação </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(CadastroEmpresa));

        /// <summary> Lista de empresas carregadas </summary>
        private List<Empresa> empresas;

        #endregion

        /// <summary>
        /// Evento de carregamento da página "CadastroEmpresa"
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.DivMensagem.Visible = false;

            if (!this.IsPostBack)
            {
                this.ReiniciaControles();
                this.DefineControleUnidadeFederativa();
            }
        }

        /// <summary>
        /// Evento de construção de cada linha de empresa na tabela
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void RptEmpresa_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e != null && (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem))
            {
                Empresa empresa = (Empresa)e.Item.DataItem;
                Label lblRazaoSocial = (Label)e.Item.FindControl("LblRazaoSocial");
                Label lblCnpj = (Label)e.Item.FindControl("LblCnpj");
                Label lblInscricaoEstadual = (Label)e.Item.FindControl("LblInscricaoEstadual");
                Label lblUnidadeFederativa = (Label)e.Item.FindControl("LblUnidadeFederativa");
                LinkButton btnEditar = (LinkButton)e.Item.FindControl("BtnEditar");
                LinkButton btnExcluir = (LinkButton)e.Item.FindControl("BtnExcluir");

                lblRazaoSocial.Text = empresa.RazaoSocial;
                lblCnpj.Text = Util.FormataCnpj(empresa.Cnpj);
                lblInscricaoEstadual.Text = empresa.InscricaoEstadual;
                lblUnidadeFederativa.Text = empresa.UnidadeFederativa;
                btnEditar.CommandArgument = empresa.Id.ToString();
                btnExcluir.CommandArgument = empresa.Id.ToString();
            }
        }

        /// <summary>
        /// Evento de execução dos comandos advindos de uma das linhas do controle "RptEmpresa"
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void RptEmpresa_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            int idEmpresa;

            if (e != null)
            {
                idEmpresa = Convert.ToInt32(e.CommandArgument);

                switch (e.CommandName)
                {
                    case "Editar": this.EditaEmpresa(idEmpresa);
                        break;
                    case "Excluir": this.ExcluiEmpresa(idEmpresa);
                        break;
                }
            }
        }

        /// <summary>
        /// Evento que salva a nova empresa ou as modificações da empresa existente
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void BtnSalvar_Click(object sender, EventArgs e)
        {
            string argument = ((LinkButton)sender).CommandArgument;
            int idEmpresa;

            if (string.IsNullOrEmpty(argument))
            {
                this.CriaEmpresa();
            }
            else
            {
                idEmpresa = Convert.ToInt32(argument);

                this.AtualizaEmpresa(idEmpresa);
            }
        }

        /// <summary>
        /// Carrega as possíveis opções de UF no Domínio e monta o controle
        /// </summary>
        private void DefineControleUnidadeFederativa()
        {
            Dictionary<int, string> unidadeFederativa = (Dictionary<int, string>)Dominio.ObtemListaEnum(typeof(UnidadeFederativa));

            unidadeFederativa.Add(0, string.Empty);

            this.DdUnidadeFederativa.DataValueField = "Key";
            this.DdUnidadeFederativa.DataTextField = "Value";
            this.DdUnidadeFederativa.DataSource = unidadeFederativa.OrderBy(u => u.Value);
            this.DdUnidadeFederativa.DataBind();
        }

        /// <summary>
        /// Cria uma nova empresa
        /// </summary>
        private void CriaEmpresa()
        {
            Empresa empresa = new Empresa();

            empresa.RazaoSocial = this.TxtRazaoSocial.Text;
            empresa.Cnpj = Util.ObtemSomenteDigitos(this.TxtCnpj.Text);
            empresa.InscricaoEstadual = Util.ObtemSomenteDigitos(this.TxtInscricaoEstadual.Text);
            empresa.Municipio = this.TxtMunicipio.Text;
            empresa.UnidadeFederativa = this.DdUnidadeFederativa.SelectedItem.Text;

            this.DivMensagem.Visible = true;
            this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_ERRO;

            if (!this.ValidaPreenchimento(empresa))
            {
                return;
            }

            empresa = GerenciamentoEmpresa.CriaEmpresa(empresa);

            if (empresa == null)
            {
                this.LblMensagem.Text = MENSAGEM_ERRO_INCLUSAO;
                return;
            }

            this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_SUCESSO;
            this.LblMensagem.Text = MENSAGEM_SUCESSO_INCLUSAO;

            this.ReiniciaControles();
        }

        /// <summary>
        /// Define os controles de edição com os valores da empresa
        /// </summary>
        /// <param name="idEmpresa">Identificador de empresa</param>
        private void EditaEmpresa(int idEmpresa)
        {
            this.empresas = (List<Empresa>)this.ViewState[ConstantesGlobais.EMPRESAS];
            Empresa empresa = this.empresas.First(e => e.Id == idEmpresa);

            this.TxtRazaoSocial.Text = empresa.RazaoSocial;
            this.TxtCnpj.Text = Util.FormataCnpj(empresa.Cnpj);
            this.TxtInscricaoEstadual.Text = empresa.InscricaoEstadual;
            this.TxtMunicipio.Text = empresa.Municipio;
            this.DdUnidadeFederativa.SelectedValue = string.IsNullOrEmpty(empresa.UnidadeFederativa) ? 
                byte.MinValue.ToString() : ((byte)(UnidadeFederativa)Dominio.ValorEnumDe(empresa.UnidadeFederativa, typeof(UnidadeFederativa))).ToString();

            this.BtnSalvar.CommandArgument = empresa.Id.ToString();
        }

        /// <summary>
        /// Atualiza as informações da empresa
        /// </summary>
        /// <param name="idEmpresa">Identificador da empresa</param>
        private void AtualizaEmpresa(int idEmpresa)
        {
            this.empresas = (List<Empresa>)this.ViewState[ConstantesGlobais.EMPRESAS];
            Empresa empresa = this.empresas.First(e => e.Id == idEmpresa);

            empresa.RazaoSocial = this.TxtRazaoSocial.Text;
            empresa.Cnpj = Util.ObtemSomenteDigitos(this.TxtCnpj.Text);
            empresa.InscricaoEstadual = Util.ObtemSomenteDigitos(this.TxtInscricaoEstadual.Text);
            empresa.Municipio = this.TxtMunicipio.Text;
            empresa.UnidadeFederativa = this.DdUnidadeFederativa.SelectedItem.Text;

            this.DivMensagem.Visible = true;
            this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_ERRO;

            if (!this.ValidaPreenchimento(empresa))
            {
                return;
            }

            if (!GerenciamentoEmpresa.EditaEmpresa(empresa))
            {
                this.LblMensagem.Text = MENSAGEM_ERRO_EDICAO;
                return;
            }

            this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_SUCESSO;
            this.LblMensagem.Text = MENSAGEM_SUCESSO_EDICAO;

            this.ReiniciaControles();
        }

        /// <summary>
        /// Exclui a empresa
        /// </summary>
        /// <param name="idEmpresa">Identificador da empresa</param>
        private void ExcluiEmpresa(int idEmpresa)
        {
            this.empresas = (List<Empresa>)this.ViewState[ConstantesGlobais.EMPRESAS];
            Empresa empresa = this.empresas.First(e => e.Id == idEmpresa);

            this.DivMensagem.Visible = true;
            this.DivMensagem.Attributes["class"] = ConstantesGlobais.CLASSE_MENSAGEM_ERRO;

            if (!GerenciamentoEmpresa.ExcluiEmpresa(empresa.Id.Value))
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
        /// <param name="empresa">Empresa a ser validada</param>
        /// <returns>Indica se está tudo válido ou não</returns>
        private bool ValidaPreenchimento(Empresa empresa)
        {
            List<Empresa> empresas = (List<Empresa>)GerenciamentoEmpresa.CarregaEmpresas();

            if (string.IsNullOrEmpty(empresa.RazaoSocial))
            {
                this.LblMensagem.Text = MENSAGEM_ERRO_NOME_VAZIO;
                return false;
            }

            if (string.IsNullOrEmpty(empresa.Cnpj))
            {
                this.LblMensagem.Text = MENSAGEM_ERRO_CNPJ_VAZIO;
                return false;
            }

            if (empresas.Any(e => e.Cnpj == empresa.Cnpj && (!empresa.Id.HasValue || e.Id != empresa.Id)))
            {
                this.LblMensagem.Text = MENSAGEM_ERRO_CNPJ_DUPLICADO;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Reinicia todos os controles de empresa para o seu estado inicial
        /// </summary>
        private void ReiniciaControles()
        {
            this.empresas = (List<Empresa>)GerenciamentoEmpresa.CarregaEmpresas();
            this.ViewState[ConstantesGlobais.EMPRESAS] = this.empresas;

            this.TxtRazaoSocial.Text = string.Empty;
            this.TxtCnpj.Text = string.Empty;
            this.TxtInscricaoEstadual.Text = string.Empty;
            this.TxtMunicipio.Text = string.Empty;
            this.DdUnidadeFederativa.SelectedValue = byte.MinValue.ToString();
            this.BtnSalvar.CommandArgument = string.Empty;

            this.RptEmpresa.DataSource = this.empresas.OrderBy(e => e.RazaoSocial);
            this.RptEmpresa.DataBind();
        }
    }
}