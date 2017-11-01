using System;
using Npgsql;
using DatabaseConnectionManager.Objects;
using System.Data;

namespace DatabaseConnectionManager.DAL
{
    class PostgreSQL : IMethodsObligatory
    {
        private NpgsqlConnection npgsqlConnection = null;
        private NpgsqlCommand npgsqlCommand = null;
        private NpgsqlTransaction npgsqlTransaction = null;
        private bool bTransaction = false;

        public PostgreSQL()
        {
            npgsqlConnection = new NpgsqlConnection();
            npgsqlConnection.ConnectionString = string.Format("Server = {0}; Port = {1}; Database = {2}; User Id = {3}; Password = {4};", Connection.Server, 
                Connection.Port, Connection.Database, Connection.UserId, Connection.Password);
        }
        public void Connect()
        {
            try
            {
                npgsqlConnection.Open();
            }
            catch(NpgsqlException ex)
            {

            }
        }
        public void Disconnect()
        {
            try
            {
                npgsqlConnection.Close();
            }
            catch(NpgsqlException ex)
            {

            }
        }
        public void Disponse()
        {
            try
            {
                npgsqlConnection.Dispose();
                npgsqlCommand.Dispose();
                npgsqlTransaction.Dispose();
            }
            catch(NpgsqlException ex)
            {

            }
        }
        public void BeginTransaction()
        {
            try
            {
                npgsqlTransaction = npgsqlConnection.BeginTransaction();
                bTransaction = true;
            }
            catch(NpgsqlException ex)
            {

            }
        }
        public void FinalizeTransaction(bool Response)
        {
            try
            {
                if (Response)
                    npgsqlTransaction.Commit();
                else
                    npgsqlTransaction.Rollback();

                bTransaction = false;
            }
            catch(NpgsqlException ex)
            {

            }
        }
        public bool Execute(string Query)
        {
            try
            {
                if (npgsqlConnection.State != ConnectionState.Open)
                    return false;

                npgsqlCommand = new NpgsqlCommand(Query, npgsqlConnection);
                if (bTransaction)
                    npgsqlCommand.Transaction = npgsqlTransaction;

                npgsqlCommand.ExecuteNonQuery();
                return true;
            }
            catch(NpgsqlException ex)
            {

            }
            catch(Exception ex)
            {

            }
            return false;
        }
        public bool Execute(string Query, ref DataTable Result)
        {
            try
            {
                if (npgsqlConnection.State != ConnectionState.Open)
                    return false;

                npgsqlCommand = new NpgsqlCommand(Query, npgsqlConnection);
                if (bTransaction)
                    npgsqlCommand.Transaction = npgsqlTransaction;

                Result.Load(npgsqlCommand.ExecuteReader());
                return true;
            }
            catch(NpgsqlException ex)
            {

            }
            catch(Exception ex)
            {

            }
            return false;
        }
        public bool Execute(object sqlCommand)
        {
            try
            {
                if (npgsqlConnection.State != ConnectionState.Open)
                    return false;

                npgsqlCommand = (NpgsqlCommand)sqlCommand;
                npgsqlCommand.CommandType = CommandType.StoredProcedure;
                npgsqlCommand.Connection = npgsqlConnection;
                if (bTransaction)
                    npgsqlCommand.Transaction = npgsqlTransaction;

                npgsqlCommand.ExecuteNonQuery();
                return true;
            }
            catch(NpgsqlException ex)
            {

            }
            catch(Exception ex)
            {

            }
            return false;
        }
        public bool Execute(object sqlCommand, ref DataTable Result)
        {
            try
            {
                if (npgsqlConnection.State != ConnectionState.Open)
                    return false;

                npgsqlCommand = (NpgsqlCommand)sqlCommand;
                npgsqlCommand.CommandType = CommandType.StoredProcedure;
                npgsqlCommand.Connection = npgsqlConnection;
                if (bTransaction)
                    npgsqlCommand.Transaction = npgsqlTransaction;

                Result.Load(npgsqlCommand.ExecuteReader());
                return true;
            }
            catch(NpgsqlException ex)
            {

            }
            catch(Exception ex)
            {

            }
            return false;
        }
    }
}
