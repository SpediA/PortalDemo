////-----------------------------------------------------------------------
//// <copyright file="M003ConceitoNotificacao.cs" company="SpediA">
//// Copyright [2014] [SPEDIA Soluções Tecnológicas Ltda]
//// Licenciado sob Licença Apache, Versão 2.0 (a "Licença"). Você não pode usar este arquivo exceto em conformidade com a Licença.
//// Você pode obter uma cópia da Licença em:
//// http://www.apache.org/licenses/LICENSE-2.0
//// Ao menos que seja exigido por lei aplicável ou com autorização por escrito, todo software distribuído sob a Licença é distribuído "COMO ESTÁ",
//// SEM GARANTIAS OU CONDIÇÕES DE NENHUMA ESPÉCIE, expressas ou implícitas.
//// Veja a Licença no idioma específico que estabelece as permissões e limitações sob a Licença.
//// </copyright>
////-----------------------------------------------------------------------
namespace SpediaLibrary.Persistence.DatabaseMigration._1._0
{
    using System.Collections.Generic;
    using FluentMigrator;
    using FluentMigrator.Runner.Extensions;
    using NHibernate;
    using NHibernate.Context;
    using SpediaLibrary.Business;
    using SpediaLibrary.Transfer;

    /// <summary>
    /// Configuração do banco de dados (DDL)
    /// Versão 1.0
    /// </summary>
    [Migration(4, "Configuração de notificações")]
    public class M003ConceitoNotificacao : Migration
    {
        /// <summary> Obtém ou define o objeto "fábrica" de sessão </summary>
        private ISessionFactory FabricaSessao { get; set; }

        /// <summary>
        /// Executa a atualização criando a tabela de notificações e os registros para os usuários cadastrados
        /// </summary>
        public override void Up()
        {
            this.FabricaSessao = AuxiliarNHibernate.ObtemFabricaSessao();
            var sessao = this.FabricaSessao.OpenSession();
            CurrentSessionContext.Bind(sessao);
            sessao.BeginTransaction();

            List<Usuario> usuarios = (List<Usuario>)GerenciamentoUsuario.CarregaUsuarios();

            // Notificacao
            Create.Table("notificacao")
                .InSchema("dbo")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("id_usuario").AsInt32().NotNullable()
                .WithColumn("ultima_notificacao").AsInt64().NotNullable();

            // Controle de notificação dos usuários
            int indice = 1;
            foreach (Usuario usuario in usuarios)
            {
                Insert.IntoTable("notificacao").WithIdentityInsert().Row(new
                {
                    id = indice,
                    id_usuario = usuario.Id,
                    ultima_notificacao = 0
                });

                indice++;
            }

            var transacao = sessao.Transaction;
            if (transacao != null && transacao.IsActive)
            {
                transacao.Commit();
            }

            sessao = CurrentSessionContext.Unbind(this.FabricaSessao);
            sessao.Close();
        }

        /// <summary>
        /// Executa a limpeza do banco de dados apagando os dados e a tabela de notificações
        /// </summary>
        public override void Down()
        {
            Delete.FromTable("notificacao").AllRows();
            Delete.Table("notificacao");
        }
    }
}
