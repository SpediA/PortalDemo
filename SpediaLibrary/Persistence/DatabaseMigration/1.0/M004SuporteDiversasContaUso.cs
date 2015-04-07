////-----------------------------------------------------------------------
//// <copyright file="M004SuporteDiversasContaUso.cs" company="SpediA">
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
    [Migration(3, "Suporte a diversas contas de uso")]
    public class M004SuporteDiversasContaUso : Migration
    {
        /// <summary> Obtém ou define o objeto "fábrica" de sessão </summary>
        private ISessionFactory FabricaSessao { get; set; }

        /// <summary>
        /// Executa a atualização, criando as novas colunas de usuário
        /// </summary>
        public override void Up()
        {
            this.FabricaSessao = AuxiliarNHibernate.ObtemFabricaSessao();
            var sessao = this.FabricaSessao.OpenSession();
            CurrentSessionContext.Bind(sessao);
            sessao.BeginTransaction();

            // Usuário
            Alter.Table("usuario")
                .InSchema("dbo")
                .AddColumn("usuario_spedia").AsString().Nullable()
                .AddColumn("senha_spedia").AsString().Nullable();

            var transacao = sessao.Transaction;
            if (transacao != null && transacao.IsActive)
            {
                transacao.Commit();
            }

            sessao = CurrentSessionContext.Unbind(this.FabricaSessao);
            sessao.Close();
        }

        /// <summary>
        /// Executa a limpeza do banco de dados apagando as tabela de notificação e usuário
        /// </summary>
        public override void Down()
        {
            Delete.Table("notificacao");
            Delete.Table("usuario");
        }
    }
}
