////-----------------------------------------------------------------------
//// <copyright file="ExtratoUso.aspx.cs" company="SpediA">
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
    using SpediaLibrary.Transfer;

    /// <summary>
    /// Classe responsável por toda a interface do extrato de uso
    /// </summary>
    public partial class ExtratoUso : System.Web.UI.Page
    {
        #region Constantes

        /// <summary> Representa o valor limite de "conta normal" da barra de progresso </summary>
        private const int LIMITE_CONTA_NORMAL = 60;

        /// <summary> Representa o valor limite de "conta em alerta" da barra de progresso </summary>
        private const int LIMITE_CONTA_ALERTA = 90;

        /// <summary> Representa o valor limite de "conta a exceder" da barra de progresso </summary>
        private const int LIMITE_CONTA_EXCEDER = 100;

        #endregion

        /// <summary> Objeto da biblioteca log4net para registro de log da aplicação </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(ExtratoUso));

        /// <summary>
        /// Evento de carregamento da página "ExtratoUso"
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Evento de construção de cada linha do extrato de uso
        /// </summary>
        /// <param name="sender">Objeto que disparou esse evento</param>
        /// <param name="e">Contém os argumentos fornecidos nesse evento</param>
        protected void RptExtrato_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
        }

        /// <summary>
        /// Define os valores dos controles que exibem um resumo do que foi consumido da conta, como nome do plano contratado, 
        /// barra de progresso, quantidade de documentos enviados
        /// </summary>
        /// <param name="planoContratado">Nome do plano contratado</param>
        /// <param name="consumido">Quantidade de arquivos enviados</param>
        /// <param name="limiteConta">Limite máximo de arquivos que podem ser enviados, de acordo com o plano contratado</param>
        private void DefineResumoConsumo(string planoContratado, int consumido, int limiteConta)
        {
            int porcentagem = (int)Math.Round((decimal)(consumido / limiteConta));

            this.LblPlanoContratado.Text = planoContratado;

            this.DivContaNormal.Attributes["style"] = string.Format("width: {0}%", porcentagem > LIMITE_CONTA_NORMAL ? LIMITE_CONTA_NORMAL : porcentagem);
            this.DivContaAlerta.Attributes["style"] = string.Format("width: {0}%", porcentagem > LIMITE_CONTA_ALERTA ? LIMITE_CONTA_ALERTA : porcentagem - LIMITE_CONTA_NORMAL);
            this.DivContaExceder.Attributes["style"] = string.Format("width: {0}%", porcentagem > LIMITE_CONTA_EXCEDER ? LIMITE_CONTA_EXCEDER : porcentagem - LIMITE_CONTA_ALERTA);
            this.LblValoresConsumo.Text = consumido + "/" + limiteConta;

            this.LblQuantidadeDocumentosEnviados.Text = consumido.ToString();
        }
    }
}