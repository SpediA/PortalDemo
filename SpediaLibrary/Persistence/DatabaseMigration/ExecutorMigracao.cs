////-----------------------------------------------------------------------
//// <copyright file="ExecutorMigracao.cs" company="SpediA">
//// Copyright [2014] [SPEDIA Soluções Tecnológicas Ltda]
//// Licenciado sob Licença Apache, Versão 2.0 (a "Licença"). Você não pode usar este arquivo exceto em conformidade com a Licença.
//// Você pode obter uma cópia da Licença em:
//// http://www.apache.org/licenses/LICENSE-2.0
//// Ao menos que seja exigido por lei aplicável ou com autorização por escrito, todo software distribuído sob a Licença é distribuído "COMO ESTÁ",
//// SEM GARANTIAS OU CONDIÇÕES DE NENHUMA ESPÉCIE, expressas ou implícitas.
//// Veja a Licença no idioma específico que estabelece as permissões e limitações sob a Licença.
//// </copyright>
////-----------------------------------------------------------------------
namespace SpediaLibrary.Persistence.DatabaseMigration
{
    using System;
    using System.Reflection;
    using FluentMigrator;
    using FluentMigrator.Runner;
    using FluentMigrator.Runner.Announcers;
    using FluentMigrator.Runner.Initialization;
    using FluentMigrator.Runner.Processors.SqlServer;
    using SpediaLibrary.Util;

    /// <summary>
    /// Classe de uso do framework FluentMigrator para aplicação das alterações feitas no banco de dados
    /// </summary>
    public static class ExecutorMigracao
    {
        /// <summary>
        /// Executa a última versão das classes de migração
        /// Está configurado para MS SQL Server
        /// </summary>
        public static void MigraParaMaisRecente()
        {
            var escritor = new TextWriterAnnouncer(s => System.Diagnostics.Debug.WriteLine(s));
            var montador = Assembly.GetExecutingAssembly();

            var contextoMigracao = new RunnerContext(escritor);

            var configuracaoBancoDados = ConfiguracaoAplicacao.ConfiguracoesConexaoBancoDados;
            var opcoes = new OpcoesMigracao { PreviewOnly = false, Timeout = 60 };
            var fabricador = new SqlServerProcessorFactory();
            var processador = fabricador.Create(configuracaoBancoDados.ConnectionString, escritor, opcoes);
            var executor = new MigrationRunner(montador, contextoMigracao, processador);
            executor.MigrateUp(true);
        }

        /// <summary>
        /// Opções do FluentMigrator
        /// </summary>
        public class OpcoesMigracao : IMigrationProcessorOptions
        {
            /// <summary> Obtém ou define um valor que indica se é somente visualização ou não </summary>
            public bool PreviewOnly { get; set; }

            /// <summary> Obtém ou define as chaves do provedor </summary>
            public string ProviderSwitches { get; set; }

            /// <summary> Obtém ou define o limite de tempo </summary>
            public int Timeout { get; set; }
        }
    }
}