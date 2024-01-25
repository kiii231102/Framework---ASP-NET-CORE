using System;
using System.ComponentModel.DataAnnotations;

namespace Musclegym.Models
{
    public class Member
    {
        public int MemberId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime TanggalTransaksi { get; set; }

        [Required(ErrorMessage = "Nama harus diisi.")]
        public string? Nama { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Format email tidak valid.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Nomor HP harus diisi.")]
        public long NoHp { get; set; }

        [DataType(DataType.Date)]
        public DateTime TanggalLahir { get; set; }

        [Required(ErrorMessage = "Jenis Kelamin harus diisi.")]
        public string? JenisKelamin { get; set; }

        public string? Kelas { get; set; }

        [Required(ErrorMessage = "Cabang harus diisi.")]
        public string? Cabang { get; set; }

        [DataType(DataType.Date)]
        public DateTime TanggalBergabung { get; set; }

        [Required(ErrorMessage = "Paket harus diisi.")]
        public string? Paket { get; set; }

        [Required(ErrorMessage = "Status Member harus diisi.")]
        public string? StatusMember { get; set; }
    }
}
