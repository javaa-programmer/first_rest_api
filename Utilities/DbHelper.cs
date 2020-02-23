using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using first_rest_api.Utilities;

namespace first_rest_api.Utilities {
    public static class DbHelper {
        public static string GetEnquotedString(string value) => "\'" + value + "\'";

        public static string GetFormattedDate(DateTime dateTime) => "\'" + string.Format("{0:dd-MMM-yyyy}", dateTime) + "\'";
    
        public static async Task<SqlConnection> GetDbConnection() {
            SqlConnection conn = new SqlConnection(Constants.GetConnectionString());
            if (conn.State == System.Data.ConnectionState.Closed) 
            {
                await conn.OpenAsync().ConfigureAwait(false);
            }
            return conn;
        }

        public static async Task CloseDbConnection(SqlConnection conn) {
            if (conn.State == System.Data.ConnectionState.Open) 
            {
                await conn.CloseAsync().ConfigureAwait(false);
            }
            await conn.DisposeAsync().ConfigureAwait(false);
            conn = null;
        }

    }
}

    
