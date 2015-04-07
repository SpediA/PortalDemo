////-----------------------------------------------------------------------
//// <copyright file="Autenticacao.cs" company="SpediA">
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
    using System.Globalization;
    using System.Security.Cryptography;
    using System.Text;
    using log4net;

    /// <summary>
    /// Classe que trata toda a parte de autenticação e criptografia
    /// </summary>
    public static class Autenticacao
    {
        /// <summary> Conjunto de caracteres usados para gerar novas senhas </summary>
        private const string CARACTERES = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%&*()_+-=";

        /// <summary> Objeto da biblioteca log4net para registro de log da aplicação </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(Autenticacao));

        /// <summary> Objeto randômico para gerar novas senhas </summary>
        private static readonly Random Randomico = new Random();

        /// <summary>
        /// Gera a hash de SHA1 para uma determinada palavra
        /// </summary>
        /// <param name="palavra">Palavra para gerar a hash</param>
        /// <returns>SHA1 hash</returns>
        public static string ObtemSHA1Hash(string palavra)
        {
            SHA1 codificador = SHA1.Create();
            byte[] palavraCodificada = Encoding.Default.GetBytes(palavra);
            byte[] hash = codificador.ComputeHash(palavraCodificada);

            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Valida se uma palavra é igual ao hash armazenado
        /// </summary>
        /// <param name="palavra">Input data to be compared</param>
        /// <param name="palavraArmazenada">Stored hash data</param>
        /// <returns>A boolean indicating whether the input data is equal to the hash or not</returns>
        public static bool ValidaSHA1Hash(string palavra, string palavraArmazenada)
        {
            string hashPalavra = ObtemSHA1Hash(palavra);
            return string.Compare(hashPalavra, palavraArmazenada, StringComparison.CurrentCultureIgnoreCase) == 0;
        }

        /// <summary>
        /// Gera uma nova senha randômica
        /// </summary>
        /// <returns>Senha randômica</returns>
        public static string GeraSenhaRandomica()
        {
            int tamanho = 8;
            char[] buffer = new char[tamanho];

            for (int i = 0; i < tamanho; i++)
            {
                buffer[i] = CARACTERES[Randomico.Next(CARACTERES.Length)];
            }

            return new string(buffer);
        }
    }
}
