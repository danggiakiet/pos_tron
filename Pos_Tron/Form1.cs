using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;

namespace Pos_Tron
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Tọa độ x,y của dòng trong panelOrder
        public int toaDoXNewRowPanelOrder = 13;

        public int toaDoYNewRowPanelOrder = 62;

        // Tạo danh sách Menu rỗng để chứa dữ liệu
        private List<doUong> Menu = new List<doUong>();

        //tạo danh sách chứa số lượng đã bán
        public Dictionary<string, int> soldList = new Dictionary<string, int>();

        public Dictionary<string, int> soldListtemp = new Dictionary<string, int>();

        private void Form1_Load(object sender, EventArgs e)
        {
            // Mở file excel
            using (var package = new ExcelPackage(new FileInfo("Menu.xlsx")))
            {
                // Lấy sheet đầu tiên
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

                // Duyệt từ dòng 1 đến hết của file excel
                for (int i = worksheet.Dimension.Start.Row; i <= worksheet.Dimension.End.Row; i++)
                {
                    // Lấy tên đồ uống từ cột A
                    string tenDoUong = worksheet.Cells[i, 1].Value.ToString();

                    // Lấy giá từ cột B
                    string gia = worksheet.Cells[i, 2].Value.ToString();

                    // Tạo đối tượng doUong và thêm vào danh sách Menu
                    doUong doUong = new doUong(tenDoUong, Convert.ToInt32(gia));
                    Menu.Add(doUong);
                }
            }

            int c = 1;
            foreach (doUong a in Menu)
            {
                string textBoxName = "textBox" + c;
                TextBox textBox = Controls.Find(textBoxName, true).FirstOrDefault() as TextBox;
                if (textBox != null)
                {
                    textBox.TextAlign = HorizontalAlignment.Center;
                    textBox.Text = a.Ten;
                }
                c++;
            }
        }

        public int TinhTongGia()
        {
            int tongGia = 0;
            foreach (Control control in PanelOrder.Controls)
            {
                if (control is TextBox && control.Name != "Name")
                {
                    TextBox priceTextBox = (TextBox)control;
                    int gia = 0;
                    int.TryParse(priceTextBox.Text.Replace(".", ""), out gia); // Chuyển đổi và xóa dấu phân cách hàng đơn vị
                    tongGia += gia;
                }
            }
            return tongGia;
        }

        public void themDongPanelOrder(int x, int y, string Ten, int Gia)
        {
            TextBox Name = new TextBox(); // Tạo một ô textbox mới
            Name.Location = new Point(x, y); // Đặt vị trí của ô textbox
            Name.Width = 151;
            Name.Height = 32;
            Name.Text = Ten;
            PanelOrder.Controls.Add(Name); // Thêm ô textbox vào panel

            NumericUpDown Quantity = new NumericUpDown(); // Tạo một ô numeric mới
            Quantity.Location = new Point(x + 157, y); // Đặt vị trí của ô numeric
            Quantity.Width = 60;
            Quantity.Height = 32;
            Quantity.Value = 1;
            PanelOrder.Controls.Add(Quantity); // Thêm ô numeric vào panel

            TextBox Price = new TextBox(); // Tạo một ô textbox mới
            Price.Location = new Point(x + 222, y); // Đặt vị trí của ô textbox
            Price.Width = 180;
            Price.Height = 32;
            Price.Text = (Gia * Quantity.Value).ToString("N0");
            PanelOrder.Controls.Add(Price); // Thêm ô textbox vào panel
                                            //Thay đổi giá trị ô price theo số lượng
            Quantity.ValueChanged += (sender, e) =>
            {
                NumericUpDown quantityTextBox = (NumericUpDown)sender;
                TextBox nameTextBox = PanelOrder.Controls
                    .OfType<TextBox>()
                    .FirstOrDefault(tb => tb.Location == new Point(x, y));

                if (nameTextBox != null)
                {
                    string itemName = nameTextBox.Text;

                    Price.Text = (Gia * quantityTextBox.Value).ToString("N0"); // Cập nhật giá trị của ô Price
                    TotalPrice.Text = TinhTongGia().ToString("N0"); // Cập nhật giá trị của ô TotalPrice

                    // Kiểm tra đã có sản phẩm đó hay chưa
                    if (!soldListtemp.ContainsKey(itemName))
                    {
                        // Chưa có thì thêm vào danh sách
                        soldListtemp.Add(itemName, Convert.ToInt32(quantityTextBox.Value));
                    }
                    else
                    {
                        // Nếu có rồi thì cập nhật lại số lượng bằng giá trị của Quantity.Value
                        soldListtemp[Name.Text] = soldListtemp[Name.Text] + Convert.ToInt32(Quantity.Value);
                    }
                }
            };

            TotalPrice.Text = TinhTongGia().ToString("N0");

            //Thêm vào danh sách số lượng đã bán
            //Kiểm tra đã có sản phảm đó hay chưa
            if (!soldListtemp.ContainsKey(Name.Text))
            {
                //Chưa có thì thêm vào danh sách
                soldListtemp.Add(Name.Text, Convert.ToInt32(Quantity.Value));
            }
            else
            {
                //Nếu có rồi thì cập nhập lại số lượng bằng giá trị của Quantity.Value
                soldListtemp[Name.Text] = soldListtemp[Name.Text] + Convert.ToInt32(Quantity.Value);
            }
        }

        public void saveSoldListTemp(Dictionary<string, int> soldListtemp, Dictionary<string, int> soldList)
        {
            foreach (var item in soldListtemp)
            {
                if (!soldList.ContainsKey(item.Key))
                {
                    soldList.Add(item.Key, item.Value);
                }
                else
                {
                    soldList[item.Key] = soldList[item.Key] + item.Value;
                }
            }
        }

        private void ClearPanelChotDon()
        {
            List<Control> controlsToRemove = new List<Control>();

            // Tìm và thêm các control TextBox và NumericUpDown vào danh sách controlsToRemove
            foreach (Control control in panelChotDon.Controls)
            {
                if (control is TextBox || control is Label)
                {
                    controlsToRemove.Add(control);
                }
            }

            // Xóa các control từ danh sách controlsToRemove và giải phóng tài nguyên của chúng
            foreach (Control control in controlsToRemove)
            {
                control.Dispose();
                panelChotDon.Controls.Remove(control);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            List<Control> controlsToRemove = new List<Control>();

            // Tìm và thêm các control TextBox và NumericUpDown vào danh sách controlsToRemove
            foreach (Control control in PanelOrder.Controls)
            {
                if (control is TextBox || control is NumericUpDown)
                {
                    controlsToRemove.Add(control);
                }
            }

            // Xóa các control từ danh sách controlsToRemove và giải phóng tài nguyên của chúng
            foreach (Control control in controlsToRemove)
            {
                control.Dispose();
                PanelOrder.Controls.Remove(control);
            }
            toaDoXNewRowPanelOrder = 13;
            toaDoYNewRowPanelOrder = 62;
            TotalPrice.Text = "";
            soldListtemp.Clear();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            try
            {
                doUong doUong = Menu[0];
                //Thêm 1 dòng vào panelOrder
                themDongPanelOrder(toaDoXNewRowPanelOrder, toaDoYNewRowPanelOrder, doUong.Ten, doUong.Price);
                //chỉnh tọa độ cho dòng tiếp theo
                toaDoYNewRowPanelOrder += 39;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hình như có lỗi gì rồi đó !!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void button2_Click(object sender, EventArgs e)
        {
            try
            {
                doUong doUong = Menu[1];
                //Thêm 1 dòng vào panelOrder
                themDongPanelOrder(toaDoXNewRowPanelOrder, toaDoYNewRowPanelOrder, doUong.Ten, doUong.Price);
                //chỉnh tọa độ cho dòng tiếp theo
                toaDoYNewRowPanelOrder += 39;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hình như có lỗi gì rồi đó !!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void button3_Click(object sender, EventArgs e)
        {
            try
            {
                doUong doUong = Menu[2];
                //Thêm 1 dòng vào panelOrder
                themDongPanelOrder(toaDoXNewRowPanelOrder, toaDoYNewRowPanelOrder, doUong.Ten, doUong.Price);
                //chỉnh tọa độ cho dòng tiếp theo
                toaDoYNewRowPanelOrder += 39;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hình như có lỗi gì rồi đó !!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void button4_Click(object sender, EventArgs e)
        {
            try
            {
                doUong doUong = Menu[3];
                //Thêm 1 dòng vào panelOrder
                themDongPanelOrder(toaDoXNewRowPanelOrder, toaDoYNewRowPanelOrder, doUong.Ten, doUong.Price);
                //chỉnh tọa độ cho dòng tiếp theo
                toaDoYNewRowPanelOrder += 39;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hình như có lỗi gì rồi đó !!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void button5_Click(object sender, EventArgs e)
        {
            try
            {
                doUong doUong = Menu[4];
                //Thêm 1 dòng vào panelOrder
                themDongPanelOrder(toaDoXNewRowPanelOrder, toaDoYNewRowPanelOrder, doUong.Ten, doUong.Price);
                //chỉnh tọa độ cho dòng tiếp theo
                toaDoYNewRowPanelOrder += 39;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hình như có lỗi gì rồi đó !!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void button6_Click(object sender, EventArgs e)
        {
            try
            {
                doUong doUong = Menu[5];
                //Thêm 1 dòng vào panelOrder
                themDongPanelOrder(toaDoXNewRowPanelOrder, toaDoYNewRowPanelOrder, doUong.Ten, doUong.Price);
                //chỉnh tọa độ cho dòng tiếp theo
                toaDoYNewRowPanelOrder += 39;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hình như có lỗi gì rồi đó !!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void button7_Click(object sender, EventArgs e)
        {
            try
            {
                doUong doUong = Menu[6];
                //Thêm 1 dòng vào panelOrder
                themDongPanelOrder(toaDoXNewRowPanelOrder, toaDoYNewRowPanelOrder, doUong.Ten, doUong.Price);
                //chỉnh tọa độ cho dòng tiếp theo
                toaDoYNewRowPanelOrder += 39;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hình như có lỗi gì rồi đó !!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void button8_Click(object sender, EventArgs e)
        {
            try
            {
                doUong doUong = Menu[7];
                //Thêm 1 dòng vào panelOrder
                themDongPanelOrder(toaDoXNewRowPanelOrder, toaDoYNewRowPanelOrder, doUong.Ten, doUong.Price);
                //chỉnh tọa độ cho dòng tiếp theo
                toaDoYNewRowPanelOrder += 39;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hình như có lỗi gì rồi đó !!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void button9_Click(object sender, EventArgs e)
        {
            try
            {
                doUong doUong = Menu[8];
                //Thêm 1 dòng vào panelOrder
                themDongPanelOrder(toaDoXNewRowPanelOrder, toaDoYNewRowPanelOrder, doUong.Ten, doUong.Price);
                //chỉnh tọa độ cho dòng tiếp theo
                toaDoYNewRowPanelOrder += 39;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hình như có lỗi gì rồi đó !!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnChotDon_Click(object sender, EventArgs e)
        {
            try
            {
                ClearPanelChotDon();
                saveSoldListTemp(soldListtemp, soldList);
                themDongPanelChotDon(Menu, soldList);
                totalSold.Text = TinhTongLy();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có gì đâu mà chốt !!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnClear_Click(sender, e);
            }
        }

        public string TinhTongLy()
        {
            int tongLy = 0;
            foreach (Control control in panelChotDon.Controls)
            {
                if (control is TextBox && control.Name != "Quantity")
                {
                    TextBox soldTextBox = (TextBox)control;
                    int ly = 0;
                    int.TryParse(soldTextBox.Text, out ly);
                    tongLy += ly;
                }
            }
            return tongLy.ToString();
        }

        public void themDongPanelChotDon(List<doUong> Menu, Dictionary<string, int> soldList)
        {
            int xName = 14;
            int yName = 65;
            int xQuantity = 200;
            int yQuantity = 56;
            for (int i = 0; i < Menu.Count; i++)
            {
                Label Name = new Label(); // Tạo một Label mới
                Name.Location = new Point(xName, yName); // Đặt vị trí của label
                Name.Height = 23;
                Name.Width = 180;
                Name.Text = Menu[i].Ten;
                panelChotDon.Controls.Add(Name); // Thêm label vào panel
                yName += 47;

                TextBox Quantity = new TextBox(); // Tạo một ô số lượng mới
                Quantity.Location = new Point(xQuantity, yQuantity); // Đặt vị trí của label
                Quantity.Height = 32;
                Quantity.Width = 32;
                Quantity.ReadOnly = true;
                panelChotDon.Controls.Add(Quantity); // Thêm ô textbox vào panel
                yQuantity += 47;

                foreach (var item in soldList)
                {
                    if (Name.Text == item.Key)
                    {
                        Quantity.Text = item.Value.ToString();
                    }
                }
            }
        }
    }
}