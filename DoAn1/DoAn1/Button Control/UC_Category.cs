using DoAn1.DAO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DoAn1.Button_Control
{
    public partial class UC_Category : UserControl
    {
        public UC_Category()
        {
            InitializeComponent();
        }

        private void UC_Category_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            DataTable data = new DataTable();
            data.Load(DataProvider.Instance.ExecuteQuery("select * from LOAI_MON"));
            dgvCategory.DataSource = data;
        }

        private void dgvCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCategory.Rows[e.RowIndex];
                txtCategoryID.Text = row.Cells[0].Value?.ToString();
                txtCategoryName.Text = row.Cells[1].Value?.ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCategoryName.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên loại món ăn.");
                    return;
                }

                string query = string.Format("INSERT INTO LOAI_MON(TEN_LOAI_MON) VALUES (N'{0}')", txtCategoryName.Text );
                int result = DataProvider.Instance.ExecuteNoneQuery(query);

                if (result > 0)
                {
                    MessageBox.Show("Thêm loại món ăn thành công!");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi thêm loại món ăn. Vui lòng thử lại.");
                }
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có lỗi xảy ra
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
        }


        private void btnRevision_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCategoryName.Text) || string.IsNullOrEmpty(txtCategoryID.Text))
            {
                MessageBox.Show("Vui lòng chọn loại món ăn để cập nhật.");
                return;
            }

            string query = "UPDATE LOAI_MON SET TEN_LOAI_MON = @tenLoai WHERE MA_LOAI_MON = @maLoaiMonAn";
            int result = DataProvider.Instance.ExecuteNoneQuery(query, new object[] { txtCategoryName.Text, txtCategoryID.Text });

            if (result > 0)
            {
                MessageBox.Show("Cập nhật loại món ăn thành công!");
                LoadData();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật loại món ăn. Vui lòng thử lại.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCategoryID.Text))
            {
                MessageBox.Show("Vui lòng chọn loại món ăn để xóa.");
                return;
            }

            // Xác nhận trước khi xóa
            var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa loại món ăn này không?",
                                     "Xác nhận xóa!!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                // Xóa loại món ăn
                try
                {
                    DeleteCategory();

                    // Tải lại dữ liệu
                    LoadData();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547)
                    {
                        MessageBox.Show("Không thể xóa loại món ăn này vì nó đang được sử dụng.");
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        // Delete category from database
        private void DeleteCategory()
        {
            string query = "DELETE FROM LOAI_MON WHERE MA_LOAI_MON = @maLoaiMonAn";
            int result = DataProvider.Instance.ExecuteNoneQuery(query, new object[] { txtCategoryID.Text });

            if (result > 0)
            {
                MessageBox.Show("Xóa loại món ăn thành công!");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi xóa loại món ăn. Vui lòng thử lại.");
            }
        }
    }
}
