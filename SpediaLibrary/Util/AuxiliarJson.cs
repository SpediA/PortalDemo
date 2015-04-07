////-----------------------------------------------------------------------
//// <copyright file="AuxiliarJson.cs" company="SpediA">
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
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using log4net;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using SpediaLibrary.Transfer;

    /// <summary>
    /// Classe que trata das operações do Json
    /// </summary>
    public static class AuxiliarJson
    {
        /// <summary> Representa o endereço da API </summary>
        private static string URL_API = ConfiguracaoAplicacao.APISpediaURL;

        /// <summary> Representa a operação DELETE</summary>
        private const string OPERACAO_DELETE = "DELETE";

        /// <summary> Representa a operação GET</summary>
        private const string OPERACAO_GET = "GET";

        /// <summary> Representa a operação POST</summary>
        private const string OPERACAO_POST = "POST";

        /// <summary> Representa a operação PUT</summary>
        private const string OPERACAO_PUT = "PUT";

        /// <summary> Objeto da biblioteca log4net para registro de log da aplicação </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(AuxiliarJson));

        /// <summary>
        /// Executa a operação POST do Json
        /// </summary>
        /// <param name="objetoASerializar">Objeto que será serializado e enviado no POST</param>
        /// <param name="endereco">Endereço da url do método da API</param>
        /// <param name="upload">Indica se é um POST de upload de arquivo</param>
        /// <returns>Json serializado</returns>
        public static string Posta(object objetoASerializar, string endereco, bool upload = false)
        {
            string json = ExecutaOperacao(OPERACAO_POST, endereco, true, objetoASerializar, upload);

            return json;
        }

        /// <summary>
        /// Executa a operação GET do Json
        /// </summary>
        /// <param name="endereco">Endereço da url do método da API</param>
        /// <returns>Json serializado</returns>
        public static string Obtem(string endereco)
        {
            string json = ExecutaOperacao(OPERACAO_GET, endereco, true);

            return json;
        }

        /// <summary>
        /// Executa a operação PUT do Json
        /// </summary>
        /// <param name="objetoASerializar">Objeto que será serializado e enviado no PUT</param>
        /// <param name="endereco">Endereço da url do método da API</param>
        /// <param name="usaUrlApi">Verifica se será necessário concatenar o endereço passado com a url padrão da API</param>
        /// <returns>Json serializado</returns>
        public static string Coloca(object objetoASerializar, string endereco, bool usaUrlApi)
        {
            string json = ExecutaOperacao(OPERACAO_PUT, endereco, usaUrlApi, objetoASerializar);

            return json;
        }

        /// <summary>
        /// Executa a operação DELETE do Json
        /// </summary>
        /// <param name="endereco">Endereço da url do método da API</param>
        public static void Exclui(string endereco)
        {
            ExecutaOperacao(OPERACAO_DELETE, endereco, true);
        }

        /// <summary>
        /// Serializa um objeto qualquer em um "json"
        /// </summary>
        /// <param name="objetoASerializar">Objeto que será serializado</param>
        /// <returns>Json serializado</returns>
        public static string Serializa(object objetoASerializar)
        {
            return JsonConvert.SerializeObject(objetoASerializar, Formatting.Indented);
        }

        /// <summary>
        /// Desserializa um "json" no tipo que foi passado na chamada desse método
        /// </summary>
        /// <typeparam name="T">Tipo no qual o json deverá ser desserializado</typeparam>
        /// <param name="json">Json para desserializar</param>
        /// <returns>Json desserializado</returns>
        public static object Desserializa<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Desserializa um "json" em um tipo dinâmico, que será montado de acordo com as informações contidas no próprio "json".
        /// </summary>
        /// <param name="json">Json para desserializar</param>
        /// <returns>Json desserializado</returns>
        public static object Desserializa(string json)
        {
            return JObject.Parse(json);
        }

        /// <summary>
        /// Executa a operação Json informada e retorna o seu resultado
        /// </summary>
        /// <param name="operacao">Método de requisição</param>
        /// <param name="endereco">Endereço da url do método da API</param>
        /// <param name="usaUrlApi">Verifica se será necessário concatenar o endereço passado com a url padrão da API</param>
        /// <param name="objetoASerializar">Objeto que será serializado e enviado no POST</param>
        /// <param name="upload">Verifica se é uma operação de upload</param>
        /// <returns>Json serializado</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "A implementação está correta.")]
        private static string ExecutaOperacao(string operacao, string endereco, bool usaUrlApi, object objetoASerializar = null, bool upload = false)
        {
            Usuario usuario = (Usuario)HttpContext.Current.Session["Usuario"];
            string retorno = string.Empty;
            string url = usaUrlApi ? (URL_API + endereco) : endereco;
            string usuarioConta = usuario.UsuarioSpedia;
            string senhaConta = usuario.SenhaSpedia;

            HttpWebRequest requisicao = (HttpWebRequest)WebRequest.Create(url);
            requisicao.Credentials = new System.Net.NetworkCredential(usuario.Nome + "@" + usuarioConta, senhaConta);
            requisicao.ContentType = ObtemTipoConteudo(operacao, usaUrlApi, upload);
            requisicao.Method = operacao;
            ////requisicao.KeepAlive = false;
            ////requisicao.Timeout = 600 * 1000; //// Limite de tempo de requisição de 10 minutos
            ////requisicao.ServicePoint.ConnectionLimit = 1000;

            if ((operacao == OPERACAO_POST || operacao == OPERACAO_PUT) && objetoASerializar == null)
            {
                requisicao.ContentLength = 0;
            }

            if (objetoASerializar != null)
            {
                string json = objetoASerializar.GetType() == typeof(string) ? objetoASerializar.ToString() : Serializa(objetoASerializar);

                StreamWriter escritor = new StreamWriter(requisicao.GetRequestStream());

                escritor.Write(json);
                escritor.Flush();
                escritor.Close();
                
                escritor.Dispose();
            }

            StreamReader leitor = null;

            try
            {
                HttpWebResponse resposta = (HttpWebResponse)requisicao.GetResponse();
                leitor = new StreamReader(resposta.GetResponseStream());

                retorno = leitor.ReadToEnd();
            }
            catch (WebException e)
            {
                using (var stream = e.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    Log.Info(reader.ReadToEnd());
                }

                throw;
            }
            finally
            {
                if (leitor != null)
                {
                    leitor.Dispose();
                }
            }

            return retorno;
        }

        /// <summary>
        /// Obtém o tipo de conteúdo para montagem do cabeçalho da requisição
        /// </summary>
        /// <param name="operacao">Método de requisição</param>
        /// <param name="usaUrlApi">Verifica se será necessário concatenar o endereço passado com a url padrão da API</param>
        /// <param name="upload">Verifica se é uma operação de upload</param>
        /// <returns>Tipo de conteúdo</returns>
        private static string ObtemTipoConteudo(string operacao, bool usaUrlApi, bool upload)
        {
            string tipoConteudo = string.Empty;

            if (operacao == OPERACAO_PUT)
            {
                tipoConteudo = usaUrlApi ? "application/json" : "text/plain";
            }
            else
            {
                tipoConteudo = upload ? "text/xml" : "text/json";
            }

            return tipoConteudo;
        }
    }
}
