using Npgsql;
using KosAPI.Models;
using KosAPI.Helper;

namespace KosAPI.Context
{
    public class PenghuniContext
    {
        private string _constr;

        public PenghuniContext(string constr)
        {
            _constr = constr;
        }

        public List<Penghuni> GetAll()
        {
            List<Penghuni> list = new List<Penghuni>();
            string query = "SELECT * FROM penghuni WHERE deleted_at IS NULL";

            SqlDBHelper db = new SqlDBHelper(_constr);
            var cmd = db.GetCommand(query);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Penghuni
                {
                    id = (int)reader["id"],
                    nama_lengkap = reader["nama_lengkap"].ToString(),
                    nomor_wa = reader["nomor_wa"].ToString(),
                    id_kamar = reader["id_kamar"] == DBNull.Value ? null : (int?)reader["id_kamar"],
                    created_at = (DateTime)reader["created_at"],
                    updated_at = (DateTime)reader["updated_at"]
                });
            }

            reader.Close();
            db.closeConnection();
            return list;
        }

        public Penghuni GetById(int id)
        {
            Penghuni data = null;
            string query = "SELECT * FROM penghuni WHERE id=@id AND deleted_at IS NULL";

            SqlDBHelper db = new SqlDBHelper(_constr);
            var cmd = db.GetCommand(query);
            cmd.Parameters.AddWithValue("@id", id);

            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                data = new Penghuni
                {
                    id = (int)reader["id"],
                    nama_lengkap = reader["nama_lengkap"].ToString(),
                    nomor_wa = reader["nomor_wa"].ToString(),
                    id_kamar = reader["id_kamar"] == DBNull.Value ? null : (int?)reader["id_kamar"],
                    created_at = (DateTime)reader["created_at"],
                    updated_at = (DateTime)reader["updated_at"]
                };
            }

            reader.Close();
            db.closeConnection();
            return data;
        }

        public void Insert(Penghuni p)
        {
            if (p == null)
                throw new Exception("Data kosong");

            if (p.id_kamar == null)
                throw new Exception("Kamar wajib diisi");

            SqlDBHelper db = new SqlDBHelper(_constr);

            string cekKamar = "SELECT jumlah_penghuni, status_ketersediaan FROM kamar WHERE id=@id";
            var cmdCek = db.GetCommand(cekKamar);
            cmdCek.Parameters.AddWithValue("@id", p.id_kamar);

            var reader = cmdCek.ExecuteReader();

            if (!reader.Read())
                throw new Exception("Kamar tidak ditemukan");

            int jumlah = (int)reader["jumlah_penghuni"];
            string status = reader["status_ketersediaan"].ToString();

            reader.Close();

            if (status == "Maintenance")
                throw new Exception("Kamar sedang maintenance");

            if (jumlah >= 2)
                throw new Exception("Kamar sudah penuh");

            string insert = @"INSERT INTO penghuni 
                             (nama_lengkap, nomor_wa, id_kamar) 
                             VALUES (@nama, @wa, @id_kamar)";

            var cmdInsert = db.GetCommand(insert);
            cmdInsert.Parameters.AddWithValue("@nama", p.nama_lengkap);
            cmdInsert.Parameters.AddWithValue("@wa", (object?)p.nomor_wa ?? DBNull.Value);
            cmdInsert.Parameters.AddWithValue("@id_kamar", p.id_kamar);

            cmdInsert.ExecuteNonQuery();

            string updateKamar = @"
                UPDATE kamar 
                SET jumlah_penghuni = jumlah_penghuni + 1,
                    status_ketersediaan = 'Terisi',
                    updated_at = CURRENT_TIMESTAMP
                WHERE id = @id";

            var cmdUpdate = db.GetCommand(updateKamar);
            cmdUpdate.Parameters.AddWithValue("@id", p.id_kamar);

            cmdUpdate.ExecuteNonQuery();

            db.closeConnection();
        }

        public void Update(int id, Penghuni p)
        {
            SqlDBHelper db = new SqlDBHelper(_constr);

            string query = @"UPDATE penghuni 
                             SET nama_lengkap=@nama,
                                 nomor_wa=@wa,
                                 updated_at=CURRENT_TIMESTAMP
                             WHERE id=@id";

            var cmd = db.GetCommand(query);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nama", p.nama_lengkap);
            cmd.Parameters.AddWithValue("@wa", (object?)p.nomor_wa ?? DBNull.Value);

            int result = cmd.ExecuteNonQuery();

            if (result == 0)
                throw new Exception("Data tidak ditemukan");

            db.closeConnection();
        }

        public void Delete(int id)
        {
            SqlDBHelper db = new SqlDBHelper(_constr);

            string getKamar = "SELECT id_kamar FROM penghuni WHERE id=@id";
            var cmdGet = db.GetCommand(getKamar);
            cmdGet.Parameters.AddWithValue("@id", id);

            var reader = cmdGet.ExecuteReader();

            if (!reader.Read())
                throw new Exception("Data tidak ditemukan");

            int? id_kamar = reader["id_kamar"] == DBNull.Value ? null : (int?)reader["id_kamar"];
            reader.Close();

            string delete = "UPDATE penghuni SET deleted_at=CURRENT_TIMESTAMP WHERE id=@id";
            var cmdDel = db.GetCommand(delete);
            cmdDel.Parameters.AddWithValue("@id", id);
            cmdDel.ExecuteNonQuery();

            if (id_kamar != null)
            {
                string update = @"
                    UPDATE kamar 
                    SET jumlah_penghuni = jumlah_penghuni - 1,
                        status_ketersediaan = CASE 
                            WHEN jumlah_penghuni - 1 = 0 THEN 'Kosong'
                            ELSE status_ketersediaan
                        END,
                        updated_at = CURRENT_TIMESTAMP
                    WHERE id = @id";

                var cmdUpdate = db.GetCommand(update);
                cmdUpdate.Parameters.AddWithValue("@id", id_kamar);
                cmdUpdate.ExecuteNonQuery();
            }

            db.closeConnection();
        }
    }
}