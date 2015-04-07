////-----------------------------------------------------------------------
//// <copyright file="FiltroBuscaSped.cs" company="SpediA">
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
    /// Classe modelo de busca de SPED
    /// </summary>
    [Serializable]
    public class FiltroBuscaSped
    {
        /// <summary>
        /// Obtém ou define um participante
        /// </summary>
        public virtual Participante Entidade { get; set; }

        /// <summary>
        /// Obtém ou define a lista de tipos da escrituração
        /// </summary>
        public virtual IList<TipoEscrituracao> TipoEscrituracao { get; set; }

        /// <summary>
        /// Obtém ou define um argumento livre para a pesquisa
        /// </summary>
        public virtual string TextoLivre { get; set; }

        /// <summary>
        /// Obtém ou define o nome do arquivo
        /// </summary>
        public virtual string NomeArquivo { get; set; }

        /// <summary>
        /// Obtém ou define a finalidade do arquivo
        /// </summary>
        public virtual FinalidadeOperacaoSped? FinalidadeArquivo { get; set; }

        /// <summary>
        /// Obtém ou define o tipo de transmissão do arquivo
        /// </summary>
        public virtual byte? TipoTransmissao { get; set; }

        /// <summary>
        /// Obtém ou define a situação do processamento do PVA
        /// </summary>
        public virtual IList<StatusPva> StatusPva { get; set; }

        /// <summary>
        /// Obtém ou define a situação do SPED
        /// </summary>
        public virtual IList<StatusSped> StatusSped { get; set; }

        /// <summary>
        /// Obtém ou define um intervalo de data da competência do arquivo
        /// </summary>
        public virtual DataIntervalo Competencia { get; set; }

        /// <summary>
        /// Obtém ou define um intervalo de data em que foi recepcionado
        /// </summary>
        public virtual DataIntervalo Recepcionado { get; set; }

        /// <summary>
        /// Obtém ou define um intervalo de data em que foi assinado
        /// </summary>
        public virtual DataIntervalo DataAssinatura { get; set; }

        /// <summary>
        /// Obtém ou define um intervalo de data em que foi processado pelo PVA
        /// </summary>
        public virtual DataIntervalo DataProcessamentoPva { get; set; }

        /// <summary>
        /// Obtém ou define um intervalo de data em que foi transmitido para a Sefaz
        /// </summary>
        public virtual DataIntervalo DataTransmissaoSefaz { get; set; }
    }
}
