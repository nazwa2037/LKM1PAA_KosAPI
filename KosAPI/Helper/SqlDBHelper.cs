using Npgsql;
using System.Data;

namespace KosAPI.Helper
{
    public class SqlDBHelper
    {
        private NpgsqlConnection __conn;

        public SqlDBHelper(string connectionString)
        {
            __conn = new NpgsqlConnection(connectionString);
        }

        public NpgsqlCommand GetCommand(string query)
        {
            if (__conn.State == ConnectionState.Closed)
            {
                __conn.Open();
            }

            return new NpgsqlCommand(query, __conn);
        }

        public void closeConnection()
        {
            if (__conn.State == ConnectionState.Open)
            {
                __conn.Close();
            }
        }
    }
}