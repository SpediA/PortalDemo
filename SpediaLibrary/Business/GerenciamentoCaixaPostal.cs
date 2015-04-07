////-----------------------------------------------------------------------
//// <copyright file="GerenciamentoCaixaPostal.cs" company="SpediA">
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
    /// Classe responsável pelas regras de negócio de caixa postal
    /// </summary>
    public static class GerenciamentoCaixaPostal
    {
        /// <summary> Objeto da biblioteca log4net para registro de log da aplicação </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(GerenciamentoCaixaPostal));

        /// <summary>
        /// Carrega todas as caixas postais cadastradas
        /// </summary>
        /// <returns>Lista de caixas postais</returns>
        public static IList<CaixaPostal> CarregaCaixasPostais()
        {
            string json = AuxiliarJson.Obtem(EnderecosApi.ContaEmail);

            return (List<CaixaPostal>)AuxiliarJson.Desserializa<List<CaixaPostal>>(json);
        }

        /// <summary>
        /// Exclui a caixa postal
        /// </summary>
        /// <param name="idCaixaPostal">Identificador da caixa postal</param>
        /// <returns>Indica se a exclusão foi bem sucedida</returns>
        public static bool ExcluiCaixaPostal(int idCaixaPostal)
        {
            try
            {
                AuxiliarJson.Exclui(EnderecosApi.ContaEmail + "/" + idCaixaPostal.ToString());

                return true;
            }
            catch (Exception ex)
            {
                Log.Info(ex.InnerException == null ? ex.Message : ex.InnerException.ToString());
                return false;
            }
        }

        /// <summary>
        /// Cria ou modifica uma caixa postal
        /// </summary>
        /// <param name="caixaPostal">Caixa postal base para a criação ou modificação</param>
        /// <returns>Indica se a criação ou modificação foi bem sucedida</returns>
        public static bool AtualizaCaixaPostal(CaixaPostal caixaPostal)
        {
            string json;

            try
            {
                if (caixaPostal.Id.HasValue)
                {
                    AuxiliarJson.Coloca(caixaPostal, EnderecosApi.ContaEmail + "/" + caixaPostal.Id, true);
                }
                else
                {
                    json = AuxiliarJson.Posta(caixaPostal, EnderecosApi.ContaEmail);
                    caixaPostal = (CaixaPostal)AuxiliarJson.Desserializa<CaixaPostal>(json);
                    return true;
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Info(ex.InnerException == null ? ex.Message : ex.InnerException.ToString());
                return false;
            }
        }
    }
}
