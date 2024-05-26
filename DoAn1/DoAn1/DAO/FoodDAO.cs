using DoAn1.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn1.DAO
{
    class FoodDAO
    {
        private static FoodDAO instance;

        public static FoodDAO Instance
        {
            get { if (instance == null) instance = new FoodDAO(); return FoodDAO.instance; }
            private set { FoodDAO.Instance = value; }
        }

        private FoodDAO() { }

        public List<Food> GetFoodByCategoryName(string name)
        {
            List<Food> list = new List<Food>();

            string query = string.Format("select f.MA_MON_AN,f.TEN_MON_AN,t.TEN_LOAI_MON,CAST(f.GIA_TIEN as int) as GIA_TIEN from DANH_SACH_MON_AN f inner join LOAI_MON t on t.MA_LOAI_MON = f.MA_LOAI_MON where f.MA_LOAI_MON = (select MA_LOAI_MON from LOAI_MON where TEN_LOAI_MON like N'{0}')",name);

            DataTable data = new DataTable();
            data.Load(DataProvider.Instance.ExecuteQuery(query));

            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }

            return list;
        }
    }
}
