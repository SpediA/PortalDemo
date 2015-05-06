////-----------------------------------------------------------------------
//// <copyright file="ConstantesGlobais.cs" company="SpediA">
//// Copyright [2014] [SPEDIA Soluções Tecnológicas Ltda]
//// Licenciado sob Licença Apache, Versão 2.0 (a "Licença"). Você não pode usar este arquivo exceto em conformidade com a Licença.
//// Você pode obter uma cópia da Licença em:
//// http://www.apache.org/licenses/LICENSE-2.0
//// Ao menos que seja exigido por lei aplicável ou com autorização por escrito, todo software distribuído sob a Licença é distribuído "COMO ESTÁ",
//// SEM GARANTIAS OU CONDIÇÕES DE NENHUMA ESPÉCIE, expressas ou implícitas.
//// Veja a Licença no idioma específico que estabelece as permissões e limitações sob a Licença.
//// </copyright>
////-----------------------------------------------------------------------
namespace SpediaWeb.Presentation.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// Classe das constantes globais
    /// </summary>
    public sealed class ConstantesGlobais
    {
        /// <summary>Constante global para o estilo de mensagem de sucesso</summary>
        public const string CLASSE_MENSAGEM_SUCESSO = "alert alert-success alert-dismissible";

        /// <summary>Constante global para o estilo de mensagem de erro</summary>
        public const string CLASSE_MENSAGEM_ERRO = "alert alert-danger alert-dismissible";

        /// <summary>Representa o caracter de controle de separação das tags</summary>
        public const char SEPARADOR_TAG = ',';

        /// <summary>Constante global para a sessão de filtro de busca</summary>
        public const string FILTRO_BUSCA = "FiltroBusca";

        /// <summary>Constante global para a sessão de usuário logado</summary>
        public const string USUARIO = "Usuario";

        /// <summary>Constante global para a sessão de empresas</summary>
        public const string EMPRESAS = "Empresas";

        /// <summary>Constante global para a sessão de caixas postais</summary>
        public const string CAIXAS_POSTAIS = "CaixasPostais";

        /// <summary>Constante global para o nome do arquivo quando é realizado um download</summary>
        public const string NOME_ARQUIVO_DOWNLOAD = "\\SpediA-arquivo.zip";

        /// <summary>
        /// Evita que uma instância default da classe <see cref="ConstantesGlobais" /> seja criada.
        /// </summary>
        private ConstantesGlobais()
        {
        }
    }
}