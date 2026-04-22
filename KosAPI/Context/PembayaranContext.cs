using Npgsql;
using KosAPI.Helper;

namespace KosAPI.Context
{
    public class PembayaranContext
    {
        private string _constr;

        public PembayaranContext(string constr)
        {
            _constr = constr;
        }

        public void BayarKamar(int id_kamar, decimal jumlah_bayar)
        {
            SqlDBHelper db = new SqlDBHelper(_constr);

            try
            {
                string cekKamar = "SELECT harga_bulanan FROM kamar WHERE id=@id";
                var cmdCek = db.GetCommand(cekKamar);
                cmdCek.Parameters.AddWithValue("@id", id_kamar);

                var result = cmdCek.ExecuteScalar();
                if (result == null)
                    throw new Exception("Kamar tidak ditemukan");

                decimal harga = Convert.ToDecimal(result);

                if (jumlah_bayar <= 0)
                    throw new Exception("Jumlah bayar tidak valid");

                string status = jumlah_bayar >= harga ? "Lunas" : "Belum Lunas";

                string update = @"
                    UPDATE pembayaran
                    SET jumlah_bayar=@jumlah,
                        status_pembayaran=@status::status_bayar_type,
                        tanggal_bayar=CURRENT_DATE,
                        updated_at=CURRENT_TIMESTAMP
                    WHERE id_kamar=@id";

                var cmdUpdate = db.GetCommand(update);
                cmdUpdate.Parameters.AddWithValue("@id", id_kamar);
                cmdUpdate.Parameters.AddWithValue("@jumlah", jumlah_bayar);
                cmdUpdate.Parameters.AddWithValue("@status", status);

                int rows = cmdUpdate.ExecuteNonQuery();

                if (rows == 0)
                    throw new Exception("Data pembayaran tidak ditemukan");
            }
            finally
            {
                db.closeConnection();
            }
        }
    }
}