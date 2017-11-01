using System;
using System.Data;
using DatabaseConnectionManager.Objects;
using System.Data.SqlClient;

namespace DatabaseConnectionManager.DAL
{
    class SQLServer : IMethodsObligatory
    {
        private SqlConnection sqlConnection = null;
        private SqlCommand sqlCommand = null;
        private SqlTransaction sqlTransaction = null;
        private bool TransactionRequest = false;

        public SQLServer()
        {
            sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = string.Format("Server = {0}; Database = {1}; User Id = {2}; Password = {3};",
                Connection.Server, Connection.Database, Connection.UserId, Connection.Password);
        }
        public void Connect()
        {
            try
            {
                sqlConnection.Open();
            }
            catch(SqlException ex)
            {
                // Write Log
            }
        }
        public void Disconnect()
        {
            try
            {
                sqlConnection.Close();
            }
            catch(SqlException ex)
            {
                // Write Log
            }
        }
        public void Disponse()
        {
            try
            {
                sqlConnection.Dispose();
                sqlCommand.Dispose();
                sqlTransaction.Dispose();
            }
            catch(SqlException ex)
            {
                // Write Log
            }
        }
        public void BeginTransaction()
        {
            try
            {
                sqlTransaction = sqlConnection.BeginTransaction();
                TransactionRequest = true;
            }
            catch(SqlException ex)
            {
                // Write log
            }
        }
        public void FinalizeTransaction(bool Response)
        {
            try
            {
                if (Response)
                    sqlTransaction.Commit();
                else
                    sqlTransaction.Rollback();

                TransactionRequest = false;
            }
            catch(SqlException ex)
            {
                // Write log
            }
        }
        public bool Execute(string Query)
        {
            try
            {
                if (sqlConnection.State != ConnectionState.Open)
                    return false;

                sqlCommand = new SqlCommand(Query, sqlConnection);
                if (TransactionRequest)
                    sqlCommand.Transaction = sqlTransaction;

                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex)
            {
                // Write log
            }
            catch(Exception ex)
            {
                // Write log
            }
            return false;
        }
        public bool Execute(string Query, ref DataTable Result)
        {
            try
            {
                if (sqlConnection.State != ConnectionState.Open)
                    return false;

                sqlCommand = new SqlCommand(Query, sqlConnection);
                if (TransactionRequest)
                    sqlCommand.Transaction = sqlTransaction;

                Result.Load(sqlCommand.ExecuteReader());
                return true;
            }
            catch (SqlException ex)
            {
                // Write log
            }
            catch(Exception ex)
            {
                // Write log
            }
            return false;
        }
        public bool Execute(object sqlCommand)
        {
            try
            {
                if (sqlConnection.State != ConnectionState.Open)
                    return false;

                this.sqlCommand = (SqlCommand)sqlCommand;
                this.sqlCommand.CommandType = CommandType.StoredProcedure;
                this.sqlCommand.Connection = sqlConnection;

                if (TransactionRequest)
                    this.sqlCommand.Transaction = sqlTransaction;

                this.sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch(SqlException ex)
            {
                // Write log
            }
            catch (Exception ex)
            {
                // Write log
            }
            return false;
        }
        public bool Execute(object sqlCommand, ref DataTable Result)
        {
            try
            {
                if (sqlConnection.State != ConnectionState.Open)
                    return false;

                this.sqlCommand = (SqlCommand) sqlCommand;
                this.sqlCommand.CommandType = CommandType.StoredProcedure;
                this.sqlCommand.Connection = sqlConnection;

                if (TransactionRequest)
                    this.sqlCommand.Transaction = sqlTransaction;

                Result.Load(this.sqlCommand.ExecuteReader());
                return true;
            }
            catch(SqlException ex)
            {
                // Write log
            }
            catch (Exception ex)
            {
                // Write log
            }
            return false;
        }
    }
}
