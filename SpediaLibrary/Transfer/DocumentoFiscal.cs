////-----------------------------------------------------------------------
//// <copyright file="DocumentoFiscal.cs" company="SpediA">
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
    /// Classe modelo de documento fiscal
    /// </summary>
    [Serializable]
    public class DocumentoFiscal
    {
        /// <summary>
        /// Obtém ou define um valor que indica se a assinatura é válida
        /// </summary>
        public virtual bool? AssinaturaValida { get; set; }

        /// <summary>
        /// Obtém ou define uma lista de códigos de status do documento na Sefaz
        /// </summary>
        public virtual List<string> CodigoStatusSefaz { get; set; }

        /// <summary>
        /// Obtém ou define um valor que indica se possui protocolo de autorização presente
        /// </summary>
        public virtual bool? ProtocoloAutorizacaoPresente { get; set; }

        /// <summary>
        /// Obtém ou define um valor que indica se o documento é juridicamente válido
        /// </summary>
        public virtual bool? JuridicamenteValido { get; set; }

        /// <summary>
        /// Obtém ou define um valor que indica se o documento tem carta correção
        /// </summary>
        public virtual bool? TemCartaCorrecao { get; set; }
    }
}
