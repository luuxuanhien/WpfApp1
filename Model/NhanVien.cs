using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class NhanVien
    {
        public int MaNV { get; set; }              // Mã nhân viên
        public string HoTen { get; set; }  
        public string CCCD { get; set; }// Họ tên
        public string MaLuong { get; set; }        // Mã lương
        public string MaPB { get; set; }           // Mã phòng ban
        public string MaBP { get; set; }           // Mã bộ phận
        public string SDT { get; set; }            // Số điện thoại
        public string SEX { get; set; }            // Giới tính
        public string HocVan { get; set; }         // Học vấn
        public DateTime? NgayKyHopDong { get; set; }  // Ngày ký hợp đồng (có thể null)
        public DateTime? NgayKetThucHopDong { get; set; } // Ngày kết thúc hợp đồng (có thể null)
        public string LoaiHopDong { get; set; }    // Loại hợp đồng
        public string Adress { get; set; }        // Địa chỉ
        public int Age { get; set; }               // Tuổi
        public string Email { get; set; }          // Email
        public string Note { get; set; }           // Ghi chú
    }
}
