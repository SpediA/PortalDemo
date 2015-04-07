////-----------------------------------------------------------------------
//// <copyright file="DetalheDfe.cs" company="SpediA">
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
    using Newtonsoft.Json;
    using NHibernate;

    /// <summary>
    /// Classe modelo dos detalhes de Dfe
    /// </summary>
    public class DetalheDfe
    {
        /// <summary>
        /// Obtém ou define a chave da Nfe
        /// </summary>
        [JsonProperty("chavenfe")]
        public virtual string Chave { get; set; }

        /// <summary>
        /// Obtém ou define a série da Nfe
        /// </summary>
        public virtual int Serie { get; set; }

        /// <summary>
        /// Obtém ou define o número da Nfe
        /// </summary>
        public virtual int Numero { get; set; }
        
        /// <summary>
        /// Obtém ou define se a Dfe possui assinatura válida
        /// </summary>
        [JsonProperty("IsAssinaturaValida")]
        public virtual bool? EAssinaturaValida { get; set; }

        /// <summary>
        /// Obtém ou define se a Dfe está cancelada
        /// </summary>
        [JsonProperty("IsCancelada")]
        public virtual bool? ECancelada { get; set; }

        /// <summary>
        /// Obtém ou define se a Dfe possui protocolo presente
        /// </summary>
        [JsonProperty("IsProtocoloPresente")]
        public virtual bool? EProtocoloPresente { get; set; }

        /// <summary>
        /// Obtém ou define se a Dfe possui carta correção
        /// </summary>
        public virtual bool? TemCartaCorrecao { get; set; }

        /// <summary>
        /// Obtém ou define a data de emissão da nfe
        /// </summary>
        public virtual DateTime? DataEmissao { get; set; }

        /// <summary>
        /// Obtém ou define a data de saída/entrada da nfe
        /// </summary>
        public virtual DateTime? DataSaidaEntrada { get; set; }

        /// <summary>
        /// Obtém ou define a data em que foi obitido o status junto a Sefaz
        /// </summary>
        public virtual DateTime? DataStatusSefaz { get; set; }

        /// <summary>
        /// Obtém ou define o CNPJ do destinatário
        /// </summary>
        [JsonProperty("destinatariocnpj")]
        public virtual string DestinatarioCnpj { get; set; }

        /// <summary>
        /// Obtém ou define a inscricao estadual do destinatário
        /// </summary>
        [JsonProperty("destinatarioie")]
        public virtual string DestinatarioIe { get; set; }

        /// <summary>
        /// Obtém ou define a razão social do destinatário
        /// </summary>
        [JsonProperty("destinatariorazao")]
        public virtual string DestinatarioRazaoSocial { get; set; }

        /// <summary>
        /// Obtém ou define a unidade federativa do destinatário
        /// </summary>
        [JsonProperty("destinatariouf")]
        public virtual string DestinatarioUf { get; set; }

        /// <summary>
        /// Obtém ou define o CNPJ do emitente
        /// </summary>
        [JsonProperty("emitentecnpj")]
        public virtual string EmitenteCnpj { get; set; }

        /// <summary>
        /// Obtém ou define a inscricao estadual do emitente
        /// </summary>
        [JsonProperty("emitenteie")]
        public virtual string EmitenteIe { get; set; }

        /// <summary>
        /// Obtém ou define a razão social do emitente
        /// </summary>
        [JsonProperty("emitenterazao")]
        public virtual string EmitenteRazaoSocial { get; set; }

        /// <summary>
        /// Obtém ou define a unidade federativa do emitente
        /// </summary>
        [JsonProperty("emitenteuf")]
        public virtual string EmitenteUf { get; set; }

        /// <summary>
        /// Obtém ou define a finalidade da operação
        /// </summary>
        public virtual string FinalidadeOperacao { get; set; }

        /// <summary>
        /// Obtém ou define a natureza da operação
        /// </summary>
        public virtual string NaturezaOperacao { get; set; }

        /// <summary>
        /// Obtém ou define o valor total da nota fiscal
        /// </summary>
        [JsonProperty("valortotalnf")]
        public virtual decimal ValorTotal { get; set; }

        /// <summary>
        /// Obtém ou define o status da Dfe junto a Sefaz
        /// </summary>
        public virtual string StatusSefaz { get; set; }
    }
}
