////-----------------------------------------------------------------------
//// <copyright file="DetalheSped.cs" company="SpediA">
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
    using SpediaLibrary.Business;

    /// <summary>
    /// Classe modelo dos detalhes de SPED
    /// </summary>
    public class DetalheSped
    {
        /// <summary>
        /// Obtém ou define a finalidade do arquivo
        /// </summary>
        public virtual FinalidadeOperacaoSped? FinalidadeArquivo { get; set; }

        /// <summary>
        /// Obtém ou define o tipo da escrituração
        /// </summary>
        public virtual TipoEscrituracao TipoEscrituracao { get; set; }

        /// <summary>
        /// Obtém ou define se o arquivo foi transmitido
        /// </summary>
        [JsonProperty("tipotransmissao")]
        public virtual bool? FoiTransmitido { get; set; }

        /// <summary>
        /// Obtém ou define a situação do processamento do PVA
        /// </summary>
        public virtual StatusPva StatusPva { get; set; }

        /// <summary>
        /// Obtém ou define a situação do SPED
        /// </summary>
        public virtual StatusSped StatusSped { get; set; }

        /// <summary>
        /// Obtém ou define a data inicial da competência
        /// </summary>
        public virtual DateTime? CompetenciaInicial { get; set; }

        /// <summary>
        /// Obtém ou define a data final da competência
        /// </summary>
        public virtual DateTime? CompetenciaFinal { get; set; }

        /// <summary>
        /// Obtém ou define a data em que o arquivo foi assinado
        /// </summary>
        public virtual DateTime? DataAssinatura { get; set; }

        /// <summary>
        /// Obtém ou define a data em que o arquivo foi entrega à Sefaz
        /// </summary>
        public virtual DateTime? DataEntregaSefaz { get; set; }

        /// <summary>
        /// Obtém ou define a data em que o arquivo foi processado pelo PVA
        /// </summary>
        public virtual DateTime? DataProcessamentoPva { get; set; }

        /// <summary>
        /// Obtém ou define a data em que o arquivo foi transmitido à Sefaz
        /// </summary>
        public virtual DateTime? DataTransmissaoSefaz { get; set; }

        /// <summary>
        /// Obtém ou define o CNPJ da entidade
        /// </summary>
        public virtual string EntidadeCnpj { get; set; }

        /// <summary>
        /// Obtém ou define a inscricao estadual da entidade
        /// </summary>
        public virtual string EntidadeIe { get; set; }

        /// <summary>
        /// Obtém ou define a razão social da entidade
        /// </summary>
        [JsonProperty("entidaderazao")]
        public virtual string EntidadeRazaoSocial { get; set; }

        /// <summary>
        /// Obtém ou define a unidade federativa da entidade
        /// </summary>
        public virtual string EntidadeUf { get; set; }
    }
}
