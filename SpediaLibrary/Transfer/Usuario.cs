////-----------------------------------------------------------------------
//// <copyright file="Usuario.cs" company="SpediA">
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
    /// Classe modelo de usuário
    /// </summary>
    public class Usuario : ModeloBase
    {
        /// <summary>
        /// Obtém ou define o id gerado pela API da Spedia
        /// </summary>
        public virtual int? IdApi { get; set; }

        /// <summary>
        /// Obtém ou define o e-mail (que também é usado para login)
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// Obtém ou define a senha do usuário
        /// </summary>
        public virtual string Senha { get; set; }

        /// <summary>
        /// Obtém ou define o nome do usuário
        /// </summary>
        public virtual string Nome { get; set; }

        /// <summary>
        /// Obtém ou define o usuário da conta de uso da SpediA
        /// </summary>
        public virtual string UsuarioSpedia { get; set; }

        /// <summary>
        /// Obtém ou define a senha da conta de uso da SpediA
        /// </summary>
        public virtual string SenhaSpedia { get; set; }

        /// <summary>
        /// Obtém ou define o perfil do usuário
        /// </summary>
        public virtual PerfilUsuario Perfil { get; set; }
    }
}