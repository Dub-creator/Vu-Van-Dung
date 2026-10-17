using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.Data.DAL
{
    public class LopHocDAO
    {
        private readonly DataHelper dataHelper = new DataHelper();

        private static LopHoc Map(DataRow row) => new LopHoc
        {
            MaLop = row["MaLop"] == DBNull.Value ? string.Empty : Convert.ToString(row["MaLop"]) ?? string.Empty,
            TenLop = row["TenLop"] == DBNull.Value ? string.Empty : Convert.ToString(row["TenLop"]) ?? string.Empty,
            MaKhoa = row["MaKhoa"] == DBNull.Value ? string.Empty : Convert.ToString(row["MaKhoa"]) ?? string.Empty
        };

        public List<LopHoc> GetAll()
        {
            DataTable table = dataHelper.GetDataTable("SELECT MaLop, TenLop, MaKhoa FROM LopHoc");
            var result = new List<LopHoc>();
            foreach (DataRow row in table.Rows) result.Add(Map(row));
            return result;
        }

        public LopHoc? GetByMaLop(string maLop)
        {
            DataTable table = dataHelper.GetDataTable(
                "SELECT MaLop, TenLop, MaKhoa FROM LopHoc WHERE MaLop=@MaLop",
                new SqlParameter("@MaLop", SqlDbType.NVarChar, 50) { Value = maLop });
            return table.Rows.Count == 0 ? null : Map(table.Rows[0]);
        }

        public void Add(LopHoc lop)
        {
            int rows = dataHelper.ExecuteNonQuery(
                "INSERT INTO LopHoc (MaLop, TenLop, MaKhoa) VALUES (@MaLop, @TenLop, @MaKhoa)",
                new SqlParameter("@MaLop", SqlDbType.NVarChar, 50) { Value = lop.MaLop },
                new SqlParameter("@TenLop", SqlDbType.NVarChar, 200) { Value = lop.TenLop },
                new SqlParameter("@MaKhoa", SqlDbType.NVarChar, 50) { Value = lop.MaKhoa });
            if (rows != 1) throw new InvalidOperationException("Không thêm được lớp học.");
        }

        public void Update(LopHoc lop)
        {
            int rows = dataHelper.ExecuteNonQuery(
                "UPDATE LopHoc SET TenLop=@TenLop, MaKhoa=@MaKhoa WHERE MaLop=@MaLop",
                new SqlParameter("@MaLop", SqlDbType.NVarChar, 50) { Value = lop.MaLop },
                new SqlParameter("@TenLop", SqlDbType.NVarChar, 200) { Value = lop.TenLop },
                new SqlParameter("@MaKhoa", SqlDbType.NVarChar, 50) { Value = lop.MaKhoa });
            if (rows == 0) throw new KeyNotFoundException($"Không tìm thấy lớp '{lop.MaLop}'.");
        }

        public void Delete(string maLop)
        {
            int rows = dataHelper.ExecuteNonQuery(
                "DELETE FROM LopHoc WHERE MaLop=@MaLop",
                new SqlParameter("@MaLop", SqlDbType.NVarChar, 50) { Value = maLop });
            if (rows == 0) throw new KeyNotFoundException($"Không tìm thấy lớp '{maLop}'.");
        }
    }
}
