namespace first_rest_api.Utilities {
    public static class CommandHelper {
        public static string GetSqlForAllUser() {
            return "SELECT * FROM [dbo].[USER_DETAILS]  ORDER BY id";
        }

        public static string GetSqlForUserById(int userId) {
            return @"SELECT * FROM [dbo].[USER_DETAILS] where [id] = " + userId ;
        }
    }
}

