namespace DatabaseConnectionManager.Objects
{
    static class Connection
    {
        public static short DBCM { get; set; }
        public static string Server { get; set; }
        public static short Port { get; set; }
        public static string Database { get; set; }
        public static string UserId { get; set; }
        public static string Password { get; set; }

        public static bool ValidateConnectionString()
        {
            return DBCM != 0 && Server.Trim().Length > 0 && Port != 0 && Database.Trim().Length > 0
                && UserId.Trim().Length > 0 && Password.Trim().Length > 0;
        }
    }
}
