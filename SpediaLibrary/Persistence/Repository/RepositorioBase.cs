////-----------------------------------------------------------------------
//// <copyright file="RepositorioBase.cs" company="SpediA">
//// Copyright [2014] [SPEDIA Soluções Tecnológicas Ltda]
//// Licenciado sob Licença Apache, Versão 2.0 (a "Licença"). Você não pode usar este arquivo exceto em conformidade com a Licença.
//// Você pode obter uma cópia da Licença em:
//// http://www.apache.org/licenses/LICENSE-2.0
//// Ao menos que seja exigido por lei aplicável ou com autorização por escrito, todo software distribuído sob a Licença é distribuído "COMO ESTÁ",
//// SEM GARANTIAS OU CONDIÇÕES DE NENHUMA ESPÉCIE, expressas ou implícitas.
//// Veja a Licença no idioma específico que estabelece as permissões e limitações sob a Licença.
//// </copyright>
////-----------------------------------------------------------------------
namespace SpediaLibrary.Persistence.Repository
{
    using System;
    using System.Collections.Generic;
    using log4net;
    using NHibernate;
    using SpediaLibrary.Transfer;

    /// <summary>
    /// Repositório base NHibernate
    /// </summary>
    /// <typeparam name="DT">Tipo de dado derivado da classe <see cref="SpediaLibrary.Transfer.ModeloBase" /></typeparam>
    public abstract class RepositorioBase<DT> where DT : ModeloBase
    {
        /// <summary> Sessão para ser usada nas operações de repositório </summary>
        protected readonly ISession Sessao;

        /// <summary>
        /// Inicia uma nova instância da classe <see cref="RepositorioBase{DT}"/>
        /// </summary>
        public RepositorioBase()
        {
            this.Sessao = AuxiliarNHibernate.ObtemFabricaSessao().GetCurrentSession();
        }

        /// <summary> 
        /// Obtém o objeto log
        /// </summary>
        protected ILog Log
        {
            get
            {
                return LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            }
        }

        /// <summary>
        /// Cria um novo objeto na base de dados
        /// </summary>
        /// <param name="dados">Dados a serem criados</param>
        public void Cria(DT dados)
        {
            try
            {
                this.Sessao.Save(dados);
            }
            catch (Exception e)
            {
                ////TRATAR DEPOIS
                this.Log.Error(e);
                throw;
            }
        }

        /// <summary>
        /// Atualiza o objeto na base de dados
        /// </summary>
        /// <param name="dados">Dados a serem atualizados</param>
        public void Exclui(DT dados)
        {
            try
            {
                this.Sessao.Delete(dados);
            }
            catch (Exception e)
            {
                ////TRATAR DEPOIS
                this.Log.Error(e);
                throw;
            }
        }

        /// <summary>
        /// Atualiza o objeto na base de dados
        /// </summary>
        /// <param name="dados">Dados a serem excluídos</param>
        public void Atualiza(DT dados)
        {
            try
            {
                this.Sessao.Update(dados);
            }
            catch (Exception e)
            {
                ////TRATAR DEPOIS
                this.Log.Error(e);
                throw;
            }
        }
    }
}
