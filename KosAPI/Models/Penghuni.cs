namespace KosAPI.Models
{
    public class Penghuni
    {
        public int id { get; set; }
        public string nama_lengkap { get; set; }
        public string nomor_wa { get; set; }
        public int? id_kamar { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime? deleted_at { get; set; }
    }
}
