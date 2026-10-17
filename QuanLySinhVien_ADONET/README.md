# QuanLySinhVien - ADO.NET SQL Server

Bộ mã mẫu cho project quản lý sinh viên: DAL/DAO thao tác SQL Server bằng ADO.NET, Business kiểm tra dữ liệu và ném ngoại lệ, View bắt ngoại lệ.

## 1. Cài package
Trong project C# của bạn chạy:

```bash
dotnet add package Microsoft.Data.SqlClient
```

Nếu project đã cài `Microsoft.Data.SqlClient` thì không cần cài lại. Không cần `MySql.Data` hoặc `Mysqlx.Cursor` cho SQL Server.

## 2. Cấu hình kết nối
Mở `Data/DAL/DataHelper.cs` và sửa `Server=...` thành SQL Server instance trên máy bạn. Ví dụ:

```csharp
private readonly string connectionString =
    @"Server=LAPTOP-6SN65NCS\HUECTMDB;Database=QLSV;Integrated Security=True;TrustServerCertificate=True";
```

## 3. Database
Chạy `Database/QLSV.sql` trong SQL Server Management Studio nếu bạn muốn tạo database mẫu. Nếu đã có database, đối chiếu tên bảng/cột với script trước khi chạy.

## 4. Ghép vào project hiện tại
- Chép các thư mục `Data`, `Business`, `View` vào project hoặc thêm file vào project hiện tại.
- Điều chỉnh namespace nếu project bạn đang dùng namespace khác.
- `View/SinhVienViewExample.cs` là ví dụ minh họa cho WinForms; thay tên control cho đúng giao diện của bạn.
- Mẫu `LopHoc` giả định bảng có `MaLop`, `TenLop`, `MaKhoa`. Hãy sửa nếu schema thật khác.

## 5. Quy tắc ngoại lệ
- DataHelper không nuốt ngoại lệ SQL; ngoại lệ truyền lên caller.
- DAL ném ngoại lệ nếu thao tác cập nhật/xóa không tác động dòng nào.
- Business xác thực dữ liệu và bọc ngoại lệ với `InnerException` để giữ nguyên nguyên nhân.
- View bắt ngoại lệ và hiển thị thông báo.

## 6. Commit lên Git
Từ thư mục gốc repository của bạn, chép các file phù hợp vào project, sau đó:

```bash
git status
git add .
git commit -m "Implement ADO.NET DAL and exception handling"
git push
```

Lệnh trên chỉ commit lên repository của bạn khi chạy trong đúng thư mục Git và đã cấu hình remote.
