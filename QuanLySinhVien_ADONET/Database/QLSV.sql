-- Script database mẫu. Chỉ chạy nếu bạn cần tạo schema mới.
IF DB_ID(N'QLSV') IS NULL CREATE DATABASE QLSV;
GO
USE QLSV;
GO

IF OBJECT_ID(N'dbo.LopHoc', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.LopHoc
    (
        MaLop NVARCHAR(50) NOT NULL PRIMARY KEY,
        TenLop NVARCHAR(200) NOT NULL,
        MaKhoa NVARCHAR(50) NOT NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.SinhVien', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.SinhVien
    (
        MaSV NVARCHAR(50) NOT NULL PRIMARY KEY,
        MaLop NVARCHAR(50) NOT NULL,
        HoTen NVARCHAR(200) NOT NULL,
        GioiTinh NVARCHAR(20) NULL,
        NgaySinh DATE NOT NULL,
        Diem FLOAT NOT NULL CONSTRAINT CK_SinhVien_Diem CHECK (Diem >= 0 AND Diem <= 10),
        SoDienThoai NVARCHAR(30) NULL,
        Email NVARCHAR(200) NULL,
        CONSTRAINT FK_SinhVien_LopHoc FOREIGN KEY (MaLop) REFERENCES dbo.LopHoc(MaLop)
    );
END;
GO
