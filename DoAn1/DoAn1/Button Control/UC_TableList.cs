using DoAn1.DAO;
using DoAn1.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Menu = DoAn1.DTO.Menu;

namespace DoAn1.Button_Control
{
    // Định nghĩa một UserControl tên UC_TableList
    public partial class UC_TableList : UserControl
    {
        // Định nghĩa các trạng thái của bàn
        private const string EMPTY_STATUS = "Trống"; // Trạng thái trống
        private const string OCCUPIED_STATUS = "Có người"; // Trạng thái có người
        private const string RESERVED_STATUS = "Đã đặt"; // Trạng thái đã đặt

        private int idTable; // ID của bàn

        public UC_TableList()
        {
            InitializeComponent(); // Khởi tạo các thành phần của UserControl
        }

        // Hàm được gọi khi UserControl được tải
        private void UC_TableList_Load(object sender, EventArgs e)
        {
            LoadTableList(); // Tải danh sách bàn
            LoadCategory(); // Tải danh mục
        }

        // Hàm tải danh mục
        void LoadCategory()
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory(); // Lấy danh sách danh mục từ cơ sở dữ liệu
            cbbCategory.DataSource = listCategory; // Đặt nguồn dữ liệu cho combobox danh mục
            cbbCategory.DisplayMember = "TEN_LOAI"; // Hiển thị tên loại trong combobox
        }

        // Hàm tải danh sách món ăn theo tên danh mục
        void LoadFoodListByCategoryName(string name)
        {
            List<Food> listFood = FoodDAO.Instance.GetFoodByCategoryName(name); // Lấy danh sách món ăn theo tên danh mục từ cơ sở dữ liệu
            cbbMenu.DataSource = listFood; // Đặt nguồn dữ liệu cho combobox menu
            cbbMenu.DisplayMember = "TenMon"; // Hiển thị tên món trong combobox

            txtDonGia.DataBindings.Clear(); // Xóa các liên kết dữ liệu hiện tại
            txtDonGia.DataBindings.Add("Text", listFood, "DonGia"); // Thêm liên kết dữ liệu mới cho đơn giá
        }

        // Hàm được gọi khi danh mục được chọn thay đổi
        private void cbbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = "";

            if (cbbCategory.SelectedItem == null) // Nếu không có mục nào được chọn
                return;

