using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace DoAn1.DTO
{
    public class Table
    {
        private int iD;
        private string name;
        private string status;

        // Hàm tạo không tham số
        public Table(DataRow rows)
        {
            this.ID = (int)rows["MA_BAN"];
            this.Name = rows["TEN_BAN_AN"].ToString();
            this.Status = rows["TRANG_THAI"].ToString();
        }

        // Hàm tạo có tham số
        public Table(int iD, string name, string status)
        {
            this.iD = iD;
            this.name = name;
            this.status = status;
        }

        // Các phương thức getter và setter
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

    }
}
