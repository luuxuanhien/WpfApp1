using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using WpfApp1.View.bophan;
using WpfApp1.View.NhanVien;
using WpfApp1.ViewModel;

namespace WpfApp1.Model
{
    //Đưa data của phong ban bộ phận lên combobox
    public class ChucNangThongTinNhanVien
    {
        private string connect = "Server = LAPTOP-9S7GSQM5\\SQLEXPRESS; Initial Catalog = QLNS; Integrated Security = True; TrustServerCertificate=True";
        //Hàm lấy ra bộ phận
        public List<bophan> GetBoPhan()
        {
            var bophans = new List<bophan>();
            using(var connection = new SqlConnection(connect))
            {
                connection.Open();
                var query = "SELECT MaBP, TenBP FROM BoPhan";
                var command = new SqlCommand(query, connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    bophans.Add(new bophan
                    {
                        MaBP = reader.GetString(0),
                        TenBP = reader.GetString(1)
                    });
                }

            }
            return bophans;
        }
        // Hàm lấy ra phòng ban phụ thuộc vào bộ phận
        public List<phongban> GetPhongbansbyBoPhan(string mabp)
        {
            var phongbans = new List<phongban>();
            using(var connection = new SqlConnection(connect))
            {
                connection.Open();
                var query = "SELECT MaPB, TenPB FROM PhongBan WHERE MaBP = @MaBP";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaBP", mabp);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    phongbans.Add(new phongban
                    {
                        

                        MaPB = reader.GetString(0),
                        TenPB = reader.GetString(1),

                    });
                }
            }
            return phongbans;
        }
        //Hàm Lấy ra phòng ban
        public List<phongban> GetPhongBan()
        {
            var phongban1s = new List<phongban>();
            using (var connection = new SqlConnection(connect))
            {
                connection.Open();
                var query = "SELECT MaPB, TenPB FROM PhongBan";
                var command = new SqlCommand(query, connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    phongban1s.Add(new phongban
                    {
                        MaPB = reader.GetString(0),
                        TenPB = reader.GetString(1)
                    });
                }

            }
            return phongban1s;
        }
        //Hàm load lấy ra tất cả nhân viên
        public List<NhanVien> GetNhanViens()
        {
            var nhanViens = new List<NhanVien>();

            string query = "SELECT MaNV, HoTen, CCCD, MaLuong, MaPB, MaBP, SDT, SEX, HocVan, NgayKyHopDong, NgayKetThucHopDong, LoaiHopDong, Adress, Age, Email, Note FROM NhanVien";

            using (SqlConnection connection = new SqlConnection(connect))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                // Lặp qua kết quả truy vấn và tạo danh sách nhân viên
                while (reader.Read())
                {
                    var nhanVien = new NhanVien
                    {
                     

                        MaNV = reader.GetInt32(0),
                        HoTen = reader.IsDBNull(1) ? "Không có tên" : reader.GetString(1),
                        CCCD = reader.IsDBNull(2) ? "Không có CCCD" : reader.GetString(2),
                        MaLuong = reader.IsDBNull(3) ? "Không có mã lương" : reader.GetString(3),
                        MaPB = reader.IsDBNull(4) ? "Không có mã phòng" : reader.GetString(4),
                        MaBP = reader.IsDBNull(5) ? "Không có mã bộ phận" : reader.GetString(5),
                        SDT = reader.IsDBNull(6) ? "Chưa có SĐT" : reader.GetString(6),
                        SEX = reader.IsDBNull(7) ? "Không rõ" : reader.GetString(7),
                        HocVan = reader.IsDBNull(8) ? "Chưa cập nhật" : reader.GetString(8),
                        NgayKyHopDong = reader.IsDBNull(9) ? (DateTime?)null : reader.GetDateTime(9),
                        NgayKetThucHopDong = reader.IsDBNull(10) ? (DateTime?)null : reader.GetDateTime(10),
                        LoaiHopDong = reader.IsDBNull(11) ? "Không có loại HĐ" : reader.GetString(11),
                        Adress = reader.IsDBNull(12) ? "Không có địa chỉ" : reader.GetString(12),
                        Age = reader.IsDBNull(13) ? 0 : reader.GetInt32(13),  // Giá trị mặc định là 0 nếu NULL
                        Email = reader.IsDBNull(14) ? "Không có email" : reader.GetString(14),
                        Note = reader.IsDBNull(15) ? "" : reader.GetString(15)


                    };

                    nhanViens.Add(nhanVien);
                }
            }

            return nhanViens;
        }
        //Lọc Nhân Viên theo phòng ban với bộ phận rồi hiển thị ra listview
        public List<NhanVien> GetNhanViensLoc(string mabp = null, string mapb = null)
        {
            var nhanViens = new List<WpfApp1.Model.NhanVien>();

            var query = "SELECT * FROM NhanVien WHERE 1=1";
            if (!string.IsNullOrEmpty(mabp))
            {
                query += " AND MaBP = @MaBP"; // Lọc theo phòng ban nếu có

            }
               

            if (!string.IsNullOrEmpty(mapb))
            {
                query += " AND MaPB = @MaPB"; // Lọc theo bộ phận nếu có

            }
              

            using (SqlConnection connection = new SqlConnection(connect))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (!string.IsNullOrEmpty(mabp))
                {
                    command.Parameters.AddWithValue("@MaBP", mabp);
                }
                    

                if (!string.IsNullOrEmpty(mapb))
                {
                    command.Parameters.AddWithValue("@MaPB", mapb);
                }
                    
                SqlDataReader reader = command.ExecuteReader();

                // Lặp qua kết quả truy vấn và tạo danh sách nhân viên
                while (reader.Read())
                {
                    var nhanVien = new NhanVien
                    {
                        MaNV = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0,
                        HoTen = !reader.IsDBNull(1) ? reader.GetString(1) : "Không có tên",
                        CCCD = !reader.IsDBNull(2) ? reader.GetString(2) : "Không có CCCD",
                        MaLuong = !reader.IsDBNull(3) ? reader.GetString(3) : "Không có mã lương",
                        MaPB = !reader.IsDBNull(4) ? reader.GetString(4) : "Không có mã phòng ban",
                        MaBP = !reader.IsDBNull(5) ? reader.GetString(5) : "Không có mã bộ phận",
                        SDT = !reader.IsDBNull(6) ? reader.GetString(6) : "Không có số điện thoại",
                        SEX = !reader.IsDBNull(7) ? reader.GetString(7) : "Không xác định",
                        HocVan = !reader.IsDBNull(8) ? reader.GetString(8) : "Chưa có thông tin học vấn",
                        NgayKyHopDong = !reader.IsDBNull(9) ? reader.GetDateTime(9) : (DateTime?)null,
                        NgayKetThucHopDong = !reader.IsDBNull(10) ? reader.GetDateTime(10) : (DateTime?)null,
                        LoaiHopDong = !reader.IsDBNull(11) ? reader.GetString(11) : "Không có loại hợp đồng",
                        Adress = !reader.IsDBNull(12) ? reader.GetString(12) : "Chưa có địa chỉ",
                        Age = !reader.IsDBNull(13) ? reader.GetInt32(13) : 0, //
                        Email = !reader.IsDBNull(14) ? reader.GetString(14) : "Không có email",
                        Note = !reader.IsDBNull(15) ? reader.GetString(15) : "Không có ghi chú",


                    };

                    nhanViens.Add(nhanVien);
                }
            }

            return nhanViens;
        }
  
        //insert nhân viên từ user
        public void AddNhanVienFromUser(NhanVien nhanvien)
        {
            try
            {
                using (var connection = new SqlConnection(connect))
                {
                    connection.Open();

                    string query = @"INSERT INTO NhanVien (MaNV, HoTen, CCCD, MaLuong, MaPB, MaBP, SDT, SEX, HocVan, 
                            NgayKyHopDong, NgayKetThucHopDong, LoaiHopDong, Adress, Age, Email, Note) 
                            VALUES (@MaNV, @HoTen, @CCCD, @MaLuong, @MaPB, @MaBP, @SDT, @SEX, @HocVan, 
                            @NgayKyHopDong, @NgayKetThucHopDong, @LoaiHopDong, @Adress, @Age, @Email, @Note)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaNV", nhanvien.MaNV);
                        command.Parameters.AddWithValue("@HoTen", string.IsNullOrEmpty(nhanvien.HoTen) ? (object)DBNull.Value : nhanvien.HoTen);
                        command.Parameters.AddWithValue("@CCCD", string.IsNullOrEmpty(nhanvien.CCCD) ? (object)DBNull.Value : nhanvien.CCCD);
                        command.Parameters.AddWithValue("@MaLuong", nhanvien.MaLuong);
                        command.Parameters.AddWithValue("@MaPB", nhanvien.MaPB);
                        command.Parameters.AddWithValue("@MaBP", nhanvien.MaBP);
                        command.Parameters.AddWithValue("@SDT", string.IsNullOrEmpty(nhanvien.SDT) ? (object)DBNull.Value : nhanvien.SDT);
                        command.Parameters.AddWithValue("@SEX", string.IsNullOrEmpty(nhanvien.SEX) ? (object)DBNull.Value : nhanvien.SEX);
                        command.Parameters.AddWithValue("@HocVan", string.IsNullOrEmpty(nhanvien.HocVan) ? (object)DBNull.Value : nhanvien.HocVan);

                        // Kiểm tra NULL cho ngày tháng
                        command.Parameters.AddWithValue("@NgayKyHopDong", nhanvien.NgayKyHopDong ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@NgayKetThucHopDong", nhanvien.NgayKetThucHopDong ?? (object)DBNull.Value);

                        command.Parameters.AddWithValue("@LoaiHopDong", nhanvien.LoaiHopDong);
                        command.Parameters.AddWithValue("@Adress", string.IsNullOrEmpty(nhanvien.Adress) ? (object)DBNull.Value : nhanvien.Adress);
                        command.Parameters.AddWithValue("@Age", nhanvien.Age);

                        // Kiểm tra NULL cho email và note
                        command.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(nhanvien.Email) ? (object)DBNull.Value : nhanvien.Email);
                        command.Parameters.AddWithValue("@Note", string.IsNullOrEmpty(nhanvien.Note) ? (object)DBNull.Value : nhanvien.Note);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                            MessageBox.Show("Thêm nhân viên thành công!");

                        else
                            MessageBox.Show("Không có dòng nào được thêm!");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi khi thêm nhân viên: " + e.Message);
            }
        }
        // xóa nhân viên đưa vào danh sách nhân viên nghỉ việc
        public void XoaNhanVien(int maNV, string cccd, string lyDoXoa, DateTime ngaythoiviec)
        {
            using(SqlConnection connection = new SqlConnection(connect))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string insertQuery = @"
                    INSERT INTO NhanVienDoiViec (MaNV, CCCD, LyDo, NgayThoiViec)
                    VALUES (@MaNV, @CCCD, @LyDoXoa, @NgayXoa)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        cmd.Parameters.AddWithValue("@CCCD", cccd);
                        cmd.Parameters.AddWithValue("@LyDoXoa", lyDoXoa);
                        cmd.Parameters.AddWithValue("@NgayXoa", ngaythoiviec);
                        cmd.ExecuteNonQuery();
                    }
                    string deleteQuery = "DELETE FROM NhanVien WHERE MaNV = @MaNV";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                         cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();

                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
               

            }
        }
        // lấy tên bộ phân từ mã bộ phận lấy tên phong ban từ mã phòng ban 
        public string LayTenBoPhanTuMaBP(string mabp)

        {
            string tenBP = "Không Xác Định";
            string query = "SELECT TenBP FROM BoPhan WHERE MaBP = @MaBP";

            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
              
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaBP", mabp);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        tenBP = result.ToString();
                    }

                }                                    
            }
            return tenBP;
        }
        public string LayMaBoPhanTuTenBoPhan(string mabp)

        {
            string tenBP = "Không Xác Định";
            string query = "SELECT MaBP FROM BoPhan WHERE TenBP = @MaBP";

            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaBP", mabp);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        tenBP = result.ToString();
                    }

                }
            }
            return tenBP;
        }
        public string LayTenPhongBanTuMaPB(string mapb)

        {
            string tenPB = "Không Xác Định";
            string query = "SELECT TenPB FROM PhongBan WHERE MaPB = @MaPB";

            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaPB", mapb);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        tenPB = result.ToString();
                    }

                }
            }
            return tenPB;
        }
        public string LayMaPhongBanTuMaPB(string mapb)

        {
            string tenPB = "Không Xác Định";
            string query = "SELECT MaPB FROM PhongBan WHERE TenPB =@MaPB";

            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaPB", mapb);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        tenPB = result.ToString();
                    }

                }
            }
            return tenPB;
        }
        // sửa đổi thông tin nhân viên
        public void SuaThongTinNhanVien(NhanVien nhanvien)
        {
            using(SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                try
                {
                    string query = "UPDATE NhanVien SET HoTen = @TenNV, CCCD = @CCCD, MaLuong = @MaLuong, MaPB = @MaPB, MaBP = @MaBP, SDT = @SDT, SEX = @SEX, HocVan = @HocVan, NgayKyHopDong = @NgayKyHopDong, NgayKetThucHopDong = @NgayKetThucHopDong, LoaiHopDong = @LoaiHopDong, Adress = @Adress, Age = @Age, Email = @Email, Note = @Note WHERE MaNV = @MaNV";
                    using(SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@MaNV", nhanvien.MaNV);
                        command.Parameters.AddWithValue("@TenNV", string.IsNullOrEmpty(nhanvien.HoTen) ? (object)DBNull.Value : nhanvien.HoTen);
                        command.Parameters.AddWithValue("@CCCD", string.IsNullOrEmpty(nhanvien.CCCD) ? (object)DBNull.Value : nhanvien.CCCD);
                        command.Parameters.AddWithValue("@MaLuong", nhanvien.MaLuong);
                        command.Parameters.AddWithValue("@MaPB", nhanvien.MaPB);
                        command.Parameters.AddWithValue("@MaBP", nhanvien.MaBP);
                        command.Parameters.AddWithValue("@SDT", string.IsNullOrEmpty(nhanvien.SDT) ? (object)DBNull.Value : nhanvien.SDT);
                        command.Parameters.AddWithValue("@SEX", string.IsNullOrEmpty(nhanvien.SEX) ? (object)DBNull.Value : nhanvien.SEX);
                        command.Parameters.AddWithValue("@HocVan", string.IsNullOrEmpty(nhanvien.HocVan) ? (object)DBNull.Value : nhanvien.HocVan);

                        // Kiểm tra NULL cho ngày tháng
                        command.Parameters.AddWithValue("@NgayKyHopDong", nhanvien.NgayKyHopDong ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@NgayKetThucHopDong", nhanvien.NgayKetThucHopDong ?? (object)DBNull.Value);

                        command.Parameters.AddWithValue("@LoaiHopDong", nhanvien.LoaiHopDong);
                        command.Parameters.AddWithValue("@Adress", string.IsNullOrEmpty(nhanvien.Adress) ? (object)DBNull.Value : nhanvien.Adress);
                        command.Parameters.AddWithValue("@Age", nhanvien.Age);

                        // Kiểm tra NULL cho email và note
                        command.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(nhanvien.Email) ? (object)DBNull.Value : nhanvien.Email);
                        command.Parameters.AddWithValue("@Note", string.IsNullOrEmpty(nhanvien.Note) ? (object)DBNull.Value : nhanvien.Note);
                        command.ExecuteNonQuery();


                    }

                }
                catch(Exception e)
                {
                    MessageBox.Show("Lỗi khi sửa nhân viên: " + e.Message);

                }

            }
        }
        // lấy ra email dựa trên tên nhân viên 
        public string LayRaEmailDuaVaoTenNhanVien(string hoTen)
        {
            string email = null;
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                string query = "SELECT Email FROM NhanVien WHERE HoTen = @HoTen";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@HoTen", hoTen);
                    var result = cmd.ExecuteScalar();

                    if (result != null)
                        email = result.ToString();
                }
            }
            return email;
        }
        //Thêm thông tin bảo hiểm vào sql
        public void ThemThongTinBaoHiem(BaoHiem baohiem)
        {
            try
            {
                using(SqlConnection con = new SqlConnection(connect))
                {
                    con.Open();
                    string query = "INSERT INTO BaoHiem (MaNV, MaBH, NgayCap, NoiCap, GhiChu) VALUES (@MaNV, @MaBH, @NgayCap, @NoiCap, @GhiChu)";
                    using(SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", baohiem.MaNV);
                        cmd.Parameters.AddWithValue("@MaBH", baohiem.MaBH);
                        cmd.Parameters.AddWithValue("@NgayCap", baohiem.NgayCap);
                        cmd.Parameters.AddWithValue("@NoiCap", baohiem.NoiCap);
                        cmd.Parameters.AddWithValue("@GhiChu", baohiem.GhiChu);
                        cmd.ExecuteNonQuery();


                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Lỗi khi thêm bảo hiểm: " + e.Message);
            }
        }
        // sửa thông tin bảo hiểm
        public void SuaBaoHiem(BaoHiem baohiem)
        {
            try
            {
                using(SqlConnection con = new SqlConnection(connect))
                {
                    con.Open();
                    string query = "UPDATE BaoHiem SET MaBH = @MaBH, NgayCap = @NgayCap, NoiCap = @NoiCap, GhiChu = @GhiChu WHERE MaNV = @MaNV";
                    using(SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", baohiem.MaNV);
                        cmd.Parameters.AddWithValue("@MaBH", baohiem.MaBH);
                        cmd.Parameters.AddWithValue("@NgayCap", baohiem.NgayCap);
                        cmd.Parameters.AddWithValue("@NoiCap", baohiem.NoiCap);
                        cmd.Parameters.AddWithValue("@GhiChu", baohiem.GhiChu);
                        cmd.ExecuteNonQuery();
                    }
                }

            } catch (Exception e)
            {
                MessageBox.Show("Lỗi khi sửa bảo hiểm: " + e.Message);
            }
        }
        // xóa thông tin bảo hiểm
        public void XoaBaoHiem(int manv)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();
                try
                {  
                    string query = "DELETE FROM BaoHiem WHERE MaNV = @MaNV";
                    using (SqlCommand cmd = new SqlCommand(query, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", manv);
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }

            }
           
        }
        public List<BaoHiem> GetBH()
        {
            using(SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                var baohiems = new List<BaoHiem>();
                string query = "SELECT * FROM BaoHiem";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var baohiem = new BaoHiem
                    {
                        MaBH = reader.GetString(0),
                        MaNV = reader.GetInt32(1),
                        NgayCap = reader.GetDateTime(2),
                        NoiCap = reader.GetString(3),
                        GhiChu = reader.GetString(4)



                    };
                    baohiems.Add(baohiem);
                }
                return baohiems;


            }
        }
        // lấy ra tên nhân viên dựa vào mã nhân viên
        public string GetTenNhanVien(int manv)
        {
            string tenNhanVien = string.Empty;
            string query = "SELECT HoTen FROM NhanVien WHERE MaNV = @MaNV";

            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con)) 
                {
                    cmd.Parameters.AddWithValue("@MaNV", manv); 

                    object result = cmd.ExecuteScalar(); 
                    if (result != null)
                    {
                        tenNhanVien = result.ToString(); 
                    }
                }
            }

            return tenNhanVien; 
        }
        //load nhân viên nghỉ việc lên listview
        public List<nhanviendoiviec> GetNhanVienDoiViecs()
        {
            var danhSach = new List<nhanviendoiviec>();
            string query = "SELECT MaNV, CCCD, NgayThoiViec, LyDo FROM NhanVienDoiViec";

            using (SqlConnection connection = new SqlConnection(connect))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var nhanVienDoiViec = new nhanviendoiviec
                    {
                        MaNV = reader.GetInt32(0),
                        CCCD = reader.IsDBNull(1) ? "Không có CCCD" : reader.GetString(1),
                        NgayThoiViec = reader.IsDBNull(2) ? (DateTime?)null : reader.GetDateTime(2),
                        LyDo = reader.IsDBNull(3) ? "Không có lý do" : reader.GetString(3)
                    };

                    danhSach.Add(nhanVienDoiViec);
                }
            }

            return danhSach;
        }
        // load nhân viên thử việc lên listview
        public List<quanlythuviec> GetDanhSachThuViec()
        {
            var danhSach = new List<quanlythuviec>();
            string query = "SELECT MaNVT, HoTen, Sex, CCCD, SDT, ViTriTV, NgayBatDau, NgayKetThuc, TruongDaiHoc, HocVan, Email, Note FROM QuanLyThuViec";

            using (SqlConnection connection = new SqlConnection(connect))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var nhanVienTV = new quanlythuviec
                            {
                                MaNVT = reader.GetInt32(0),
                                HoTen = reader.IsDBNull(1) ? "Không có họ tên" : reader.GetString(1),
                                Sex = reader.IsDBNull(2) ? "Không có giới tính" : reader.GetString(2),
                                CCCD = reader.IsDBNull(3) ? "Không có CCCD" : reader.GetString(3),
                                SDT = reader.IsDBNull(4) ? "Không có SĐT" : reader.GetString(4),
                                ViTriTV = reader.IsDBNull(5) ? "Không có vị trí" : reader.GetString(5),
                                NgayBatDau = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6),
                                NgayKetThuc = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7),
                                TruongDaiHoc = reader.IsDBNull(8) ? "Không có trường ĐH" : reader.GetString(8),
                                HocVan = reader.IsDBNull(9) ? "Không có học vấn" : reader.GetString(9),
                                Email = reader.IsDBNull(10) ? "Không có email" : reader.GetString(10),
                                Note = reader.IsDBNull(11) ? "Không có ghi chú" : reader.GetString(11)
                            };
                            danhSach.Add(nhanVienTV);
                        }
                    }
                }
            }

            return danhSach;
        }
        // thêm nhân viên thử việc
        public void AddQuanLyThuViec(quanlythuviec nhanVienTV)
        {
            try
            {
                using (var connection = new SqlConnection(connect))
                {
                    connection.Open();

                    string query = @"INSERT INTO QuanLyThuViec (MaNVT, HoTen, Sex, CCCD, SDT, ViTriTV, NgayBatDau, NgayKetThuc, TruongDaiHoc, HocVan, Email, Note) 
                               VALUES (@MaNVT, @HoTen, @Sex, @CCCD, @SDT, @ViTriTV, @NgayBatDau, @NgayKetThuc, @TruongDaiHoc, @HocVan, @Email, @Note)";

                    using (var con = new SqlConnection(connect))
                    {
                        con.Open();
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@MaNVT", nhanVienTV.MaNVT);
                            command.Parameters.AddWithValue("@HoTen", string.IsNullOrEmpty(nhanVienTV.HoTen) ? (object)DBNull.Value : nhanVienTV.HoTen);
                            command.Parameters.AddWithValue("@Sex", string.IsNullOrEmpty(nhanVienTV.Sex) ? (object)DBNull.Value : nhanVienTV.Sex);
                            command.Parameters.AddWithValue("@CCCD", string.IsNullOrEmpty(nhanVienTV.CCCD) ? (object)DBNull.Value : nhanVienTV.CCCD);
                            command.Parameters.AddWithValue("@SDT", string.IsNullOrEmpty(nhanVienTV.SDT) ? (object)DBNull.Value : nhanVienTV.SDT);
                            command.Parameters.AddWithValue("@ViTriTV", string.IsNullOrEmpty(nhanVienTV.ViTriTV) ? (object)DBNull.Value : nhanVienTV.ViTriTV);
                            command.Parameters.AddWithValue("@NgayBatDau", nhanVienTV.NgayBatDau ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@NgayKetThuc", nhanVienTV.NgayKetThuc ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@TruongDaiHoc", string.IsNullOrEmpty(nhanVienTV.TruongDaiHoc) ? (object)DBNull.Value : nhanVienTV.TruongDaiHoc);
                            command.Parameters.AddWithValue("@HocVan", string.IsNullOrEmpty(nhanVienTV.HocVan) ? (object)DBNull.Value : nhanVienTV.HocVan);
                            command.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(nhanVienTV.Email) ? (object)DBNull.Value : nhanVienTV.Email);
                            command.Parameters.AddWithValue("@Note", string.IsNullOrEmpty(nhanVienTV.Note) ? (object)DBNull.Value : nhanVienTV.Note);

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                                MessageBox.Show("Thêm nhân viên thực tập thành công!");
                            else
                                MessageBox.Show("Không có dòng nào được thêm!");
                        }


                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi khi thêm nhân viên thực tập: " + e.Message);
            }
        }
        // xóa nhân viên thử việc
        public void XoaNhanVienThuViec(int maNVT)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    string query = "DELETE FROM QuanLyThuViec WHERE MaNVT = @MaNVT";
                    using (SqlCommand cmd = new SqlCommand(query, con, transaction))
                    {
                        cmd.Parameters.AddWithValue("@MaNVT", maNVT);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        // lấy ra all bộ phận
        public List<bophan> GetAllBoPhan()
        {
            List<bophan> danhSachBoPhan = new List<bophan>();
            string query = "SELECT MaBP, TenBP, NgayThanhLap, Note FROM BoPhan";

            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bophan bp = new bophan
                            {
                                MaBP = reader["MaBP"].ToString(),
                                TenBP = reader["TenBP"].ToString(),
                                NgayThanhLap = Convert.ToDateTime(reader["NgayThanhLap"]),
                                Note = reader["Note"] != DBNull.Value ? reader["Note"].ToString() : null
                            };
                            danhSachBoPhan.Add(bp);
                        }
                    }
                }
            }
            return danhSachBoPhan;
        }
        // lấy ra all phòng ban
        public List<phongban> GetAllPhongBan()
        {
            List<phongban> danhSachPhongBan = new List<phongban>();

            string query = "SELECT MaBP, MaPB, NgayThanhLap, Note, TenPB FROM PhongBan";

            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())  // Đảm bảo reader không null
                        {
                            danhSachPhongBan.Add(new phongban
                            {
                                MaBP = reader["MaBP"].ToString(),
                                MaPB = reader["MaPB"].ToString(),
                                NgayThanhLap = reader["NgayThanhLap"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["NgayThanhLap"]),
                                Note = reader["Note"] != DBNull.Value ? reader["Note"].ToString() : string.Empty,
                                TenPB = reader["TenPB"].ToString()
                            });
                        }
                    }
                }
            }
            return danhSachPhongBan;
        }
        // insert bộ phận 
        public void ThemBoPhan(bophan boPhan)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connect))
                {
                    con.Open();
                    string query = "INSERT INTO BoPhan (MaBP, TenBP, NgayThanhLap, Note) VALUES (@MaBP, @TenBP, @NgayThanhLap, @Note)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@MaBP", boPhan.MaBP);
                        cmd.Parameters.AddWithValue("@TenBP", boPhan.TenBP);

                        if (boPhan.NgayThanhLap.HasValue)
                            cmd.Parameters.AddWithValue("@NgayThanhLap", boPhan.NgayThanhLap.Value);
                        else
                            cmd.Parameters.AddWithValue("@NgayThanhLap", DBNull.Value);

                        if (!string.IsNullOrEmpty(boPhan.Note))
                            cmd.Parameters.AddWithValue("@Note", boPhan.Note);
                        else
                            cmd.Parameters.AddWithValue("@Note", DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi khi cập nhật thông tin bộ phận: " + e.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // sửa bộ phận
        public void SuaBoPhan(bophan boPhan)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connect))
                {
                    con.Open();
                    string query = "UPDATE BoPhan SET TenBP = @TenBP, NgayThanhLap = @NgayThanhLap, Note = @Note WHERE MaBP = @MaBP";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@MaBP", boPhan.MaBP);
                        cmd.Parameters.AddWithValue("@TenBP", boPhan.TenBP);

                        if (boPhan.NgayThanhLap.HasValue)
                            cmd.Parameters.AddWithValue("@NgayThanhLap", boPhan.NgayThanhLap.Value);
                        else
                            cmd.Parameters.AddWithValue("@NgayThanhLap", DBNull.Value);

                        if (!string.IsNullOrEmpty(boPhan.Note))
                            cmd.Parameters.AddWithValue("@Note", boPhan.Note);
                        else
                            cmd.Parameters.AddWithValue("@Note", DBNull.Value);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật thông tin bộ phận thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy bộ phận để cập nhật!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi khi cập nhật thông tin bộ phận: " + e.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // thêm phòng ban
        public void ThemPhongBan(phongban phongBan)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connect))
                {
                    con.Open();
                    string query = "INSERT INTO PhongBan (MaBP, MaPB, TenPB, NgayThanhLap, Note) VALUES (@MaBP, @MaPB, @TenPB, @NgayThanhLap, @Note)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@MaBP", phongBan.MaBP);
                        cmd.Parameters.AddWithValue("@MaPB", phongBan.MaPB);
                        cmd.Parameters.AddWithValue("@TenPB", phongBan.TenPB);

                        if (phongBan.NgayThanhLap.HasValue)
                            cmd.Parameters.AddWithValue("@NgayThanhLap", phongBan.NgayThanhLap.Value);
                        else
                            cmd.Parameters.AddWithValue("@NgayThanhLap", DBNull.Value);

                        if (!string.IsNullOrEmpty(phongBan.Note))
                            cmd.Parameters.AddWithValue("@Note", phongBan.Note);
                        else
                            cmd.Parameters.AddWithValue("@Note", DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi khi cập nhật thông tin phòng ban: " + e.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void SuaPhongBan(phongban phongBan)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connect))
                {
                    con.Open();
                    string query = "UPDATE PhongBan SET TenPB = @TenPB, NgayThanhLap = @NgayThanhLap, Note = @Note WHERE MaPB = @MaPB";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@MaPB", phongBan.MaPB);
                        cmd.Parameters.AddWithValue("@TenPB", phongBan.TenPB);

                        if (phongBan.NgayThanhLap.HasValue)
                            cmd.Parameters.AddWithValue("@NgayThanhLap", phongBan.NgayThanhLap.Value);
                        else
                            cmd.Parameters.AddWithValue("@NgayThanhLap", DBNull.Value);

                        if (!string.IsNullOrEmpty(phongBan.Note))
                            cmd.Parameters.AddWithValue("@Note", phongBan.Note);
                        else
                            cmd.Parameters.AddWithValue("@Note", DBNull.Value);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật thông tin phòng ban thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy phòng ban để cập nhật!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật thông tin phòng ban: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



    }


}
