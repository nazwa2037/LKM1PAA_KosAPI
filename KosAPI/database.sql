DROP TABLE IF EXISTS pembayaran;
DROP TABLE IF EXISTS penghuni;
DROP TABLE IF EXISTS kamar;
DROP TYPE IF EXISTS status_kamar_type;
DROP TYPE IF EXISTS status_bayar_type;

CREATE TYPE status_kamar_type AS ENUM ('Kosong', 'Terisi', 'Maintenance');
CREATE TYPE status_bayar_type AS ENUM ('Lunas', 'Belum Lunas');

CREATE TABLE kamar (
    id SERIAL PRIMARY KEY,
    nomor_kamar VARCHAR(10) NOT NULL UNIQUE,
    harga_bulanan DECIMAL(12,2) NOT NULL CHECK (harga_bulanan > 0),
    jumlah_penghuni INT DEFAULT 0 CHECK (jumlah_penghuni >= 0 AND jumlah_penghuni <= 2),
    status_ketersediaan status_kamar_type DEFAULT 'Kosong',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE penghuni (
    id SERIAL PRIMARY KEY,
    nama_lengkap VARCHAR(100) NOT NULL,
    nomor_wa VARCHAR(15),
    id_kamar INT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    deleted_at TIMESTAMP,
    FOREIGN KEY (id_kamar) REFERENCES kamar(id) ON DELETE SET NULL
);

CREATE TABLE pembayaran (
    id SERIAL PRIMARY KEY,
    id_kamar INT NOT NULL,
    tanggal_bayar DATE DEFAULT CURRENT_DATE,
    jumlah_bayar DECIMAL(12,2) NOT NULL CHECK (jumlah_bayar >= 0),
    status_pembayaran status_bayar_type DEFAULT 'Belum Lunas',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (id_kamar) REFERENCES kamar(id) ON DELETE CASCADE
);

CREATE INDEX idx_kamar_nomor ON kamar(nomor_kamar);
CREATE INDEX idx_penghuni_nama ON penghuni(nama_lengkap);
CREATE INDEX idx_pembayaran_status ON pembayaran(status_pembayaran);


INSERT INTO kamar (nomor_kamar, harga_bulanan, jumlah_penghuni, status_ketersediaan) VALUES
('01', 500000, 2, 'Terisi'),
('02', 500000, 0, 'Kosong'),
('03', 600000, 2, 'Terisi'),
('04', 600000, 1, 'Terisi'),
('05', 500000, 0, 'Maintenance');

INSERT INTO penghuni (nama_lengkap, nomor_wa, id_kamar) VALUES
('Nazwa Ulul Azmi', '081234567890', 1),
('Meidhita Faulina', '081234567891', 3),
('Dina Zakiyah', '081234567892', 4),
('Anggita Zulfia', '081234567893', 1),
('Sinta Putri', '081234567894', 3);

INSERT INTO pembayaran (id_kamar, jumlah_bayar, status_pembayaran) VALUES
(1, 600000, 'Lunas'),
(2, 600000, 'Belum Lunas'),
(3, 500000, 'Belum Lunas'),
(4, 600000, 'Lunas'),
(5, 500000, 'Belum Lunas');