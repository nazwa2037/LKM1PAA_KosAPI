namespace KosAPI.Models
{
    public class Kamar
    {
        public int id { get; set; }
        public string nomor_kamar { get; set; }
        public decimal harga_bulanan { get; set; }
        public int jumlah_penghuni { get; set; }
        public string status_ketersediaan { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
