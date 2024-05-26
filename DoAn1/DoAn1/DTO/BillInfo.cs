using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DoAn1.DTO
{
    public class BillInfo
    {
 
        public int MA_HOA_DON { get; set; }
        public int MA_MON_AN { get; set; }
        public int SO_LUONG { get; set; }
        public decimal THANH_TIEN { get; set; }

        public BillInfo(int maHoaDon, int maMonAn, int soLuong, decimal thanhTien)
        {
            MA_HOA_DON = maHoaDon;
            MA_MON_AN = maMonAn;
            SO_LUONG = soLuong;
            THANH_TIEN = thanhTien;
        }

        public BillInfo(DataRow row)
        {
            this.MA_MON_AN = (int)row["MA_MON_AN"];
            this.MA_HOA_DON = (int)row["MA_HOA_DON"];
            this.SO_LUONG= (int)row["SO_LUONG"];
            this.THANH_TIEN = (decimal)row["THANH_TIEN"];
        }
    }
}
