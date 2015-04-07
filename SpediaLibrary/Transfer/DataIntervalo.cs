////-----------------------------------------------------------------------
//// <copyright file="DataIntervalo.cs" company="SpediA">
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

    /// <summary>
    /// Classe modelo de intervalo de data
    /// </summary>
    [Serializable]
    public class DataIntervalo
    {
        /// <summary>
        /// Obtém ou define a data de inicio do intervalo
        /// </summary>
        public virtual DateTime? De { get; set; }

        /// <summary>
        /// Obtém ou define a data de fim do intervalo
        /// </summary>
        public virtual DateTime? Ate { get; set; }
    }
}
