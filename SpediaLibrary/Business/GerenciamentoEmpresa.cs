////-----------------------------------------------------------------------
//// <copyright file="GerenciamentoEmpresa.cs" company="SpediA">
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
    /// Classe responsável pelas regras de negócio de empresa
    /// </summary>
    public static class GerenciamentoEmpresa
    {
        /// <summary> Representa o texto para composição dos dados de um participante </summary>
        private const string TEXTO_PARTICIPANTE = "<b>CNPJ:</b> {0} <b>| IE:</b> {1} <b>|</b> {2}";

        /// <summary> Objeto da biblioteca log4net para registro de log da aplicação </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(GerenciamentoEmpresa));

        /// <summary>
        /// Carrega todas as empresas cadastradas
        /// </summary>
        /// <returns>Lista de empresas</returns>
        public static IList<Empresa> CarregaEmpresas()
        {
            string json = AuxiliarJson.Obtem(EnderecosApi.Empresa);

            return (List<Empresa>)AuxiliarJson.Desserializa<List<Empresa>>(json);
        }

        /// <summary>
        /// Cria uma nova empresa
        /// </summary>
        /// <param name="empresa">Empresa base para a criação</param>
        /// <returns>A nova empresa criada</returns>
        public static Empresa CriaEmpresa(Empresa empresa)
        {
            Empresa novaEmpresa;
            string json;

            try
            {
                json = AuxiliarJson.Posta(empresa, EnderecosApi.Empresa);
                novaEmpresa = (Empresa)AuxiliarJson.Desserializa<Empresa>(json);
                return novaEmpresa;
            }
            catch (Exception ex)
            {
                Log.Info(ex.InnerException == null ? ex.Message : ex.InnerException.ToString());
                return null;
            }
        }

        /// <summary>
        /// Exclui a empresa
        /// </summary>
        /// <param name="idEmpresa">Identificador da empresa</param>
        /// <returns>Indica se a exclusão foi bem sucedida</returns>
        public static bool ExcluiEmpresa(int idEmpresa)
        {
            try
            {
                AuxiliarJson.Exclui(EnderecosApi.Empresa + "/" + idEmpresa.ToString());

                return true;
            }
            catch (Exception ex)
            {
                Log.Info(ex.InnerException == null ? ex.Message : ex.InnerException.ToString());
                return false;
            }
        }

        /// <summary>
        /// Edita uma empresa
        /// </summary>
        /// <param name="empresa">Empresa base para a edição</param>
        /// <returns>Indica se a edição foi bem sucedida</returns>
        public static bool EditaEmpresa(Empresa empresa)
        {
            try
            {
                AuxiliarJson.Coloca(empresa, EnderecosApi.Empresa + "/" + empresa.Id, true);

                return true;
            }
            catch (Exception ex)
            {
                Log.Info(ex.InnerException == null ? ex.Message : ex.InnerException.ToString());
                return false;
            }
        }

        /// <summary>
        /// Obtém a lista de valores dos atributos adicionais vinculados a empresa
        /// </summary>
        /// <param name="idEmpresa">Identificador da empresa</param>
        /// <returns>Lista de valores de atributos adicionais</returns>
        public static IList<AtributoAdicional> ObtemAtributosAdicionais(int idEmpresa)
        {
            try
            {
                string json = AuxiliarJson.Obtem(EnderecosApi.Empresa + "/" + idEmpresa + "/" + EnderecosApi.Atributos);

                return (List<AtributoAdicional>)AuxiliarJson.Desserializa<List<AtributoAdicional>>(json);
            }
            catch (Exception ex)
            {
                Log.Info(ex.InnerException == null ? ex.Message : ex.InnerException.ToString());
                return null;
            }
        }

        /// <summary>
        /// Atribui a empresa os valores de atributo adicional
        /// </summary>
        /// <param name="idEmpresa">Identificador da empresa</param>
        /// <param name="valoresAtributoAdicional">Lista de identificadores dos valores do atributo adicional</param>
        /// <returns>Indica se a atribuição foi bem sucedida</returns>
        public static bool AtribuiValorAtributoAdicional(int idEmpresa, List<int> valoresAtributoAdicional)
        {
            try
            {
                foreach (int valor in valoresAtributoAdicional)
                {
                    AuxiliarJson.Coloca(null, EnderecosApi.Empresa + "/" + idEmpresa + "/" + EnderecosApi.Atributo + "/" + valor, true);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Info(ex.InnerException == null ? ex.Message : ex.InnerException.ToString());
                return false;
            }
        }

        /// <summary>
        /// Remove da empresa os valores de atributo adicional
        /// </summary>
        /// <param name="idEmpresa">Identificador da empresa</param>
        /// <param name="valoresAtributoAdicional">Lista de identificadores dos valores do atributo adicional</param>
        /// <returns>Indica se a remoção foi bem sucedida</returns>
        public static bool RemoveValorAtributoAdicional(int idEmpresa, List<int> valoresAtributoAdicional)
        {
            try
            {
                foreach (int valor in valoresAtributoAdicional)
                {
                    AuxiliarJson.Exclui(EnderecosApi.Empresa + "/" + idEmpresa + "/" + EnderecosApi.Atributo + "/" + valor);
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Info(ex.InnerException == null ? ex.Message : ex.InnerException.ToString());
                return false;
            }
        }

        /// <summary>
        /// Formata os dados de um participante em um formato pré-definido
        /// </summary>
        /// <param name="participante">Emissor ou destinatário de uma NFe</param>
        /// <returns>Texto montado</returns>
        public static string MontaTextoParticipante(Participante participante)
        {
            const string NO_TEXT = "-";

            string cnpj = string.IsNullOrEmpty(participante.Cnpj) ? NO_TEXT : Util.FormataCnpj(participante.Cnpj);
            string inscricaoEstadual = string.IsNullOrEmpty(participante.Ie) ? NO_TEXT : participante.Ie;
            string razaoSocial = string.IsNullOrEmpty(participante.RazaoSocial) ? NO_TEXT : participante.RazaoSocial;

            return string.Format(TEXTO_PARTICIPANTE, cnpj, inscricaoEstadual, razaoSocial);
        }
    }
}
