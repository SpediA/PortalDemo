////-----------------------------------------------------------------------
//// <copyright file="FiltroBusca.cs" company="SpediA">
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
    /// Classe modelo de busca de DFe
    /// </summary>
    [Serializable]
    public class FiltroBusca
    {
        /// <summary>
        /// Obtém ou define um destinatário com suas propriedades
        /// </summary>
        public virtual Participante Destinatario { get; set; }

        /// <summary>
        /// Obtém ou define um documento fiscal com suas propriedades
        /// </summary>
        public virtual DocumentoFiscal DocumentoFiscal { get; set; }

        /// <summary>
        /// Obtém ou define um emitente com suas propriedades
        /// </summary>
        public virtual Participante Emitente { get; set; }

        /// <summary>
        /// Obtém ou define um participante com suas propriedades
        /// </summary>
        public virtual Participante Participante { get; set; }

        /// <summary>
        /// Obtém ou define uma operação fiscal com suas propriedades
        /// </summary>
        public virtual OperacaoFiscal OperacaoFiscal { get; set; }

        /// <summary>
        /// Obtém ou define um produto com suas propriedades
        /// </summary>
        public virtual ProdutoServico Produto { get; set; }

        /// <summary>
        /// Obtém ou define um intervalo de data em que foi recepcionado
        /// </summary>
        public virtual DataIntervalo Recepcionado { get; set; }

        /// <summary>
        /// Obtém ou define um argumento livre para a pesquisa
        /// </summary>
        public virtual string TextoLivre { get; set; }

        /// <summary>
        /// Obtém ou define um intervalo de data da última consulta na Sefaz
        /// </summary>
        public virtual DataIntervalo UltimaConsultaSefaz { get; set; }
    }
}
