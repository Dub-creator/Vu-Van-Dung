using System;
using System.Collections.Generic;
using QuanLySinhVien.Data.DAL;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.Business
{
    public class SinhVienBUS
    {
        private readonly SinhVienDAO dao = new SinhVienDAO();

        public List<SinhVien> GetAll()
        {
            try { return dao.GetAll(); }
            catch (Exception ex) { throw new Exception("Không thể tải danh sách sinh viên.", ex); }
        }

        public SinhVien? GetByMaSV(string maSV)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(maSV)) throw new ArgumentException("Mã sinh viên không được để trống.");
                return dao.GetByMaSV(maSV.Trim());
            }
            catch (Exception ex) { throw new Exception("Không thể tìm sinh viên.", ex); }
        }

        public List<SinhVien> GetByMaLop(string maLop)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(maLop)) throw new ArgumentException("Mã lớp không được để trống.");
                return dao.GetByMaLop(maLop.Trim());
            }
            catch (Exception ex) { throw new Exception("Không thể lấy danh sách sinh viên theo lớp.", ex); }
        }

        public void Add(SinhVien sv)
        {
            try { Validate(sv); dao.Add(sv); }
            catch (Exception ex) { throw new Exception("Thêm sinh viên thất bại.", ex); }
        }

        public void Update(SinhVien sv)
        {
            try { Validate(sv); dao.Update(sv); }
            catch (Exception ex) { throw new Exception("Cập nhật sinh viên thất bại.", ex); }
        }

        public void Delete(string maSV)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(maSV)) throw new ArgumentException("Mã sinh viên không được để trống.");
                dao.Delete(maSV.Trim());
            }
            catch (Exception ex) { throw new Exception("Xóa sinh viên thất bại.", ex); }
        }

        private static void Validate(SinhVien sv)
        {
            if (sv == null) throw new ArgumentNullException(nameof(sv));
            if (string.IsNullOrWhiteSpace(sv.MaSV)) throw new ArgumentException("Mã sinh viên không được để trống.");
            if (string.IsNullOrWhiteSpace(sv.HoTen)) throw new ArgumentException("Họ tên không được để trống.");
            if (!DateTime.TryParse(sv.NgaySinh, out _)) throw new ArgumentException("Ngày sinh không hợp lệ.");
            if (sv.Diem < 0 || sv.Diem > 10) throw new ArgumentOutOfRangeException(nameof(sv.Diem), "Điểm phải nằm trong khoảng 0 đến 10.");
        }
    }
}
