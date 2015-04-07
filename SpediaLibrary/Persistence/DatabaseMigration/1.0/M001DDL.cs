////-----------------------------------------------------------------------
//// <copyright file="M001DDL.cs" company="SpediA">
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

    /// <summary>
    /// Configuração do banco de dados (DDL)
    /// Versão 1.0
    /// </summary>
    [Migration(1, "Configuração do banco de dados")]
    public class M001DDL : Migration
    {
        /// <summary>
        /// Executa a atualização criando a tabela de usuários
        /// </summary>
        public override void Up()
        {
            // Usuário
            Create.Table("usuario")
                .InSchema("dbo")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("id_api").AsInt32().NotNullable()
                .WithColumn("email").AsString(50).NotNullable()
                .WithColumn("senha").AsString(50).NotNullable()
                .WithColumn("nome").AsString(255).NotNullable()
                .WithColumn("perfil").AsByte().NotNullable();
        }

        /// <summary>
        /// Executa a limpeza do banco de dados apagando a tabela de usuários
        /// </summary>
        public override void Down()
        {
            Delete.Table("usuario");
        }
    }
}