            Category selected = cbbCategory.SelectedItem as Category; // Lấy mục được chọn
            name = selected.TEN_LOAI; // Lấy tên loại
            LoadFoodListByCategoryName(name); // Tải danh sách món ăn theo tên loại
        }

        // Hàm tải danh sách bàn
        private void LoadTableList()
        {
            flpnlDSBA.Controls.Clear(); // Xóa tất cả các control hiện tại
            List<Table> tableList = TableDAO.Instance.LoadTableList(); // Lấy danh sách bàn từ cơ sở dữ liệu
            foreach (Table item in tableList) // Duyệt qua từng bàn
            {
                Button btn = CreateButtonForTable(item); // Tạo button cho bàn
                flpnlDSBA.Controls.Add(btn); // Thêm button vào panel
            }
        }

        // Hàm tạo button cho bàn
        private Button CreateButtonForTable(Table table)
        {
            Button btn = new Button() // Tạo mới button
            {
                Width = TableDAO.tableWidth, // Đặt chiều rộng
                Height = TableDAO.tableHeight, // Đặt chiều cao
                Text = table.Name + Environment.NewLine + table.Status, // Đặt text hiển thị
                Tag = table // Đặt tag
            };
            btn.Click += btn_Click; // Thêm sự kiện click
            switch (table.Status) // Đặt màu nền tương ứng với trạng thái
            {
                case EMPTY_STATUS:
                    btn.BackColor = Color.Aqua;
                    break;
                case OCCUPIED_STATUS:
                    btn.BackColor = Color.Red;
                    break;
                default:
                    btn.BackColor = Color.Yellow;
                    break;
            }
            return btn; // Trả về button
        }

        // Hàm được gọi khi button được click
        private void btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).ID; // Lấy ID của bàn từ tag của button
            ShowBill(tableID); // Hiển thị hóa đơn của bàn
            idTable = tableID; // Lưu ID bàn
        }

        // Hàm hiển thị hóa đơn của bàn
        private void ShowBill(int id)
        {
            List<Menu> lstBill = MenuDAO.Instance.GetListMenuByTable(id); // Lấy danh sách menu của bàn từ cơ sở dữ liệu
            int totalPrice = 0; // Tổng giá
            lsvBill.Items.Clear(); // Xóa tất cả các mục trong listview hóa đơn

            foreach (Menu menu in lstBill) // Duyệt qua từng món trong menu
            {
                ListViewItem lsvItem = new ListViewItem(menu.TEN_MON_AN); // Tạo mới mục cho listview
                lsvItem.SubItems.Add(menu.SO_LUONG.ToString()); // Thêm số lượng
                lsvItem.SubItems.Add(menu.GIA_MON.ToString()); // Thêm giá món
                lsvItem.SubItems.Add(menu.TONG_GIA.ToString()); // Thêm tổng giá

                lsvBill.Items.Add(lsvItem); // Thêm mục vào listview
                totalPrice += int.Parse(menu.TONG_GIA.ToString()); // Cộng dồn tổng giá
            }
            CultureInfo culture = new CultureInfo("vi-VN"); // Tạo mới đối tượng culture

            txtTotalPrice.Text = totalPrice.ToString("c", culture); // Hiển thị tổng giá
        }

        // Hàm được gọi khi mục trong listview hóa đơn được chọn
        private void lsvBill_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvBill.SelectedItems.Count > 0) // Nếu có mục được chọn
            {
                ListViewItem selectedItem = lsvBill.SelectedItems[0];

                // Lấy mục được chọn
                cbbMenu.Text = selectedItem.Text;
                nrcQuantity.Value = int.Parse(selectedItem.SubItems[1].Text);
                txtDonGia.Text = selectedItem.SubItems[2].Text;
            }
        }


        // Hàm cập nhật trạng thái của button
        private void UpdateButtonStatus(int idTable, string status, Color color)
        {
            foreach (Control control in flpnlDSBA.Controls) // Duyệt qua từng control trong panel
            {
                if (control is Button && ((control.Tag as Table).ID == idTable)) // Nếu control là button và ID của bàn tương ứng với ID được cung cấp
                {
                    control.Text = (control.Tag as Table).Name + Environment.NewLine + status; // Cập nhật text của button
                    control.BackColor = color; // Cập nhật màu nền của button
                    break;
                }
            }
        }

        // Hàm được gọi khi button "Trống" được click
        private void btnTrong_Click(object sender, EventArgs e)
        {
            DataProvider.Instance.ExecuteQuery("update DANH_SACH_BAN set TRANG_THAI = N'Trống' where MA_BAN = " + idTable); // Cập nhật trạng thái của bàn trong cơ sở dữ liệu
            UpdateButtonStatus(idTable, EMPTY_STATUS, Color.Aqua); // Cập nhật trạng thái của button
        }

        // Hàm được gọi khi button "Đã đặt" được click
        private void btnDaDat_Click(object sender, EventArgs e)
        {
            DataProvider.Instance.ExecuteQuery("update DANH_SACH_BAN set TRANG_THAI = N'Đã đặt' where MA_BAN = " + idTable); // Cập nhật trạng thái của bàn trong cơ sở dữ liệu
            UpdateButtonStatus(idTable, RESERVED_STATUS, Color.Yellow); // Cập nhật trạng thái của button
        }

        // Hàm được gọi khi button "Có người" được click
        private void btnCoNguoi_Click(object sender, EventArgs e)
        {
            DataProvider.Instance.ExecuteQuery("update DANH_SACH_BAN set TRANG_THAI = N'Có người' where MA_BAN = " + idTable); // Cập nhật trạng thái của bàn trong cơ sở dữ liệu
            UpdateButtonStatus(idTable, OCCUPIED_STATUS, Color.Red); // Cập nhật trạng thái của button
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool found = false;
            if (lsvBill.Items.Count > 0)
            {
                foreach (ListViewItem item in lsvBill.Items)
                {
                    if (item.SubItems[0].Text == cbbMenu.Text) // So sánh với giá trị trong cột 0
                    {
                        BillInfoDAO.Instance.UpdateBillInfo(idTable, cbbMenu.Text, (int)nrcQuantity.Value);
                        found = true;
                        break;
                    }
                }
            }
            if (!found)
            {
                BillInfoDAO.Instance.InsertBillInfo(idTable, cbbMenu.Text, (int)nrcQuantity.Value, int.Parse(txtDonGia.Text));
            }
            ShowBill(idTable);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lsvBill.SelectedItems.Count > 0) // Kiểm tra xem có mục nào được chọn hay không
            {
                ListViewItem selectedItem = lsvBill.SelectedItems[0];
                int temp = int.Parse(selectedItem.SubItems[1].Text);
                try
                {
                    if (temp - nrcQuantity.Value >= 1)
                    {
                        BillInfoDAO.Instance.UpdateBillInfo(idTable, cbbMenu.Text, (int)nrcQuantity.Value * (-1));
                    }
                    else
                        BillInfoDAO.Instance.DeleteBillInfo(cbbMenu.Text, idTable);

                    
                }
                catch (Exception ex) // Xác định loại ngoại lệ cụ thể
                {
                    ex.ToString();
                    MessageBox.Show("Vui lòng chọn món ăn muốn xóa !");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn món ăn muốn xóa !");
            }
            ShowBill(idTable);
        }

    }
}
