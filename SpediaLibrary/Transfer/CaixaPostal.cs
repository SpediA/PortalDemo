////-----------------------------------------------------------------------
//// <copyright file="CaixaPostal.cs" company="SpediA">
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
    /// Classe modelo de caixa postal
    /// </summary>
    [Serializable]
    public class CaixaPostal : ModeloBase
    {
        /// <summary>
        /// Obtém ou define o nome da caixa postal
        /// </summary>
        [JsonProperty("descricao")]
        public virtual string NomeCaixaPostal { get; set; }

        /// <summary>
        /// Obtém ou define o endereço de e-mail
        /// </summary>
        [JsonProperty("imapuser")]
        public virtual string EnderecoEmail { get; set; }

        /// <summary>
        /// Obtém ou define a senha da conta de e-mail
        /// </summary>
        [JsonProperty("imappassword")]
        public virtual string Senha { get; set; }

        /// <summary>
        /// Obtém ou define o endereço POP3
        /// </summary>
        [JsonProperty("imapserver")]
        public virtual string EnderecoServidor { get; set; }

        /// <summary>
        /// Obtém ou define a porta do endereço POP3
        /// </summary>
        [JsonProperty("imapport")]
        public virtual string Porta { get; set; }

        /// <summary>
        /// Obtém ou define quando foi a última vez que a caixa postal foi consultada
        /// </summary>
        [JsonProperty("dataultimofetch")]
        public virtual DateTime? DataUltimaConsulta { get; set; }

        /// <summary>
        /// Obtém ou define quando foi a última vez que a caixa postal foi consultada com sucesso
        /// </summary>
        [JsonProperty("dataultimofetchsucesso")]
        public virtual DateTime? DataUltimaConsultaSucesso { get; set; }
    }
}
