////-----------------------------------------------------------------------
//// <copyright file="GerenciamentoAtributoAdicional.cs" company="SpediA">
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
    /// Classe responsável pelas regras de negócio dos atributos adicionais
    /// </summary>
    public class GerenciamentoAtributoAdicional
    {
        /// <summary>Representa o caracter de controle de separação das tags</summary>
        public const char SEPARADOR_TAG = ',';

        /// <summary> Objeto da biblioteca log4net para registro de log da aplicação </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(GerenciamentoAtributoAdicional));

        /// <summary>
        /// Obtem todos os atributos adicionais
        /// </summary>
        /// <returns>Lista de atributos adicionais</returns>
        public static List<AtributoAdicional> ObtemAtributosAdicionais()
        {
            List<AtributoAdicional> atributos;
            string json;

            try
            {
                json = AuxiliarJson.Obtem(EnderecosApi.AtributoAdicional);
                atributos = (List<AtributoAdicional>)AuxiliarJson.Desserializa<List<AtributoAdicional>>(json);
                return atributos;
            }
            catch (Exception ex)
            {
                Log.Info(ex.InnerException == null ? ex.Message : ex.InnerException.ToString());
                return null;
            }
        }

        /// <summary>
        /// Obtem um atributo adicional e todos os seus valores
        /// </summary>
        /// <param name="idAtributoAdicional">Identificador do atributo adicional</param>
        /// <returns>Atributo adicional e a sua lista de valores</returns>
        public static AtributoAdicional ObtemAtributoAdicional(int idAtributoAdicional)
        {
            AtributoAdicional atributo;
            string json;

            try
            {
                json = AuxiliarJson.Obtem(EnderecosApi.AtributoAdicional + "/" + idAtributoAdicional);
                atributo = (AtributoAdicional)AuxiliarJson.Desserializa<AtributoAdicional>(json);
                return atributo;
            }
            catch (Exception ex)
            {
                Log.Info(ex.InnerException == null ? ex.Message : ex.InnerException.ToString());
                return null;
            }
        }

        /// <summary>
        /// Cria ou modifica o atributo adicional
        /// </summary>
        /// <param name="atributoAdicional">Atributo adicional base para a criação ou modificação</param>
        /// <param name="valoresAdicionados">Valores de domínio do atributo adicional a serem criados</param>
        /// <param name="valoresExcluidos">Valores de domínio do atributo adicional a serem excluídos</param>
        /// <returns>Indica se a criação ou modificação foi bem sucedida</returns>
        public static bool AtualizaAtributoAdicional(AtributoAdicional atributoAdicional, List<string> valoresAdicionados, List<int> valoresExcluidos)
        {
            string json;

            try
            {
                if (atributoAdicional.Id.HasValue)
                {
                    string lAtributoAdicional = "[{ \"Nome\" : \"" + atributoAdicional.Nome + "\" }]";
                    json = AuxiliarJson.Coloca(lAtributoAdicional, EnderecosApi.AtributoAdicional + "/" + atributoAdicional.Id.Value, true);
                }
                else
                {
                    json = AuxiliarJson.Posta(atributoAdicional, EnderecosApi.AtributoAdicional);
                    atributoAdicional = (AtributoAdicional)AuxiliarJson.Desserializa<AtributoAdicional>(json);
                }
                
                foreach (string valorAdicionado in valoresAdicionados)
                {
                    CriaValorAtributoAdicional(atributoAdicional.Id.Value, valorAdicionado);
                }

                foreach (int valorExcluido in valoresExcluidos)
                {
                    ExcluiValorAtributoAdicional(atributoAdicional.Id.Value, valorExcluido);
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
        /// Exclui o atributo adicional e todos os seus valores
        /// </summary>
        /// <param name="idAtributoAdicional">Identificador do atributo adicional</param>
        /// <param name="valoresExcluidos">Valores de domínio do atributo adicional a serem excluídos</param>
        /// <returns>Indica se a exclusão foi bem sucedida</returns>
        public static bool ExcluiAtributoAdicional(int idAtributoAdicional, List<int> valoresExcluidos)
        {
            try
            {
                foreach (int valorExcluido in valoresExcluidos)
                {
                    ExcluiValorAtributoAdicional(idAtributoAdicional, valorExcluido);
                }
                
                AuxiliarJson.Exclui(EnderecosApi.AtributoAdicional + "/" + idAtributoAdicional);
                
                return true;
            }
            catch (Exception ex)
            {
                Log.Info(ex.InnerException == null ? ex.Message : ex.InnerException.ToString());
                return false;
            }
        }

        /// <summary>
        /// Obtem os domínios de todos os atributos adicionais
        /// </summary>
        /// <returns>Lista de valores de atributos adicionais</returns>
        public static IList<Parametro> ObtemValoresAtributosAdicionais()
        {
            AtributoAdicional atributoAdicional;
            List<Parametro> valores = new List<Parametro>();
            List<AtributoAdicional> atributos = ObtemAtributosAdicionais();

            foreach (AtributoAdicional atributo in atributos)
            {
                atributoAdicional = ObtemAtributoAdicional(atributo.Id.Value);

                if (atributoAdicional.Valores != null && atributoAdicional.Valores.Count > 0)
                {
                    valores.AddRange(atributoAdicional.Valores);
                }
            }

            return valores;
        }

        /// <summary>
        /// Atualiza os valores de atributo adicional associados a usuário ou empresa
        /// </summary>
        /// <param name="idObjeto">Identificador de usuário ou empresa</param>
        /// <param name="objetoEEmpresa">Indica se o objeto a ser tratado é uma empresa ou um usuário</param>
        /// <param name="permissoesDigitadas">Valores de atributos adicionais digitados</param>
        /// <returns>Indica se a atualização foi bem sucedida</returns>
        public static bool AtualizaValoresAtributoAdicional(int idObjeto, bool objetoEEmpresa, string permissoesDigitadas)
        {
            try
            {
                List<AtributoAdicional> atributosAdicionais = objetoEEmpresa ? (List<AtributoAdicional>)GerenciamentoEmpresa.ObtemAtributosAdicionais(idObjeto)
                    : (List<AtributoAdicional>)GerenciamentoUsuario.ObtemAtributosAdicionais(idObjeto);
                List<int> valoresDigitados = new List<int>();
                List<int> valoresAExcluir = new List<int>();
                List<int> valoresAAdicionar = new List<int>();

                if (!string.IsNullOrEmpty(permissoesDigitadas))
                {
                    foreach (string valorDigitado in permissoesDigitadas.Split(SEPARADOR_TAG))
                    {
                        valoresDigitados.Add(Convert.ToInt32(valorDigitado));
                        valoresAAdicionar.Add(Convert.ToInt32(valorDigitado));
                    }
                }

                if (atributosAdicionais != null && atributosAdicionais.Count > 0)
                {
                    foreach (AtributoAdicional atributo in atributosAdicionais)
                    {
                        foreach (Parametro valor in atributo.Valores)
                        {
                            if (valoresDigitados.Contains(valor.Id.Value))
                            {
                                valoresAAdicionar.Remove(valor.Id.Value);
                            }
                            else
                            {
                                valoresAExcluir.Add(valor.Id.Value);
                            }
                        }
                    }
                }

                if (objetoEEmpresa)
                {
                    GerenciamentoEmpresa.AtribuiValorAtributoAdicional(idObjeto, valoresAAdicionar);
                    GerenciamentoEmpresa.RemoveValorAtributoAdicional(idObjeto, valoresAExcluir);
                }
                else
                {
                    GerenciamentoUsuario.AtribuiValorAtributoAdicional(idObjeto, valoresAAdicionar);
                    GerenciamentoUsuario.RemoveValorAtributoAdicional(idObjeto, valoresAExcluir);
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
        /// Atualiza o arquivo de valores de atributos adicionais
        /// </summary>
        public static void AtualizaJsonValoresAtributosAdicionais()
        {
            List<Parametro> valores = (List<Parametro>)GerenciamentoAtributoAdicional.ObtemValoresAtributosAdicionais();
            string json = string.Empty;

            json = (valores != null && valores.Count > 0) ? AuxiliarJson.Serializa(valores) : AuxiliarJson.Serializa(string.Empty);

            System.IO.File.WriteAllText(ConfiguracaoAplicacao.EnderecoAtributosAdicionais, json);
        }

        /// <summary>
        /// Cria um novo valor de domínio para o atributo adicional
        /// </summary>
        /// <param name="idAtributoAdicional">Identificador do atributo adicional</param>
        /// <param name="valorAtributoAdicional">Valor de domínio para o atributo adicional</param>
        private static void CriaValorAtributoAdicional(int idAtributoAdicional, string valorAtributoAdicional)
        {
            AuxiliarJson.Posta(null, EnderecosApi.AtributoAdicional + "/" + idAtributoAdicional + "/" + valorAtributoAdicional);
        }

        /// <summary>
        /// Exclui um valor de domínio do atributo adicional
        /// </summary>
        /// <param name="idAtributoAdicional">Identificador do atributo adicional</param>
        /// <param name="idValorAtributoAdicional">Identificador do valor de domínio do atributo adicional</param>
        private static void ExcluiValorAtributoAdicional(int idAtributoAdicional, int idValorAtributoAdicional)
        {
            AuxiliarJson.Exclui(EnderecosApi.AtributoAdicional + "/" + idAtributoAdicional + "/" + idValorAtributoAdicional);
        }
    }
}
