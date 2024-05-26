using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using DoAn1.DTO;

namespace DoAn1.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return BillDAO.instance; }
            private set { BillDAO.instance = value; }
        }

        public BillDAO() { }

        public int GetUnCheckBillIdByTableID(int id)
        {
            DataTable data = new DataTable();
            data.Load(DataProvider.Instance.ExecuteQuery("select * from HOA_DON where MA_BAN = "+id+" and TRANG_THAI = 1"));

            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.MA_HOA_DON;
            }
            return -1;
        }
    }
}
