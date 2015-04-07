////-----------------------------------------------------------------------
//// <copyright file="AcaoArquivo.cs" company="SpediA">
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
    using NHibernate;
    using SpediaLibrary.Business;

    /// <summary>
    /// Classe modelo de tipos de ação e seus respectivos arquivos
    /// </summary>
    public class AcaoArquivo
    {
        /// <summary>
        /// Obtém ou define o tipo de ação
        /// </summary>
        public virtual TipoAcao? TipoAcao { get; set; }

        /// <summary>
        /// Obtém ou define a data da execução da ação
        /// </summary>
        public virtual DateTime? DataExecucao { get; set; }

        /// <summary>
        /// Obtém ou define uma lista com os identificadores dos arquivos pertencentes a essa ação
        /// </summary>
        public virtual IList<int> IdArquivos { get; set; }
    }
}
