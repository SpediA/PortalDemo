////-----------------------------------------------------------------------
//// <copyright file="Empresa.cs" company="SpediA">
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

    /// <summary>
    /// Classe modelo de empresa
    /// </summary>
    [Serializable]
    public class Empresa : ModeloBase
    {
        /// <summary>
        /// Obtém ou define o CNPJ
        /// </summary>
        public virtual string Cnpj { get; set; }

        /// <summary>
        /// Obtém ou define a I.E.
        /// </summary>
        [JsonProperty("ie")]
        public virtual string InscricaoEstadual { get; set; }

        /// <summary>
        /// Obtém ou define a razão social
        /// </summary>
        public virtual string RazaoSocial { get; set; }

        /// <summary>
        /// Obtém ou define o município da empresa
        /// </summary>
        public virtual string Municipio { get; set; }

        /// <summary>
        /// Obtém ou define a U.F. do município da empresa
        /// </summary>
        [JsonProperty("uf")]
        public virtual string UnidadeFederativa { get; set; }

        /// <summary>
        /// Obtém ou define um valor que indica se é a empresa tem substituição tributária
        /// </summary>
        [JsonProperty("isst")]
        public virtual bool? TemSubstituicaoTributaria { get; set; }

        /// <summary>
        /// Obtém ou define um valor que indica se é a empresa matriz
        /// </summary>
        [JsonProperty("ismatriz")]
        public virtual bool? EMatriz { get; set; }

        /// <summary>
        /// Obtém ou define a data de abertura da empresa
        /// </summary>
        public virtual DateTime? DataAbertura { get; set; }

        /// <summary>
        /// Obtém ou define a data de encerramento da empresa
        /// </summary>
        public virtual DateTime? DataEncerramento { get; set; }
    }
}
