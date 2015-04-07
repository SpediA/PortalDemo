////-----------------------------------------------------------------------
//// <copyright file="GerenciamentoPlataforma.cs" company="SpediA">
//// Copyright [2014] [SPEDIA Soluções Tecnológicas Ltda]
//// Licenciado sob Licença Apache, Versão 2.0 (a "Licença"). Você não pode usar este arquivo exceto em conformidade com a Licença.
//// Você pode obter uma cópia da Licença em:
//// http://www.apache.org/licenses/LICENSE-2.0
//// Ao menos que seja exigido por lei aplicável ou com autorização por escrito, todo software distribuído sob a Licença é distribuído "COMO ESTÁ",
//// SEM GARANTIAS OU CONDIÇÕES DE NENHUMA ESPÉCIE, expressas ou implícitas.
//// Veja a Licença no idioma específico que estabelece as permissões e limitações sob a Licença.
//// </copyright>
////-----------------------------------------------------------------------
namespace SpediaLibrary.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using log4net;
    using SpediaLibrary.Transfer;
    using SpediaLibrary.Util;

    /// <summary>
    /// Classe responsável pelas regras de gerais da plataforma como status, consumo, etc.
    /// </summary>
    public static class GerenciamentoPlataforma
    {
        /// <summary> Objeto da biblioteca log4net para registro de log da aplicação </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(GerenciamentoPlataforma));

        /// <summary>
        /// Obtem o status de cada um dos módulos da plataforma
        /// </summary>
        /// <returns>Lista de módulos da plataforma e as datas de consulta de status</returns>
        public static IList<StatusModulo> ObtemStatusPlataforma()
        {
            string json = AuxiliarJson.Obtem(EnderecosApi.StatusPlataforma);

            return (List<StatusModulo>)AuxiliarJson.Desserializa<List<StatusModulo>>(json);
        }
    }
}
