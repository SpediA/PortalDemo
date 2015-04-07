////-----------------------------------------------------------------------
//// <copyright file="StatusModuloData.cs" company="SpediA">
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
    /// Classe modelo de data do status de um módulo
    /// </summary>
    public class StatusModuloData
    {
        /// <summary>
        /// Obtém ou define a data em que o status foi consultado
        /// </summary>
        [JsonProperty("data")]
        public virtual DateTime DataConsulta { get; set; }

        /// <summary>
        /// Obtém ou define o status do módulo
        /// </summary>
        [JsonProperty("status")]
        public virtual byte StatusModulo { get; set; }

        /// <summary>
        /// Obtém ou define uma descrição complementar sobre o status do módulo
        /// </summary>
        [JsonProperty("info")]
        public virtual string Descricao { get; set; }
    }
}
