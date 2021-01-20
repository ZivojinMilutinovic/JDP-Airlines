using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JDP_Airlines
{
    public static class SessionManager
    {
        public static IAsyncSession session;

        public static IAsyncSession GetSession()
        {
            
                IDriver driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "admin"));
                session = driver.AsyncSession();

                return session;
        }
    }
}
