using System;
using System.Data;

namespace DatabaseConnectionManager.Objects
{
    public abstract class OperationsSql : IMethodsObligatory
    {
        public abstract void BeginTransaction();

        public abstract void Connect();

        public abstract void Disconnect();

        public abstract void Disponse();

        public abstract bool Execute(object sqlCommand);

        public abstract bool Execute(string Query);

        public abstract bool Execute(object sqlCommand, ref DataTable Result);

        public abstract bool Execute(string Query, ref DataTable Result);

        public abstract void FinalizeTransaction(bool Response);
    }
}
