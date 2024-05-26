using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace DoAn1.DTO
{
    public class Bill
    {
        // Khai báo các trường dữ liệu riêng tư

        public int MA_HOA_DON { get; set; }
        public int MA_BAN { get; set; }
        public int MA_NHAN_VIEN { get; set; }
        public DateTime? NGAY_LAP { get; set; } // Sử dụng DateTime? để cho phép NGAY_LAP có thể null
        public int TRANG_THAI { get; set; }

        // Hàm tạo không tham số
        public Bill(DataRow row)
        {
            this.MA_HOA_DON = (int)row["MA_HOA_DON"];
            this.MA_BAN = (int)row["MA_BAN"];
            this.MA_NHAN_VIEN = (int)row["MA_NHAN_VIEN"];
            this.NGAY_LAP = (DateTime?)row["NGAY_LAP"];
            this.TRANG_THAI = (int)row["STATUS"];
        }

        // Hàm tạo có tham số
        public Bill(int maHoaDon, int maBan, int maNhanVien, DateTime? ngayLap, int trangThai)
        {
            this.MA_HOA_DON = maHoaDon;
            this.MA_BAN = maBan;
            this.MA_NHAN_VIEN = maNhanVien;
            this.NGAY_LAP = ngayLap;
            this.TRANG_THAI = trangThai;
        }
    }

}
