using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using DoAn1.DTO;

namespace DoAn1.DAO
{
    public class TableDAO
    {
        private static TableDAO instance;

        public static int tableWidth = 106;
        public static int tableHeight = 106;

        public static TableDAO Instance
        {
            get { if (instance == null) instance = new TableDAO(); return TableDAO.instance; }
            set { TableDAO.instance = value; }
        }

        public TableDAO() { }

        public List<Table> LoadTableList() 
        {
            List<Table> tableList = new List<Table>();

            DataTable data = new DataTable();
            data.Load(DataProvider.Instance.ExecuteQuery("select * from DANH_SACH_BAN"));

            foreach (DataRow item in data.Rows) 
            {
                Table table = new Table(item);
                tableList.Add(table);
            }

            return tableList;
        }
    }
}
