////-----------------------------------------------------------------------
//// <copyright file="UsuarioRepositorio.cs" company="SpediA">
//// Copyright [2014] [SPEDIA Soluções Tecnológicas Ltda]
//// Licenciado sob Licença Apache, Versão 2.0 (a "Licença"). Você não pode usar este arquivo exceto em conformidade com a Licença.
//// Você pode obter uma cópia da Licença em:
//// http://www.apache.org/licenses/LICENSE-2.0
//// Ao menos que seja exigido por lei aplicável ou com autorização por escrito, todo software distribuído sob a Licença é distribuído "COMO ESTÁ",
//// SEM GARANTIAS OU CONDIÇÕES DE NENHUMA ESPÉCIE, expressas ou implícitas.
//// Veja a Licença no idioma específico que estabelece as permissões e limitações sob a Licença.
//// </copyright>
////-----------------------------------------------------------------------
namespace SpediaLibrary.Persistence.Repository
{
    using System.Collections.Generic;
    using NHibernate;
    using NHibernate.Criterion;
    using SpediaLibrary.Transfer;

    /// <summary>
    /// Classe repositório de usuário
    /// </summary>
    public class UsuarioRepositorio : RepositorioBase<Usuario>
    {
        /// <summary>
        /// Obtém um usuário do banco de dados de acordo com o identificador
        /// </summary>
        /// <param name="parametro">Usuário a ser usado como filtro</param>
        /// <returns>Usuário correspondente ao filtro da busca</returns>
        public Usuario Obtem(Usuario parametro)
        {
            Usuario usuario = Sessao
                .CreateCriteria(typeof(Usuario))
                .Add(Restrictions.Eq("Id", parametro.Id))
                .UniqueResult<Usuario>();
            return usuario;
        }

        /// <summary>
        /// Obtém um usuário do banco de dados de acordo com o e-mail
        /// </summary>
        /// <param name="parametro">Usuário a ser usado como filtro</param>
        /// <returns>Usuário correspondente ao filtro da busca</returns>
        public Usuario ObtemPorEmail(Usuario parametro)
        {
            Usuario usuario = Sessao
                .CreateCriteria(typeof(Usuario))
                .Add(Restrictions.Eq("Email", parametro.Email))
                .UniqueResult<Usuario>();
            return usuario;
        }

        /// <summary>
        /// Obtém um usuário do banco de dados de acordo com o nome
        /// </summary>
        /// <param name="parametro">Usuário a ser usado como filtro</param>
        /// <returns>Usuário correspondente ao filtro da busca</returns>
        public Usuario ObtemPorNome(Usuario parametro)
        {
            Usuario usuario = Sessao
                .CreateCriteria(typeof(Usuario))
                .Add(Restrictions.Eq("Nome", parametro.Nome))
                .UniqueResult<Usuario>();
            return usuario;
        }

        /// <summary>
        /// Obtém a lista de usuários do banco de dados
        /// </summary>
        /// <returns>Lista de usuários</returns>
        public IList<Usuario> ObtemLista()
        {
            IList<Usuario> usuario = Sessao
                .CreateCriteria(typeof(Usuario))
                .List<Usuario>();
            return usuario;
        }

        /// <summary>
        /// Obtém a lista de usuários do banco de dados de acordo com o usuario Spedia
        /// </summary>
        /// <returns>Lista de usuários</returns>
        public IList<Usuario> ObtemPorUsuarioSpedia(string UsuarioSpedia)
        {
            IList<Usuario> usuario = Sessao
                .CreateCriteria(typeof(Usuario))
                .Add(Restrictions.Eq("UsuarioSpedia", UsuarioSpedia))
                .List<Usuario>();
            return usuario;
        }
    }
}
