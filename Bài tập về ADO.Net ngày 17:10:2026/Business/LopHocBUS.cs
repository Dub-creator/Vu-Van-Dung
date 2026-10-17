using System;
using System.Collections.Generic;
using QuanLySinhVien.Data.DAL;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.Business
{
    public class LopHocBUS
    {
        private readonly LopHocDAO dao = new LopHocDAO();

        public List<LopHoc> GetAll()
        {
            try { return dao.GetAll(); }
            catch (Exception ex) { throw new Exception("Không thể tải danh sách lớp học.", ex); }
        }

        public LopHoc? GetByMaLop(string maLop)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(maLop)) throw new ArgumentException("Mã lớp không được để trống.");
                return dao.GetByMaLop(maLop.Trim());
            }
            catch (Exception ex) { throw new Exception("Không thể tìm lớp học.", ex); }
        }

        public void Add(LopHoc lop)
        {
            try { Validate(lop); dao.Add(lop); }
            catch (Exception ex) { throw new Exception("Thêm lớp học thất bại.", ex); }
        }

        public void Update(LopHoc lop)
        {
            try { Validate(lop); dao.Update(lop); }
            catch (Exception ex) { throw new Exception("Cập nhật lớp học thất bại.", ex); }
        }

        public void Delete(string maLop)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(maLop)) throw new ArgumentException("Mã lớp không được để trống.");
                dao.Delete(maLop.Trim());
            }
            catch (Exception ex) { throw new Exception("Xóa lớp học thất bại. Có thể lớp vẫn đang có sinh viên.", ex); }
        }

        private static void Validate(LopHoc lop)
        {
            if (lop == null) throw new ArgumentNullException(nameof(lop));
            if (string.IsNullOrWhiteSpace(lop.MaLop)) throw new ArgumentException("Mã lớp không được để trống.");
            if (string.IsNullOrWhiteSpace(lop.TenLop)) throw new ArgumentException("Tên lớp không được để trống.");
            if (string.IsNullOrWhiteSpace(lop.MaKhoa)) throw new ArgumentException("Mã khoa không được để trống.");
        }
    }
}
