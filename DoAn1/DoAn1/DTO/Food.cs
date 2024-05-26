using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DoAn1.DTO
{
    public class Food
    {
        public int MaMonAn { get; set; }
        public string TenMon { get; set; }
        public string LoaiMon { get; set; }
        public int DonGia { get; set; }

        public Food() { }

        public Food(int id, string name, string categoryName, int price)
        {
            this.MaMonAn = id;
            this.TenMon = name;
            this.LoaiMon = categoryName;
            this.DonGia = price;
        }

        public Food(DataRow row)
        {
            MaMonAn = int.Parse(row["MA_MON_AN"].ToString());
            TenMon = row["TEN_MON_AN"].ToString();
            LoaiMon = row["TEN_LOAI_MON"].ToString();
            DonGia = (int)row["GIA_TIEN"];
        }
    }

}
