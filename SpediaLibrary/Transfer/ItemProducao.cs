////-----------------------------------------------------------------------
//// <copyright file="ItemProducao.cs" company="SpediA">
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
    using SpediaLibrary.Business;

    /// <summary>
    /// Classe modelo de item de um mapa de produção
    /// </summary>
    public class ItemProducao
    {
        /// <summary>
        /// Obtém ou define o identificador da empresa
        /// </summary>
        public virtual int IdEmpresa { get; set; }

        /// <summary>
        /// Obtém ou define a data de referência do item do mapa de produção
        /// </summary>
        public virtual DateTime DataMapa { get; set; }

        /// <summary>
        /// Obtém ou define o tipo de arquivo ao qual o item se refere
        /// </summary>
        public virtual int TipoArquivo { get; set; }

        /// <summary>
        /// Obtém ou define o tipo de arquivos ao qual o item se refere
        /// </summary>
        public virtual Dictionary<TipoOperacao, ItemProducaoValores> Quantidades { get; set; }
    }
}
