////-----------------------------------------------------------------------
//// <copyright file="Dominio.cs" company="SpediA">
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
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Permissões de usuário
    /// </summary>
    public enum PerfilUsuario
    {
        /// <summary>Representa o perfil de usuário administrador.</summary>
        [DescriptionAttribute("Administrador")]
        Administrador = 1,

        /// <summary>Representa o perfil de usuário operador.</summary>
        [DescriptionAttribute("Operação")]
        Operacao = 2
    }

    /// <summary>
    /// Status dos módulos da plataforma
    /// </summary>
    public enum TipoStatusPlataforma
    {
        /// <summary>Representa o status da plataforma quando está tudo funcionando.</summary>
        [DescriptionAttribute("Operante")]
        Ok = 1,

        /// <summary>Representa o status da plataforma quando está funcionando parcialmente.</summary>
        [DescriptionAttribute("Operando Parcialmente")]
        Atencao = 2,

        /// <summary>Representa o status da plataforma quando nada está funcionando.</summary>
        [DescriptionAttribute("Inoperante")]
        Erro = 3,
    }

    /// <summary>
    /// Tipos de arquivo
    /// </summary>
    public enum TipoArquivo
    {
        /// <summary>Representa o arquivo que sofreu upload.</summary>
        [DescriptionAttribute("Upload")]
        Upload = 0,

        /// <summary>Representa o arquivo de escrituração contábil digital.</summary>
        [DescriptionAttribute("ECD")]
        Ecd = 1,

        /// <summary>Representa o arquivo de escrituração fiscal digital.</summary>
        [DescriptionAttribute("EFD")]
        Efd = 2,

        /// <summary>Representa o arquivo de escrituração fiscal digital.</summary>
        [DescriptionAttribute("Nfe")]
        Nfe = 4,

        /// <summary>Representa o arquivo de escrituração fiscal digital para PIS/COFINS.</summary>
        [DescriptionAttribute("EFD PIS/COFINS")]
        EfdPisCofins = 6,

        /// <summary>Representa um arquivo compactado.</summary>
        [DescriptionAttribute("Compactado")]
        Compactado = 10,

        /// <summary>Representa arquivo desconhecido.</summary>
        [DescriptionAttribute("Desconhecido")]
        Desconhecido = 100,

        /// <summary>Representa o arquivo que sofreu upload em duplicidade.</summary>
        [DescriptionAttribute("Duplicado")]
        Duplicado = 101,

        /// <summary>Representa o arquivo que foi consultado o seu status na Sefaz.</summary>
        [DescriptionAttribute("Consulta do Status na Sefaz")]
        ConsultaStatusSefaz = 102,

        /// <summary>Representa o arquivo que foi validado pelo PVA.</summary>
        [DescriptionAttribute("Evidência do PVA")]
        EvidenciaPva = 103
    }

    /// <summary>
    /// Situações de um arquivo
    /// </summary>
    public enum StatusArquivo
    {
        /// <summary>Representa que o arquivo está em processo de upload.</summary>
        [DescriptionAttribute("Upload")]
        Upload = 0,

        /// <summary>Representa que o arquivo está em processo de identificação.</summary>
        [DescriptionAttribute("Identificando")]
        Identificando = 1,

        /// <summary>Representa que o arquivo já foi reconhecido.</summary>
        [DescriptionAttribute("Reconhecido")]
        Reconhecido = 2,

        /// <summary>Representa que o arquivo passou no processo de validação.</summary>
        [DescriptionAttribute("Valido")]
        Valido = 3,

        /// <summary>Representa que o arquivo não passou no processo de validação.</summary>
        [DescriptionAttribute("Invalido")]
        Invalido = 4,

        /// <summary>Representa que o arquivo possui arquivo em anexo.</summary>
        [DescriptionAttribute("Arquivo Anexo")]
        ArquivoAnexo = 100
    }

    /// <summary>
    /// Situações resultantes do processamento do PVA
    /// </summary>
    public enum StatusPva
    {
        /// <summary>Representa que o arquivo que foi processado pelo PVA com sucesso.</summary>
        [DescriptionAttribute("Validado com Sucesso")]
        ValidadoSucesso = 1,

        /// <summary>Representa que o arquivo que foi processado pelo PVA com advertência.</summary>
        [DescriptionAttribute("Validado com Advertência")]
        ValidadoAdvertencia = 2,

        /// <summary>Representa que o arquivo que foi processado pelo PVA com erro.</summary>
        [DescriptionAttribute("Validado com Erro")]
        ValidadoErro = 3,

        /// <summary>Representa que o arquivo que foi rejeitado pelo PVA.</summary>
        [DescriptionAttribute("Rejeitado")]
        Rejeitado = 4
    }

    /// <summary>
    /// Situações de um SPED
    /// </summary>
    public enum StatusSped
    {
        /// <summary>Representa um SPED que ainda não passou pelo PVA.</summary>
        [DescriptionAttribute("PVA Não Executado")]
        PVANaoExecutado = 1,

        /// <summary>Representa um SPED que foi validado.</summary>
        [DescriptionAttribute("Validado")]
        Validado = 2,

        /// <summary>Representa um SPED assinado.</summary>
        [DescriptionAttribute("Assinado")]
        Assinado = 3,

        /// <summary>Representa um SPED que já foi transmitido.</summary>
        [DescriptionAttribute("Transmitido")]
        Transmitido = 4
    }

    /// <summary>
    /// Tipos de finalidade de operação DFe
    /// </summary>
    public enum FinalidadeOperacaoDfe
    {
        /// <summary>Representa que uma operação de NF-e normal.</summary>
        [DescriptionAttribute("NF-e Normal")]
        NfeNormal = 1,

        /// <summary>Representa que uma operação de NF-e complementar.</summary>
        [DescriptionAttribute("NF-e Complementar")]
        NfeComplementar = 2,

        /// <summary>Representa que uma operação de NF-e de ajuste.</summary>
        [DescriptionAttribute("NF-e de Ajuste")]
        NfeAjuste = 3,

        /// <summary>Representa que uma operação de devolução de mercadoria.</summary>
        [DescriptionAttribute("Devolução de Mercadoria")]
        DevolucaoMercadoria = 4
    }

    /// <summary>
    /// Tipos de finalidade de operação SPED
    /// </summary>
    public enum FinalidadeOperacaoSped
    {
        /// <summary>Representa que uma operação de SPED original.</summary>
        [DescriptionAttribute("Original")]
        Original = 0,

        /// <summary>Representa que uma operação de SPED retificadora.</summary>
        [DescriptionAttribute("Retificador")]
        Retificador = 1
    }

    /// <summary>
    /// Tipos de operação
    /// </summary>
    public enum TipoOperacao
    {
        /// <summary>Representa o documento emitido pela própria empresa.</summary>
        [DescriptionAttribute("Próprios")]
        Emitente = 1,

        /// <summary>Representa o documento emitido por terceiros.</summary>
        [DescriptionAttribute("Terceiros")]
        Destinatario = 2,

        /// <summary>Representa documento SPED.</summary>
        [DescriptionAttribute("Entidade")]
        Entidade = 3
    }

    /// <summary>
    /// Tipos de operação de DFe
    /// </summary>
    public enum TipoOperacaoDfe
    {
        /// <summary>Representa uma operação de entrada para uma DFe.</summary>
        [DescriptionAttribute("Entrada")]
        Entrada = 0,

        /// <summary>Representa uma operação de saída para uma DFe.</summary>
        [DescriptionAttribute("Saída")]
        Saida = 1
    }

    /// <summary>
    /// Tipos de emissão
    /// </summary>
    public enum TipoEmissao
    {
        /// <summary>Representa um documento emitido pela própria empresa.</summary>
        [DescriptionAttribute("Própria")]
        Propria = 1,

        /// <summary>Representa um documento emitido por terceiros.</summary>
        [DescriptionAttribute("Terceiros")]
        Terceiros = 2
    }

    /// <summary>
    /// Tipos de escrituração
    /// </summary>
    public enum TipoEscrituracao
    {
        /// <summary>Representa escrituração de ECD.</summary>
        [DescriptionAttribute("ECD")]
        Ecd = 1,

        /// <summary>Representa escrituração de EFD ICMS/IPI.</summary>
        [DescriptionAttribute("EFD ICMS/IPI")]
        EfdII = 2,

        /// <summary>Representa escrituração de EFD ICMS/IPI.</summary>
        [DescriptionAttribute("EFD Contribuições")]
        EfdPisCofins = 6
    }

    /// <summary>
    /// Tipos de notificação
    /// </summary>
    public enum TipoNotificacao
    {
        /// <summary>Representa uma notificação de quando há um novo arquivo.</summary>
        [DescriptionAttribute("Novo Arquivo Recebido")]
        NovoArquivoRecebido = 1,

        /// <summary>Representa uma notificação de quando o arquivo foi identificado.</summary>
        [DescriptionAttribute("Arquivo Identificado")]
        ArquivoIdentificado = 2,

        /// <summary>Representa uma notificação de quando o arquivo foi taggeado.</summary>
        [DescriptionAttribute("Arquivo Taggeado")]
        ArquivoTaggeado = 3,

        /// <summary>Representa uma notificação de quando o arquivo é SPED e foi validado.</summary>
        [DescriptionAttribute("SPED Validado")]
        SpedValidado = 4,

        /// <summary>Representa uma notificação de quando o arquivo é SPED e foi assinado.</summary>
        [DescriptionAttribute("SPED Assinado")]
        SpedAssinado = 5,

        /// <summary>Representa uma notificação de quando o arquivo é DFe e foi validado.</summary>
        [DescriptionAttribute("DFe Validado")]
        DfeValidado = 6,

        /// <summary>Representa uma notificação de quando o arquivo é DFe e foi consultado na Sefaz.</summary>
        [DescriptionAttribute("DFe Consultado Sefaz")]
        DfeConsultadoSefaz = 7
    }

    /// <summary>
    /// Tipos de ação
    /// </summary>
    public enum TipoAcao
    {
        /// <summary>Representa a ação de um arquivo foi identificado.</summary>
        [DescriptionAttribute("Arquivo Identificado")]
        ArquivoIdentificado = 1001,

        /// <summary>Representa a ação de extração das tags do arquivo.</summary>
        [DescriptionAttribute("Tags Extraídas")]
        TagsExtraidas = 1002,

        /// <summary>Representa a ação de descompactação de arquivo.</summary>
        [DescriptionAttribute("Arquivo Descompactado")]
        ArquivoDescompactado = 1003,

        /// <summary>Representa a ação de consulta na SEFAZ.</summary>
        [DescriptionAttribute("Consulta SEFAZ")]
        ConsultaSefaz = 1004,

        /// <summary>Representa a ação de um validação pelo PVA.</summary>
        [DescriptionAttribute("Validação PVA")]
        ValidacaoPva = 1005,

        /// <summary>Representa a ação de assinatura pelo PVA.</summary>
        [DescriptionAttribute("Assinatura PVA")]
        AssinaturaPva = 1006,

        /// <summary>Representa a ação de transmissão de SPED.</summary>
        [DescriptionAttribute("Transmissão do SPED")]
        TransmissaoSped = 1007
    }

    /// <summary>
    /// Unidades federativas
    /// </summary>
    public enum UnidadeFederativa
    {
        /// <summary>Representa a unidade federativa Acre.</summary>
        [DescriptionAttribute("AC")]
        AC = 1,

        /// <summary>Representa a unidade federativa Alagoas.</summary>
        [DescriptionAttribute("AL")]
        AL = 2,

        /// <summary>Representa a unidade federativa Amapá.</summary>
        [DescriptionAttribute("AP")]
        AP = 3,

        /// <summary>Representa a unidade federativa Amazonas.</summary>
        [DescriptionAttribute("AM")]
        AM = 4,

        /// <summary>Representa a unidade federativa Bahia.</summary>
        [DescriptionAttribute("BA")]
        BA = 5,

        /// <summary>Representa a unidade federativa Ceará.</summary>
        [DescriptionAttribute("CE")]
        CE = 6,

        /// <summary>Representa a unidade federativa Distrito Federal.</summary>
        [DescriptionAttribute("DF")]
        DF = 7,

        /// <summary>Representa a unidade federativa Espírito Santo.</summary>
        [DescriptionAttribute("ES")]
        ES = 8,

        /// <summary>Representa a unidade federativa Goiás.</summary>
        [DescriptionAttribute("GO")]
        GO = 9,

        /// <summary>Representa a unidade federativa Maranhão.</summary>
        [DescriptionAttribute("MA")]
        MA = 10,

        /// <summary>Representa a unidade federativa Mato Grosso.</summary>
        [DescriptionAttribute("MT")]
        MT = 11,

        /// <summary>Representa a unidade federativa Mato Grosso do Sul.</summary>
        [DescriptionAttribute("MS")]
        MS = 12,

        /// <summary>Representa a unidade federativa Minas Gerais.</summary>
        [DescriptionAttribute("MG")]
        MG = 13,

        /// <summary>Representa a unidade federativa Pará.</summary>
        [DescriptionAttribute("PA")]
        PA = 14,

        /// <summary>Representa a unidade federativa Paraíba.</summary>
        [DescriptionAttribute("PB")]
        PB = 15,

        /// <summary>Representa a unidade federativa Paraná.</summary>
        [DescriptionAttribute("PR")]
        PR = 16,
        
        /// <summary>Representa a unidade federativa Pernambuco.</summary>
        [DescriptionAttribute("PE")]
        PE = 17,

        /// <summary>Representa a unidade federativa Piauí.</summary>
        [DescriptionAttribute("PI")]
        PI = 18,

        /// <summary>Representa a unidade federativa Rio de Janeiro.</summary>
        [DescriptionAttribute("RJ")]
        RJ = 19,

        /// <summary>Representa a unidade federativa Rio Grande do Norte.</summary>
        [DescriptionAttribute("RN")]
        RN = 20,

        /// <summary>Representa a unidade federativa Rio Grande do Sul.</summary>
        [DescriptionAttribute("RS")]
        RS = 21,

        /// <summary>Representa a unidade federativa Rondônia.</summary>
        [DescriptionAttribute("RO")]
        RO = 22,

        /// <summary>Representa a unidade federativa Roraima.</summary>
        [DescriptionAttribute("RR")]
        RR = 23,

        /// <summary>Representa a unidade federativa Santa Catarina.</summary>
        [DescriptionAttribute("SC")]
        SC = 24,

        /// <summary>Representa a unidade federativa São Paulo.</summary>
        [DescriptionAttribute("SP")]
        SP = 25,

        /// <summary>Representa a unidade federativa Sergipe.</summary>
        [DescriptionAttribute("SE")]
        SE = 26,

        /// <summary>Representa a unidade federativa Tocantins.</summary>
        [DescriptionAttribute("TO")]
        TO = 27
    }

    /// <summary>
    /// Classe responsável por todo o gerenciamento de domínios
    /// </summary>
    public static class Dominio
    {
        /// <summary>
        /// Obtém o atributo "Description" (ou o nome em texto) de uma enumeração.
        /// </summary>
        /// <param name="valor">Valor da enumeração.</param>
        /// <returns>Atributo "Description" ou o valor texto</returns>
        public static string ValorTextoDe(Enum valor)
        {
            FieldInfo informacaoCampo;
            DescriptionAttribute[] atributos;

            if (valor != null)
            {
                informacaoCampo = valor.GetType().GetField(valor.ToString());
                atributos = (DescriptionAttribute[])informacaoCampo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (atributos.Length > 0)
                {
                    return atributos[0].Description;
                }
                else
                {
                    return valor.ToString();
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Obtém o valor enumeração baseado em um texto.
        /// </summary>
        /// <param name="valor">Text to return the enumeration value.</param>
        /// <param name="tipoEnum">Tipo da enumeração.</param>
        /// <returns>Atributo "Description" ou o valor texto</returns>
        public static object ValorEnumDe(string valor, Type tipoEnum)
        {
            string[] nomes = Enum.GetNames(tipoEnum);
            foreach (string nome in nomes)
            {
                if (ValorTextoDe((Enum)Enum.Parse(tipoEnum, nome)).Equals(valor))
                {
                    return Enum.Parse(tipoEnum, nome);
                }

                if (nome.Equals(valor))
                {
                    return Enum.Parse(tipoEnum, nome);
                }
            }

            throw new ArgumentException("O texto não é uma descrição ou valor do enum especificado.");
        }

        /// <summary>
        /// Obtém uma lista baseada na enumeração.
        /// </summary>
        /// <param name="tipoEnum">Tipo da enumeração.</param>
        /// <returns>Lista de valores da enumeração e descrições</returns>
        public static IDictionary<int, string> ObtemListaEnum(Type tipoEnum)
        {
            Dictionary<int, string> lista = new Dictionary<int, string>();

            foreach (string s in Enum.GetNames(tipoEnum))
            {
                object value = Enum.Parse(tipoEnum, s);
                int i = (int)value;
                string description = ValorTextoDe((Enum)value);

                lista.Add(i, description);
            }

            return lista;
        }
    }
}
