// Ví dụ code-behind cho WinForms. Thay tên control theo form của bạn.
using System;
using System.Windows.Forms;
using QuanLySinhVien.Business;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.View
{
    public partial class SinhVienForm : Form
    {
        private readonly SinhVienBUS bus = new SinhVienBUS();

        public SinhVienForm()
        {
            InitializeComponent();
        }

        private void LoadSinhVien()
        {
            try
            {
                dgvSinhVien.DataSource = bus.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                var sv = new SinhVien
                {
                    MaSV = txtMaSV.Text.Trim(),
                    HoTen = txtHoTen.Text.Trim(),
                    GioiTinh = cboGioiTinh.Text,
                    NgaySinh = dtpNgaySinh.Value.ToString("yyyy-MM-dd"),
                    MaLop = txtMaLop.Text.Trim(),
                    Diem = double.Parse(txtDiem.Text),
                    SoDienThoai = txtSoDienThoai.Text.Trim(),
                    Email = txtEmail.Text.Trim()
                };

                bus.Add(sv);
                MessageBox.Show("Thêm sinh viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadSinhVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
