using DoAn1.DAO;
using System;
using System.Data;
using System.Windows.Forms;

// UserControl cho Menu
namespace DoAn1.Button_Control
{
    public partial class UC_Menu : UserControl
    {
        // Khởi tạo UC_Menu
        public UC_Menu()
        {
            InitializeComponent();
        }

        // Sự kiện Load cho UC_Menu
        private void UC_Menu_Load(object sender, EventArgs e)
        {
            RefreshData(); // Tải lại dữ liệu
        }

        // Hàm tải dữ liệu từ cơ sở dữ liệu
        private DataTable LoadData()
        {
            var data = new DataTable();
            // Thực hiện truy vấn SQL để lấy dữ liệu
            data.Load(DataProvider.Instance.ExecuteQuery("select f.MA_MON_AN,f.TEN_MON_AN,t.TEN_LOAI_MON,CAST(f.GIA_TIEN as INT) AS GIA_TIEN " +
                "from DANH_SACH_MON_AN f , LOAI_MON t WHERE f.MA_LOAI_MON = t.MA_LOAI_MON"));
            return data;
        }

        // Hàm liên kết dữ liệu với các điều khiển
        private void BindData(DataTable data)
        {
            dgvMenu.DataSource = data; // Liên kết dữ liệu với DataGridView

            // Lấy các loại món ăn khác nhau
            var viewCategoryName = new DataView(data);
            var distinctValuesCategoryName = viewCategoryName.ToTable(true, "TEN_LOAI_MON");

            // Liên kết dữ liệu với ComboBox
            cbbCategory.DataSource = distinctValuesCategoryName;
            cbbCategory.DisplayMember = "TEN_LOAI_MON";

            // Liên kết dữ liệu với các TextBox
            txtID.DataBindings.Clear();
            txtID.DataBindings.Add("text", data, "MA_MON_AN");

            txtFoodName.DataBindings.Clear();
            txtFoodName.DataBindings.Add("text", data, "TEN_MON_AN");

            cbbCategory.DataBindings.Clear();
            cbbCategory.DataBindings.Add("text", data, "TEN_LOAI_MON");

            txtPrice.DataBindings.Clear();
            txtPrice.DataBindings.Add("text", data, "GIA_TIEN");
        }

        // Sự kiện Click cho nút Add
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(txtFoodName.Text) || string.IsNullOrEmpty(txtPrice.Text))
                return;

            AddNewItem(); // Thêm mục mới

