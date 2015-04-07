////-----------------------------------------------------------------------
//// <copyright file="Arquivo.cs" company="SpediA">
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
    /// Classe modelo de arquivo
    /// </summary>
    public class Arquivo
    {
        /// <summary>
        /// Obtém ou define o identificador do arquivo
        /// </summary>
        public virtual int IdArquivo { get; set; }

        /// <summary>
        /// Obtém ou define a natureza da operação
        /// </summary>
        public virtual string NaturezaOperacao { get; set; }

        /// <summary>
        /// Obtém ou define a finalidade da operação
        /// </summary>
        public virtual FinalidadeOperacaoDfe? FinalidadeOperacao { get; set; }

        /// <summary>
        /// Obtém ou define a data em que o arquivo foi emitido
        /// </summary>
        public virtual DateTime? DataEmissao { get; set; }

        /// <summary>
        /// Obtém ou define a data em que o arquivo foi importado na SpediA
        /// </summary>
        public virtual DateTime? DataUpload { get; set; }

        /// <summary>
        /// Obtém ou define o número do documento
        /// </summary>
        public virtual int? Numero { get; set; }

        /// <summary>
        /// Obtém ou define a série do documento
        /// </summary>
        public virtual int? Serie { get; set; }

        /// <summary>
        /// Obtém ou define a chave de acesso
        /// </summary>
        public virtual string ChaveAcesso { get; set; }

        /// <summary>
        /// Obtém ou define o emitente do arquivo
        /// </summary>
        public virtual Participante Emitente { get; set; }

        /// <summary>
        /// Obtém ou define o destinatário do arquivo
        /// </summary>
        public virtual Participante Destinatario { get; set; }

        /// <summary>
        /// Obtém ou define o valor total contido no arquivo
        /// </summary>
        [JsonProperty("valortotalnf")]
        public virtual decimal? ValorTotal { get; set; }

        /// <summary>
        /// Obtém ou define um valor que indica se a situação do arquivo é válida ou inválida
        /// </summary>
        [JsonProperty("statusdocumento")]
        public virtual StatusArquivo? StatusArquivo { get; set; }
    }
}
