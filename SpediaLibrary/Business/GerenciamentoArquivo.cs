////-----------------------------------------------------------------------
//// <copyright file="GerenciamentoArquivo.cs" company="SpediA">
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
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using log4net;
    using SpediaLibrary.Transfer;
    using SpediaLibrary.Util;

    /// <summary>
    /// Classe responsável pelas regras de negócio de arquivo
    /// </summary>
    public static class GerenciamentoArquivo
    {
        /// <summary> Objeto da biblioteca log4net para registro de log da aplicação </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(GerenciamentoArquivo));

        /// <summary>
        /// Realiza a busca de arquivos Dfe na API, dados os filtros escolhidos pelo usuário
        /// </summary>
        /// <param name="filtro">Objeto filtro com todos os critérios da pesquisa</param>
        /// <param name="inicio">Índice do primeiro registro a ser retornado</param>
        /// <param name="quantidade">Quantidade máxima de registros que deverá retornar</param>
        /// <returns>Lista de arquivos encontrados</returns>
        public static ResultadoBusca BuscaArquivosDfe(FiltroBusca filtro, int inicio, int quantidade)
        {
            string json = AuxiliarJson.Posta(filtro, EnderecosApi.BuscaDfe + "/" + inicio + "/" + quantidade);

            return (ResultadoBusca)AuxiliarJson.Desserializa<ResultadoBusca>(json);
        }

        /// <summary>
        /// Realiza a busca de arquivos SPED na API, dados os filtros escolhidos pelo usuário
        /// </summary>
        /// <param name="filtro">Objeto filtro com todos os critérios da pesquisa</param>
        /// <param name="inicio">Índice do primeiro registro a ser retornado</param>
        /// <param name="quantidade">Quantidade máxima de registros que deverá retornar</param>
        /// <returns>Lista de arquivos encontrados</returns>
        public static ResultadoBuscaSped BuscaArquivosSped(FiltroBuscaSped filtro, int inicio, int quantidade)
        {
            string json = AuxiliarJson.Posta(filtro, EnderecosApi.BuscaSped + "/" + inicio + "/" + quantidade);

            return (ResultadoBuscaSped)AuxiliarJson.Desserializa<ResultadoBuscaSped>(json);
        }

        /// <summary>
        /// Obtem o id e a url gerada pela API para fazer o "put" do arquivo
        /// </summary>
        /// <param name="nomeArquivo">Nome do arquivo inserido para upload</param>
        /// <returns>Identificador e a url para upload de um arquivo</returns>
        public static Parametro ObtemParametrosUpload(string nomeArquivo)
        {
            string json = AuxiliarJson.Obtem(EnderecosApi.Upload + "/" + nomeArquivo);
            dynamic parametrosUploadDinamico = AuxiliarJson.Desserializa(json);

            return new Parametro() { Id = parametrosUploadDinamico.id, Valor = parametrosUploadDinamico.url };
        }

        /// <summary>
        /// Obtém o endereço local em que o arquivo será salvo
        /// </summary>
        /// <param name="idUsuario">Identificador do usuário</param>
        /// <param name="nomeArquivo">Nome do arquivo</param>
        /// <returns>Endereço completo onde o arquivo será salvo</returns>
        public static string ObtemEnderecoArquivo(int idUsuario, string nomeArquivo)
        {
            string endereco = ConfiguracaoAplicacao.EnderecoDiretorioTemporario;
            string enderecoCompleto = endereco + "\\" + nomeArquivo;

            return enderecoCompleto;
        }

        /// <summary>
        /// Executa um PUT na url retornada pela API para realizar o upload do arquivo
        /// </summary>
        /// <param name="urlUpload">Url gerada pela Api</param>
        /// <param name="nomeArquivo">Endereço do arquivo que será feito o upload</param>
        public static void ExecutaUploadApi(string urlUpload, string nomeArquivo)
        {
            using (var webclient = new System.Net.WebClient())
            {
                webclient.Headers.Add("Content-Type", "text/plain");
                byte[] result = webclient.UploadFile(urlUpload, "PUT", nomeArquivo);
            }
        }

        /// <summary>
        /// Informa para a plataforma que o upload do arquivo foi finalizado
        /// </summary>
        /// <param name="idArquivo">Identificador do arquivo que foi feito o upload</param>
        public static void InformaUploadConcluido(int idArquivo)
        {
            AuxiliarJson.Posta(null, EnderecosApi.Upload + "/" + idArquivo + "/concluido", true);
        }

        /// <summary>
        /// Realiza a consulta das Dfe's na secretaria da fazenda
        /// </summary>
        /// <param name="arquivos">Lista de identificador de arquivos</param>
        /// <returns>Lista de arquivos com problema</returns>
        public static IList<int> ConsultaSefaz(List<int> arquivos)
        {
            string json = AuxiliarJson.Posta(arquivos, EnderecosApi.ConsultaSefaz);

            return (List<int>)AuxiliarJson.Desserializa<List<int>>(json);
        }

        /// <summary>
        /// Solicita a assinatura dos SPED's
        /// </summary>
        /// <param name="assinatura">Estrutura com as informações necessárias para solicitação de assinatura</param>
        /// <returns>Lista de arquivos rejeitados</returns>
        public static IList<int> AssinaSped(AssinaturaSped assinatura)
        {
            string json = AuxiliarJson.Posta(assinatura, EnderecosApi.Assina);

            return (List<int>)AuxiliarJson.Desserializa<List<int>>(json);
        }

        /// <summary>
        /// Obtem o link para download dos arquivos escolhidos
        /// </summary>
        /// <param name="arquivos">Lista de identificador de arquivos</param>
        /// <returns>Link do download</returns>
        public static string ObtemLinkParaDownload(List<int> arquivos)
        {
            string json = AuxiliarJson.Posta(arquivos, EnderecosApi.Download + "/" + true);

            return (string)AuxiliarJson.Desserializa<string>(json);
        }

        /// <summary>
        /// Realiza a consulta das propriedades do objeto informado
        /// </summary>
        /// <param name="idObjeto">Identificador do objeto</param>
        /// <returns>Propriedades e detalhes do objeto</returns>
        public static PropriedadeObjeto ObtemPropriedadesObjeto(int idObjeto)
        {
            string json = AuxiliarJson.Obtem(EnderecosApi.Objeto + "/" + idObjeto);

            return (PropriedadeObjeto)AuxiliarJson.Desserializa<PropriedadeObjeto>(json);
        }

        /// <summary>
        /// Procura dentro da propriedade do objeto todos os arquivo que estiverem "pendurados" nele
        /// </summary>
        /// <param name="propriedadeObjeto">Objeto a ser verificado</param>
        /// <param name="arquivos">Lista de arquivos pertencentes ao objeto</param>
        public static void ExtraiDocumentosObjeto(PropriedadeObjeto propriedadeObjeto, ref List<AcaoArquivo> arquivos)
        {
            if (arquivos == null)
            {
                arquivos = new List<AcaoArquivo>();
            }

            if (propriedadeObjeto.Acoes != null && propriedadeObjeto.Acoes.Count > 0)
            {
                foreach (PropriedadeAcao propriedadeAcao in propriedadeObjeto.Acoes)
                {
                    if (propriedadeAcao.Arquivos != null && propriedadeAcao.Arquivos.Count > 0)
                    {
                        foreach (PropriedadeObjeto lPropriedadeObjeto in propriedadeAcao.Arquivos)
                        {
                            if (lPropriedadeObjeto.Acoes != null && lPropriedadeObjeto.Acoes.Count > 0)
                            {
                                ExtraiDocumentosObjeto(lPropriedadeObjeto, ref arquivos);
                            }

                            AcaoArquivo arquivo = null;

                            if (!arquivos.Any(a => a.TipoAcao == propriedadeAcao.TipoAcao))
                            {
                                arquivo = new AcaoArquivo()
                                {
                                    TipoAcao = propriedadeAcao.TipoAcao,
                                    DataExecucao = propriedadeAcao.ExecutadoEm,
                                    IdArquivos = new List<int>()
                                };

                                arquivos.Add(arquivo);
                            }
                            else
                            {
                                arquivo = arquivos.First(a => a.TipoAcao == propriedadeAcao.TipoAcao);
                            }

                            arquivo.IdArquivos.Add(lPropriedadeObjeto.IdArquivo);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Realiza a consulta das propriedades do objeto informado
        /// </summary>
        /// <param name="arquivoBinario">Formato binário do arquivo do certificado digital</param>
        /// <returns>Propriedades e detalhes do objeto</returns>
        public static string ObtemChaveCertificadoDigital(byte[] arquivoBinario)
        {
            return Convert.ToBase64String(arquivoBinario);
        }
    }
}
