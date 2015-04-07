////-----------------------------------------------------------------------
//// <copyright file="PropriedadeObjeto.cs" company="SpediA">
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
    /// Classe modelo de propriedade do objeto (Dfe ou Sped, por enquanto)
    /// </summary>
    public class PropriedadeObjeto
    {
        /// <summary>
        /// Obtém ou define o identificador do arquivo
        /// </summary>
        public virtual int IdArquivo { get; set; }

        /// <summary>
        /// Obtém ou define a ação do objeto
        /// </summary>
        public virtual IList<PropriedadeAcao> Acoes { get; set; }

        /// <summary>
        /// Obtém ou define a data em que o upload foi realizado
        /// </summary>
        public virtual DateTime DataUpload { get; set; }

        /// <summary>
        /// Obtém ou define os detalhes da Dfe
        /// </summary>
        public virtual DetalheDfe DetalheDfe { get; set; }

        /// <summary>
        /// Obtém ou define os detalhes do Sped
        /// </summary>
        public virtual DetalheSped DetalheSped { get; set; }

        /// <summary>
        /// Obtém ou define o identificador do usuário que realizou o upload
        /// </summary>
        public virtual int IdUsuarioUpload { get; set; }

        /// <summary>
        /// Obtém ou define o nome original do arquivo que sofreu o upload
        /// </summary>
        public virtual string NomeOriginal { get; set; }

        /// <summary>
        /// Obtém ou define a situação em que o arquivo se encontra
        /// </summary>
        public virtual StatusArquivo Status { get; set; }

        /// <summary>
        /// Obtém ou define a data em que o status foi alterado
        /// </summary>
        public virtual DateTime DataStatus { get; set; }

        /// <summary>
        /// Obtém ou define o tamanho do arquivo
        /// </summary>
        public virtual int? Tamanho { get; set; }

        /// <summary>
        /// Obtém ou define o tipo do arquivo
        /// </summary>
        public virtual TipoArquivo TipoArquivo { get; set; }

        /// <summary>
        /// Obtém ou define as "tags" extraídas do arquivo para possibilitar buscas
        /// </summary>
        public virtual Dictionary<string, string> Metadados { get; set; }
    }
}
