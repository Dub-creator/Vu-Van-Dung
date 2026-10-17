using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.Data.DAL
{
    public class SinhVienDAO
    {
        private readonly DataHelper dataHelper = new DataHelper();

        private static SinhVien Map(DataRow row) => new SinhVien
        {
            MaSV = Value(row, "MaSV"),
            HoTen = Value(row, "HoTen"),
            GioiTinh = Value(row, "GioiTinh"),
            NgaySinh = row["NgaySinh"] == DBNull.Value ? string.Empty : Convert.ToDateTime(row["NgaySinh"]).ToString("yyyy-MM-dd"),
            SoDienThoai = Value(row, "SoDienThoai"),
            MaLop = Value(row, "MaLop"),
            Diem = row["Diem"] == DBNull.Value ? 0 : Convert.ToDouble(row["Diem"]),
            Email = Value(row, "Email")
        };

        private static string Value(DataRow row, string column) =>
            row[column] == DBNull.Value ? string.Empty : Convert.ToString(row[column]) ?? string.Empty;

        public List<SinhVien> GetAll()
        {
            DataTable table = dataHelper.GetDataTable("SELECT MaSV, HoTen, GioiTinh, NgaySinh, SoDienThoai, MaLop, Diem, Email FROM SinhVien");
            var result = new List<SinhVien>();
            foreach (DataRow row in table.Rows) result.Add(Map(row));
            return result;
        }

        public SinhVien? GetByMaSV(string maSV)
        {
            DataTable table = dataHelper.GetDataTable(
                "SELECT MaSV, HoTen, GioiTinh, NgaySinh, SoDienThoai, MaLop, Diem, Email FROM SinhVien WHERE MaSV = @MaSV",
                new SqlParameter("@MaSV", SqlDbType.NVarChar, 50) { Value = maSV });
            return table.Rows.Count == 0 ? null : Map(table.Rows[0]);
        }

        public List<SinhVien> GetByMaLop(string maLop)
        {
            DataTable table = dataHelper.GetDataTable(
                "SELECT MaSV, HoTen, GioiTinh, NgaySinh, SoDienThoai, MaLop, Diem, Email FROM SinhVien WHERE MaLop = @MaLop",
                new SqlParameter("@MaLop", SqlDbType.NVarChar, 50) { Value = maLop });
            var result = new List<SinhVien>();
            foreach (DataRow row in table.Rows) result.Add(Map(row));
            return result;
        }

        public void Add(SinhVien sv)
        {
            const string sql = @"INSERT INTO SinhVien (MaSV, MaLop, HoTen, GioiTinh, NgaySinh, Diem, SoDienThoai, Email)
                                 VALUES (@MaSV, @MaLop, @HoTen, @GioiTinh, @NgaySinh, @Diem, @SoDienThoai, @Email)";
            int rows = dataHelper.ExecuteNonQuery(sql, Parameters(sv));
            if (rows != 1) throw new InvalidOperationException("Không thêm được sinh viên.");
        }

        public void Update(SinhVien sv)
        {
            const string sql = @"UPDATE SinhVien SET MaLop=@MaLop, HoTen=@HoTen, GioiTinh=@GioiTinh,
                                 NgaySinh=@NgaySinh, Diem=@Diem, SoDienThoai=@SoDienThoai, Email=@Email
                                 WHERE MaSV=@MaSV";
            int rows = dataHelper.ExecuteNonQuery(sql, Parameters(sv));
            if (rows == 0) throw new KeyNotFoundException($"Không tìm thấy sinh viên có mã '{sv.MaSV}'.");
        }

        public void Delete(string maSV)
        {
            int rows = dataHelper.ExecuteNonQuery(
                "DELETE FROM SinhVien WHERE MaSV=@MaSV",
                new SqlParameter("@MaSV", SqlDbType.NVarChar, 50) { Value = maSV });
            if (rows == 0) throw new KeyNotFoundException($"Không tìm thấy sinh viên có mã '{maSV}'.");
        }

        private static SqlParameter[] Parameters(SinhVien sv) => new[]
        {
            new SqlParameter("@MaSV", SqlDbType.NVarChar, 50) { Value = sv.MaSV },
            new SqlParameter("@MaLop", SqlDbType.NVarChar, 50) { Value = sv.MaLop },
            new SqlParameter("@HoTen", SqlDbType.NVarChar, 200) { Value = sv.HoTen },
            new SqlParameter("@GioiTinh", SqlDbType.NVarChar, 20) { Value = sv.GioiTinh },
            new SqlParameter("@NgaySinh", SqlDbType.Date) { Value = DateTime.Parse(sv.NgaySinh) },
            new SqlParameter("@Diem", SqlDbType.Float) { Value = sv.Diem },
            new SqlParameter("@SoDienThoai", SqlDbType.NVarChar, 30) { Value = (object?)sv.SoDienThoai ?? DBNull.Value },
            new SqlParameter("@Email", SqlDbType.NVarChar, 200) { Value = (object?)sv.Email ?? DBNull.Value }
        };
    }
}
