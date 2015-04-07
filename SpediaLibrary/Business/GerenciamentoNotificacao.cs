////-----------------------------------------------------------------------
//// <copyright file="GerenciamentoNotificacao.cs" company="SpediA">
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
    using SpediaLibrary.Persistence.Repository;
    using SpediaLibrary.Transfer;
    using SpediaLibrary.Util;

    /// <summary>
    /// Classe responsável pelas regras de negócio de notificação
    /// </summary>
    public static class GerenciamentoNotificacao
    {
        /// <summary> Objeto da biblioteca log4net para registro de log da aplicação </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(GerenciamentoNotificacao));

        /// <summary>
        /// Carrega, do banco de dados, as configurações de notificação para o usuário especificado
        /// </summary>
        /// <param name="idUsuario">Identificador do usuário</param>
        /// <returns>Configuração das notificações</returns>
        public static ControleNotificacao CarregaControleNotificacao(int idUsuario)
        {
            NotificacaoRepositorio notificacaoRepositorio = new NotificacaoRepositorio();

            return notificacaoRepositorio.Obtem(idUsuario);
        }

        /// <summary>
        /// Atualiza, no banco de dados, as configurações de notificação do usuário
        /// </summary>
        /// <param name="controleNotificacao">Objeto controle de notificação que será atualizado</param>
        /// <returns>Indica se a atualização foi bem sucedida</returns>
        public static bool AtualizaControleNotificacao(ControleNotificacao controleNotificacao)
        {
            NotificacaoRepositorio notificacaoRepositorio = new NotificacaoRepositorio();

            try
            {
                notificacaoRepositorio.Atualiza(controleNotificacao);

                return true;
            }
            catch (Exception ex)
            {
                Log.Info(ex.InnerException == null ? ex.Message : ex.InnerException.ToString());
                return false;
            }
        }

        /// <summary>
        /// Obtém as notificacões a partir do ponto solicitado
        /// </summary>
        /// <param name="aPartirDe">Ponto a partir do qual as notificações serão carregadas</param>
        /// <returns>Lista de notificações</returns>
        public static NotificacaoConta ObtemNotificacoes(long aPartirDe)
        {
            string json = AuxiliarJson.Obtem(EnderecosApi.Notificacao + aPartirDe);
            NotificacaoConta notificacaoConta = (NotificacaoConta)AuxiliarJson.Desserializa<NotificacaoConta>(json);

            return notificacaoConta;
        }
    }
}
