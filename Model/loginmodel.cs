using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Data.SqlClient;
using System.Windows.Controls;
using WpfApp1.View.HeThong;
using WpfApp1.Model;
namespace WpfApp1.Model
{
    public class loginmodel
    {
        private string connect = "Server = LAPTOP-9S7GSQM5\\SQLEXPRESS; Initial Catalog = QLNS; Integrated Security = True; TrustServerCertificate=True";

        public taikhoan GetTaikhoan(string tenDN, string matKhau)
        {
            using (var connection = new SqlConnection(connect))
            {
                
                
                    string query = "SELECT * FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau";
                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TenDangNhap", tenDN);
                    command.Parameters.AddWithValue("@MatKhau", matKhau);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // Chỉ cần kiểm tra lần đầu nếu có dữ liệu
                        {
                            return new taikhoan
                            {
                                MaTK = (int)reader["MaTK"],
                                MaLoaiTK = (int)reader["MaLoaiTK"],
                                TenTK = reader["TenTK"].ToString(),
                                TenDN = reader["TenDangNhap"].ToString(),
                                MatKhau = reader["MatKhau"].ToString()
                            };
                        }
                    }
                
                
            }
            return null;
        }

        public phanquyen phanloaitk(int maloaitk)
        {
            using (var connection = new SqlConnection(connect))
            {
                
                
                    string query = "SELECT * FROM PhanLoaiTK WHERE MaLoaiTK = @MaLoaiTK";
                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaLoaiTK", maloaitk);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // Kiểm tra nếu có dữ liệu
                        {
                            return new phanquyen
                            {
                                MaLoaiTK = (int)reader["MaLoaiTK"],
                                TenMaLoaiTK = reader["TenMaLoaiTK"].ToString()
                            };
                        }
                    }
                
                
            }
            return null;
        }
    }
}

