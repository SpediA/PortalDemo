////-----------------------------------------------------------------------
//// <copyright file="NotificacaoRepositorio.cs" company="SpediA">
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
    /// Classe repositório de notificação
    /// </summary>
    public class NotificacaoRepositorio : RepositorioBase<ControleNotificacao>
    {
        /// <summary>
        /// Obtém um controle de notificação do banco de dados de acordo com o identificador do usuário
        /// </summary>
        /// <param name="idUsuario">Identificador do usuário a ser usado como filtro</param>
        /// <returns>Usuário correspondente ao filtro da busca</returns>
        public ControleNotificacao Obtem(int idUsuario)
        {
            ControleNotificacao controle = Sessao
                .CreateCriteria(typeof(ControleNotificacao))
                .Add(Restrictions.Eq("IdUsuario", idUsuario))
                .UniqueResult<ControleNotificacao>();
            return controle;
        }

        /// <summary>
        /// Obtém o identificador da última notificação vista por algum usuário
        /// </summary>
        /// <returns>Identificador da última notificação vista por algum usuário</returns>
        public long ObtemUltimaNotificacaoArmazenada()
        {
            long ultimaNotificacao = Sessao
                .CreateCriteria(typeof(ControleNotificacao))
                .SetProjection(Projections.Max("UltimaNotificacao"))
                .UniqueResult<long>();
            return ultimaNotificacao;
        }
    }
}
