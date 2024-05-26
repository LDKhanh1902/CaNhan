using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn1.DTO
{
    public class Menu
    {
        public string TEN_MON_AN { set; get; }
        public int SO_LUONG { set; get; }
        public float GIA_MON { set; get; }
        public float TONG_GIA { set; get; }

        public Menu(string tenmmonan,int soluong,float giamon,float tonggia)
        {
            this.TEN_MON_AN = tenmmonan;
            this.SO_LUONG = soluong;
            this.GIA_MON = giamon;
            this.TONG_GIA = tonggia;
        }
        
        public Menu(DataRow row)
        {
            this.TEN_MON_AN = row["TEN_MON_AN"].ToString()       ;
            this.SO_LUONG = (int)row["SO_LUONG"];
            this.GIA_MON = (float)Convert.ToDouble(row["GIA_TIEN"]);
            this.TONG_GIA = (float)Convert.ToDouble(row["THANH_TIEN"]);
        }

    }
}
      