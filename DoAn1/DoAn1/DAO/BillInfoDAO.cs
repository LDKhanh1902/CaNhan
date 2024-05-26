using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DoAn1.DTO;
using System.Windows;

namespace DoAn1.DAO
{
    class BillInfoDAO
    {
        private static BillInfoDAO instance;

        public static BillInfoDAO Instance
        {
            get { if (instance == null) instance = new BillInfoDAO(); return BillInfoDAO.instance; }
            private set { BillInfoDAO.Instance = value; }
        }

        private BillInfoDAO() { }

        public List<BillInfo> GetListBillInfo(int id)
        {
            List<BillInfo> listBillInfo = new List<BillInfo>();
            DataTable data = new DataTable();
            data.Load(DataProvider.Instance.ExecuteQuery("select * from CHI_TIET_HOA_DON where MA_HOA_DON = " + id));

            foreach (DataRow item in data.Rows)
            {
                BillInfo info = new BillInfo(item);
                listBillInfo.Add(info);
            }

            return listBillInfo;
        }

        public void InsertBillInfo(int idTable, string foodName, int quantity, int donGia)
        {
            object result = DataProvider.Instance.ExecuteScalar("SELECT count(*) FROM HOA_DON " +
                "WHERE (SELECT MA_HOA_DON FROM HOA_DON WHERE MA_BAN = " + idTable + " and TRANG_THAI = 1) IS null");
            
            if (result != null && result is int)
            {
                int count = (int)result;
                if (count <= 0)
                {
                    string query = string.Format("insert into CHI_TIET_HOA_DON(MA_HOA_DON,MA_MON_AN,SO_LUONG,THANH_TIEN) " +
                        "values((select MA_HOA_DON from HOA_DON where MA_BAN = {0} and TRANG_THAI = 1)," +
                        "(select MA_MON_AN from DANH_SACH_MON_AN where TEN_MON_AN like N'{1}'),{2}," +
                        "{3})", idTable, foodName, quantity, donGia);

                    DataProvider.Instance.ExecuteQuery(query);
                }
                else
                {
                    string query = string.Format("declare @id int insert into HOA_DON(MA_BAN, MA_NHAN_VIEN, NGAY_LAP, TRANG_THAI) " +
                        "values({0}, 1, GETDATE(), 1) set @id = SCOPE_IDENTITY() insert into CHI_TIET_HOA_DON(MA_HOA_DON, MA_MON_AN, SO_LUONG, THANH_TIEN) " +
                        "values(@id, (select MA_MON_AN from DANH_SACH_MON_AN where TEN_MON_AN like N'{1}'), {2}," +
                        "{3})", idTable, foodName, quantity, donGia);

                    DataProvider.Instance.ExecuteQuery(query);
                }
            }
        }


        public void UpdateBillInfo(int idTable, string foodName, int quantity)
        {
            string query = string.Format("UPDATE CHI_TIET_HOA_DON SET SO_LUONG = {0} + (SELECT SO_LUONG FROM CHI_TIET_HOA_DON WHERE MA_HOA_DON = " +
                "(SELECT MA_HOA_DON FROM HOA_DON WHERE MA_BAN = {1} AND TRANG_THAI = 1) AND MA_MON_AN = " +
                "(SELECT MA_MON_AN FROM DANH_SACH_MON_AN WHERE TEN_MON_AN LIKE N'{2}')) WHERE MA_HOA_DON = " +
                "(SELECT MA_HOA_DON FROM HOA_DON WHERE MA_BAN = {1} AND TRANG_THAI = 1) AND MA_MON_AN = " +
                "(SELECT MA_MON_AN FROM DANH_SACH_MON_AN WHERE TEN_MON_AN LIKE N'{2}')", quantity, idTable, foodName);

            DataProvider.Instance.ExecuteQuery(query);
        }


        public void DeleteBillInfo(string foodName,int idTable)
        {
            string query = string.Format("delete CHI_TIET_HOA_DON where MA_MON_AN = (select MA_MON_AN from DANH_SACH_MON_AN " +
                "where TEN_MON_AN like N'{0}') and MA_HOA_DON = (SELECT MA_HOA_DON FROM HOA_DON WHERE MA_BAN = {1} AND TRANG_THAI = 1)",foodName,idTable);

            DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
