using System.ComponentModel.DataAnnotations;

namespace Musclegym.Models
{
    public class Promo
    {
        public int Id { get; set; }

        
        [Required(ErrorMessage = "Nama harus diisi")]
        public string Nama { get; set; }

        [Required(ErrorMessage = "Email harus diisi")]
        [EmailAddress(ErrorMessage = "Format email tidak valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Nomor HP harus diisi")]
        [RegularExpression(@"^\d{10,15}$", ErrorMessage = "Nomor HP harus terdiri dari 10 hingga 15 digit angka")]
        public string NoHp { get; set; }

        [Required(ErrorMessage = "Tanggal Lahir harus diisi")]
        [DataType(DataType.Date)]
        public DateTime TanggalLahir { get; set; }

        [Required(ErrorMessage = "Jenis Kelamin harus dipilih")]
        public string JenisKelamin { get; set; }

        [Required(ErrorMessage = "Durasi harus diisi")]
        public string Durasi { get; set; }

        [Required(ErrorMessage = "Class harus dipilih")]
        public string Class { get; set; }

        [Required(ErrorMessage = "Cabang harus dipilih")]
        public string Cabang { get; set; }

    }
}
