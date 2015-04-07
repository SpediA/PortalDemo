////-----------------------------------------------------------------------
//// <copyright file="ProdutoServico.cs" company="SpediA">
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
    /// Classe modelo de um produto/serviço
    /// </summary>
    [Serializable]
    public class ProdutoServico
    {
        /// <summary>
        /// Obtém ou define o valor do CFOP
        /// </summary>
        public virtual string Cfop { get; set; }

        /// <summary>
        /// Obtém ou define o valor do código interno
        /// </summary>
        public virtual string CodigoInterno { get; set; }

        /// <summary>
        /// Obtém ou define a descricao do produto/serviço
        /// </summary>
        public virtual string Descricao { get; set; }

        /// <summary>
        /// Obtém ou define o valor do EAN
        /// </summary>
        public virtual string Ean { get; set; }

        /// <summary>
        /// Obtém ou define o valor do NCM
        /// </summary>
        public virtual string Ncm { get; set; }

        /// <summary>
        /// Obtém ou define o valor do nFCI
        /// </summary>
        public virtual string Nfci { get; set; }
    }
}
