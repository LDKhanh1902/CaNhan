using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn1.DAO
{
    public class AccountDAO
    {
        public AccountDAO() { }

        private static AccountDAO instance;

        public static AccountDAO Instrance
        {
            get { if (AccountDAO.instance == null) instance = new AccountDAO(); return AccountDAO.instance; }
            private set { AccountDAO.instance = value; }
        }

        public bool Login(string username, string password)
        {
            SqlDataReader reader = DataProvider.Instance.ExecuteQuery("SELECT TEN_DANG_NHAP, MAT_KHAU FROM TAI_KHOAN WHERE TEN_DANG_NHAP = @tendangnhap AND MAT_KHAU = @matkhau",
            new object[] { username, password });

            if (reader.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
