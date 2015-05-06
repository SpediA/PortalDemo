////-----------------------------------------------------------------------
//// <copyright file="GerenciamentoUsuario.cs" company="SpediA">
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
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using log4net;
    using Newtonsoft.Json;
    using SpediaLibrary.Persistence.Repository;
    using SpediaLibrary.Transfer;
    using SpediaLibrary.Util;

    /// <summary>
    /// Classe responsável pelas regras de negócio de usuário
    /// </summary>
    public static class GerenciamentoUsuario
    {
        /// <summary> Objeto da biblioteca log4net para registro de log da aplicação </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(GerenciamentoUsuario));

        /// <summary>
        /// Carrega, do banco de dados, os dados do usuário
        /// </summary>
        /// <param name="login">Nome de usuário</param>
        /// <param name="senha">Senha descriptografada</param>
        /// <returns>Objeto usuário</returns>
        public static Usuario CarregaUsuario(string login, string senha)
        {
            Usuario usuario;
            UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio();
            Aes aes = Aes.Create();

            Usuario parametro = new Usuario() 
            {
                Email = login
            };

            usuario = usuarioRepositorio.ObtemPorEmail(parametro);

            if (usuario != null && Autenticacao.ValidaSHA1Hash(senha, usuario.Senha))
            {
                return usuario;
            }

            return null;
        }

        /// <summary>
        /// Carrega, do banco de dados, os dados do usuário
        /// </summary>
        /// <param name="idUsuario">Identificador do usuário</param>
        /// <returns>Objeto usuário</returns>
        public static Usuario CarregaUsuario(int idUsuario)
        {
            UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio();

            Usuario parametro = new Usuario()
            {
                Id = idUsuario
            };

            return usuarioRepositorio.Obtem(parametro);
        }

        /// <summary>
        /// Carrega, do banco de dados, os dados do usuário
        /// </summary>
        /// <param name="nome">Nome do usuário</param>
        /// <returns>Objeto usuário</returns>
        public static Usuario CarregaUsuario(string nome)
        {
            Usuario usuario;
            UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio();

            Usuario parametro = new Usuario()
            {
                Nome = nome
            };

            usuario = usuarioRepositorio.ObtemPorNome(parametro);

            if (usuario != null)
            {
                return usuario;
            }

            return null;
        }

        /// <summary>
        /// Carrega, do banco de dados, o usuário que tenha o e-mail informado
        /// </summary>
        /// <param name="email">E-mail do usuário</param>
        /// <returns>Objeto usuário</returns>
        public static Usuario CarregaUsuarioPorEmail(string email)
        {
            Usuario usuario;
            UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio();

            Usuario parametro = new Usuario()
            {
                Email = email
            };

            usuario = usuarioRepositorio.ObtemPorEmail(parametro);

            if (usuario != null)
            {
                return usuario;
            }

            return null;
        }

        /// <summary>
        /// Carrega todos os usuários cadastrados
        /// </summary>
        /// <returns>Lista de usuários</returns>
        public static IList<Usuario> CarregaUsuarios()
        {
            UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio();
            List<Usuario> usuarios = (List<Usuario>)usuarioRepositorio.ObtemLista();
            
            return usuarios;
        }

        /// <summary>
        /// Carrega os usuários cadastrados com o mesmo usuário Spedia
        /// </summary>
        /// <param name="UsuarioSpedia">Filtro do usuário Spedia</param>
        /// <returns>Lista de usuários</returns>
        public static IList<Usuario> CarregaUsuarios(string UsuarioSpedia)
        {
            UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio();
            List<Usuario> usuarios = (List<Usuario>)usuarioRepositorio.ObtemPorUsuarioSpedia(UsuarioSpedia);

            return usuarios;
        }

        /// <summary>
        /// Carrega todos os usuários da API
        /// </summary>
        /// <returns>Lista de usuários da API</returns>
        public static IList<UsuarioApi> CarregaUsuariosApi()
        {
            string json = AuxiliarJson.Obtem(EnderecosApi.Usuario);
            List<UsuarioApi> usuarios = (List<UsuarioApi>)AuxiliarJson.Desserializa<List<UsuarioApi>>(json);

            return usuarios;
        }

        /// <summary>
        /// Cria, no banco de dados, um novo usuário
        /// </summary>
        /// <param name="usuario">Usuário que será criado</param>
        /// <param name="novaSenha">Senha gerada para o novo usuário</param>
        /// <returns>Retorna o usuário criado</returns>
        public static Usuario CriaUsuario(Usuario usuario, string novaSenha)
        {
            UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio();
            NotificacaoRepositorio notificacaoRepositorio = new NotificacaoRepositorio();
            ControleNotificacao controle;

            usuario.Senha = Autenticacao.ObtemSHA1Hash(novaSenha);

            try
            {
                usuarioRepositorio.Cria(usuario);

                controle = new ControleNotificacao()
                {
                    IdUsuario = usuario.Id.Value,
                    UltimaNotificacao = notificacaoRepositorio.ObtemUltimaNotificacaoArmazenada()
                };

                notificacaoRepositorio.Cria(controle);

                return usuario;
            }
            catch (Exception ex)
            {
                Log.Info(ex.InnerException == null ? ex.Message : ex.InnerException.ToString());
                return null;
            }
        }

        /// <summary>
        /// Exclui, do banco de dados, os dados do usuário
        /// </summary>
        /// <param name="usuario">Usuário que será excluído</param>
        /// <returns>Indica se a exclusão foi bem sucedida</returns>
        public static bool ExcluiUsuario(Usuario usuario)
        {
            UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio();

            try
            {
                usuarioRepositorio.Exclui(usuario);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Atualiza, no banco de dados, os dados do usuário
        /// </summary>
        /// <param name="usuario">Usuário que será atualizado</param>
        /// <returns>Indica se a atualização foi bem sucedida</returns>
        public static bool AtualizaUsuario(Usuario usuario)
        {
            UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio();

            try
            {
                usuarioRepositorio.Atualiza(usuario);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Cria um novo usuário na API
        /// </summary>
        /// <param name="usuario">Usuário base para a criação</param>
        /// <returns>Identificador do novo usuário na API</returns>
        public static int? CriaUsuarioApi(UsuarioApi usuario)
        {
            UsuarioApi novoUsuario;
            string json;

            try
            {
                json = AuxiliarJson.Posta(usuario, EnderecosApi.Usuario);
                novoUsuario = (UsuarioApi)AuxiliarJson.Desserializa<UsuarioApi>(json);
                return (int)novoUsuario.Id;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Exclui o usuário da API
        /// </summary>
        /// <param name="idUsuario">Identificador do usuário na API</param>
        /// <returns>Indica se a exclusão foi bem sucedida</returns>
        public static bool ExcluiUsuarioApi(int idUsuario)
        {
            try
            {
                AuxiliarJson.Exclui(EnderecosApi.Usuario + "/" + idUsuario.ToString());

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Edita um usuário da API
        /// </summary>
        /// <param name="usuario">Usuário base para a edição</param>
        /// <returns>Indica se a edição foi bem sucedida</returns>
        public static bool EditaUsuarioApi(Usuario usuario)
        {
            UsuarioApi usuarioApi = new UsuarioApi() { Id = usuario.IdApi, Email = usuario.Email, Nome = usuario.Nome };

            try
            {
                AuxiliarJson.Coloca(usuarioApi, EnderecosApi.Usuario + "/" + usuarioApi.Id, true);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
