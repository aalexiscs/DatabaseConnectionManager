using System;
using System.Data;
using DatabaseConnectionManager.Objects;
using MySql.Data.MySqlClient;

namespace DatabaseConnectionManager.DAL
{
    class MySQL : IMethodsObligatory
    {
        private MySqlConnection mySqlConnection;
        private MySqlCommand mySqlCommand;
        private MySqlTransaction mySqlTransaction;
        private bool TransactionRequest = false;

        public MySQL()
        {
            mySqlConnection = new MySqlConnection();
            mySqlConnection.ConnectionString = string.Format("Server = {0}; Database = {1}; Uid = {2}; Psw = {3};", Connection.Server,
                Connection.Database, Connection.UserId, Connection.Password);
        }

        public void Connect()
        {
            try
            {
                mySqlConnection.Open();
            }
            catch(MySqlException ex)
            {
                // Write log
            }
        }
        public void Disconnect()
        {
            try
            {
                mySqlConnection.Close();
            }
            catch(MySqlException ex)
            {
                // Write log
            }
        }
        public void Disponse()
        {
            try
            {
                mySqlConnection.Dispose();
                mySqlCommand.Dispose();
                mySqlTransaction.Dispose();
            }
            catch(MySqlException ex)
            {
                // Write log
            }
        }
        public void BeginTransaction()
        {
            try
            {
                mySqlTransaction = mySqlConnection.BeginTransaction();
                TransactionRequest = true;
            }
            catch(MySqlException ex)
            {
                // Write log
            }
        }
        public void FinalizeTransaction(bool Response)
        {
            try
            {
                if (Response)
                    mySqlTransaction.Commit();
                else
                    mySqlTransaction.Rollback();

                TransactionRequest = false;
            }
            catch(MySqlException ex)
            {
                // Write log
            }
        }
        public bool Execute(string Query)
        {
            try
            {
                if (mySqlConnection.State != ConnectionState.Open)
                    return false;

                mySqlCommand = new MySqlCommand(Query, mySqlConnection);
                if (TransactionRequest)
                    mySqlCommand.Transaction = mySqlTransaction;

                mySqlCommand.ExecuteNonQuery();
                return true;
            }
            catch(MySqlException ex)
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
                if (mySqlConnection.State != ConnectionState.Open)
                    return false;

                mySqlCommand = new MySqlCommand(Query, mySqlConnection);
                if (TransactionRequest)
                    mySqlCommand.Transaction = mySqlTransaction;

                Result.Load(mySqlCommand.ExecuteReader());
                return true;
            }
            catch(MySqlException ex)
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
                if (mySqlConnection.State != ConnectionState.Open)
                    return false;

                mySqlCommand = (MySqlCommand)sqlCommand;
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Connection = mySqlConnection;

                if (TransactionRequest)
                    mySqlCommand.Transaction = mySqlTransaction;

                mySqlCommand.ExecuteNonQuery();
                return true;
            }
            catch(MySqlException ex)
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
                if (mySqlConnection.State != ConnectionState.Open)
                    return false;

                mySqlCommand = (MySqlCommand)sqlCommand;
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Connection = mySqlConnection;

                if (TransactionRequest)
                    mySqlCommand.Transaction = mySqlTransaction;

                Result.Load(mySqlCommand.ExecuteReader());
                return true;
            }
            catch(MySqlException ex)
            {
                // Write log
            }
            catch(Exception ex)
            {
                // Write log
            }
            return false;
        }
    }
}
