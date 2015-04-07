////-----------------------------------------------------------------------
//// <copyright file="ConfiguracaoAplicacao.cs" company="SpediA">
//// Copyright [2014] [SPEDIA Soluções Tecnológicas Ltda]
//// Licenciado sob Licença Apache, Versão 2.0 (a "Licença"). Você não pode usar este arquivo exceto em conformidade com a Licença.
//// Você pode obter uma cópia da Licença em:
//// http://www.apache.org/licenses/LICENSE-2.0
//// Ao menos que seja exigido por lei aplicável ou com autorização por escrito, todo software distribuído sob a Licença é distribuído "COMO ESTÁ",
//// SEM GARANTIAS OU CONDIÇÕES DE NENHUMA ESPÉCIE, expressas ou implícitas.
//// Veja a Licença no idioma específico que estabelece as permissões e limitações sob a Licença.
//// </copyright>
////-----------------------------------------------------------------------
namespace SpediaLibrary.Util
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Web;

    /// <summary>
    /// Classe para carregamento das configurações
    /// </summary>
    public static class ConfiguracaoAplicacao
    {
        /// <summary> Obtém as configurações de conexão do banco de dados </summary>
        public static ConnectionStringSettings ConfiguracoesConexaoBancoDados
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["Spedia"];
            }
        }

        /// <summary> Obtém o endereço inicial do site </summary>
        public static string EnderecoAplicacao
        {
            get
            {
                return ConfigurationManager.AppSettings["ApplicationPath"];
            }
        }

        /// <summary> Obtém o endereço do diretório temporário da aplicação </summary>
        public static string EnderecoDiretorioTemporario
        {
            get
            {
                return ConfigurationManager.AppSettings["TempDirectory"];
            }
        }

        /// <summary> Obtém o nome de usuário da conta de uso </summary>
        public static string UsuarioConta
        {
            get
            {
                return ConfigurationManager.AppSettings["AccountUsername"];
            }
        }

        /// <summary> Obtém a senha da conta de uso </summary>
        public static string SenhaConta
        {
            get
            {
                return ConfigurationManager.AppSettings["AccountPassword"];
            }
        }

        /// <summary> Obtém a senha da conta de uso </summary>
        public static string EnderecoTemplate
        {
            get
            {
                return HttpContext.Current.Server.MapPath("~/Templates");
            }
        }

        /// <summary> Obtém o endereço do arquivo que contém os valores dos atributos adicionais</summary>
        public static string EnderecoAtributosAdicionais
        {
            get
            {
                return HttpContext.Current.Server.MapPath("~/Content/Auxiliary/AtributosAdicionais.txt");
            }
        }

        /// <summary>
        /// Obtém a URL da API da Spedia
        /// </summary>
        public static string APISpediaURL
        {
            get
            {
                return ConfigurationManager.AppSettings["URLSpediaAPI"];
            }
        }

    }
}
