////-----------------------------------------------------------------------
//// <copyright file="ResultadoBusca.cs" company="SpediA">
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

    /// <summary>
    /// Classe modelo de resultado da busca de arquivos
    /// </summary>
    public class ResultadoBusca
    {
        /// <summary>
        /// Obtém ou define a quantidade de registros resultantes da aplicação do filtro na busca
        /// </summary>
        public virtual int Quantidade { get; set; }

        /// <summary>
        /// Obtém ou define a lista de resultados
        /// </summary>
        public virtual IList<Arquivo> Resultados { get; set; }
    }
}
