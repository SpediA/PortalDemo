////-----------------------------------------------------------------------
//// <copyright file="OperacaoFiscal.cs" company="SpediA">
//// Copyright [2014] [SPEDIA Soluções Tecnológicas Ltda]
//// Licenciado sob Licença Apache, Versão 2.0 (a "Licença"). Você não pode usar este arquivo exceto em conformidade com a Licença.
//// Você pode obter uma cópia da Licença em:
//// http://www.apache.org/licenses/LICENSE-2.0
//// Ao menos que seja exigido por lei aplicável ou com autorização por escrito, todo software distribuído sob a Licença é distribuído "COMO ESTÁ",
//// SEM GARANTIAS OU CONDIÇÕES DE NENHUMA ESPÉCIE, expressas ou implícitas.
//// Veja a Licença no idioma específico que estabelece as permissões e limitações sob a Licença.
//// </copyright>
////-----------------------------------------------------------------------
namespace SpediaLibrary.Transfer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using SpediaLibrary.Business;

    /// <summary>
    /// Classe modelo de operação fiscal
    /// </summary>
    [Serializable]
    public class OperacaoFiscal
    {
        /// <summary>
        /// Obtém ou define o valor da chave de acesso
        /// </summary>
        public virtual string ChaveAcesso { get; set; }

        /// <summary>
        /// Obtém ou define um intervalo de datas de emissão
        /// </summary>
        public virtual DataIntervalo DataEmissao { get; set; }

        /// <summary>
        /// Obtém ou define um intervalo de datas de entrada/saída
        /// </summary>
        public virtual DataIntervalo DataSaidaEntrada { get; set; }

        /// <summary>
        /// Obtém ou define a finalidade da operação
        /// </summary>
        public virtual string FinalidadeOperacao { get; set; }

        /// <summary>
        /// Obtém ou define a natureza da operação
        /// </summary>
        public virtual string NaturezaOperacao { get; set; }

        /// <summary>
        /// Obtém ou define um intervalo de números de operação fiscal
        /// </summary>
        public virtual ValorIntervalo Numero { get; set; }

        /// <summary>
        /// Obtém ou define um intervalo de séries
        /// </summary>
        public virtual ValorIntervalo Serie { get; set; }

        /// <summary>
        /// Obtém ou define o tipo da operação de DFe
        /// </summary>
        public virtual TipoOperacaoDfe? TipoOperacao { get; set; }

        /// <summary>
        /// Obtém ou define o tipo de emissão
        /// </summary>
        [JsonProperty("propriaterceiros")]
        public virtual TipoEmissao? TipoEmissao { get; set; }

        /// <summary>
        /// Obtém ou define um intervalo de valores
        /// </summary>
        [JsonProperty("valortotalnf")]
        public virtual ValorIntervalo ValorTotal { get; set; }
    }
}
