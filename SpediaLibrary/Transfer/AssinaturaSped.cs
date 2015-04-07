////-----------------------------------------------------------------------
//// <copyright file="AssinaturaSped.cs" company="SpediA">
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
    /// Classe modelo de solicitação de assinatura de SPED
    /// </summary>
    public class AssinaturaSped
    {
        /// <summary>
        /// Obtém ou define a lista de identificadores de SPED
        /// </summary>
        [JsonProperty("spedids")]
        public virtual IList<int> Speds { get; set; }

        /// <summary>
        /// Obtém ou define o código do certificado que será usado para solicitar a assinatura
        /// </summary>
        public virtual string Certificado { get; set; }

        /// <summary>
        /// Obtém ou define a senha do certificado que será usado para solicitar a assinatura
        /// </summary>
        public virtual string SenhaCertificado { get; set; }
    }
}
