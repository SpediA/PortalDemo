////-----------------------------------------------------------------------
//// <copyright file="ItemProducaoValores.cs" company="SpediA">
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
    /// Classe modelo de valores de um item de um mapa de produção
    /// </summary>
    public class ItemProducaoValores
    {
        /// <summary>
        /// Obtém ou define a quantidade de arquivos válidos de um item de mapa de produção
        /// </summary>
        [JsonProperty("qtdvalido")]
        public virtual int QuantidadeValido { get; set; }

        /// <summary>
        /// Obtém ou define a quantidade de arquivos inválidos de um item de mapa de produção
        /// </summary>
        [JsonProperty("qtdinvalido")]
        public virtual int QuantidadeInvalido { get; set; }
    }
}
