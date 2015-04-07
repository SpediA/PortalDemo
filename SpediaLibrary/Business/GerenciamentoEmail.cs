////-----------------------------------------------------------------------
//// <copyright file="GerenciamentoEmail.cs" company="SpediA">
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
    using System.IO;
    using System.Linq;
    using System.Net.Mail;
    using System.Text;
    using System.Threading.Tasks;
    using log4net;
    using SpediaLibrary.Util;

    /// <summary>
    /// Classe responsável pelas regras de e-mails
    /// </summary>
    public static class GerenciamentoEmail
    {
        #region Constantes

        /// <summary> Representa o assunto do e-mail de novo usuário </summary>
        private const string ASSUNTO_NOVO_USUARIO = "Dados para acesso SpediA";

        /// <summary> Representa o assunto do e-mail de novo usuário </summary>
        private const string ASSUNTO_RECUPERACAO_SENHA = "SpediA - Solicitação de Recuperação de Senha";

        /// <summary> Representa o endereço do template do e-mail de novo usuário </summary>
        private const string TEMPLATE_NOVO_USUARIO = "\\NovoUsuario.html";

        /// <summary> Representa o endereço do template do e-mail de recuperação de senha </summary>
        private const string TEMPLATE_RECUPERACAO_SENHA = "\\RecuperacaoSenha.html";

        #endregion

        /// <summary> Objeto da biblioteca log4net para registro de log da aplicação </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(GerenciamentoEmail));

        /// <summary>
        /// Configura os atributos do e-mail para poder enviá-lo
        /// </summary>
        /// <param name="emailDestinatario">Endereço de e-mail do destinatário.</param>
        /// <param name="nomeDestinatario">Nome do destinatário.</param>
        /// <param name="usuarioDestinatario">Usuário do destinatário.</param>
        /// <param name="senhaDestinatario">Nova senha do destinatário.</param>
        public static void EnviaEmailNovoUsuario(string emailDestinatario, string nomeDestinatario, string usuarioDestinatario, string senhaDestinatario)
        {
            MailMessage email = new MailMessage();

            email.To.Add(new MailAddress(emailDestinatario));
            email.Subject = ASSUNTO_NOVO_USUARIO;
            email.Body = MontaCorpoEmailNovoUsuario(nomeDestinatario, usuarioDestinatario, senhaDestinatario);
            email.IsBodyHtml = true;

            EnviaEmail(email);
        }

        /// <summary>
        /// Configura os atributos do e-mail para poder enviá-lo
        /// </summary>
        /// <param name="emailDestinatario">Endereço de e-mail do destinatário.</param>
        /// <param name="nomeDestinatario">Nome do destinatário.</param>
        /// <param name="usuarioDestinatario">Usuário do destinatário.</param>
        /// <param name="senhaDestinatario">Nova senha do destinatário.</param>
        public static void EnviaEmailRecuperacaoSenha(string emailDestinatario, string nomeDestinatario, string usuarioDestinatario, string senhaDestinatario)
        {
            MailMessage email = new MailMessage();

            email.To.Add(new MailAddress(emailDestinatario));
            email.Subject = ASSUNTO_RECUPERACAO_SENHA;
            email.Body = MontaCorpoEmailRecuperacaoSenha(nomeDestinatario, usuarioDestinatario, senhaDestinatario);
            email.IsBodyHtml = true;

            EnviaEmail(email);
        }

        /// <summary>
        /// Envia o e-mail
        /// </summary>
        /// <param name="mensagemEmail">Mensagem de e-mail com todos os seus atributos.</param>
        private static void EnviaEmail(MailMessage mensagemEmail)
        {
            SmtpClient smtp = new SmtpClient();

            smtp.Send(mensagemEmail);
        }

        /// <summary>
        /// Monta a estrutura do e-mail para novos usuários
        /// </summary>
        /// <param name="nome">Nome do destinatário.</param>
        /// <param name="usuario">Usuário do destinatário.</param>
        /// <param name="senha">Nova senha do destinatário.</param>
        /// <returns>Corpo do e-mail</returns>
        private static string MontaCorpoEmailNovoUsuario(string nome, string usuario, string senha)
        {
            Dictionary<string, string> variaveis = new Dictionary<string, string>();
            string enderecoTemplate = ConfiguracaoAplicacao.EnderecoTemplate + TEMPLATE_NOVO_USUARIO;
            string corpo;

            variaveis.Add("NOME", nome);
            variaveis.Add("USUARIO", usuario);
            variaveis.Add("SENHA", senha);
            variaveis.Add("ENDERECO", ConfiguracaoAplicacao.EnderecoAplicacao);

            corpo = Mescla(enderecoTemplate, variaveis);

            return corpo;
        }

        /// <summary>
        /// Monta a estrutura do e-mail de recuperação de senha
        /// </summary>
        /// <param name="nome">Nome do destinatário.</param>
        /// <param name="usuario">Usuário do destinatário.</param>
        /// <param name="senha">Nova senha do destinatário.</param>
        /// <returns>Corpo do e-mail</returns>
        private static string MontaCorpoEmailRecuperacaoSenha(string nome, string usuario, string senha)
        {
            Dictionary<string, string> variaveis = new Dictionary<string, string>();
            string enderecoTemplate = ConfiguracaoAplicacao.EnderecoTemplate + TEMPLATE_RECUPERACAO_SENHA;
            string corpo;

            variaveis.Add("NOME", nome);
            variaveis.Add("USUARIO", usuario);
            variaveis.Add("SENHA", senha);
            variaveis.Add("ENDERECO", ConfiguracaoAplicacao.EnderecoAplicacao);

            corpo = Mescla(enderecoTemplate, variaveis);

            return corpo;
        }

        /// <summary>
        /// Monta a estrutura do e-mail antes de enviar
        /// </summary>
        /// <param name="template">Template do corpo do e-mail.</param>
        /// <param name="variaveis">Valores e nomes das variáveis que serão inseridas no template.</param>
        /// <returns>Corpo do e-mail com as variáveis substituídas</returns>
        private static string Mescla(string template, Dictionary<string, string> variaveis)
        {
            string texto = ObtemTemplate(template);

            foreach (KeyValuePair<string, string> variavel in variaveis)
            {
                texto = texto.Replace("#" + variavel.Key + "#", variavel.Value);
            }

            return texto;
        }

        /// <summary>
        /// Obtém o template do corpo do e-mail
        /// </summary>
        /// <param name="template">Nome do template</param>
        /// <returns>Template corpo do e-mail</returns>
        private static string ObtemTemplate(string template)
        {
            StreamReader sr = File.OpenText(template);
            string texto = null;

            try
            {
                texto = sr.ReadToEnd();
            }
            finally
            {
                sr.Close();
            }

            return texto;
        }
    }
}
