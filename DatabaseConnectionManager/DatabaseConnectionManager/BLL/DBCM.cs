using DatabaseConnectionManager.Objects;
using DatabaseConnectionManager.DAL;
using System.Data;

namespace DatabaseConnectionManager.BLL
{
    public class DBCM : IMethodsObligatory
    {
        private SQLServer sQLServer = null;
        private MySQL mySQL = null;
        private PostgreSQL postgreSQL = null;
        public DBCM()
        {
            Connection.Server = Connection.Database = Connection.UserId = Connection.Password = string.Empty;
            Connection.DBCM = Connection.Port = 0;
        }
        public DBCM(short DBCM, string Server, short Port, string Database, string UserId, string Password)
        {
            Connection.DBCM = DBCM;
            Connection.Server = Server;
            Connection.Port = Port;
            Connection.Database = Database;
            Connection.UserId = UserId;
            Connection.Password = Password;

            if (Connection.ValidateConnectionString())
                Initialize();
        }
        private void Initialize()
        {
            switch (Connection.DBCM)
            {
                case Servers.SQL_SERVER:
                    sQLServer = new SQLServer();
                    break;

                case Servers.MYSQL:
                    mySQL = new MySQL();
                    break;

                case Servers.POSTGRESQL:
                    postgreSQL = new PostgreSQL();
                    break;

                default:
                    break;
            }
        }
        public void Connect()
        {
            switch (Connection.DBCM)
            {
                case Servers.SQL_SERVER:
                    sQLServer.Connect();
                    break;

                case Servers.MYSQL:
                    mySQL.Connect();
                    break;

                case Servers.POSTGRESQL:
                    postgreSQL.Connect();
                    break;

                default:
                    //do nothing
                    break;
            }
        }
        public void Disconnect()
        {
            switch (Connection.DBCM)
            {
                case Servers.SQL_SERVER:
                    sQLServer.Disconnect();
                    break;

                case Servers.MYSQL:
                    mySQL.Disconnect();
                    break;

                case Servers.POSTGRESQL:
                    postgreSQL.Disconnect();
                    break;

                default:
                    break;
            }
        }
        public void Disponse()
        {
            switch (Connection.DBCM)
            {
                case Servers.SQL_SERVER:
                    sQLServer.Disponse();
                    break;

                case Servers.MYSQL:
                    mySQL.Disponse();
                    break;

                case Servers.POSTGRESQL:
                    postgreSQL.Disponse();
                    break;

                default:
                    break;
            }
        }
        public void BeginTransaction()
        {
            switch (Connection.DBCM)
            {
                case Servers.SQL_SERVER:
                    sQLServer.BeginTransaction();
                    break;

                case Servers.MYSQL:
                    mySQL.BeginTransaction();
                    break;

                case Servers.POSTGRESQL:
                    postgreSQL.BeginTransaction();
                    break;

                default:
                    break;
            }
        }
        public void FinalizeTransaction(bool Response)
        {
            switch (Connection.DBCM)
            {
                case Servers.SQL_SERVER:
                    sQLServer.FinalizeTransaction(Response);
                    break;

                case Servers.MYSQL:
                    mySQL.FinalizeTransaction(Response);
                    break;

                case Servers.POSTGRESQL:
                    postgreSQL.FinalizeTransaction(Response);
                    break;

                default:
                    break;
            }
        }
        public bool Execute(string Query)
        {
            switch (Connection.DBCM)
            {
                case Servers.SQL_SERVER:
                    return sQLServer.Execute(Query);
                    
                case Servers.MYSQL:
                    return mySQL.Execute(Query);

                case Servers.POSTGRESQL:
                    return postgreSQL.Execute(Query);

                default:
                    return false;
            }
        }
        public bool Execute(string Query, ref DataTable Result)
        {
            switch (Connection.DBCM)
            {
                case Servers.SQL_SERVER:
                    return sQLServer.Execute(Query, ref Result);

                case Servers.MYSQL:
                    return mySQL.Execute(Query, ref Result);

                case Servers.POSTGRESQL:
                    return postgreSQL.Execute(Query, ref Result);

                default:
                    return false;
            }
        }
        public bool Execute(object sqlCommand)
        {
            switch (Connection.DBCM)
            {
                case Servers.SQL_SERVER:
                    return sQLServer.Execute(sqlCommand);

                case Servers.MYSQL:
                    return mySQL.Execute(sqlCommand);

                case Servers.POSTGRESQL:
                    return postgreSQL.Execute(sqlCommand);

                default:
                    return false;
            }
        }
        public bool Execute(object sqlCommand, ref DataTable Result)
        {
            switch (Connection.DBCM)
            {
                case Servers.SQL_SERVER:
                    return sQLServer.Execute(sqlCommand, ref Result);
                    
                case Servers.MYSQL:
                    return mySQL.Execute(sqlCommand, ref Result);

                case Servers.POSTGRESQL:
                    return postgreSQL.Execute(sqlCommand, ref Result);

                default:
                    return false;
            }
        }
    }
}
