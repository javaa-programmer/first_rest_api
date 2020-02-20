namespace first_rest_api.Utilities {

    public static class Constants {

        private static string ConnectionString;

        public static void SetConnectionString(string connStr) {
            System.Console.WriteLine("Connection String : " + connStr);
            ConnectionString = connStr;
        }

        public static string GetConnectionString() {
            return ConnectionString;
        }
    }
}
