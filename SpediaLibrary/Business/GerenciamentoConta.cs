////-----------------------------------------------------------------------
//// <copyright file="GerenciamentoConta.cs" company="SpediA">
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
    /// Classe responsável pelas regras de negócio das contas de uso da API
    /// </summary>
    public static class GerenciamentoConta
    {
        /// <summary> Objeto da biblioteca log4net para registro de log da aplicação </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(GerenciamentoConta));

        /// <summary>
        /// Carrega os valores do mapa de produção do mês atual para o tipo de arquivo especificado
        /// </summary>
        /// <param name="tipoArquivo">Tipo do arquivo que será carregado no mapa</param>
        /// <returns>Mapa de produção</returns>
        public static IList<ItemProducao> CarregaMapaProducao(TipoArquivo tipoArquivo)
        {
            List<ItemProducao> mapa = new List<ItemProducao>();
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            string dataInicial = year + "-" + month + "-1";
            string dataFinal = year + "-" + month + "-" + DateTime.DaysInMonth(year, month);

            string json = AuxiliarJson.Obtem(EnderecosApi.MapaProducao + "/" + (byte)tipoArquivo + "/" + dataInicial + "/" + dataFinal);

            return (List<ItemProducao>)AuxiliarJson.Desserializa<List<ItemProducao>>(json);
        }
    }
}
