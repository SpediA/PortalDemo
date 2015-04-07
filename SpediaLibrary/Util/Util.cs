////-----------------------------------------------------------------------
//// <copyright file="Util.cs" company="SpediA">
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
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using log4net;
    using SpediaLibrary.Transfer;

    /// <summary>
    /// Classe responsável pelas regras de negócio de empresa
    /// </summary>
    public static class Util
    {
        /// <summary> Objeto da biblioteca log4net para registro de log da aplicação </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(Util));

        /// <summary>
        /// Limpa um texto deixando somente dígitos
        /// </summary>
        /// <param name="texto">Texto a ser limpo</param>
        /// <returns>Texto somente com dígitos</returns>
        public static string ObtemSomenteDigitos(string texto)
        {
            return new string(texto.Where(char.IsDigit).ToArray());
        }

        /// <summary>
        /// Formata um CNPJ somente com números para o formato padrão com ".", "/" e "-"
        /// </summary>
        /// <param name="cnpj">Número do CNPJ</param>
        /// <returns>CNPJ formatado</returns>
        public static string FormataCnpj(string cnpj)
        {
            return string.IsNullOrEmpty(cnpj) ? cnpj : Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00");
        }

        /// <summary>
        /// Monta objetos do tipo intervalo de valores
        /// </summary>
        /// <param name="valorDe">Valor inicial do intervalo</param>
        /// <param name="valorAte">Valor final do intervalo</param>
        /// <returns>Objeto do tipo intervalo de valores</returns>
        public static ValorIntervalo MontaValorIntervalo(string valorDe, string valorAte)
        {
            ValorIntervalo valorIntervalo = null;
            valorDe = Util.ObtemSomenteDigitos(valorDe);
            valorAte = Util.ObtemSomenteDigitos(valorAte);

            if (!string.IsNullOrEmpty(valorDe))
            {
                valorIntervalo = new ValorIntervalo()
                {
                    De = Convert.ToDecimal(valorDe)
                };
            }

            if (!string.IsNullOrEmpty(valorAte))
            {
                if (valorIntervalo == null)
                {
                    valorIntervalo = new ValorIntervalo();
                }

                valorIntervalo.Ate = Convert.ToDecimal(valorAte);
            }

            return valorIntervalo;
        }

        /// <summary>
        /// Monta objetos do tipo intervalo de datas
        /// </summary>
        /// <param name="dataDe">Data inicial do intervalo</param>
        /// <param name="dataAte">Data final do intervalo</param>
        /// <returns>Objeto do tipo intervalo de datas</returns>
        public static DataIntervalo MontaDataIntervalo(string dataDe, string dataAte)
        {
            DataIntervalo dataIntervalo = null;

            if (!string.IsNullOrEmpty(dataDe))
            {
                dataIntervalo = new DataIntervalo()
                {
                    De = Convert.ToDateTime(dataDe)
                };
            }

            if (!string.IsNullOrEmpty(dataAte))
            {
                if (dataIntervalo == null)
                {
                    dataIntervalo = new DataIntervalo();
                }

                // O intervalo inclui hora, então adicionamos um dia na data até
                dataIntervalo.Ate = Convert.ToDateTime(dataAte).AddDays(1);
            }

            return dataIntervalo;
        }

        /// <summary>
        /// Monta objetos do tipo intervalo de datas, baseado nas opções pré-definidas
        /// </summary>
        /// <param name="valorSelecionado">Valor selecionado no controle de intervalo de datas de recepção</param>
        /// <returns>Objeto do tipo intervalo de datas</returns>
        public static DataIntervalo MontaDataIntervaloOpcao(string valorSelecionado)
        {
            DataIntervalo dataIntervalo = new DataIntervalo();
            DateTime dataAtual = DateTime.Now;

            dataIntervalo.Ate = dataAtual;

            if (valorSelecionado == "Ultimas24Horas")
            {
                dataIntervalo.De = dataAtual.AddDays(-1);
            }
            else if (valorSelecionado == "Ultimos7Dias")
            {
                dataIntervalo.De = dataAtual.AddDays(-7);
            }
            else if (valorSelecionado == "Ultimos30Dias")
            {
                dataIntervalo.De = dataAtual.AddDays(-30);
            }

            return dataIntervalo;
        }

        /// <summary>
        /// Verifica se o e-mail passado está um um formato válido
        /// </summary>
        /// <param name="email">Endereço de e-mail</param>
        /// <returns>Retorna se é um formato válido de e-mail</returns>
        public static bool EEmailValido(string email)
        {
            string expressao = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

            try
            {
                return Regex.IsMatch(email, expressao, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (Exception ex)
            {
                Log.Info(ex.InnerException == null ? ex.Message : ex.InnerException.ToString());
                return false;
            }
        }
    }
}
