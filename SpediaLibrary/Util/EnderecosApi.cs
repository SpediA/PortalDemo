////-----------------------------------------------------------------------
//// <copyright file="EnderecosApi.cs" company="SpediA">
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
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Classe dos endereços dos métodos da Api
    /// </summary>
    public sealed class EnderecosApi
    {
        /// <summary>Endereço da api para realizar busca simples ou avançada de DFe</summary>
        public const string BuscaDfe = "Dfe/Busca";

        /// <summary>Endereço da api para realizar busca simples ou avançada de SPED</summary>
        public const string BuscaSped = "Sped/Busca";

        /// <summary>Endereço da api para realizar upload de arquivos</summary>
        public const string Upload = "Upload";

        /// <summary>Endereço da api para realizar consulta de propriedades de objetos</summary>
        public const string Objeto = "Objeto";

        /// <summary>Endereço da api para realizar a consulta de DFe's na secretaria da fazenda</summary>
        public const string ConsultaSefaz = "Dfe/ConsultaSefaz";

        /// <summary>Endereço da api para solicitar a assinatura de arquivo SPED</summary>
        public const string Assina = "Sped/Assina";

        /// <summary>Endereço da api para realizar download de arquivos</summary>
        public const string Download = "Download/SolicitaDownload";

        /// <summary>Endereço da api para criar, editar, excluir ou alterar um novo usuário, ou ainda listar todos</summary>
        public const string Usuario = "Usuario";

        /// <summary>Endereço da api para criar, editar, excluir ou alterar uma nova empresa, ou ainda listar todas</summary>
        public const string Empresa = "Empresa";


        /// <summary>Endereço da api para listar todas as caixas postais</summary>
        public const string ContaEmail = "ContaEmail";

        /// <summary>Endereço da api para exibir o mapa de produção</summary>
        public const string MapaProducao = "MapaProducao";

        /// <summary>Endereço da api para listar os status de cada módulo da plataforma</summary>
        public const string StatusPlataforma = "StatusPlataforma";

        /// <summary>Endereço da api para listar as notificações da conta de uso</summary>
        public const string Notificacao = "Notificacao?s=";

        /// <summary>
        /// Evita que uma instância default da classe <see cref="EnderecosApi" /> seja criada.
        /// </summary>
        private EnderecosApi()
        {
        }
    }
}
