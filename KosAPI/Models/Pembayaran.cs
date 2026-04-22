namespace KosAPI.Models
{
    public class Pembayaran
    {
        public int id { get; set; }
        public int id_kamar { get; set; }
        public DateTime tanggal_bayar { get; set; }
        public decimal jumlah_bayar { get; set; }
        public string status_pembayaran { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}