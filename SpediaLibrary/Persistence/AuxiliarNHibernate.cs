////-----------------------------------------------------------------------
//// <copyright file="AuxiliarNHibernate.cs" company="SpediA">
//// Copyright [2014] [SPEDIA Soluções Tecnológicas Ltda]
//// Licenciado sob Licença Apache, Versão 2.0 (a "Licença"). Você não pode usar este arquivo exceto em conformidade com a Licença.
//// Você pode obter uma cópia da Licença em:
//// http://www.apache.org/licenses/LICENSE-2.0
//// Ao menos que seja exigido por lei aplicável ou com autorização por escrito, todo software distribuído sob a Licença é distribuído "COMO ESTÁ",
//// SEM GARANTIAS OU CONDIÇÕES DE NENHUMA ESPÉCIE, expressas ou implícitas.
//// Veja a Licença no idioma específico que estabelece as permissões e limitações sob a Licença.
//// </copyright>
////-----------------------------------------------------------------------
namespace SpediaLibrary.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Context;
    using SpediaLibrary.Transfer;

    /// <summary>
    /// Classe com métodos auxilares do NHibernate
    /// </summary>
    public class AuxiliarNHibernate
    {
        /// <summary> Objeto "fábrica" de sessão </summary>
        private static ISessionFactory fabricaSessao;

        /// <summary>
        /// Abre sessão do NHibernate
        /// </summary>
        /// <returns>Sessão do NHibernate aberta</returns>
        public static ISession AbreSessao()
        {
            return ObtemFabricaSessao().OpenSession();
        }

        /// <summary> 
        /// Obtém uma instância da "fábrica" de sessão do NHibernate
        /// </summary>
        /// <returns>Uma instância da "fábrica" de sessão do NHibernate</returns>
        public static ISessionFactory ObtemFabricaSessao()
        {
            if (fabricaSessao == null)
            {
                var configuracao = new Configuration();
                configuracao.Configure();
                configuracao.AddAssembly(Assembly.GetExecutingAssembly());
                fabricaSessao = configuracao.BuildSessionFactory();
            }

            return fabricaSessao;
        }
    }
}
