using Npgsql;
using KosAPI.Helper;

namespace KosAPI.Context
{
    public class KamarContext
    {
        private string __constr;

        public KamarContext(string constr)
        {
            __constr = constr;
        }

        public List<object> GetAll()
        {
            List<object> list = new List<object>();

            string query = "SELECT * FROM kamar";
            SqlDBHelper db = new SqlDBHelper(__constr);

            try
            {
                NpgsqlCommand cmd = db.GetCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new
                    {
                        id = Convert.ToInt32(reader["id"]),
                        nomor_kamar = reader["nomor_kamar"].ToString(),
                        harga_bulanan = Convert.ToDecimal(reader["harga_bulanan"]),
                        jumlah_penghuni = Convert.ToInt32(reader["jumlah_penghuni"]),
                        status_ketersediaan = reader["status_ketersediaan"].ToString()
                    });
                }

                db.closeConnection();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return list;
        }
    }
}