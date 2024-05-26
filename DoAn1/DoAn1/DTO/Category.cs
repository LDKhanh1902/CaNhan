using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn1.DTO
{
    public class Category
    {
        public int MA_LOAI_MON_AN { get; set; }
        public string TEN_LOAI { get; set; }

        public Category(DataRow row)
        {
            MA_LOAI_MON_AN = int.Parse(row["MA_LOAI_MON"].ToString());
            TEN_LOAI = row["TEN_LOAI_MON"].ToString();
        }
    }
}
