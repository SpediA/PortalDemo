////-----------------------------------------------------------------------
//// <copyright file="M002DML.cs" company="SpediA">
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
    using FluentMigrator;
    using FluentMigrator.Runner.Extensions;

    /// <summary>
    /// Configuração do banco de dados (DML)
    /// Versão 1.0
    /// </summary>
    [Migration(2, "Configuração do banco de dados")]
    public class M002DML : Migration
    {
        /// <summary>
        /// Executa a atualização criando os dados de usuário
        /// </summary>
        public override void Up()
        {
            // Usuário padrão
            Insert.IntoTable("usuario").WithIdentityInsert().Row(new
            {
                id = 1,
                id_api = 36,
                email = "usuario@spedia.com.br",
                senha = "tlifxqsNyCzxIJnRwtQKuZToQQw=",  // 0
                nome = "Usuário Padrão",
                perfil = 1
            });
        }

        /// <summary>
        /// Executa a limpeza da tabela de usuário, no banco de dados, apagando os dados
        /// </summary>
        public override void Down()
        {
            Delete.FromTable("usuario").AllRows();
        }
    }
}
