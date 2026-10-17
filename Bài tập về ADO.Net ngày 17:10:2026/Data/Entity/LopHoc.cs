namespace QuanLySinhVien.Data.Entity
{
    // Giả định schema LopHoc gồm MaLop, TenLop, MaKhoa.
    public class LopHoc
    {
        public string MaLop { get; set; } = string.Empty;
        public string TenLop { get; set; } = string.Empty;
        public string MaKhoa { get; set; } = string.Empty;
    }
}