            RefreshData(); // Tải lại dữ liệu

        }

        // Hàm thêm mục mới vào cơ sở dữ liệu
        private void AddNewItem()
        {
            try
            {
                // Tạo câu truy vấn SQL để thêm mục mới
                string query = string.Format("declare @i int = (select MA_LOAI_MON  from LOAI_MON where TEN_LOAI_MON = N'{0}') " +
                    "insert into DANH_SACH_MON_AN(MA_LOAI_MON, TEN_MON_AN, GIA_TIEN) values(@i, N'{1}', {2})", cbbCategory.Text, txtFoodName.Text, txtPrice.Text);

                // Thực thi câu truy vấn SQL
                int chk = DataProvider.Instance.ExecuteNoneQuery(query);

                // Kiểm tra xem câu truy vấn SQL có được thực thi thành công hay không
                if (chk > 0)
                {
                    MessageBox.Show("Thêm thành công");
                }
                else
                {
                    MessageBox.Show("Có lỗi xãy ra !!!");
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có lỗi xảy ra
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
        }

        // Sự kiện Click cho nút Search
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Tìm kiếm món ăn và liên kết dữ liệu với DataGridView
            dgvMenu.DataSource = SearchFood(cbbCategory.Text);
        }

        // Hàm tìm kiếm món ăn
        public DataTable SearchFood(string Category)
        {
            // Tạo câu truy vấn SQL
            string query = string.Format("select f.MA_MON_AN,f.TEN_MON_AN,t.TEN_LOAI_MON,CAST(f.GIA_TIEN as INT) AS GIA_TIEN "+
                            "from DANH_SACH_MON_AN f inner join LOAI_MON t on f.MA_LOAI_MON = t.MA_LOAI_MON WHERE TEN_LOAI_MON like N'%{0}%'", Category);

            // Thực thi câu truy vấn và lấy kết quả
            DataTable data = new DataTable();

            data.Load(DataProvider.Instance.ExecuteQuery(query));

            return data;
        }

        // Sự kiện Click cho nút Revision
        private void btnRevision_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(txtFoodName.Text) || string.IsNullOrEmpty(txtPrice.Text))
                return;

            UpdateItem(); // Cập nhật mục

            RefreshData(); // Tải lại dữ liệu
        }

        // Hàm cập nhật mục trong cơ sở dữ liệu
        private void UpdateItem()
        {
            try
            {
                // Tạo câu lệnh SQL để cập nhật mục
                string query = string.Format("DECLARE @i int = (SELECT MA_LOAI_MON FROM LOAI_MON WHERE TEN_LOAI_MON = N'{0}') " +
                    "UPDATE DANH_SACH_MON_AN " +
                    "SET TEN_MON_AN = N'{1}', MA_LOAI_MON = @i, GIA_TIEN = {2} " +
                    "WHERE MA_MON_AN = {3}",cbbCategory.Text, txtFoodName.Text, txtPrice.Text, txtID.Text);

                // Thực thi câu lệnh SQL
                int result = DataProvider.Instance.ExecuteNoneQuery(query);

                // Kiểm tra xem câu lệnh SQL có được thực thi thành công hay không
                if (result > 0)
                {
                    MessageBox.Show("Cập nhật mục thành công!");
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi cập nhật mục. Vui lòng thử lại.");
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có lỗi xảy ra
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
        }

        // Sự kiện Click cho nút Delete
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có mã món ăn nào được chọn để xóa không
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Vui lòng chọn một món ăn để xóa.");
                return;
            }

            // Xác nhận trước khi xóa
            var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa món ăn này không?",
                                     "Xác nhận xóa!!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                // Xóa món ăn
                DeleteItem();

                // Tải lại dữ liệu
                RefreshData();
            }
        }

        // Hàm xóa mục từ cơ sở dữ liệu
        private void DeleteItem()
        {
            try
            {
                // Tạo câu lệnh SQL để xóa món ăn
                string query = string.Format("DELETE FROM DANH_SACH_MON_AN WHERE MA_MON_AN = {0}", txtID.Text);

                // Thực thi câu lệnh SQL
                int result = DataProvider.Instance.ExecuteNoneQuery(query);

                // Kiểm tra xem câu lệnh SQL có được thực thi thành công hay không
                if (result > 0)
                {
                    MessageBox.Show("Xóa món ăn thành công!");
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi xóa món ăn. Vui lòng thử lại.");
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có lỗi xảy ra
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
        }

        // Sự kiện Click cho DataGridView
        private void dgvMenu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy dữ liệu từ hàng được chọn
                DataGridViewRow row = dgvMenu.Rows[e.RowIndex];
                txtID.Text = row.Cells[0].Value?.ToString();
                txtFoodName.Text = row.Cells[1].Value?.ToString();
                cbbCategory.Text = row.Cells[2].Value?.ToString();
                txtPrice.Text = row.Cells[3].Value?.ToString();
            }

        }

        // Hàm xóa dữ liệu từ các TextBox
        private void ClearData()
        {
            txtID.Clear(); // Xóa dữ liệu từ TextBox ID
            txtFoodName.Clear(); // Xóa dữ liệu từ TextBox FoodName
            txtPrice.Clear(); // Xóa dữ liệu từ TextBox Price
        }

        // Hàm tải lại dữ liệu
        private void RefreshData()
        {
            var data = LoadData(); // Tải dữ liệu từ cơ sở dữ liệu
            BindData(data); // Liên kết dữ liệu với các điều khiển
            ClearData(); // Xóa dữ liệu từ các TextBox
        }
    }
}

