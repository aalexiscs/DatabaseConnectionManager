using System.Data;

namespace DatabaseConnectionManager.Objects
{
    interface IMethodsObligatory
    {
        void Connect();
        void Disconnect();
        void Disponse();
        void BeginTransaction();
        void FinalizeTransaction(bool Response);
        bool Execute(string Query);
        bool Execute(string Query, ref DataTable Result);
        bool Execute(object sqlCommand);
        bool Execute(object sqlCommand, ref DataTable Result);
    }
}
