# Kos API

## Deskripsi Project
Kos API merupakan REST API sederhana untuk mengelola data kos, yang meliputi data kamar, penghuni, serta pencatatan pembayaran.

Domain yang dipilih adalah manajemen kos, karena alur datanya saling berelasi antara kamar yang tersedia, siapa penghuninya, dan bagaimana status pembayarannya.

## Fitur Utama
* Menampilkan data kamar
* Mengelola data penghuni (CRUD)
* Update pembayaran berdasarkan id kamar
* Soft delete pada data penghuni

## Teknologi yang Digunakan
* Bahasa: C#
* Framework: ASP.NET Core Web API
* Database: PostgreSQL
* Library:
  * Npgsql
  * Swagger (Swashbuckle)

## Cara Instalasi dan Menjalankan Project

### 1. Clone Repository
```bash
git clone https://github.com/nazwa2037/LKM1PAA_KosAPI.git
cd LKM1PAA_KosAPI
```

### 2. Setup Database
Buat database baru di PostgreSQL:
```sql
CREATE DATABASE KosAPI;
```

### 3. Import Database
1. Buka pgAdmin
2. Pilih database KosAPI
3. Klik Query Tool
4. Masukkan isi file database.sql
5. Klik tombol Execute

### 4. Konfigurasi Connection String
Buka file `appsettings.json`, lalu sesuaikan bagian "ConnectionStrings" dengan database PostgreSQL yang digunakan
Keterangan:
* Database harus sama dengan nama database yang dibuat di PostgreSQL
* Username dan Password disesuaikan dengan PostgreSQL masing-masing

### 5. Install Dependencies
Jika package belum terinstall, jalankan:
```bash
dotnet restore
```

### 6. Menjalankan Project
Jalankan project melalui Visual Studio dengan menekan tombol Run.

### 7. Akses Swagger
Setelah project dijalankan, Swagger akan terbuka otomatis di browser.

Jika tidak terbuka, akses secara manual menggunakan port pada `launchSetting.json`
```
https://localhost:<port>/swagger
```

## Daftar Endpoint

### Penghuni

| Method | URL | Keterangan |
|------|-----|----------|
| GET | `/api/penghuni` | Menampilkan semua data penghuni |
| GET | `/api/penghuni/{id}` | Menampilkan data penghuni berdasarkan id |
| POST | `/api/penghuni` | Menambahkan data penghuni |
| PUT | `/api/penghuni/{id}` | Mengupdate data penghuni |
| DELETE | `/api/penghuni/{id}` | Menghapus data penghuni (soft delete) |

---

### Kamar

| Method | URL | Keterangan |
|------|-----|----------|
| GET | `/api/kamar` | Menampilkan semua data kamar |

---

### Pembayaran

| Method | URL | Keterangan |
|------|-----|----------|
| PUT | `/api/pembayaran/kamar/{id_kamar}` | Mengubah status pembayaran berdasarkan id kamar |


## Link Video Presentasi
https://youtu.be/XlgwiFDq1Nk

## Author
Nama: Nazwa Ulul Azmi

NIM: 242410102037
