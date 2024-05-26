using DoAn1.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn1.DAO
{
    public class MenuDAO
    {
        private static MenuDAO instance;

        public static MenuDAO Instance
        {
            get { if (instance == null) instance = new MenuDAO(); return MenuDAO.instance; }
            private set { MenuDAO.instance = value; }
        }

        private MenuDAO() { }

        public List<Menu> GetListMenuByTable(int id)
        {
            List<Menu> listMenu = new List<Menu>();

            string query = string.Format("SELECT f.TEN_MON_AN, bi.SO_LUONG, f.GIA_TIEN, bi.SO_LUONG*f.GIA_TIEN AS THANH_TIEN " +
                "FROM CHI_TIET_HOA_DON bi JOIN HOA_DON b ON b.MA_HOA_DON = bi.MA_HOA_DON JOIN DANH_SACH_MON_AN f " +
                "ON bi.MA_MON_AN = f.MA_MON_AN WHERE b.MA_BAN = {0} and b.TRANG_THAI = 1",id);
            
            DataTable data = new DataTable();
            data.Load(DataProvider.Instance.ExecuteQuery(query));

            foreach (DataRow item in data.Rows)
            {
                Menu menu = new Menu(item);
                listMenu.Add(menu);
            }

            return listMenu;
            
        }
    }
}