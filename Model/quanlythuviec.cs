using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class quanlythuviec
    {
        public int MaNVT { get; set; }  // Mã nhân viên thôi việc
        public string HoTen { get; set; }
        public string Sex { get; set; }  // Giới tính
        public string CCCD { get; set; }  // Căn cước công dân
        public string SDT { get; set; }  // Số điện thoại
        public string ViTriTV { get; set; }  // Vị trí trước khi nghỉ việc
        public DateTime? NgayBatDau { get; set; }  // Ngày bắt đầu làm việc
        public DateTime? NgayKetThuc { get; set; }  // Ngày kết thúc làm việc
        public string TruongDaiHoc { get; set; }  // Trường Đại học
        public string HocVan { get; set; }  // Trình độ học vấn
        public string Email { get; set; }
        public string Note { get; set; }  // Ghi chú
    }
}
